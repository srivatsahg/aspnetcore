using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DinChiPusMin.Core;
using DinChiPusMin.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Core.Pages.Restaurants
{
    public class EditModel : PageModel
    {
        private readonly IRestaurantData restaurantData;

        private readonly IHtmlHelper htmlHelper;

        public IEnumerable<SelectListItem> Cuisines { get; set; }

        [BindProperty]
        public Restaurant Restaurant { get; set; }

        public EditModel(IRestaurantData restaurantData,
                            IHtmlHelper htmlHelper)
        {
            this.restaurantData = restaurantData;
            this.htmlHelper = htmlHelper;
        }
        public IActionResult OnGet(int? restId)
        {
            Cuisines = htmlHelper.GetEnumSelectList<CuisineType>();

            if (restId.HasValue)
            {
                Restaurant = restaurantData.GetRestaurantById(restId.Value);
            }
            else
            {
                Restaurant = new Restaurant();
            }

            if(Restaurant == null)
            {
                return RedirectToPage("./NotFound");
            }
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                Cuisines = htmlHelper.GetEnumSelectList<CuisineType>(); //cos it is stateless, we need to build it during post as well
                return Page();
            }

            if(Restaurant.Id > 0)
            {
                Restaurant = restaurantData.UpdateRestaurant(Restaurant);
            }
            else
            {
                Restaurant = restaurantData.AddRestaurant(Restaurant);
            }

            TempData["Message"] = "Restaurant information saved successfully !";

            restaurantData.Commit();

            return RedirectToPage("./Detail", new { restId = Restaurant.Id });
        }
    }
}