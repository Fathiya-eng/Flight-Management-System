using Flight_Management_System.Models;

namespace Flight_Management_System
{
    public class Program
    {
        //system storage
        public static FlightContext context = new FlightContext()
        {
            Passengers = new List<Passenger>(),
            Pilots = new List<Pilot>(),
            Flights = new List<Flight>(),
            Bookings = new List<Booking>(),
            Aircrafts = new List<Aircraft>()
        };

        public static void RegisterPassenger() // 01 
        {
            Console.WriteLine("\n=== Register New Passenger ===");

            Console.Write("Enter Passenger Name: ");
            string passengerName = Console.ReadLine();

            Console.Write("Enter Passenger Email: ");
            string passengerEmail = Console.ReadLine();

            Console.Write("Enter Passenger Phone Number: ");
            string passengerPhone = Console.ReadLine();

            Console.Write("Enter Passport Number: ");
            string passportNumber = Console.ReadLine();

            bool passportExists = context.Passengers.Any(p => p.passportNumber == passportNumber); // Passport / national ID number — must be unique per passenger
                                                                                                   // (Any) to return true or false 
            if (passportExists)
            {
                Console.WriteLine("Passenger already exists with this Passport Number.");
                return;
            }

            Console.Write("Enter Passenger Nationality: ");
            string nationality = Console.ReadLine();

            int passengerId = context.Passengers.Count + 1;

            context.Passengers.Add(

                new Passenger
                {
                    passengerId = passengerId,
                    passengerName = passengerName,
                    passengerEmail = passengerEmail,
                    passengerPhone = passengerPhone,
                    passportNumber = passportNumber,
                    nationality = nationality

                }

                );

            Console.WriteLine($"Passenger registered successfully. Assigned ID: {passengerId}");
        }

        public static void AddAircraft() // 02 
        {
            Console.WriteLine("\n=== Add New Aircraft ===");

            Console.Write("Enter Aircraft Model: ");
            string model = Console.ReadLine();

            Console.Write("Enter Total Seats: ");
            int totalSeats = int.Parse(Console.ReadLine());

            int aircraftId = context.Aircrafts.Count + 1;

            context.Aircrafts.Add(
                new Aircraft
                {
                    aircraftId = aircraftId,
                    model = model,
                    totalSeats = totalSeats,
                    isOperational = true
                });

            Console.WriteLine($"Aircraft Added Successfully. ID = {aircraftId}");
        }

        public static void RegisterPilot() // 03 
        {
            Console.WriteLine("\n=== Register Pilot ===");

            Console.Write("Enter Pilot Name: ");
            string pilotName = Console.ReadLine();

            Console.Write("Enter Phone Number: ");
            string pilotPhone = Console.ReadLine();

            Console.Write("Enter License Number: ");
            string licenseNumber = Console.ReadLine();

            int pilotId = context.Pilots.Count + 1;

            context.Pilots.Add(
                new Pilot
                {
                    pilotId = pilotId,
                    pilotName = pilotName,
                    pilotPhone = pilotPhone,
                    licenseNumber = licenseNumber,
                    flightHours = 0, //updated after each completed flight  // When a flight departs, the pilot's flightHours increase by the flight duration
                    isAvailable = true
                });

            Console.WriteLine($"Pilot Registered Successfully. ID = {pilotId}");
        }

        public static void ViewAllFlights() // 04 
        {
            Console.WriteLine("\n=== View All Flights ===");

            if (context.Flights.Any() == false) 
            {
                Console.WriteLine("No Flights Found");
                return;
            }

            foreach (Flight flight in context.Flights)
            {
                Console.WriteLine(
                    $"{flight.flightCode} | " +
                    $"{flight.origin} -> {flight.destination} | " +
                    $"{flight.departureDate} {flight.departureTime} | " +
                    $"Seats: {flight.availableSeats} | " +
                    $"Price: {flight.ticketPrice} | " +
                    $"Status: {flight.status}");
            }
        }

        public static void ScheduleFlight() //05
        {
            Console.WriteLine("\n=== Schedule Flight ===");

            if (context.Aircrafts.Count == 0)
            {
                Console.WriteLine("No Aircraft Available");
                return;
            }

            if (context.Pilots.Count == 0)
            {
                Console.WriteLine("No Pilots Available");
                return;
            }
            //=================================================
            Console.WriteLine($"\n Available Aircraft :");

            foreach (Aircraft a in context.Aircrafts.Where(a => a.isOperational == true))
            {
                Console.WriteLine($" Aircraft ID: {a.aircraftId}  | Model: {a.model} | Total no.of Seats: {a.totalSeats}");
            }

            Console.Write("Enter Aircraft ID: ");
            int aircraftId = int.Parse(Console.ReadLine());

            bool result01 = context.Aircrafts.Any(a => a.aircraftId == aircraftId && a.isOperational == true);
            if (result01 == false) 
            {
                Console.WriteLine("Aircraft Not Found Or Not Operational");
                return;
            }

            //==================================================
            Console.WriteLine($"\n Available Pilot :");

            foreach (Pilot p in context.Pilots.Where(p => p.isAvailable == true))
            {
                Console.WriteLine($" pilot ID: {p.pilotId} | Pilot Name: {p.pilotName}");
            }

            Console.Write("Enter Pilot ID: ");
            int pilotId = int.Parse(Console.ReadLine());

            bool result02 = context.Pilots.Any(p => p.pilotId == pilotId);
            if (result02 == false)
            {
                Console.WriteLine("Pilot Not Found Or Not Available");
                return;
            }

            //===================================================
            Console.Write("Origin: ");
            string origin = Console.ReadLine();

            Console.Write("Destination: ");
            string destination = Console.ReadLine();

            Console.Write("Departure Date: ");
            string departureDate = Console.ReadLine();

            Console.Write("Departure Time: ");
            string departureTime = Console.ReadLine();

            Console.Write("Ticket Price: ");
            decimal ticketPrice = decimal.Parse(Console.ReadLine());

            int flightId = context.Flights.Count + 1;

            Aircraft aircraft = context.Aircrafts.FirstOrDefault(a => a.aircraftId == aircraftId);

            Pilot pilot = context.Pilots.FirstOrDefault(p => p.pilotId == pilotId); 

            context.Flights.Add( new Flight
                {
                    flightId = flightId,
                    flightCode = $"OA-{200 + flightId}",
                    aircraftId = aircraftId,
                    pilotId = pilotId,
                    origin = origin,
                    destination = destination,
                    departureDate = departureDate,
                    departureTime = departureTime,
                    ticketPrice = ticketPrice,
                    availableSeats = aircraft.totalSeats,
                    status = "Scheduled"
                });

            pilot.isAvailable = false;

            Console.WriteLine("\nFlight Scheduled Successfully");
            Console.WriteLine($"Flight Code: OA-{200 + flightId}");
        }

        public static void BookFlight() // 06 
        {
            Console.WriteLine("\n=== Book Flight ===");

            Console.WriteLine("Enter Passenger ID:");
            int passengerId = int.Parse(Console.ReadLine());

            Passenger passenger = context.Passengers.FirstOrDefault(p => p.passengerId == passengerId);

            if (passenger == null)
            {
                Console.WriteLine(RegisterPassenger);
            }

            Console.Write("Enter Destination: ");
            string destination = Console.ReadLine();

            List<Flight> AvailableFlights = context.Flights.Where(f =>  f.destination == destination && f.status == "Scheduled" && f.availableSeats > 0)
                                                           .ToList();

            if (AvailableFlights.Count == 0)
            {
                Console.WriteLine("No Available Flights");
                return;
            }

            foreach (Flight flight in AvailableFlights)
            {
                Console.WriteLine($"{flight.flightId} | {flight.flightCode}");
            }

                Console.WriteLine("Enter Suitable Flight ID:");
                int flightId = int.Parse(Console.ReadLine());

            Flight selectedFlight = context.Flights.FirstOrDefault(f => f.flightId == flightId);

            if (selectedFlight == null)
            {
                Console.WriteLine("Flight Not Found");
                return;
            }

            Console.WriteLine("Enter Booking Date: ");
                string bookingDate = Console.ReadLine();

            int bookingId = context.Bookings.Count + 1;

            context.Bookings.Add(new Booking
            {
                bookingId = bookingId,
                passengerId = passengerId,
                flightId = flightId,
                bookingDate = bookingDate,
                seatNumber = $"{selectedFlight.availableSeats}",
                totalPrice = selectedFlight.ticketPrice,
                status = "Confirmed"
            });

            selectedFlight.availableSeats--;

            Console.WriteLine($"Booking Created Successfully.. Booking ID: {bookingId}");
        }

        public static void CancelBooking() // 07
        {
            Console.WriteLine("\n=== Cancel Booking ===");

            Console.WriteLine("Enter Booking ID:");
            int bookingId = int.Parse(Console.ReadLine());

            Booking booking = context.Bookings.FirstOrDefault(b=> b.bookingId == bookingId);

            if (booking == null)
            {
                Console.WriteLine("Booking Not Found .. Enter correct ID ");
            }

            booking.status = "Cancelled";

            Flight flight = context.Flights.FirstOrDefault(f => f.flightId == booking.flightId);

            flight.availableSeats++;

            Console.WriteLine("Booking Cancelled Successfully");

        }

        public static void DepartFlight() // 08 
        {
            Console.WriteLine("\n=== Depart Flight ===");

            Console.WriteLine("Enter Flight ID:");
            int flightId = int.Parse(Console.ReadLine());

            Flight flight = context.Flights.FirstOrDefault(f=> f.flightId == flightId);

            if (flight == null)
            {
                Console.WriteLine("Flight Not Found");
                return;
            }

            if (flight.status != "Scheduled")
            {
                Console.WriteLine("Only Scheduled Flights Can Depart");
                return;
            }
            flight.status = "Departed";

            Pilot pilot = context.Pilots.FirstOrDefault(p => p.pilotId == flight.pilotId);

            pilot.flightHours += flight.flightDuration;

            Console.WriteLine("Flight Departed Successfully");
            Console.WriteLine($"Pilot Flight Hours Updated To: {pilot.flightHours}");

        }

        public static void CancelFlight() // 09 
        {
            Console.WriteLine("\n=== Cancel Flight ===");

            Console.WriteLine("Enter Flight ID:");
            int flightId = int.Parse(Console.ReadLine());

            Flight flight = context.Flights.FirstOrDefault(f => f.flightId == flightId);

            if (flight == null)
            {
                Console.WriteLine("Flight Not Found");
                return;
            }

           flight.status = "Cancelled";

            List<Booking> booking = context.Bookings.Where(b => b.flightId == flightId && b.status == "Confirmed")
                                                    .ToList();

            foreach (Booking bookings in booking)
            {
                bookings.status = "Cancelled";
            }

            Pilot pilot = context.Pilots.FirstOrDefault(p => p.pilotId == flight.pilotId);

            pilot.isAvailable = true;

            Console.WriteLine($"Flight {flight.flightCode} Cancelled Successfully");
        }

        public static void PassengerBookingHistory() // 10 
        {
            Console.WriteLine("\n=== Passenger Booking History ===");


        }

        public static void FlightRevenueLoadFactorReport() // 11 
        {
            Console.WriteLine("\n=== Flight Revenue Load Factor Report ===");


        }


        static void Main(string[] args)
        {
            bool exit = false;

            while (exit == false) 
            {
                Console.WriteLine("\n========================================");
                Console.WriteLine("   Flight Management System   ");
                Console.WriteLine("========================================");
                Console.WriteLine(" 1  - Register Passenger");
                Console.WriteLine(" 2  - Add Aircraft");
                Console.WriteLine(" 3  - Register Pilot");
                Console.WriteLine(" 4  - View All Flights");
                Console.WriteLine(" 5  - Schedule Flight");
                Console.WriteLine(" 6  - Book Flight");
                Console.WriteLine(" 7  - Cancel Booking");
                Console.WriteLine(" 8  - Depart Flight");
                Console.WriteLine(" 9  - Cancel Flight");
                Console.WriteLine(" 10 - Passenger Booking History");
                Console.WriteLine(" 11 - Flight Revenue & Load Factor Report");
                Console.WriteLine(" 0  - Exit");
                Console.WriteLine("========================================");
                Console.Write("Select option: ");

                int option = int.Parse(Console.ReadLine());

                switch (option)
                {
                    case 1: RegisterPassenger(); break;
                    case 2: AddAircraft(); break;
                    case 3: RegisterPilot(); break;
                    case 4: ViewAllFlights(); break;
                    case 5: ScheduleFlight(); break;
                    case 6: BookFlight(); break;
                    case 7: CancelBooking(); break;
                    case 8: DepartFlight(); break;
                    case 9: CancelFlight(); break;
                    case 10: PassengerBookingHistory(); break;
                    case 11: FlightRevenueLoadFactorReport(); break;
                    case 0: exit = true; break;
                    default: Console.WriteLine("Invalid option. Please try again."); break;
                }

                if (!exit)
                {
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                    Console.Clear();
                }
            }

            Console.WriteLine("Goodbye!");

        }
    }
}
