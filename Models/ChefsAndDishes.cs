using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChefsNDishes.Models {

    public class ChefsAndDishes {

        public Chef NewChef { get; set; }
        public Dish NewDish { get; set; }
    }
}