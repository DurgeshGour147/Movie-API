using System;
using System.Collections.Generic;
using System.Text;

namespace Movie_store.Common.DBConnection
{
    public class MovieStoreConnection : LinqToDB.Data.DataConnection
    {
        public MovieStoreConnection() : base("MovieStore") { }
    }
}
