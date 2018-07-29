using System;
using System.Collections.Generic;
using System.Text;

namespace ClaimsReservation.Models
{
    public class DataRow
    {

        public DataRow(string product, int originYear, int devYear, decimal value)
        {
            IncrementalValue = value;
            DevelopmentYear = devYear;
            OriginYear = originYear;
            Product = product;
        }

        public string Product;
        public int OriginYear;
        public int DevelopmentYear;
        public decimal IncrementalValue;
    }

}
