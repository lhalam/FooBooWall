using System.Web.Http;
using Microsoft.Owin;
using Owin;
using PmiOfficial;

[assembly: OwinStartup(typeof(Startup))]

namespace PmiOfficial
{
    public class Startup
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
