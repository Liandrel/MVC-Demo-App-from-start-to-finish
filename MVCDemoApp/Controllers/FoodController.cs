using DataLibrary.Data;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace MVCDemoApp.Controllers
{
    public class FoodController : BaseController
    {
        private readonly IFoodData _foodData;

        public FoodController(IFoodData foodData)
        {
            _foodData = foodData;
        }

        public async Task<IActionResult> Index()
        {
            var food = await _foodData.GetFood();

            

            return View(food);
        }
    }
}
