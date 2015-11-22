﻿using System.Web.Http;
using Microsoft.Owin;
using Owin;
using Microsoft.AspNet.Identity;
using PmiOfficial;

[assembly: OwinStartup(typeof(Startup))]

namespace PmiOfficial
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseWebApi(RegisterHttpConfiguration());
            ConfigureAuth(app);
        }


        public static HttpConfiguration RegisterHttpConfiguration()
        {
            HttpConfiguration config = new HttpConfiguration();
            WebApiConfig.Register(config);
            return config;
        }

        public static void ConfigureAuth(IAppBuilder app)
        {
            app.UseCookieAuthentication(new Microsoft.Owin.Security.Cookies.CookieAuthenticationOptions
            {
                LoginPath = new PathString("/api/Account/Login"),
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
            });
            return;
        }

    }
}
