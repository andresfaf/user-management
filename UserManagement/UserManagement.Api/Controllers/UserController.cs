using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using UserManagement.Api.Dtos;
using UserManagement.Api.Interfaces;

namespace UserManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;
        
        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> Add(UserDto userDto)
        {
            try
            {
                await userService.Add(userDto);
                return Ok(new { Message = "Usuario creado correctamente." });
            }
            catch (ValidationException ex)
            {
                return BadRequest(new { Message = ex.Message});
            }
            catch (Exception)
            {
                return StatusCode(500, new { Message = "Ocurrió un error inesperado al crear el usuario." });
            }     
        }

        [HttpPut]
        public async Task<IActionResult> Update(UserDto userDto)
        {
            try
            {
                await userService.Update(userDto);
                return Ok(new { Message = "Usuario actualizado correctamente." });
            }
            catch (ValidationException ex)
            {
                return BadRequest(new { Message = ex.ToString() });
            }
            catch (Exception)
            {
                return StatusCode(500, new { Message = "Ocurrió un error inesperado al actualizar el usuario." });
                throw;
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var usersDto = await userService.GetAll();
                return Ok(usersDto);
            }   
            catch (Exception)
            {
                return StatusCode(500, new { Message = "Ocurrió un error inesperado al obtener lista de usuarios." });
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await userService.Delete(id);
                return Ok(new { Message = "Usuario eliminado correctamente." });
            }
            catch (Exception)
            {
                return StatusCode(500, new { Message = "Ocurrió un error inesperado al eliminar el usuario." });
                throw;
            }
        }

    }
}
