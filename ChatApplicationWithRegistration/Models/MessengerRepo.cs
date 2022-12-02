using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ChatApplicationWithRegistration.Models
{
    public class MessengerRepo
    {
       public void InsertMessage(Messenger messenger)
        {

            string CS = ConfigurationManager.ConnectionStrings["DBCS_Chat"].ConnectionString;



            // SqlConnection is in System.Data.SqlClient namespace
            using (SqlConnection con = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("spInsertMessage", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter sender = new SqlParameter("@Sender", messenger.Sender);
                SqlParameter receiver = new SqlParameter("@Receiver", messenger.Receiver);
              
                SqlParameter message = new SqlParameter("@Message", messenger.Message);


                cmd.Parameters.Add(sender);
                cmd.Parameters.Add(receiver);
                cmd.Parameters.Add(message);


                con.Open();
                cmd.ExecuteNonQuery();

            }


        }


        public List<Messenger> ReadMessage(string sender, string receiver, int pageNumber, int pageSize)
        {
            List<Messenger> lista = new List<Messenger>();
            string CS = ConfigurationManager.ConnectionStrings["DBCS_Chat"].ConnectionString;

            using (SqlConnection con = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "spGetMessage";
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter send = new SqlParameter("@Sender", sender);
                SqlParameter rec = new SqlParameter("@Receiver", receiver);
                SqlParameter num = new SqlParameter("@PageNumber", pageNumber);
                SqlParameter size = new SqlParameter("@PageSize", pageSize);

                cmd.Parameters.Add(send);
                cmd.Parameters.Add(rec);
                cmd.Parameters.Add(num);
                cmd.Parameters.Add(size);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Messenger messenger = new Messenger();
                    messenger.Id = Convert.ToInt32(rdr["Id"]);

                    messenger.Sender = Convert.ToString(rdr["Sender"]);
                    messenger.Receiver = Convert.ToString(rdr["Receiver"]);
                    messenger.Message = Convert.ToString(rdr["Message"]);
                    messenger.Datum = Convert.ToDateTime(rdr["Datum"]);
                    lista.Add(messenger);

                }
                return lista;


            }
        }

    }
}