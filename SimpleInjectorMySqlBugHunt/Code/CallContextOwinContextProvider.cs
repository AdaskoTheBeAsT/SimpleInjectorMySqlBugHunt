namespace SimpleInjectorMySqlBugHunt.Code
{
    using System.Runtime.Remoting.Messaging;

    using Microsoft.Owin;

    public class CallContextOwinContextProvider : IOwinContextProvider
    {
        public IOwinContext CurrentContext
        {
            get { return (IOwinContext)CallContext.LogicalGetData("IOwinContext"); }
        }
    }
}