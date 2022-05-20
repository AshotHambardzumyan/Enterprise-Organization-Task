
namespace MVC_Enterprise_Organization.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Employee
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Employee()
        {
            this.HRDatas = new HashSet<HRData>();
        }

        public int Id { get; set; }

        [Required]
        [Display(Name = "Employee Name")]
        [RegularExpression("^[a-zA-Z]*$", ErrorMessage = "Only Alphabets allowed.")]

        public string Name { get; set; }

        [Required]
        [RegularExpression("^[a-zA-Z]*$", ErrorMessage = "Only Alphabets allowed.")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Date Of Birth")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MMM/yyyy}")]
        public System.DateTime BirthDate { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required]
        //[RegularExpression("[^0-9]", ErrorMessage = "PhoneNumber must be numeric")]
        public string PhoneNumber { get; set; }

        [Required]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Please enter a valid e-mail adress")]
        public string Email { get; set; }

        public string Letter { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HRData> HRDatas { get; set; }
    }
}
