using Microsoft.AspNetCore.Mvc;
using Microsoft.Data;
using Microsoft.Data.SqlClient;
using System.Diagnostics;
using demo5.Models;
using DocumentFormat.OpenXml.Office.CustomUI;
using System.Data;

namespace demo5.Controllers
{
    public class UserRegistrationController : Controller
    {
        public static string loggedin;
        public static List<ReminderClass> results = new List<ReminderClass>();
        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(UserRegClass ur)
        {

            string salt = CreatSalt(3);
            string hashedpw = GenerateSHA256Hash(ur.HashedPassword, salt);
            string Sqlconn = "Server=tcp:progpart3.database.windows.net,1433;Initial Catalog=progdbpart3;Persist Security Info=False;User ID=admn;Password=Wentnews46;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            SqlConnection conn = new SqlConnection(Sqlconn);
            string command = "INSERT INTO UserDetails (Username , HashedPassword, Salt) VALUES ('" + ur.Username + "','" + hashedpw + "','" + salt + "')";
            SqlCommand cmd = new SqlCommand(command, conn);
            conn.Open();
            cmd.ExecuteNonQuery();
            loggedin = ur.Username;
            ViewData["Message"] = "Account created";
            return View(ur);


        }
        public string CreatSalt(int size)
        {
            var rng = new System.Security.Cryptography.RNGCryptoServiceProvider();
            var buff = new byte[size];
            rng.GetBytes(buff);
            return Convert.ToBase64String(buff);
        }
        public string GenerateSHA256Hash(string input, string salt)
        {
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(input + salt);
            System.Security.Cryptography.SHA256Managed sha256hashstring =
                new System.Security.Cryptography.SHA256Managed();
            byte[] hash = sha256hashstring.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(UserRegClass ur)
        {


            string Sqlconn = "Server=tcp:progpart3.database.windows.net,1433;Initial Catalog=progdbpart3;Persist Security Info=False;User ID=admn;Password=Wentnews46;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            SqlConnection conn = new SqlConnection(Sqlconn);

            string CheckUser = "select count(*) from UserDetails where Username = @Username";
            string CheckPassword = "SELECT HashedPassword FROM UserDetails Where Username = @Username";
            string CheckSalt = "SELECT Salt FROM UserDetails Where Username = @Username";
            loggedin = ur.Username;
            SqlCommand com2 = new SqlCommand(CheckUser, conn);
            SqlCommand com3 = new SqlCommand(CheckPassword, conn);
            SqlCommand com4 = new SqlCommand(CheckSalt, conn);

            com2.Parameters.AddWithValue("@Username", ur.Username);
            com3.Parameters.AddWithValue("@Username", ur.Username);
            com4.Parameters.AddWithValue("@Username", ur.Username);

            conn.Open();
            string dbval = Convert.ToString(com3.ExecuteScalar());
            string userinput = GenerateSHA256Hash(ur.HashedPassword, Convert.ToString(com4.ExecuteScalar()));

            if (((int)com2.ExecuteScalar() > 0) && dbval.Equals(userinput))
            {
                ReminderClass rc = new ReminderClass();
                ViewData["Message"] = "Login Succesful";
                Thread.Sleep(2000);

                string query = "SELECT * FROM Reminders JOIN UserDetails_Reminders ON Reminders.ReminderID = UserDetails_Reminders.ReminderID Join UserDetails on UserDetails.Username = UserDetails_Reminders.Username WHERE userDetails.Username= '"+UserRegistrationController.loggedin+"'";
                




                // Create a command to execute the query
                using (SqlCommand command = new SqlCommand(query, conn))
                {
                   
                    // Execute the query and get the result set
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        // Read each row in the result set
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {

                                rc.modstudy = reader.GetString(1);
                                rc.datestudy = reader.GetDateTime(2);

                                results.Add(rc);
                                TempData["Message"] = "Study for " + rc.modstudy;
                            }
                        }
                        else
                        {
                            TempData["Message"] = null;
                        }


                    }

                }

               





                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewData["Message"] = "Create Account";

                return RedirectToAction("Create", "UserRegistration");
            }
            return View(ur);


        }
    }
}
