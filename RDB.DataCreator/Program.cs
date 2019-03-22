using RDB.Data.DAL;
using RDB.DataCreator.Generators;
using System;

namespace RDB.DataCreator
{
    class Program
    {
        #region Init method

        static void Main(string[] args)
        {
            DefaultContext defaultContext = new DefaultContext();

            // Generate test data
            new BasicGenerator(defaultContext).Generate();

            Console.WriteLine(" --- DATA BYLA USPESNE GENEROVANA ---");
            Console.ReadKey();
        }

        #endregion
    }
}
