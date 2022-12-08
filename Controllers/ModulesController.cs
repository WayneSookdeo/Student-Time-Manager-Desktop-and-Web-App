using Microsoft.AspNetCore.Mvc;
using CalculationsClass;
using Microsoft.Data.SqlClient;
using demo5.Models;
using System.Data;

//Code created with the assitance with the following videos and links:
//https://www.youtube.com/watch?v=VSODgYafRxk
//https://www.youtube.com/watch?v=OGmQjXxOTx8
//https://www.youtube.com/watch?v=MHgaraW2Fc0
namespace demo5.Controllers
{
    public class ModulesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult InsertModules()
        {
            return View();
        }

        [HttpPost]
        public IActionResult InsertModules(Calculations c)
        {
            UserRegClass ur = new UserRegClass();
            string Sqlconn = "Server=tcp:progpart3.database.windows.net,1433;Initial Catalog=progdbpart3;Persist Security Info=False;User ID=admn;Password=Wentnews46;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
           
            SqlConnection conn = new SqlConnection(Sqlconn);
            conn.Open();
            string insertmodule = "INSERT INTO Module (ModuleCode, ModuleName,ModuleCredits,ModuleWeeklyHours,ModuleHoursStudied,SemesterWeeks) VALUES (@mc,@mn,@mcr,@mchw,@mhs,@w)";
            string insertbridge = "INSERT INTO UserDetail_Module(Username,ModuleID) Values (@Username,@id)";
            string moduleidget = "SELECT COUNT(ModuleID) FROM Module";
            SqlCommand moduleinsertquery = new SqlCommand(insertmodule, conn);
            SqlCommand modulebridgequery = new SqlCommand(insertbridge, conn);
            SqlCommand moduleidgetquery = new SqlCommand(moduleidget, conn);
            moduleinsertquery.Parameters.AddWithValue("@mc", c.SCode);
            moduleinsertquery.Parameters.AddWithValue("@mn", c.SName);
            moduleinsertquery.Parameters.AddWithValue("@mcr", c.DCredits);
            moduleinsertquery.Parameters.AddWithValue("@mchw", c.DHoursWeeks);
            moduleinsertquery.Parameters.AddWithValue("@w", c.DWeeks);
            moduleinsertquery.Parameters.AddWithValue("@mhs", c.Hourstobestudied());


            moduleinsertquery.ExecuteNonQuery();

            modulebridgequery.Parameters.AddWithValue("@id", ((Int32)moduleidgetquery.ExecuteScalar()));
            modulebridgequery.Parameters.AddWithValue("@Username",UserRegistrationController.loggedin);
            modulebridgequery.ExecuteNonQuery();

            ViewData["Message"] = "Modules Entered Succesful";
            return View(c);
        }

        [HttpGet]
        public IActionResult showinfo()
        {
            string Sqlconn = "Server=tcp:progpart3.database.windows.net,1433;Initial Catalog=progdbpart3;Persist Security Info=False;User ID=admn;Password=Wentnews46;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

            SqlConnection conn = new SqlConnection(Sqlconn);
            conn.Open();
            string views = "SELECT * FROM Module JOIN UserDetail_Module ON Module.ModuleID = UserDetail_Module.ModuleID Join UserDetails on UserDetails.Username = UserDetail_module.Username WHERE userDetails.Username= @Username";
            SqlCommand viewsquery = new SqlCommand(views, conn);
            viewsquery.Parameters.AddWithValue("@Username", UserRegistrationController.loggedin);
            SqlDataAdapter sda = new SqlDataAdapter(viewsquery);
           DataSet ds = new DataSet();
            sda.Fill(ds);
            List<Calculations> ca = new List<Calculations>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                ca.Add(new Calculations
                {
          SCode= dr["ModuleCode"].ToString(),
          SName=  dr["ModuleName"].ToString(),
          DCredits= Convert.ToDouble(dr["ModuleCredits"]),
          DHoursWeeks = Convert.ToDouble(dr["ModuleWeeklyHours"]),
           DtoCompleteStudy = Convert.ToDouble(dr["ModuleHoursStudied"]),


                });
            }
            ViewData["ca"]=ca;
            return View();
        }
       
    }
}
