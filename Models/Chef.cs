using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChefsNDishes.Models {

    public class RestrictedDate : ValidationAttribute {
        public override bool IsValid (object submittedDate) {
            DateTime date = (DateTime) submittedDate;
            return date < DateTime.Now;
        }
    }

    public class Chef {

        [Key]
        public int Id { get; set; }

        [Required (ErrorMessage = "First name is required.")]
        [MinLength (2, ErrorMessage = "First Name must be at least 2 characters.")]
        [Display (Name = "First Name:")]
        public string FirstName { get; set; }

        [Required (ErrorMessage = "Last name is required.")]
        [MinLength (2, ErrorMessage = "Last Name must be at least 2 characters.")]
        [Display (Name = "Last Name:")]
        public string LastName { get; set; }

        [Required (ErrorMessage = "Date of birth is required.")]
        [DataType (DataType.Date)]
        [Display (Name = "Date of Birth:")]
        [RestrictedDate]
        public DateTime DateOfBirth { get; set; }

        public int Age { get; set; }

        public List<Dish> SubmittedDishes { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}