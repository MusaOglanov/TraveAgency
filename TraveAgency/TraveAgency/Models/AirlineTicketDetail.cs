using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace TraveAgency.Models
{
    public class AirlineTicketDetail
    {
        public int Id { get; set; }
        public bool IsTransfer { get; set; }
        public DateTime TransferTime { get; set; }
        public TimeSpan FlightDuration { get; set; }
        public int TransferPrice { get; set; }
        public bool IsReturn { get; set; }
        public DateTime ReturnTime { get; set; }
        public int ReturnPrice { get; set; }
        public bool HassBaggage { get; set; }
        public string BaggageAllowance { get; set; }
        public int BaggagePrice { get; set; }
        public string Handluggage { get; set; }
        public string FlightDescription { get; set; }
        public bool HassMealService { get; set; }
        public string MealDescription { get; set; }
        public int MealPrice { get; set; }
        public bool IsDeactive { get; set; }
        [ForeignKey("AirlineTicket")]
        [NotMapped]
        public AirlineTicket AirlineTicket { get; set; }

    }
}
