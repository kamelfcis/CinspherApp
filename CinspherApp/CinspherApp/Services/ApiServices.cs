using CinspherApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace CinspherApp.Services
{
    public static class ApiServices
    {
        public static async Task<bool> Login(string email, string password)
        {
            var login = new AccountLogin()
            {
                UserName = email,
                Password = password
            };
            var httpClient = new HttpClient();
            var json = JsonConvert.SerializeObject(login);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            string url = AppSettings.ApiUrl + "api/Auth/CustomerAccountLogin";
            var response = await httpClient.PostAsync(url, content);
            if (!response.IsSuccessStatusCode) return false;
            var jsonResult = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Token>(jsonResult);
            Preferences.Set("accessToken", result.AccessToken);
            Preferences.Set("userId", result.UserId.ToString());
            Preferences.Set("userName", result.UserName);

            Preferences.Set("tokenExpirationTime", result.ExpirationTime);



            return true;

        }
        public static async Task<bool> Register(string Email, string FullName, string Password, string UserName,string Phone)
        {
            await TokenValidator.CheckTokenValidity();
            var userWeb = new AccountRegister()
            {

                Email = Email,
                FullName = FullName,
                Password = Password,
                UserName = UserName,
                Phone=Phone,
                Role="Customer"
            };
            var httpClient = new HttpClient();
            var jsonString = JsonConvert.SerializeObject(userWeb);
            var content = new StringContent(jsonString, Encoding.UTF8, "application/json");




            var response = await httpClient.PostAsync(AppSettings.ApiUrl + "api/Auth/CustomerRegister", content);
            if (response.StatusCode == System.Net.HttpStatusCode.Created)
            {
                return true;
            }
            else
            {
                return false
                               ;
            }

        }
        //public static async Task<bool> AddBusiness(string businessName, int businessTypeId, int yearsInOperation, int userId)
        //{
        //    await TokenValidator.CheckTokenValidity();
        //    var userWeb = new AddBusiness()
        //    {

        //        BusinessName = businessName,
        //        BusinessTypeId = businessTypeId,
        //        YearsInOperation = yearsInOperation,
        //        UserId = userId
        //    };
        //    var httpClient = new HttpClient();
        //    var jsonString = JsonConvert.SerializeObject(userWeb);
        //    var content = new StringContent(jsonString, Encoding.UTF8, "application/json");




        //    var response = await httpClient.PostAsync(AppSettings.ApiUrl + "api/Businesses", content);
        //    if (response.StatusCode == System.Net.HttpStatusCode.Created)
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false
        //                       ;
        //    }

        //}

        public static async Task<bool> CreateBooking(int userId, int showId, List<int> seatIds, string note)
        {
            await TokenValidator.CheckTokenValidity();
            var booking = new CreateBookingDTO
            {
                UserId = userId,
                ShowId = showId,
                SeatIds = seatIds,
                Note = note
            };

            var httpClient = new HttpClient();
            var json = JsonConvert.SerializeObject(booking);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync(AppSettings.ApiUrl + "api/Booking", content);
            return response.StatusCode == System.Net.HttpStatusCode.Created;
        }
        public static async Task<List<Models.Movie>> GetMovies()
        {
            await TokenValidator.CheckTokenValidity();
            var httpClient = new HttpClient();

            var response = await httpClient.GetStringAsync(AppSettings.ApiUrl + "api/Movies");

            return JsonConvert.DeserializeObject<List<Models.Movie>>(response);
        }
      
    }
    public static class TokenValidator
    {
        public static async Task CheckTokenValidity()
        {

            var expirationTime = Preferences.Get("tokenExpirationTime", 0);
            var currentTime = Preferences.Get("currentTime", 0);
            if (expirationTime < currentTime)
            {
                var username = Preferences.Get("userName", string.Empty);
                var password = Preferences.Get("password", string.Empty);
                await ApiServices.Login(username, password);


            }
        }
    }
}
