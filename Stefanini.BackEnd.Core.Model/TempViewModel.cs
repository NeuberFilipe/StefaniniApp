using System.Collections.Generic;

namespace Stefanini.BackEnd.Core.Model
{
   public class TempViewModel
    {
        public class Coord
        {
            public string lon { get; set; }
            public string lat { get; set; }
        }

        public class Weather
        {
            public string id { get; set; }
            public string main { get; set; }
            public string description { get; set; }
            public string icon { get; set; }
        }

        public class Main
        {
            public string temp { get; set; }
            public string pressure { get; set; }
            public string humidity { get; set; }
            public string temp_min { get; set; }
            public string temp_max { get; set; }
        }

        public class Wind
        {
            public string speed { get; set; }
            public string deg { get; set; }
        }

        public class Clouds
        {
            public string all { get; set; }
        }

        public class Sys
        {
            public string type { get; set; }
            public string id { get; set; }
            public string message { get; set; }
            public string country { get; set; }
            public string sunrise { get; set; }
            public string sunset { get; set; }
        }

        public class RootObject
        {
            public Coord coord { get; set; }
            public List<Weather> weather { get; set; }
            public string @base { get; set; }
            public Main main { get; set; }
            public string visibility { get; set; }
            public Wind wind { get; set; }
            public Clouds clouds { get; set; }
            public int dt { get; set; }
            public Sys sys { get; set; }
            public string id { get; set; }
            public string name { get; set; }
            public string cod { get; set; }
        }
    }
}
