using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using QccHub.Data;
using QccHub.Data.Models;
using QccHub.Logic.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace QccHub.Data
{
    public class CountryDTO
    {
        public string Name { get; set; }
    }
    public class SeedingData
    {
        public ApplicationDbContext Context;
        public SeedingData(ApplicationDbContext _context)
        {
            Context = _context;
        }

        public void SeedAllCountry()
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://restcountries.eu/rest/v2/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var result = Task.Run(() => client.GetAsync("all?fields=name")).GetAwaiter().GetResult();

                if (result.IsSuccessStatusCode)
                {
                    var readTaskResult = Task.Run(() => result.Content.ReadAsStringAsync()).GetAwaiter().GetResult();

                    var JsonReuslt = JsonConvert.DeserializeObject<List<CountryDTO>>(readTaskResult);

                    foreach (var country in JsonReuslt)
                    {
                        if (!Context.Country.Any(x => x.Name == country.Name))
                             Context.Country.Add(new Country { IsDeleted = false, CreatedDate = DateTime.Now, Name = country.Name });
                    }
                     Context.SaveChanges();
                }
            }
        }


        public void SeedGender()
        {
            List<Gender> genders = new List<Gender>
            {
                new Gender{Name = "Male",IsDeleted =false , CreatedDate = DateTime.Now},
                new Gender{Name="Female",IsDeleted =false , CreatedDate = DateTime.Now},
                new Gender{Name="Other",IsDeleted =false , CreatedDate = DateTime.Now}
            };

            foreach (var record in genders)
            {
                if (!Context.Gender.Any(g => g.Name == record.Name))
                {
                    Context.Gender.Add(record);
                }
            }
            Context.SaveChanges();
        }

        public void SeedPaymentStatus()
        {
            List<PaymentStatus> status = new List<PaymentStatus>
            {
                new PaymentStatus{StatusName = "Done",IsDeleted =false , CreatedDate = DateTime.Now},
                new PaymentStatus{StatusName = "Pending",IsDeleted =false , CreatedDate = DateTime.Now},
                new PaymentStatus{StatusName="Canceled",IsDeleted =false , CreatedDate = DateTime.Now}
            };

            foreach (var record in status)
            {
                if (!Context.PaymentStatus.Where(s => s.StatusName == record.StatusName).Any())
                {
                Context.PaymentStatus.Add(record);
                }
            }
            Context.SaveChanges();
        }
    }

    public class ApplicationDbInitializer
    {
        public static void SeedingData(UserManager<ApplicationUser> userManager , RoleManager<ApplicationRole> roleManager , ApplicationDbContext context)
        {
            SeedRoles(roleManager);
            SeedUsers(userManager);

            SeedingData data = new SeedingData(context);
            data.SeedGender();
            data.SeedPaymentStatus();
            if (!context.Country.Any())
            {
                data.SeedAllCountry();
            }
        }

        public static void SeedRoles(RoleManager<ApplicationRole> roleManager)
        {
            

            // create admin role :
            if (!Task.Run(() => roleManager.RoleExistsAsync(RolesEnum.Admin.ToString())).GetAwaiter().GetResult())
            {
                ApplicationRole role = new ApplicationRole { Name = RolesEnum.Admin.ToString() };
                Task.Run(() => roleManager.CreateAsync(role)).GetAwaiter().GetResult();
            }

            // create Vendor role :
            if (!Task.Run(() => roleManager.RoleExistsAsync(RolesEnum.Company.ToString())).GetAwaiter().GetResult())
            {
                ApplicationRole role = new ApplicationRole { Name = RolesEnum.Company.ToString() };
                Task.Run(() => roleManager.CreateAsync(role)).GetAwaiter().GetResult();
            }

            // create User role :
            if (!Task.Run(() => roleManager.RoleExistsAsync(RolesEnum.User.ToString())).GetAwaiter().GetResult())
            {
                ApplicationRole role = new ApplicationRole { Name = RolesEnum.User.ToString() };
                Task.Run(() => roleManager.CreateAsync(role)).GetAwaiter().GetResult();
            }
        }

        public static void SeedUsers(UserManager<ApplicationUser> userManager)
        {
            if (Task.Run(() => userManager.FindByEmailAsync("qcchub@admin.com")).GetAwaiter().GetResult() == null)
            {
                ApplicationUser admin = new ApplicationUser
                {
                    UserName = "admin",
                    Email = "qcchub@admin.com",
                    NormalizedUserName = "ADMIN",
                    NormalizedEmail = "COOKIESADMIN.ADMIN.COM",
                    EmailConfirmed = true,
                    LockoutEnabled = false,
                    PhoneNumber = "01032873503",
                    PhoneNumberConfirmed = true,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    IsTrusted = true
                };

                IdentityResult result = userManager.CreateAsync(admin, "Admin@2020").Result;
                if (result.Succeeded)
                {
                    Task.Run(() => userManager.AddToRoleAsync(admin, RolesEnum.Admin.ToString())).GetAwaiter().GetResult();
                }

            }
        }

        
    }
}
