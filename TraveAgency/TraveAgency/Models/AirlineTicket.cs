using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace TraveAgency.Models
{
    public class AirlineTicket
    {
        public int Id { get; set; }
        [Required]
        public string AirlineCompany { get; set; }
        public string FlightNumber { get; set; }
        public int DepartureAirportId { get; set; }
        public Airport DepartureAirport { get; set; }
        public int ArrivalAirportId { get; set; }
        public Airport ArrivalAirport { get; set; }
        public int TransferAirportId { get; set; }
        public Airport TransferAirport { get; set; }
        public int ReturnAirportId { get; set; }
        public Airport ReturnAirport { get; set; }
        public DateTime DepartureDateTime { get; set; }
        public DateTime ArrivalDateTime { get; set; }
        public TimeSpan FlightDuration { get; set; }
        public int TicketPrice { get; set; }
        public AirlineTicketDetail AirlineTicketDetail { get; set; }
        public ICollection<SeatClass> SeatClasses { get; set; }


    }
}
