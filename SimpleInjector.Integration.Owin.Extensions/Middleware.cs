namespace SimpleInjector.Integration.Owin.Extensions
{
    using System.Web.Http;

    using global::Owin;

    using SimpleInjector.Integration.Owin;

    public static class Middleware
    {
        public static void UseSimpleInjector(this IAppBuilder app, Container container, HttpConfiguration configuration)
        {
            app.UseOwinRequestLifestyle();
            container.Options.DefaultScopedLifestyle = new OwinRequestLifestyle();
            container.RegisterSingleton<IAppBuilder>(app);
            configuration.DependencyResolver = new SimpleInjectorDependencyResolver(container);
        }
    }
}
