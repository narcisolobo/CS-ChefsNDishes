using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChefsNDishes.Models {

    public class Positive : ValidationAttribute {
        public override bool IsValid (object submittedCalorieCount) {
            int count = (int) submittedCalorieCount;
            return count > 0;
        }
    }

    public class Dish {

        [Key]
        public int Id { get; set; }

        [Required (ErrorMessage = "Name of dish is required.")]
        [MinLength (2, ErrorMessage = "Name of dish must be at least 2 characters.")]
        [Display (Name = "Name of Dish:")]
        public string Name { get; set; }

        [Required (ErrorMessage = "Description of dish is required.")]
        [MinLength (10, ErrorMessage = "Description of dish must be at least 10 characters.")]
        [Display (Name = "Description of Dish:")]
        public string Description { get; set; }

        [Required (ErrorMessage = "Calories are required.")]
        [Display (Name = "Calorie Count:")]
        [Positive]
        public int Calories { get; set; }

        [Required (ErrorMessage = "Tastiness is required.")]
        [Display (Name = "Tastiness Rating:")]
        [Range(1,6, ErrorMessage = "Tastiness rating must be between 1 and 5.")]
        public int Tastiness { get; set; }

        [Required (ErrorMessage = "Please choose a chef.")]
        [Display (Name = "Submitted By:")]
        public int ChefId {get;set;}

        public Chef SubmittedBy { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}