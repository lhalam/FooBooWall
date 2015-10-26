using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Owin;
using System.Web.Http;

[assembly: OwinStartup(typeof(PmiOfficial.Startup))]

namespace PmiOfficial
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseWebApi(RegisterHttpConfiguration());
        }


        public static HttpConfiguration RegisterHttpConfiguration()
        {
            HttpConfiguration config = new HttpConfiguration();
            WebApiConfig.Register(config);
            return config;
        }
    }
}
