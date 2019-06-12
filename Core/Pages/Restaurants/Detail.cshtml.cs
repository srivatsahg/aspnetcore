using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DinChiPusMin.Core;
using DinChiPusMin.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Core.Pages.Restaurants
{
    public class DetailModel : PageModel
    {
        private readonly IRestaurantData restaurantData;

        public Restaurant Restaurant { get; set; }

        [TempData]
        public string Message { get; set; }

        public DetailModel(IRestaurantData restaurantData) 
        {
            this.restaurantData = restaurantData;
        }

        public IActionResult OnGet(int restId)
        {
            Restaurant = restaurantData.GetRestaurantById(restId);
            if(Restaurant == null)
            {
                return RedirectToPage("./NotFound");
            }
            return Page();
        }
    }
}