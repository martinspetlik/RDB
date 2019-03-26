using RDB.Data.DAL;
using RDB.Data.Models;
using RDB.DataCreator.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RDB.DataCreator.Generators
{
    public class BasicGenerator
    {
        #region Fields

        private readonly DefaultContext defaultContext;

        #endregion

        #region Constructors

        public BasicGenerator(DefaultContext defaultContext)
        {
            this.defaultContext = defaultContext;
        }

        #endregion

        #region Public methods

        public void Generate()
        {
            IEnumerable<String> Buses = GenerateBuses();

            defaultContext.SaveChanges();

            IEnumerable<String> Routes = GenerateRoutes();

            defaultContext.SaveChanges();

            IEnumerable<String> Drivers = GenerateDrivers();

            defaultContext.SaveChanges();

            IEnumerable<Drive> Drives = GenerateDrives(Routes, Buses, Drivers);

            defaultContext.SaveChanges();
        }

        #endregion

        #region Private methods

        private IEnumerable<String> GenerateModels()
        {
            List<Model> models = new List<Model>();

            for (Int32 i = 0; i < 100; i++)
            {
                models.Add(new Model
                {
                    Name = Randomize.Text()
                });
            }

            defaultContext.Models.AddRange(models);

            return models.Select(m => m.Name);
        }

        private IEnumerable<String> GenerateBuses()
        {
            IEnumerable<String> models = GenerateModels();

            List<Bus> buses = new List<Bus>();
            for (Int32 i = 0; i < 500; i++)
            {
                buses.Add(new Bus
                {
                    Plate = Randomize.Plate(),
                    ModelName = models.ElementAt(Randomize.Position(models.Count()))
                });
            }

            defaultContext.Buses.AddRange(buses);

            return buses.Select(b => b.Plate).ToList();
        }

        private IEnumerable<String> GenerateLocations()
        {
            List<Location> locations = new List<Location>();

            for (Int32 i = 0; i < 1000; i++)
            {
                locations.Add(new Location
                {
                    Name = Randomize.Text()
                });
            }

            defaultContext.Locations.AddRange(locations);

            return locations.Select(l => l.Name);
        }

        private IEnumerable<String> GenerateRoutes()
        {
            IEnumerable<String> locations = GenerateLocations();

            List<Route> routes = new List<Route>();
            for (Int32 i = 0; i < 100; i++)
            {
                routes.Add(new Route
                {
                    Number = Randomize.String(40).ToUpper(),
                    DepartureName = locations.ElementAt(Randomize.Position(locations.Count())),
                    ArrivalName = locations.ElementAt(Randomize.Position(locations.Count()))
                });
            }

            defaultContext.Routes.AddRange(routes);

            return routes.Select(r => r.Number);
        }

        private IEnumerable<String> GenerateDrivers()
        {
            List<Driver> drivers = new List<Driver>();
            for (Int32 i = 0; i < 100; i++)
            {
                drivers.Add(new Driver
                {
                    LicenseNumber = Randomize.Numeric(25),
                    Firstname = Randomize.Text(),
                    Surname = Randomize.Text()
                });
            }

            defaultContext.Drivers.AddRange(drivers);

            return drivers.Select(d => d.LicenseNumber);
        }

        private IEnumerable<Drive> GenerateDrives(IEnumerable<String> routes, IEnumerable<String> buses, IEnumerable<String> drivers)
        {
            DateTime date = DateTime.Now.Date;

            List<Drive> drives = new List<Drive>();
            for (Int32 i = 0; i < 5000; i++)
            {
                drives.Add(new Drive
                {
                    Time = date.AddHours(i),
                    RouteNumber = routes.ElementAt(Randomize.Position(routes.Count())),
                    BusPlate = buses.ElementAt(Randomize.Position(buses.Count())),
                    DriveLicenseNumber = drivers.ElementAt(Randomize.Position(drivers.Count()))
                });
            }

            defaultContext.Drives.AddRange(drives);

            return drives;
        }

        #endregion
    }
}
