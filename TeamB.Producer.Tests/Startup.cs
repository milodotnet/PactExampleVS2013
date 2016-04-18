using System.Web.Http;
using Owin;

namespace TeamB.Producer.Tests
{
    public class Startup
    {
       
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();
            WebApiConfig.Register(config);
            app.UseWebApi(config);
        }
      

    }
}