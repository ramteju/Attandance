using ProductTracking.Models;
using ProductTracking.Models.Core;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Microsoft.AspNet.Identity;


namespace ProductTracking.Services.Core
{
    public class UserService
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ApplicationUser SyncLdapUser(string userName, string email, string password, ApplicationUserManager UserManager)
        {
            ApplicationUser user = db.Users.Where(u => u.UserName.CompareTo(userName) == 0).FirstOrDefault();//UserManager.Find(model.UserName, model.Password);
            //create user if not exists in db.
            if (user == null)
            {
                user = new ApplicationUser { UserName = userName, Email = email };
                var identityResult = UserManager.Create(user, password);
            }

            //update password if ldap password is different from db password.
            else if (user != null)
            {
                UserManager.RemovePassword(user.Id);
                UserManager.AddPassword(user.Id, password);
            }
            return user;
        }

    }
}