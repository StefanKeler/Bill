using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BillMicroService.Models
{
    public class Calendar
    {
        public int? Id { get; set; }

        [Required]
        public int Duration { get; set; }

        [Required]
        public string Type { get; set; }

        public List<Appointment> AppointmentList { get; set; }



    }
}