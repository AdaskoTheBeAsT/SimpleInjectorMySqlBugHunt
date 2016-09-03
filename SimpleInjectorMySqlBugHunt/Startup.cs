using Microsoft.Owin;
using SimpleInjectorMySqlBugHunt;

[assembly: OwinStartup(typeof(Startup))]
namespace SimpleInjectorMySqlBugHunt
{
    using System.Runtime.Remoting.Messaging;
    using System.Web.Http;

    using Microsoft.Owin.Extensions;

    using Owin;

    using SimpleInjector;
    using SimpleInjector.Extensions.ExecutionContextScoping;

    using SimpleInjectorMySqlBugHunt.Code;

    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var httpConfiguration = new HttpConfiguration();

            var container = new Container();
            container.Options.DefaultScopedLifestyle = new ExecutionContextScopeLifestyle();
            app.Use(
                async (context, next) =>
                    {
                        using (container.BeginExecutionContextScope())
                        {
                            await next();
                        }
                    });

            app.Use(
                async (context, next) =>
                    {
                        CallContext.LogicalSetData("IOwinContext", context);
                        await next();
                    });

            container.RegisterSingleton<IOwinContextProvider>(new CallContextOwinContextProvider());


            // Configure Web API Routes:
            // - Enable Attribute Mapping
            // - Enable Default routes at /api.
            httpConfiguration.MapHttpAttributeRoutes();
            httpConfiguration.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional });

            app.UseWebApi(httpConfiguration);

            app.UseStageMarker(PipelineStage.MapHandler);
        }
    }
}
