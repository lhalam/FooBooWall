using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Owin;
using System.Web.Http;
using Microsoft.AspNet.Identity;

[assembly: OwinStartup(typeof(PmiOfficial.Startup))]

namespace PmiOfficial
{
    public partial class Startup
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
