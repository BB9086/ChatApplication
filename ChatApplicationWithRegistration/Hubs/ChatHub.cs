using ChatApplicationWithRegistration.Models;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace ChatApplicationWithRegistration.Hubs
{
    public class ChatHub:Hub
    {
        static long counter = 0;
        public static Dictionary<string, List<string>> dictionaryOfConnections = new Dictionary<string, List<string>>();

        public void SendMessage(string message, string userId)
        {
            MessengerRepo repo = new MessengerRepo();
            Messenger messenger = new Messenger();

            List<string> connectionIds = dictionaryOfConnections[userId];
            var senderId = System.Web.HttpContext.Current.User.Identity.Name;
           
            messenger.Sender = senderId;
            messenger.Receiver = userId;
            messenger.Message = message;
            string dateAndTime = DateTime.Now.ToString("dd/MM/yyyy h:mm tt");

            Clients.Clients(connectionIds).SendMessage(message, userId, senderId, dateAndTime);

            repo.InsertMessage(messenger);


        }



        public void SendUserList(List<string> users)
        {
            var context = GlobalHost.ConnectionManager.GetHubContext<ChatHub>();
            context.Clients.All.UpdateConnections(users);
        }

        public override Task OnConnected()
        {
            string userName= Thread.CurrentPrincipal.Identity.Name;
            var connectionId = Context.ConnectionId;

            if (dictionaryOfConnections.ContainsKey(userName))
            {

                List<string> listOfConnections = dictionaryOfConnections[userName];
                listOfConnections.Add(connectionId);
                dictionaryOfConnections.Remove(userName);
                dictionaryOfConnections.Add(userName, listOfConnections);
                List<string> usersList = new List<string>();
                foreach (string key in dictionaryOfConnections.Keys)
                {
                    usersList.Add(key);
                }
                SendUserList(usersList);
                //Clients.All.UpdateCount(counter);

            }
            else
            {
                counter = counter + 1;
                List<string> listOfConnections = new List<string>();
                listOfConnections.Add(connectionId);
                dictionaryOfConnections.Add(userName, listOfConnections);
                List<string> usersList = new List<string>();
                foreach (string key in dictionaryOfConnections.Keys)
                {
                    usersList.Add(key);
                }
                SendUserList(usersList);
                //Clients.All.UpdateCount(counter);

            }

            return base.OnConnected();

            
        }

        public void Disconnect()
        {
            string userName = System.Web.HttpContext.Current.User.Identity.Name;


            if (dictionaryOfConnections.ContainsKey(userName))
            {
                counter = counter - 1;
                dictionaryOfConnections.Remove(userName);
                List<string> usersList = new List<string>();
                foreach (string key in dictionaryOfConnections.Keys)
                {
                    usersList.Add(key);
                }
                SendUserList(usersList);

                var context = GlobalHost.ConnectionManager.GetHubContext<ChatHub>();
                //context.Clients.All.UpdateCount(counter);

            }

        }
    }
}