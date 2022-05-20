
namespace MVC_Enterprise_Organization.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class HRData
    {
        public int Id { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "SSN must be numeric")]
        public string SSN { get; set; }

        [Required]
        public Nullable<int> EmployeeId { get; set; }

        [Required]
        public Nullable<int> Salary { get; set; }

        //[Required]
        public virtual Employee Employee { get; set; }
    }
}
