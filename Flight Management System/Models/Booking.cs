using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flight_Management_System.Models
{
    public class Booking
    {
        public int bookingId { get; set; }

        public int passengerId { get; set; }
        public int flightId { get; set; }

        public string seatNumber { get; set; }
        public string bookingDate { get; set; }

        public decimal totalPrice { get; set; }

        public string status { get; set; }

    }
}
