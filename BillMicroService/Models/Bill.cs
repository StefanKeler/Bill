using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BillMicroService.Models
{
    public class Bill
    {
        public int? Id { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public string BillNumber { get; set; }

        [Required]
        public decimal TotalPrice { get; set; }

        public int BuyerId { get; set; }

        public string Valute { get; set; }

        [Required]
        public int isIssued { get; set; }

        public List<BillItem> BillItems { get; set; }

    }
}