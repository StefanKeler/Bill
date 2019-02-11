using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BillMicroService.Models
{
    public class ServiceCategory
    {
        public int? Id { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Name { get; set; }

        public int IdParent { get; set; }

        public ServiceCategory Parent { get; set; }
    }
}