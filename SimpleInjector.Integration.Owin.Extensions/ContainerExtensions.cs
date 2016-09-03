namespace SimpleInjector.Integration.Owin.Extensions
{
    using System;

    internal static class ContainerExtensions
    {
        internal static object TryGet(this Container container, Type type)
        {
            object result;
            try
            {
                result = container.GetInstance(type);
            }
            catch (ActivationException)
            {
                result = null;
            }

            return result;
        }
    }
}
