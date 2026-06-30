using DAL;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;

namespace Host
{
    public static class SeedData
    {
        private const string PicturesPath = @"C:\Users\1\Downloads\פרוייקט\Pictures\";

        public static void SeedIfEmpty()
        {
            using (RacheliEntities db = new RacheliEntities())
            {
                if (db.Areas.Any())
                    return;

                Area north = new Area { NameArea = "צפון" };
                Area center = new Area { NameArea = "מרכז" };
                Area south = new Area { NameArea = "דרום" };
                db.Areas.Add(north);
                db.Areas.Add(center);
                db.Areas.Add(south);
                db.SaveChanges();

                City haifa = new City { NameCity = "חיפה" };
                City telAviv = new City { NameCity = "תל אביב" };
                City jerusalem = new City { NameCity = "ירושלים" };
                City beerSheva = new City { NameCity = "באר שבע" };
                City eilat = new City { NameCity = "אילת" };
                db.Cities.Add(haifa);
                db.Cities.Add(telAviv);
                db.Cities.Add(jerusalem);
                db.Cities.Add(beerSheva);
                db.Cities.Add(eilat);
                db.SaveChanges();

                string[] streetNames =
                {
                    "רחוב 1", "רחוב 2", "רחוב 3", "רחוב 4", "רחוב 5",
                    "רחוב 6", "רחוב 7", "רחוב 8", "רחוב 9", "רחוב 10"
                };

                foreach (string name in streetNames)
                    db.StreetsNames.Add(new StreetsName { StreetName = name });
                db.SaveChanges();

                var streets = db.StreetsNames.OrderBy(s => s.IdStreet).ToList();

                db.AraesCitiesStreets.Add(new AraesCitiesStreet { IdArea = north.IdArea, IdCities = haifa.IdCity, IdStreet = streets[0].IdStreet });
                db.AraesCitiesStreets.Add(new AraesCitiesStreet { IdArea = north.IdArea, IdCities = haifa.IdCity, IdStreet = streets[1].IdStreet });
                db.AraesCitiesStreets.Add(new AraesCitiesStreet { IdArea = center.IdArea, IdCities = telAviv.IdCity, IdStreet = streets[2].IdStreet });
                db.AraesCitiesStreets.Add(new AraesCitiesStreet { IdArea = center.IdArea, IdCities = telAviv.IdCity, IdStreet = streets[3].IdStreet });
                db.AraesCitiesStreets.Add(new AraesCitiesStreet { IdArea = center.IdArea, IdCities = jerusalem.IdCity, IdStreet = streets[4].IdStreet });
                db.AraesCitiesStreets.Add(new AraesCitiesStreet { IdArea = south.IdArea, IdCities = beerSheva.IdCity, IdStreet = streets[5].IdStreet });
                db.AraesCitiesStreets.Add(new AraesCitiesStreet { IdArea = south.IdArea, IdCities = eilat.IdCity, IdStreet = streets[6].IdStreet });
                db.SaveChanges();

                Extra ac = new Extra { NameExtra = "מיזוג" };
                Extra parking = new Extra { NameExtra = "חניה" };
                Extra wifi = new Extra { NameExtra = "WiFi" };
                Extra balcony = new Extra { NameExtra = "מרפסת" };
                Extra elevator = new Extra { NameExtra = "מעלית" };
                db.Extras.Add(ac);
                db.Extras.Add(parking);
                db.Extras.Add(wifi);
                db.Extras.Add(balcony);
                db.Extras.Add(elevator);
                db.SaveChanges();

                Apartment apt1 = new Apartment
                {
                    NameOwner = "דוד כהן",
                    PhoneOwner = "0501234567",
                    IdStreet = streets[0].IdStreet,
                    IdCities = haifa.IdCity,
                    NumberHouse = "12",
                    Floor = 2,
                    NumberRooms = 3,
                    NumberBeds = 4,
                    MinimumPrice = 350,
                    ExtraForBed = 50,
                    Status = true,
                    note = "דירה מרווחת עם נוף"
                };
                Apartment apt2 = new Apartment
                {
                    NameOwner = "דוד כהן",
                    PhoneOwner = "0501234567",
                    IdStreet = streets[2].IdStreet,
                    IdCities = telAviv.IdCity,
                    NumberHouse = "45",
                    Floor = 5,
                    NumberRooms = 2,
                    NumberBeds = 2,
                    MinimumPrice = 500,
                    ExtraForBed = 80,
                    Status = true,
                    note = "במרכז העיר"
                };
                Apartment apt3 = new Apartment
                {
                    NameOwner = "שרה לוי",
                    PhoneOwner = "0529876543",
                    IdStreet = streets[4].IdStreet,
                    IdCities = jerusalem.IdCity,
                    NumberHouse = "8",
                    Floor = 1,
                    NumberRooms = 4,
                    NumberBeds = 6,
                    MinimumPrice = 400,
                    ExtraForBed = 60,
                    Status = true,
                    note = "קרוב לעיר העתיקה"
                };
                Apartment apt4 = new Apartment
                {
                    NameOwner = "שרה לוי",
                    PhoneOwner = "0529876543",
                    IdStreet = streets[5].IdStreet,
                    IdCities = beerSheva.IdCity,
                    NumberHouse = "3",
                    Floor = 0,
                    NumberRooms = 3,
                    NumberBeds = 3,
                    MinimumPrice = 250,
                    ExtraForBed = 40,
                    Status = true,
                    note = "דירת גן"
                };
                Apartment apt5 = new Apartment
                {
                    NameOwner = "דוד כהן",
                    PhoneOwner = "0501234567",
                    IdStreet = streets[6].IdStreet,
                    IdCities = eilat.IdCity,
                    NumberHouse = "100",
                    Floor = 8,
                    NumberRooms = 2,
                    NumberBeds = 2,
                    MinimumPrice = 600,
                    ExtraForBed = 100,
                    Status = true,
                    note = "נוף לים"
                };
                db.Apartments.Add(apt1);
                db.Apartments.Add(apt2);
                db.Apartments.Add(apt3);
                db.Apartments.Add(apt4);
                db.Apartments.Add(apt5);
                db.SaveChanges();

                db.Hirers.Add(new Hirer { C_IDHirer = "123456782", NameHirer = "יוסי אברהם", FhoneNumberHirer = "0501111111" });
                db.Hirers.Add(new Hirer { C_IDHirer = "234567891", NameHirer = "מיכל דוד", FhoneNumberHirer = "0502222222" });
                db.Hirers.Add(new Hirer { C_IDHirer = "345678902", NameHirer = "אבי מזרחי", FhoneNumberHirer = "0503333333" });
                db.SaveChanges();

                db.Rentings.Add(new Renting
                {
                    IdHirer = "123456782",
                    KodHapartment = apt1.IdApartment,
                    Date = DateTime.Today.AddDays(7),
                    SumPayment = 450,
                    SumBeds = 2
                });
                db.Rentings.Add(new Renting
                {
                    IdHirer = "234567891",
                    KodHapartment = apt3.IdApartment,
                    Date = DateTime.Today.AddDays(14),
                    SumPayment = 520,
                    SumBeds = 2
                });
                db.SaveChanges();

                EnsurePictureFile("image1.jpg");
                EnsurePictureFile("image2.jpg");
                EnsurePictureFile("image3.jpg");

                db.Images.Add(new DAL.Image { IdApartement = apt1.IdApartment, NumImage = 1, Image1 = "image1.jpg", Stataus = true });
                db.Images.Add(new DAL.Image { IdApartement = apt2.IdApartment, NumImage = 1, Image1 = "image2.jpg", Stataus = true });
                db.Images.Add(new DAL.Image { IdApartement = apt3.IdApartment, NumImage = 1, Image1 = "image3.jpg", Stataus = true });
                db.SaveChanges();

                db.ExtrasApartements.Add(new ExtrasApartement { IdExtra = wifi.IdExtra, IdAapartment = apt1.IdApartment, Status = true });
                db.ExtrasApartements.Add(new ExtrasApartement { IdExtra = parking.IdExtra, IdAapartment = apt2.IdApartment, Status = true });
                db.ExtrasApartements.Add(new ExtrasApartement { IdExtra = ac.IdExtra, IdAapartment = apt5.IdApartment, Status = true });
                db.SaveChanges();
            }
        }

        private static void EnsurePictureFile(string fileName)
        {
            if (!Directory.Exists(PicturesPath))
                Directory.CreateDirectory(PicturesPath);

            string fullPath = PicturesPath + fileName;
            if (File.Exists(fullPath))
                return;

            using (Bitmap bmp = new Bitmap(200, 150))
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.LightGray);
                g.DrawString("Demo", new Font("Arial", 16), Brushes.Black, 60, 60);
                bmp.Save(fullPath, ImageFormat.Jpeg);
            }
        }
    }
}
