using DinChiPusMin.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace DinChiPusMin.Data
{
    public interface IRestaurantData
    {
        IEnumerable<Restaurant> GetRestaurantsByName(string names);
        Restaurant GetRestaurantById(int restId);
        Restaurant UpdateRestaurant(Restaurant updatedRestaurant);
        Restaurant AddRestaurant(Restaurant addRestaurant);
        int Commit();
    }

    public class InMemoryRestaurantData : IRestaurantData
    {
        List<Restaurant> restaurants;
        public InMemoryRestaurantData()
        {
            restaurants = new List<Restaurant>()
            {
                new Restaurant{Id=1,Name="Ambrosia",Cuisine=CuisineType.Italian,Location="RRNagar" },
                new Restaurant{Id=2,Name="Fennys",Cuisine=CuisineType.Mexican,Location="Koramangala" },
                new Restaurant{Id=3,Name="Hoot",Cuisine=CuisineType.Indian,Location="Sarjapur" }
            };
        }

        public Restaurant GetRestaurantById(int restId)
        {
            return restaurants.SingleOrDefault(r => r.Id == restId);
        }

        public IEnumerable<Restaurant> GetRestaurantsByName(string name = null)
        {
            //return from r in restaurants
            //       orderby r.Name,
            //       select r;

            return restaurants.Where(r => (string.IsNullOrEmpty(name) || r.Name.StartsWith(name)))
                                    .OrderBy(r => r.Name);
        }

        public Restaurant UpdateRestaurant(Restaurant updatedRestaurant)
        {
            var restaurant = restaurants.SingleOrDefault(r => r.Id == updatedRestaurant.Id);
            if(restaurant != null)
            {
                restaurant.Name = updatedRestaurant.Name;
                restaurant.Location = updatedRestaurant.Location;
                restaurant.Cuisine = updatedRestaurant.Cuisine;
            }
            return restaurant;
        }

        public int Commit()
        {
            return 0;
        }

        public Restaurant AddRestaurant(Restaurant newRestaurant)
        {
            restaurants.Add(newRestaurant);
            newRestaurant.Id = restaurants.Max(r => r.Id) + 1;
            return newRestaurant;
        }
    }

}
