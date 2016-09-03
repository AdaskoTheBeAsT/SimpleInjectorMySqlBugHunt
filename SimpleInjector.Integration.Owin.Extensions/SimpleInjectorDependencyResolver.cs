namespace SimpleInjector.Integration.Owin.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Web.Http.Dependencies;

    internal class SimpleInjectorDependencyResolver : IDependencyResolver
    {
        private readonly Container _container;

        public SimpleInjectorDependencyResolver(Container kernel)
        {
            if (kernel == null)
            {
                throw new ArgumentNullException(nameof(kernel));
            }

            _container = kernel;
        }

        public IDependencyScope BeginScope()
        {
            return this;
        }

        public object GetService(Type serviceType)
        {
            return _container.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            IEnumerable<object> result;
            try
            {
                result = _container.GetAllInstances(serviceType);
            }
            catch (ActivationException)
            {
                result = new List<object>();
            }

            return result;
        }

        public void Dispose()
        {
            _container.Dispose();
        }
    }
}
