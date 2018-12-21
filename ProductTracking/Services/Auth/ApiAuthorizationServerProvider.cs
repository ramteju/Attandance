using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security.OAuth;
using Microsoft.Practices.Unity;
using ProductTracking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using System.Web.Security;
using ProductTracking.Services.Core;

namespace ProductTracking.Services.Auth
{
    public class ApiAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        UserService userService = new UserService();

        private ApplicationUserManager _userManager;

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

            ApplicationUser user = null;
            if (Membership.ValidateUser(context.UserName, context.Password))
            {
                MembershipUser ldapUser = Membership.GetUser(context.UserName);
                user = userService.SyncLdapUser(ldapUser.UserName, ldapUser.Email, context.Password, UserManager);
            }

            if (user == null)
            {
                context.SetError("invalid", "The user name or password is incorrect.");
                return;
            }

            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            identity.AddClaim(new Claim(ClaimTypes.Name, user.UserName));
            identity.AddClaim(new Claim(ClaimTypes.WindowsAccountName, user.Id));

            using (var db = new ApplicationDbContext())
            {
                var usr = (from u in db.Users where u.UserName.Equals(context.UserName) select u).FirstOrDefault();
                var userLogin = new Models.Core.UserLogin { ApplicationUser = usr, Ip = context.OwinContext.Request.RemoteIpAddress };
                db.UserLogins.Add(userLogin);
                db.Entry(userLogin).State = System.Data.Entity.EntityState.Added;
                db.SaveChanges();
            }

            context.Validated(identity);
        }
    }
}