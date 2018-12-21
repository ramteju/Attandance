using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Mvc;
using System.Web.Mvc;
using ProductTracking.Controllers;
using System.Web.Http;
using ProductTracking.Controllers.Auth;

namespace ProductTracking.App_Start
{
    public class Bootstrapper
    {
        public static IUnityContainer Initialise()
        {
            var container = BuildUnityContainer();
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
            // register dependency resolver for WebAPI
            GlobalConfiguration.Configuration.DependencyResolver = new Unity.WebApi.UnityDependencyResolver(container);
            return container;
        }
        private static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();

            container.RegisterType<AccountController>(new InjectionConstructor());

            //service classes
           

            RegisterTypes(container);
            return container;
        }
        public static void RegisterTypes(IUnityContainer container)
        {

        }
    }
}