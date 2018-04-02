using MvvmCross.Core.ViewModels;

namespace Stefanini.Xamarin.Core.ViewModels
{
   public class CitiesViewModel :  
       MvxViewModel
    {
        public string Weather { get; set; }

        public string Temp { get; set; }

        public string TempMin { get; set; }

        public string TempMax { get; set; }

        public string City { get; set; }

        public string Url { get; set; }

        public bool Favorite { get; set; }
    }
}
