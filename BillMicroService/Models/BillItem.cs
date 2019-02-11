using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BillMicroService.Models
{
    public class BillItem
    {
        public int? Id { get; set; }

        [Required]
        public string BillItemNumber { get; set; }

        [Required]
        public int Amount { get; set; }
     
        public int Discount { get; set; }

        public string ItemType { get; set; }

        public int IdBill { get; set; }
        public Bill Bill { get; set; }

        public int IdService { get; set; }
        public Service Service { get; set; }

        public int IdProduct { get; set; }
        public Product Product { get; set; }

        public int IdAppointment { get; set; }
        public Appointment Appointment { get; set; }
    }

}