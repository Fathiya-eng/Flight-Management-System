using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flight_Management_System.Models
{
    public class Passenger
    {
        public int passengerId { get; set; }
        public string passengerName { get; set; }
        public string passengerEmail { get; set; }
        public string passengerPhone { get; set; }
        public string passportNumber { get; set; }
        public string nationality { get; set; }

    }
}
