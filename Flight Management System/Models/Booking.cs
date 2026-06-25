using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flight_Management_System.Models
{
    public class Booking
    {
        public int bookingId { get; set; } //system generated

        public int passengerId { get; set; } // user input
        public int flightId { get; set; } // user input

        public string seatNumber { get; set; } //system calculated
        public string bookingDate { get; set; } // user input

        public decimal totalPrice { get; set; } //system calculated

        public string status { get; set; } // default value

    }
}
