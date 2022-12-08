using CalculationsClass;
using demo5.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;

namespace demo5.Controllers
{
    public class ReminderController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult CreateReminder()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateReminder(ReminderClass rc)
        {


            string Sqlconn = "Server=tcp:progpart3.database.windows.net,1433;Initial Catalog=progdbpart3;Persist Security Info=False;User ID=admn;Password=Wentnews46;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            SqlConnection conn = new SqlConnection(Sqlconn);
            string reminderidget = "SELECT COUNT(ReminderID) FROM Reminders";
            string insertbridge = "INSERT INTO UserDetails_Reminders(Username,ReminderID) Values (@Username,@id)";
            string command = "INSERT INTO Reminders (ModuleStudy , StudyDate) VALUES ('" + rc.modstudy + "','" + Convert.ToDateTime(rc.datestudy) + "')";


            SqlCommand remindereidgetquery = new SqlCommand(reminderidget, conn);
            SqlCommand insertbridgequry = new SqlCommand(insertbridge, conn);
            SqlCommand cmd = new SqlCommand(command, conn);

            conn.Open();
            remindereidgetquery.ExecuteNonQuery();
            insertbridgequry.Parameters.AddWithValue("@id", ((Int32)remindereidgetquery.ExecuteScalar()));
            insertbridgequry.Parameters.AddWithValue("@Username", UserRegistrationController.loggedin);
            insertbridgequry.ExecuteNonQuery();
            cmd.ExecuteNonQuery();
            
           
            conn.Close();
            return View(rc);


        }
        [HttpGet]
        public IActionResult showinforeminders()
        {
            string Sqlconn = "Server=tcp:progpart3.database.windows.net,1433;Initial Catalog=progdbpart3;Persist Security Info=False;User ID=admn;Password=Wentnews46;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

           
            return View();
        }
    }
}
