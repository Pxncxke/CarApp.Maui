using CarApp.Maui.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace CarApp.Maui.ViewModel
{
    [QueryProperty(nameof(Id), nameof(Id))]
    public partial class CarDetailsViewModel : BaseViewModel, IQueryAttributable
    {
        [ObservableProperty]
        Car car;

        [ObservableProperty]
        Guid id;

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            _ = Guid.TryParse(HttpUtility.UrlDecode(query["Id"].ToString()), out Guid guid);

            Id =   guid;
            Car = App.CarService.GetCar(Id);
        }
        //public CarDetailsViewModel()
        //{
        //    Title = $"Car Details - {car.Make} {car.Model}";
        //}
    }
}
