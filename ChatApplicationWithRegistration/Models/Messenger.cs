using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChatApplicationWithRegistration.Models
{
    public class Messenger
    {
        public int Id { get; set; }
        public string Sender { get; set; }
        public string Receiver { get; set; }
        public string Message { get; set; }
        public DateTime Datum { get; set; }
    }
}