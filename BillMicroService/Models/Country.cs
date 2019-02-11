using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BillMicroService.Models
{
    public class Country
    {
        public int? Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string CountryCode { get; set; }
    }
}