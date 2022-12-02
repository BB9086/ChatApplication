using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace ChatApplicationWithRegistration.Models
{
    public class UserRepo
    {

        public void RegistrationOfUser(User user)
        {
            string CS = ConfigurationManager.ConnectionStrings["DBCS_Chat"].ConnectionString;



            // SqlConnection is in System.Data.SqlClient namespace
            using (SqlConnection con = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("spRegisterUser", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter username = new SqlParameter("@UserName", user.UserName);
                // FormsAuthentication calss is in System.Web.Security namespace
                string encryptedPassword = FormsAuthentication.
                    HashPasswordForStoringInConfigFile(user.Password, "SHA1");
                SqlParameter password = new SqlParameter("@Password", encryptedPassword);
                SqlParameter photo = new SqlParameter("@Photo", user.Photo);


                cmd.Parameters.Add(username);
                cmd.Parameters.Add(password);
                cmd.Parameters.Add(photo);

                con.Open();
                cmd.ExecuteNonQuery();

            }


        }
    }
}