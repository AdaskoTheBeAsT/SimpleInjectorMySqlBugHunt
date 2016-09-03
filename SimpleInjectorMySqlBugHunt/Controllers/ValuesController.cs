namespace SimpleInjectorMySqlBugHunt.Controllers
{
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;
    using System.Web.Http;

    using Dapper;

    using MySql.Data.MySqlClient;

    public class ValuesController : ApiController
    {
        public IHttpActionResult GetValues()
        {
            List<string> retval;
            using (var conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["simysqlbughunt"].ConnectionString))
            {
                retval = conn.Query<string>("SELECT testcol FROM test").ToList();
            }

            return Ok(retval);
        }
    }
}
