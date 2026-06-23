using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flight_Management_System.Models
{
    public class Aircraft
    {
        public int aircraftId { get; set; }
        public string model { get; set; }
        public int totalSeats { get; set; }
        public bool isOperational { get; set; }

    }
}
