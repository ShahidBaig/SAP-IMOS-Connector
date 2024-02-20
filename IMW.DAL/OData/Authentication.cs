using System;
using System.Threading.Tasks;
using Microsoft.OData.Client;
using SAPB1;
namespace IMW.DAL
{
    public class Authentication
    {
        public static void Login(ServiceLayer slContext,
        string CompanyDB,
        string UserName,
        string Password,
        string language)
        {
            B1Session session = slContext.Login(CompanyDB, UserName, Password,
           language).GetValue();
            Console.WriteLine("SessionId={0}, Version={1}", session.SessionId,
           session.Version);
        }
        public static void Logout(ServiceLayer slContext)
        {
            OperationResponse operationResponse = slContext.Logout().Execute();
            Console.WriteLine("Logout code = {0}", operationResponse.StatusCode);
        }
    }
}