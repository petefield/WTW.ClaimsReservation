using System;
using System.Collections.Generic;
using System.IO;
using ClaimsReservation.Models;

namespace ClaimsReservation
{

    public interface IParser
    {
        IEnumerable<InputRow> Read();
    }


}