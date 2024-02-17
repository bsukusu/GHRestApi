using GoRestApi.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NuGet.Protocol.Plugins;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using GoRestApi.Entities;
namespace GoRestAPIDemo.Controllers
{
    public class UserController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public UserController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("https://gorest.co.in/public/v2/users");

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var users = JsonConvert.DeserializeObject<List<User>>(jsonString);
                return View(users);
            }
            else
            {
                return View(new List<User>());
            }
        }


        public IActionResult AddNewUserPartial()
        {
            return PartialView("_AddNewUserPartial", new CreateUserModel());
        }

        [HttpPost]
        public async Task<IActionResult> AddNewUser(CreateUserModel model)
        {
            if (ModelState.IsValid)
            {
                var client = _httpClientFactory.CreateClient();
                var jsonUser = JsonConvert.SerializeObject(model);
                var content = new StringContent(jsonUser, Encoding.UTF8, "application/json");

                var response = await client.PostAsync("https://gorest.co.in/public/v2/users", content);

                if (response.IsSuccessStatusCode)
                {
                    var responseData = await response.Content.ReadAsStringAsync();
                    return PartialView("_AddNewUserPartial", new CreateUserModel());
                }
                else

                    return PartialView("_AddNewUserPartial", new CreateUserModel());
            }
            return PartialView("_AddNewUserPartial", model);
        }


    }

    public async Task<IActionResult> DeleteUser(Guid id)
    {
        HttpResponseMessage response = await _httpClient.DeleteAsync($"users/{id}");

        if (response.IsSuccessStatusCode)
        {
            return Ok(); // Başarılı ise 200 OK dön
        }
        else
        {
            // Başarısız ise hata durumunu dön
            return StatusCode((int)response.StatusCode, response.ReasonPhrase);
        }

    }
}
