using DataLibrary.Data;
using DataLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVCDemoApp.Models;

namespace MVCDemoApp.Controllers
{
    public class OrdersController : BaseController
    {
        private readonly IOrderData _orderData;
        private readonly IFoodData _foodData;

        public OrdersController(IOrderData orderData, IFoodData foodData)
        {
            _orderData = orderData;
            _foodData = foodData;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Create()
        {
            var food = await _foodData.GetFood();

            OrderCreateModel model = new();

            food.ForEach(x =>
            {
                model.FoodItems.Add(new SelectListItem { Value = x.Id.ToString(), Text = x.Title });
            });

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(OrderModel order)
        {
            if(ModelState.IsValid == false)
            {
                return View();
            }

            var food = await  _foodData.GetFood();

            order.Total = order.Quantity * food.Where(x => x.Id == order.FoodId).First().Price;

            int id = await _orderData.CreateOrder(order);

            return RedirectToAction("Display", new { Id = id});
        }

        public async Task<IActionResult> Display(int id)
        {
            OrderDisplayModel displayModel = new();

            displayModel.Order = await _orderData.GetOrderById(id);

            if(displayModel.Order != null)
            {
                var food = await _foodData.GetFood();

                displayModel.ItemPurchased = food.Where(x => x.Id == displayModel.Order.FoodId).FirstOrDefault()?.Title;
            }
           
            return View(displayModel);
        }
    }
}
