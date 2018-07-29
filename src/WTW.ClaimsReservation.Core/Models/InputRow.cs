using System;
using System.Collections.Generic;
using System.Text;

namespace ClaimsReservation.Models
{
    /// <summary>
    /// A DTO representing data returned by an IDataSource.
    /// </summary>
    public class DataRow
    {

        private readonly string _product;

        private readonly int _originYear;

        private readonly int _developmentYear;

        private readonly decimal _incrementalValue;

        public DataRow(string product, int originYear, int devYear, decimal incrementalValue)
        {
            _product = product;
            _originYear = originYear;
            _incrementalValue = incrementalValue;
            _developmentYear = devYear;
        }
  
        public string Product { get => _product; }
        public int OriginYear { get => _originYear; }
        public int DevelopmentYear { get => _developmentYear; }
        public decimal IncrementalValue { get => _incrementalValue; }
    }

}
