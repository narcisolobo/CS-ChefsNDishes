using System;
using System.Collections.Generic;
using System.Linq;
using ChefsNDishes.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ChefsNDishes.Controllers {
    public class HomeController : Controller {

        private ChefsNDishesContext DbContext;

        public HomeController (ChefsNDishesContext context) {
            DbContext = context;
        }

        [HttpGet]
        [Route ("")]
        public IActionResult Index () {
            List<Chef> chefsWithDishes = DbContext.Chefs
            .Include(chef => chef.SubmittedDishes)
            .ToList();
            return View (chefsWithDishes);
        }

        [HttpGet]
        [Route ("dishes")]
        public IActionResult Dishes () {
            List<Dish> dishesWithChefs = DbContext.Dishes
            .Include(dish => dish.SubmittedBy)
            .ToList();
            return View ("Dishes", dishesWithChefs);
        }

        [HttpGet]
        [Route ("viewdish/{Id}")]
        public IActionResult ViewDish (int Id) {
            Dish retrievedDish = DbContext.Dishes.FirstOrDefault (dish => dish.Id == Id);
            Chef retrievedChef = DbContext.Chefs.FirstOrDefault (chef => chef.Id == retrievedDish.ChefId);
            ViewBag.Chef = retrievedChef;
            return View ("ViewDish", retrievedDish);
        }

        [HttpGet]
        [Route ("deletedish/{Id}")]
        public IActionResult DeleteDish (int Id) {
            Dish retrievedDish = DbContext.Dishes.FirstOrDefault (dish => dish.Id == Id);
            DbContext.Dishes.Remove(retrievedDish);
            DbContext.SaveChanges();
            List<Dish> dishesWithChefs = DbContext.Dishes
                .Include(dish => dish.SubmittedBy)
                .ToList();
            return View ("Dishes", dishesWithChefs);
        }

        [HttpGet]
        [Route ("new")]
        public IActionResult New () {
            return View ("New");
        }

        [HttpGet]
        [Route ("dishes/new")]
        public IActionResult NewDish () {
            ChefsToBag();
            return View ("NewDish");
        }

        [HttpPost]
        [Route ("processchef")]
        public IActionResult ProcessChef (Chef submittedChef) {
            if (ModelState.IsValid) {
                DbContext.Chefs.Add (submittedChef);
                DateTime today = DateTime.Today;
                int age = today.Year - submittedChef.DateOfBirth.Year;
                submittedChef.Age = age;
                DbContext.SaveChanges ();
                return RedirectToAction ("Index");
            } else {
                return View ("New");
            }
        }

        [HttpPost]
        [Route ("processdish")]
        public IActionResult ProcessDish (Dish submittedDish) {
            submittedDish.SubmittedBy = DbContext.Chefs.FirstOrDefault(p => p.Id == submittedDish.ChefId);
            if (ModelState.IsValid) {
                DbContext.Dishes.Add (submittedDish);
                DbContext.SaveChanges ();
                return RedirectToAction ("Dishes");
            } else {
                ChefsToBag();
                return View ("NewDish");
            }
        }
        public void ChefsToBag(){
            ViewBag.Chefs = DbContext.Chefs.ToList();
        }
    }
}