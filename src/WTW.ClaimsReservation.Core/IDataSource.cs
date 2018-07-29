using System.Collections.Generic;
using ClaimsReservation.Models;

namespace ClaimsReservation
{

    public interface IDataSource
    {
        IEnumerable<DataRow> ParsedData { get; }
    }


}