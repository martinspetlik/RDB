using System;

namespace RDB.UI.Watermarking.Models
{
    public class ResultModel
    {
        #region Properties

        public Double Percentage { get; set; }

        public Double MinimumPercentage { get; set; }

        public Boolean Result => Percentage > MinimumPercentage;

        public String PercentageResult => (Percentage * 100).ToString("0.00") + "%";

        #endregion

        #region Constructors

        public ResultModel(Double percentage, Double minimumProbability)
        {
            Percentage = percentage;
            MinimumPercentage = minimumProbability;
        }

        #endregion
    }
}
