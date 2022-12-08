using demo5.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using CalculationsClass;
namespace demo5.Controllers
{
    public class RecordHoursController : Controller
    {
       
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Record()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Record(RecordHoursClass rhc)
        {
         
            
            string Sqlconn = "Server=tcp:progpart3.database.windows.net,1433;Initial Catalog=progdbpart3;Persist Security Info=False;User ID=admn;Password=Wentnews46;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

            SqlConnection conn = new SqlConnection(Sqlconn);
            conn.Open();
            string recordhour = "UPDATE Module SET ModuleHoursStudied = ModuleHoursStudied - @Hoursstudied FROM Module JOIN UserDetail_Module ON Module.ModuleID = UserDetail_Module.ModuleID Join UserDetails on UserDetails.username = UserDetail_module.Username WHERE userDetails.Username= '" + UserRegistrationController.loggedin + "' AND Module.ModuleCode=@module ";
            SqlCommand recordhourquery = new SqlCommand(recordhour, conn);
            recordhourquery.Parameters.AddWithValue("@Hoursstudied", rhc.HoursStudied);
            recordhourquery.Parameters.AddWithValue("@module", rhc.CodeStudied);
            recordhourquery.ExecuteNonQuery();


            conn.Close();
            return View(rhc);
        }
    }
}
