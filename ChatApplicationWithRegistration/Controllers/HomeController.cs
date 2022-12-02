using ChatApplicationWithRegistration.Hubs;
using ChatApplicationWithRegistration.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ChatApplicationWithRegistration.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Registration()
        {
            ViewBag.Message = "Registration page";

            return View();
        }

        public JsonResult IsUserNameAvailable(string UserName)
        {
            UserDBContext db = new UserDBContext();

            return Json(!db.Users.Any(x => x.UserName == UserName), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Registration(User user)
        {
            UserRepo repo = new UserRepo();
            UserDBContext db = new UserDBContext();
            if (ModelState.IsValid)
            {
                if (db.Users.Any(x => x.UserName == user.UserName))
                {

                    ModelState.AddModelError("UserName", "User Name is already in use. Try another?");
                }
                else
                {
                    repo.RegistrationOfUser(user);
                    return RedirectToAction("Login");
                }


            }
            return View(user);

        }

        [HttpGet]
        public ActionResult Login(string ReturnUrl)
        {
            ViewBag.DemandedUrl = ReturnUrl;
            return View();
        }
        [HttpPost]
        public ActionResult Login(string userName, string password, string ReturnUrl)
        {

            if (AuthenticateUser(userName, password))
            {
                FormsAuthentication.SetAuthCookie(userName, false);
                return RedirectToLocal(ReturnUrl);
            }
            else
            {
                ModelState.AddModelError("Password", "User Name/Password is invalid");
                return View();
            }



        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Welcome", "Home");
        }

        private bool AuthenticateUser(string username, string password)
        {
            string CS = ConfigurationManager.ConnectionStrings["DBCS_Chat"].ConnectionString;



            // SqlConnection is in System.Data.SqlClient namespace
            using (SqlConnection con = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("spAuthenticateUser", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramUsername = new SqlParameter("@UserName", username);
                // FormsAuthentication calss is in System.Web.Security namespace
                string encryptedPassword = FormsAuthentication.
                    HashPasswordForStoringInConfigFile(password, "SHA1");
                SqlParameter paramPassword = new SqlParameter("@Password", encryptedPassword);



                cmd.Parameters.Add(paramUsername);
                cmd.Parameters.Add(paramPassword);


                con.Open();
                int ReturnCode = (int)cmd.ExecuteScalar();
                return ReturnCode == 1;


            }


        }
        [Authorize]
        public ActionResult Welcome()
        {//Get current user name!!!
            string username = HttpContext.User.Identity.Name;
            ViewBag.UserName = username;

            return View();
        }

        public JsonResult GetTop6Message(string sender, string receiver, int pageNumber, int pageSize)
        {
            MessengerRepo repo = new MessengerRepo();
           List<Messenger> list= repo.ReadMessage(sender, receiver, pageNumber, pageSize);
            return Json(list, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public ActionResult LogOff()
        {//For LogOut!!!
            ChatHub myHub = new ChatHub();
            myHub.Disconnect();


            FormsAuthentication.SignOut();

            return RedirectToAction("Login");
        }



    }
}