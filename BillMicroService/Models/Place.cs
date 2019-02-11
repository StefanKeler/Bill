using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BillMicroService.Models
{
    public class Place
    {
        public int? Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int ZipCode { get; set; }

        [Required]
        public int IdCountry { get; set; }

        public Country Country { get; set; }
    }
}