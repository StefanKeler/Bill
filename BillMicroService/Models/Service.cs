using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BillMicroService.Models
{
    public class Service
    {
        public int? Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public string Currency { get; set; }

        public string Description { get; set; }

        public string ShortDescription { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public int IdImage { get; set; }

        public int IdAdvert { get; set; }

        public DateTime DateAdvert { get; set; }

        public int IdAddress { get; set; }

        public Address Address { get; set; }

        public int IdRateing { get; set; }

        public int IdCalendar { get; set; }

        public int IdSeller { get; set; }

        /*Kategorija*/
        public int IdCategory { get; set; }

        public ServiceCategory Category { get; set; }
    }
}