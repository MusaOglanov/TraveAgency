﻿using System;
using System.ComponentModel.DataAnnotations;

namespace TraveAgency.Models
{
    public class Income
    {
        public int Id { get; set; }
        public decimal  Money { get; set; }
        [Required]
        public string  About { get; set; }
        public DateTime  CreateTime { get; set; }
        public Kassa Kassa { get; set; }
        public int KassaId { get; set; }
         public AppUser AppUser { get; set; }
        public string AppUserId { get; set; }

    }
}
