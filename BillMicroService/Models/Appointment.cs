using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BillMicroService.Models
{
    public class Appointment
    {
        public int? Id { get; set; }

        [Required]
        public DateTime StartDate { get; set; }


        public DateTime EndDate { get; set; }

        [Required]
        public int IdCalendar { get; set; }
       

        public int Reserved { get; set; }
    }
}