namespace SimpleInjectorMySqlBugHunt.Code
{
    using Microsoft.Owin;

    public interface IOwinContextProvider
    {
        IOwinContext CurrentContext { get; }
    }
}