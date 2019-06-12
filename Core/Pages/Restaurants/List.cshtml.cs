using DinChiPusMin.Core;
using DinChiPusMin.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace Core.Pages.Restaurants
{
    public class ListModel : PageModel
    {
        private readonly IConfiguration config;

        private readonly IRestaurantData restaurantData;

        [BindProperty(SupportsGet =true)]
        public string SearchTerm { get; set; }

        public string ConnectionString { get; set; }
        public IEnumerable<Restaurant> Restaurants  { get; set; }

        public ListModel(IConfiguration configuration, IRestaurantData restaurantData)
        {
            config = configuration;
            this.restaurantData = restaurantData;
        }

        public void OnGet()
        {
            ConnectionString = config["ConnectionString"];
            Restaurants = restaurantData.GetRestaurantsByName(SearchTerm);
        }
    }
}