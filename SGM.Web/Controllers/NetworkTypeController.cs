using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SGM.Domain.Entities.Insurance;
using SGM.Web.Models;
using System.Net.Http;

namespace SGM.Web.Controllers
{
    public class NetworkTypeController : Controller
    {
        // GET: NetworkTypeController
        public async Task<IActionResult> Index()
        {

            GetAllNetworkTypeResponse getAllNetworkTypeResponse = null;

            try
            {

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:5277/api/");

                    var response = await client.GetAsync("NetworkType/GetNetworkTypes");

                    if (response.IsSuccessStatusCode)
                    {
                        var responseString = await response.Content.ReadAsStringAsync();
                        getAllNetworkTypeResponse = System.Text.Json.JsonSerializer.Deserialize<GetAllNetworkTypeResponse>(responseString);
                    }
                    else
                    {
                        getAllNetworkTypeResponse = new GetAllNetworkTypeResponse
                        {
                            isSuccess = false,
                            message = "Error retrieving network types."
                        };
                    }

                }
            }
            catch (Exception ex)
            {
                getAllNetworkTypeResponse = new GetAllNetworkTypeResponse
                {
                    isSuccess = false,
                    message = $"Error retrieving network types {ex.Message}."
                };

            }

            return View(getAllNetworkTypeResponse.data);
        }

        // GET: NetworkTypeController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            GetNetworkTypeResponse getNetworkTypeResponse = null;
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:5277/api/");

                    var response = await client.GetAsync($"NetworkType/GetNetworkTypeById?id={id}");

                    if (response.IsSuccessStatusCode)
                    {
                        var responseString = await response.Content.ReadAsStringAsync();
                        getNetworkTypeResponse = System.Text.Json.JsonSerializer.Deserialize<GetNetworkTypeResponse>(responseString);
                    }
                    else
                    {
                        getNetworkTypeResponse = new GetNetworkTypeResponse
                        {
                            isSuccess = false,
                            message = "Error retrieving network types."
                        };
                    }

                }
            }
            catch (Exception ex)
            {

                getNetworkTypeResponse = new GetNetworkTypeResponse
                {
                    isSuccess = false,
                    message = $"Error retrieving network types {ex.Message}."
                };
            }
            return View(getNetworkTypeResponse.data);
        }

        // GET: NetworkTypeController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: NetworkTypeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: NetworkTypeController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            GetNetworkTypeResponse getNetworkTypeResponse = null;
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:5277/api/");

                    var response = await client.GetAsync($"NetworkType/GetNetworkTypeById?id={id}");

                    if (response.IsSuccessStatusCode)
                    {
                        var responseString = await response.Content.ReadAsStringAsync();
                        getNetworkTypeResponse = System.Text.Json.JsonSerializer.Deserialize<GetNetworkTypeResponse>(responseString);
                    }
                    else
                    {
                        getNetworkTypeResponse = new GetNetworkTypeResponse
                        {
                            isSuccess = false,
                            message = "Error retrieving network types."
                        };
                    }

                }
            }
            catch (Exception ex)
            {

                getNetworkTypeResponse = new GetNetworkTypeResponse
                {
                    isSuccess = false,
                    message = $"Error retrieving network types {ex.Message}."
                };
            }
            return View(getNetworkTypeResponse.data);
        }

        // POST: NetworkTypeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }


    }
}
