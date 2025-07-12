using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SGM.Application.Contracts.Services.insuranse;
using SGM.Application.Dtos.insurance.InsuranceProvider;
using SGM.Application.Services.Insurance;
using SGM.Domain.Entities.Insurance;

namespace SGM.Web.Controllers
{
    public class InsanceProviderController : Controller
    {
        private readonly IInsuranceProviderService _insuranceProviderService;

        public InsanceProviderController(IInsuranceProviderService insuranceProviderService)
        {
            _insuranceProviderService = insuranceProviderService;
        }
        public async Task<IActionResult> Index()
        {
            var result = await _insuranceProviderService.GetInsuranceProviders();

            if (result.IsSuccess)
            {
                List<InsuranceProvider> insuranceProvidersList = (List<InsuranceProvider>)result.Data;
                return View(insuranceProvidersList);
            }
            else
            {
                // Log the error message or handle it accordingly
                ModelState.AddModelError(string.Empty, result.Message);
                return View(new List<InsuranceProvider>()); // Return an empty list or handle as needed
            }
            return View();
        }

        // GET: InsanceProviderController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var result = await _insuranceProviderService.GetInsuranceProviderById(id);

            if (result.IsSuccess)
            {
                InsuranceProvider insuranceProvider = (InsuranceProvider)result.Data;
                return View(insuranceProvider);
            }
            else
            {
                // Log the error message or handle it accordingly
                ModelState.AddModelError(string.Empty, result.Message);
                return View(new InsuranceProvider()); // Return an empty object or handle as needed
            }

            return View();
        }

        // GET: InsanceProviderController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: InsanceProviderController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateInsuranceProviderDto createInsuranceProvider)
        {
            try
            {
                var result = await _insuranceProviderService.CreateInsuranceProvider(createInsuranceProvider);
               
                if (!result.IsSuccess)
                {
                    // Log the error message or handle it accordingly
                    ModelState.AddModelError(string.Empty, result.Message);
                    return View(createInsuranceProvider); // Return the same view with the model to show errors
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: InsanceProviderController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var result = await _insuranceProviderService.GetInsuranceProviderById(id);

            if (result.IsSuccess)
            {
                InsuranceProvider insuranceProvider = (InsuranceProvider)result.Data;
                return View(insuranceProvider);
            }
            else
            {
                // Log the error message or handle it accordingly
                ModelState.AddModelError(string.Empty, result.Message);
                return View(new InsuranceProvider()); // Return an empty object or handle as needed
            }

            return View();
        }

        // POST: InsanceProviderController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ModifyInsuranceProviderDto insuranceProvider)
        {
            try
            {
                var result = await _insuranceProviderService.UpdateInsuranceProvider(insuranceProvider);
                
                if (!result.IsSuccess)
                {
                    // Log the error message or handle it accordingly
                    ModelState.AddModelError(string.Empty, result.Message);
                    return View(insuranceProvider); // Return the same view with the model to show errors
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

      
    }
}
