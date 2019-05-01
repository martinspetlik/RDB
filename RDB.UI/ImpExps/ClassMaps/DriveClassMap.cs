﻿using CsvHelper.Configuration;
using RDB.Data.Models;

namespace RDB.UI.ImpExps.ClassMaps
{
    public sealed class DriveClassMap : ClassMap<Drive>
    {
        #region Constructors

        public DriveClassMap()
        {
            Map(m => m.RouteNumber).Index(0);
            Map(m => m.BusPlate).Index(1);
            Map(m => m.DriveLicenseNumber).Index(2);
            Map(m => m.Time).Index(3);
        }

        #endregion
    }
}
