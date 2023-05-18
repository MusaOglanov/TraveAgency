using System.Collections.Generic;
using TraveAgency.Models;

namespace TraveAgency.ViewModels
{
    public class HomeVM
    {
        public Tour Tour { get; set; }
        public AirlineTicket AirlineTicket { get; set; }
        public Hotel Hotel { get; set; }
    }
}
