using System.Collections.Generic;

namespace Stefanini.BackEnd.Core.Model
{
    public class ListClimateViewModel 
    {
        public class Coord
        {
            public double lon { get; set; }
            public double lat { get; set; }
        }

        public class Datum
        {
            public int id { get; set; }
            public Coord coord { get; set; }
            public string country { get; set; }
            public string name { get; set; }
            public int zoom { get; set; }
        }

        public class RootObject
        {
            public List<Datum> data { get; set; }
        }
    }
}
