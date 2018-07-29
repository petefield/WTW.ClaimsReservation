using System.Collections.Generic;
using ClaimsReservation.Models;

namespace ClaimsReservation.DataSources
{
    /// <summary>
    /// Interface to allow creation of alternative datasources
    /// </summary>
    public interface IDataSource
    {
        /// <summary>
        /// A list of DataRow items, that represent the data parsed by the DataSource
        /// </summary>
        IEnumerable<DataRow> ParsedData { get; }
    }


}