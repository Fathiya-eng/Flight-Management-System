using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flight_Management_System.Models
{
    public class Flight
    {
        public int flightId { get; set; } //system generated
        public string flightCode { get; set; } //system calculated

        public int aircraftId { get; set; } // user input
        public int pilotId { get; set; } // user input

        public string origin { get; set; } // user input
        public string destination { get; set; } // user input

        public string departureDate { get; set; } // user input
        public string departureTime { get; set; } // user input

        public decimal ticketPrice { get; set; } // user input
        public int flightDuration { get; set; } //system calculated

        public int availableSeats { get; set; } //system calculated

        public string status { get; set; } // default value

    }
}
