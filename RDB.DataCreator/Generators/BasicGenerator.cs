using RDB.Data.DAL;
using RDB.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            List<String> BusPlates = GenerateBuses();

            //List<String> Places = 

            defaultContext.SaveChanges();
        }

        #endregion

        #region Private methods

        private void GenerateModels()
        {
            defaultContext.Models.AddRange(new Model[]
            {
                new Model
                {
                    Name= "Karosa"
                },
                new Model
                {
                    Name = "Iveco"
                },
                new Model
                {
                    Name= "Volvo"
                }
            });
        }

        private List<String> GenerateBuses()
        {
            GenerateModels();

            List<Bus> buses = new List<Bus>
            {
                new Bus
                {
                    Plate = "123 4567",
                    ModelName ="Karosa"
                },
                new Bus
                {
                    Plate = "4LC 4732",
                    ModelName = "Iveco"
                },
                new Bus
                {
                    Plate = "1AC 7D4C1",
                    ModelName = "Volvo"
                }
            };

            defaultContext.Buses.AddRange(buses);

            return buses.Select(b => b.Plate).ToList();
        }

        #endregion
    }
}
