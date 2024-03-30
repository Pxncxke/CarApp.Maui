using CarApp.Maui.Models;
using CarApp.Maui.Services;
using CarApp.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarApp.Maui.ViewModel
{
    public partial class CarListViewModel : BaseViewModel
    {
        const string editButtonText = "Update Car";
        const string createButtonText = "Add Car";
        public ObservableCollection<Car> Cars { get; private set; } = new();

        public CarListViewModel()
        {
            Title = "Car List";
            AddEditButtonText = createButtonText;
            GetCarList().Wait();
        }

        [ObservableProperty]
        bool isRefreshing;
        [ObservableProperty]
        string make;
        [ObservableProperty]
        string model;
        [ObservableProperty]
        string vin;
        [ObservableProperty]
        string addEditButtonText;
        [ObservableProperty]
        Guid carId;

        [RelayCommand]
        public async Task GetCarList()
        {
            if(IsLoading)
            {
                return;
            }

            try
            {
                IsLoading = true;
                if(Cars.Any())
                {
                    Cars.Clear();
                }

                //Cars = new ObservableCollection<Car>(_carService.GetCars());
                var cars = App.CarService.GetCars();
                foreach (var car in cars)
                {
                    Cars.Add(car);
                }

               
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                await Shell.Current.DisplayAlert("Error", "Unable to retrieve car list", "Ok");
            }
            finally
            {
                IsLoading = false;
                IsRefreshing = false;
            }

           
        }

        [RelayCommand]
        public async Task GetCarDetails(Car car)
        {
            if(car == null)
            {
                return;
            }

             //await Shell.Current.GoToAsync($"carDetails?id={car.Id}");
             await Shell.Current.GoToAsync(nameof(CarDetailsPage), true, new Dictionary<string, object> { { nameof(Car), car } });
        }

        [RelayCommand]
        async Task GetCarDetailsById(Guid id)
        {
            //if (id == 0) return;

            await Shell.Current.GoToAsync($"{nameof(CarDetailsPage)}?Id={id}", true);
        }

        [RelayCommand]
        async Task SaveCar()
        {
            if (string.IsNullOrEmpty(Make) || string.IsNullOrEmpty(Model) || string.IsNullOrEmpty(Vin))
            {
                await Shell.Current.DisplayAlert("Invalid Data", "Please insert valid data", "Ok");
                return;
            }

            var car = new Car
            {
                Make = Make,
                Model = Model,
                Vin = Vin
            };

            if (CarId != Guid.Empty)
            {
                car.Id = CarId;
                App.CarService.UpdateCar(car);
                await Shell.Current.DisplayAlert("Info", App.CarService.StatusMessage, "Ok");
            }
            else
            {
                App.CarService.AddCar(car);
                await Shell.Current.DisplayAlert("Info", App.CarService.StatusMessage, "Ok");
            }

            await GetCarList();
            await ClearForm();
        }

        [RelayCommand]
        async Task DeleteCar(Guid id)
        {
            if (id == Guid.Empty)
            {
                await Shell.Current.DisplayAlert("Invalid Record", "Please try again", "Ok");
                return;
            }
            var result = App.CarService.DeleteCar(id);
            if (result == 0) await Shell.Current.DisplayAlert("Failed", "Please insert valid data", "Ok");
            else
            {
                await Shell.Current.DisplayAlert("Deletion Successful", "Record Removed Successfully", "Ok");
                await GetCarList();
            }
        }

        [RelayCommand]
        async Task UpdateCar(Guid id)
        {
            AddEditButtonText = editButtonText;
            return;
        }

        [RelayCommand]
        async Task SetEditMode(Guid id)
        {
            AddEditButtonText = editButtonText;
            CarId = id;
            var car = App.CarService.GetCar(id);
            Make = car.Make;
            Model = car.Model;
            Vin = car.Vin;
        }

        [RelayCommand]
        async Task ClearForm()
        {
            AddEditButtonText = createButtonText;
            CarId = Guid.Empty;
            Make = string.Empty;
            Model = string.Empty;
            Vin = string.Empty;
        }
    }
}
