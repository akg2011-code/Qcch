using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using QccHub.DTOS;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Net;
using QccHub.Data.Models;
using System.IO;
using System.Net.Http.Headers;
using System.Linq;
using QccHub.Logic.Enums;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace QccHub.Controllers.Website
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("[controller]/[action]")]
    public class AccountController : BaseController
    {
        public AccountController(IConfiguration iConfig, IHttpClientFactory clientFactory) : base(iConfig, clientFactory)
        {
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            //var token = HttpContext.Session.GetString("AccessToken");
            //var httpClient = HttpClientHelper.GetHttpClient(APIURL, token);

            var httpClient = _clientFactory.CreateClient("API");
            HttpResponseMessage response = await httpClient.PostAsync("Account/Logout", new StringContent(""));
            HttpContext.Session.Clear();
            return RedirectToAction("index","home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM model, string returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            ViewData["ReturnUrl"] = returnUrl;
            var httpClient = _clientFactory.CreateClient("API");
            var jsonContent = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync("Account/Login", jsonContent);

            var result = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                AddModelError(result);
                return View(model);
            }

            var loginResult = JsonConvert.DeserializeObject<LoginResultVM>(result);
            HttpContext.Session.SetString("UserName", loginResult.UserName);
            HttpContext.Session.SetString("AccessToken", loginResult.AccessToken);
            HttpContext.Session.SetString("RoleName", loginResult.RoleName);
            HttpContext.Session.SetString("UserId", loginResult.UserId.ToString());

            var decodedUrl = WebUtility.HtmlDecode(returnUrl);
            if (Url.IsLocalUrl(decodedUrl))
                return Redirect(decodedUrl);
            else
                return RedirectToAction(nameof(Profile), new { id = loginResult.UserId });
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(UserRegisteration model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var httpClient = _clientFactory.CreateClient("API");
            var jsonContent = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync("Account/Register", jsonContent);

            var result = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                AddModelError(result);
                return View(model);
            }

            return LocalRedirect("/Account/Login");
        }

        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordVM model)
        {
            var httpClient = _clientFactory.CreateClient("API");
            var jsonContent = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync("Account/ForgotPassword", jsonContent);

            var result = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                AddModelError(result);
                return View(model);
            }
            return Content("an email sent to you with a link to reset your password");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Profile(int id)
        {
            var httpClient = _clientFactory.CreateClient("API");
            var response = await httpClient.GetAsync($"Account/Get/{id}");
            var result = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                AddModelError(result);
                return RedirectToAction("Index", "Home");
            }
            var user = JsonConvert.DeserializeObject<ApplicationUser>(result);
            if (user.UserRoles.FirstOrDefault().RoleId == (int)RolesEnum.Company)
            {
                return View("CompanyProfile", user);
            }
            else if (user.UserRoles.FirstOrDefault().RoleId == (int)RolesEnum.User)
            {
                return View(user);
            }

            return Content("Error");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> UpdateInfo(int id)
        {
            var httpClient = _clientFactory.CreateClient("API");
            var response = await httpClient.GetAsync($"Account/GetUserUpdateViewModel/{id}");
            var result = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                AddModelError(result);
                return RedirectToAction("Index", "Home");
            }
            var userUpdateVM = JsonConvert.DeserializeObject<UpdateInfoVM>(result);
            var countriesResponse = await httpClient.GetAsync("Country/Get");
            var countriesResult = await countriesResponse.Content.ReadAsStringAsync();
            if (!countriesResponse.IsSuccessStatusCode)
            {
                AddModelError(countriesResult);
                return RedirectToAction("Index", "Home");
            }
            var countryList = JsonConvert.DeserializeObject<List<Country>>(countriesResult);
            var selectedCountry = countryList.FirstOrDefault(c => c.ID == userUpdateVM.NationalityID);
            ViewData["Countries"] = new SelectList(countryList, "ID", "Name",selectedCountry);
            
            if (userUpdateVM.RoleId == (int)RolesEnum.User)
                return View(userUpdateVM);

            else if (userUpdateVM.RoleId == (int)RolesEnum.Company)
                return View("UpdateCompanyInfo", userUpdateVM);
            
            else
                return Content("error");
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> ChangeProfilePicture(int id, IFormFile file)
        {
            var httpClient = _clientFactory.CreateClient("API");
            byte[] PPBytes;
            var multipartContent = new MultipartFormDataContent();

            if (file == null || file.Length == 0)
            {
                return BadRequest("file seems to be empty");
            }

            using (var ms = new MemoryStream())
            {
                await file.CopyToAsync(ms);
                PPBytes = ms.ToArray();
                var byteArrayContent = new ByteArrayContent(PPBytes);
                multipartContent.Add(byteArrayContent, "file", file.FileName);
            }

            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("multipart/form-data"));
            var response = await httpClient.PostAsync($"Account/ChangeProfilePicture/{id}", multipartContent);
            var result = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> DeleteProfilePicture(int id)
        {
            var httpClient = _clientFactory.CreateClient("API");
            var response = await httpClient.DeleteAsync($"Account/DeleteProfilePicture/{id}");
            var result = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateBasicInfo(BasicInfoVM model)
        {
            var httpClient = _clientFactory.CreateClient("API");
            var jsonContent = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            var response = await httpClient.PatchAsync($"Account/UpdateBasicInfo", jsonContent);
            var result = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateBio(UpdateBioVM model)
        {
            var httpClient = _clientFactory.CreateClient("API");
            var jsonContent = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            var response = await httpClient.PatchAsync($"Account/UpdateBio", jsonContent);
            var result = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateBasicCompanyInfo(BasicCompanyInfo model)
        {
            var httpClient = _clientFactory.CreateClient("API");
            var jsonContent = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            var response = await httpClient.PatchAsync($"Account/UpdateBasicCompanyInfo", jsonContent);
            var result = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }


        [HttpPost]
        public async Task<IActionResult> UpdateCompanyOverview(CompanyOverviewInfo model)
        {
            var httpClient = _clientFactory.CreateClient("API");
            var jsonContent = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            var response = await httpClient.PatchAsync($"Account/UpdateCompanyOverview", jsonContent);
            var result = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }


    }
}
