using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Owin;
using PmiOfficial;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

[assembly: OwinStartup(typeof(PmiOfficial.Startup))]

namespace PmiOfficial
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            //app.UseWebApi(RegisterHttpConfiguration());
            
        }


        public static HttpConfiguration RegisterHttpConfiguration()
        {
            HttpConfiguration config = new HttpConfiguration();
            WebApiConfig.Register(config);
            return config;
        }
    }
}
