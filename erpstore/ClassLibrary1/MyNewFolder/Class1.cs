using gdfglfdjglfdkgfd.Models;

namespace SomeRandomAssembly.MyNewFolder
{
    public class Class1
    {
        public void Authenticate()
        {
            var authenticationServices = new AuthenticationServices();

            var customerServices = new CustomerServices();


            authenticationServices.Authenticate("user", "pass");


        }

    }
}
