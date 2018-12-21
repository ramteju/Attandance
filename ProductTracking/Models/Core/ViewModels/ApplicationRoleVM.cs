using System;
using System.ComponentModel.DataAnnotations;

namespace ProductTracking.Models.Core.ViewModels
{
    public class ApplicationRoleVM
    {
        [Required(ErrorMessage = "Name is mandatory")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Code is mandatory")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Order is mandatory")]
        public int PriorityOrder { get; set; }        

        [Required]
        public string oper { get; set; }
    }
}