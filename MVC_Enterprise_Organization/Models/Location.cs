
namespace MVC_Enterprise_Organization.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Location
    {
        public int Id { get; set; }

        [Required]
        public string CompanyLocation { get; set; }

        [Required]
        public string BuildingId { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string ZipCode { get; set; }

        [Required]
        [RegularExpression("^[a-zA-Z]*$", ErrorMessage = "Only Alphabets allowed.")]
        public string ManagerName { get; set; }
    }
}