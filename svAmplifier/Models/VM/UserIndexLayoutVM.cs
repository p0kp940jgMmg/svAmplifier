using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace svAmplifier.Models.VM
{
    public class UserIndexLayoutVM
    {

        public PickVM[] Picks { get; set; }
        public MarketItemVM[] LatestMarketItems { get; set; }

        //public UserIndexLayoutVM()
        //{
        //    this.Picks = new PickVM[]
        //    {
        //            new PickVM
        //            {
        //                DatePicked = new DateTime(),
        //                MushroomPicUrl = "https://st2.depositphotos.com/5155283/10941/i/950/depositphotos_109410278-stock-photo-mushrooms-champignon-watercolor-drawing-3.jpg",
        //                Latitude = "5",
        //                Longitude = "3",
        //                MushroomName = "Kantarell"
        //            },
        //            new PickVM
        //            {
        //                DatePicked = new DateTime(),
        //                MushroomPicUrl = "https://st2.depositphotos.com/5155283/10941/i/950/depositphotos_109410278-stock-photo-mushrooms-champignon-watercolor-drawing-3.jpg",
        //                Latitude = "5",
        //                Longitude = "3",
        //                MushroomName = "KarlJohan"
        //            }
        //    }


        //                     //Type = MushroomType.Chanterelle},
        //                 PickLocation = new Location{ Latitude = 3, Longitude = 5},
        //                 UserId = 1,
        //                 Weight = 1000
        //            },

        //            new PickVM
        //            {
        //                PickDate = new DateTime(),
        //                 PickedMushroom = new Mushroom {
        //                     PicUrl = "https://st2.depositphotos.com/5155283/10941/i/950/depositphotos_109410278-stock-photo-mushrooms-champignon-watercolor-drawing-3.jpg",
        //                     Type = MushroomType.Karl_Johan},
        //                 PickLocation = new Location{ Latitude = 4, Longitude = 6},
        //                 UserId = 2,
        //                 Weight = 500
        //            },
        //        };
        //        this.LatestMarketItems = new MarketItemVM[]
        //        {
        //            new MarketItemVM
        //            {
        //                Mushroom = new Mushroom {PicUrl = "http://cliparting.com/wp-content/uploads/2017/01/Dollar-sign-clipart-free-to-use-clip-art-resource.jpeg",
        //                Type=MushroomType.White_Mushroom},
        //                PickDate = new DateTime(),
        //                Price = 2000,
        //                SalesAdress = new Address
        //                {
        //                    City = "Solna",
        //                    Street= "Ankdammsgatan",
        //                    Zipcode= "17143"
        //                },
        //                SalesPersonUsername="Cristian",
        //                Weight=2000
        //            },
        //            new MarketItemVM
        //            {
        //                Mushroom = new Mushroom {PicUrl = "http://cliparting.com/wp-content/uploads/2017/01/Dollar-sign-clipart-free-to-use-clip-art-resource.jpeg",
        //                Type=MushroomType.White_Mushroom},
        //                PickDate = new DateTime(),
        //                Price = 2000,
        //                SalesAdress = new Address
        //                {
        //                    City = "blabla",
        //                    Street= "bla",
        //                    Zipcode= "12345"
        //                },
        //                SalesPersonUsername="Danne",
        //                Weight=2000
        //            }
        //        };
        //    }
    }
    //{

    //    public UserIndexLayoutVM()
    //    {
    //        this.Picks = new PickVM[]
    //        {
    //            new PickVM
    //            {
    //                PickDate = new DateTime(),
    //                 PickedMushroom = new Mushroom {
    //                     PicUrl = "https://st2.depositphotos.com/5155283/10941/i/950/depositphotos_109410278-stock-photo-mushrooms-champignon-watercolor-drawing-3.jpg",
    //                     //Type = MushroomType.Chanterelle},
    //                 PickLocation = new Location{ Latitude = 3, Longitude = 5},
    //                 UserId = 1,
    //                 Weight = 1000
    //            },

    //            new PickVM
    //            {
    //                PickDate = new DateTime(),
    //                 PickedMushroom = new Mushroom {
    //                     PicUrl = "https://st2.depositphotos.com/5155283/10941/i/950/depositphotos_109410278-stock-photo-mushrooms-champignon-watercolor-drawing-3.jpg",
    //                     Type = MushroomType.Karl_Johan},
    //                 PickLocation = new Location{ Latitude = 4, Longitude = 6},
    //                 UserId = 2,
    //                 Weight = 500
    //            },
    //        };
    //        this.LatestMarketItems = new MarketItemVM[]
    //        {
    //            new MarketItemVM
    //            {
    //                Mushroom = new Mushroom {PicUrl = "http://cliparting.com/wp-content/uploads/2017/01/Dollar-sign-clipart-free-to-use-clip-art-resource.jpeg",
    //                Type=MushroomType.White_Mushroom},
    //                PickDate = new DateTime(),
    //                Price = 2000,
    //                SalesAdress = new Address
    //                {
    //                    City = "Solna",
    //                    Street= "Ankdammsgatan",
    //                    Zipcode= "17143"
    //                },
    //                SalesPersonUsername="Cristian",
    //                Weight=2000
    //            },
    //            new MarketItemVM
    //            {
    //                Mushroom = new Mushroom {PicUrl = "http://cliparting.com/wp-content/uploads/2017/01/Dollar-sign-clipart-free-to-use-clip-art-resource.jpeg",
    //                Type=MushroomType.White_Mushroom},
    //                PickDate = new DateTime(),
    //                Price = 2000,
    //                SalesAdress = new Address
    //                {
    //                    City = "blabla",
    //                    Street= "bla",
    //                    Zipcode= "12345"
    //                },
    //                SalesPersonUsername="Danne",
    //                Weight=2000
    //            }
    //        };
    //    }

    //}
}
