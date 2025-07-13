using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using UserManagement.Web.Dtos;
using UserManagement.Web.Models;
using UserManagement.Web.Services.Interfaces;

namespace UserManagement.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserHttpService userHttpService;

        public UserController(IUserHttpService userHttpService)
        {
            this.userHttpService = userHttpService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(UserViewModel viewModel)
        {
            try
            {
                if (viewModel.Id == 0)
                {
                    ApiResponse response = await userHttpService.Add(viewModel);
                    TempData[response.Status.ToString()] = response.Message;
                    return RedirectToAction("Management");
                }
                else
                {
                    if (viewModel.EnabledUpdate is true)
                    {
                        ApiResponse response = await userHttpService.Update(viewModel);
                        TempData[response.Status.ToString()] = response.Message;
                        return RedirectToAction("Management");
                    }
                    else
                    {
                        return View(viewModel);
                    }
                }
            }
            catch (ValidationException ex)
            {
                TempData["Error"] = ex.Message;
                return View(viewModel);
            }
            catch (Exception)
            {
                TempData["Error"] = "Error inesperado";
                return View(viewModel);
            }
           
        }

        [HttpGet]
        public async Task<IActionResult> Management()
        {
            try
            {
                var users = await userHttpService.GetAll();
                return View(users);
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return View();
            }
            
        }

        [HttpGet]
        public IActionResult ConfirmDelete(int id)
        {
            return PartialView("_ModalConfirmDelete", id);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                ApiResponse response = await userHttpService.Delete(id);
                TempData[response.Status.ToString()] = response.Message;      
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }
            return RedirectToAction("Management");
        }
    }
}
