using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using UserManagement.Web.Dtos;
using UserManagement.Web.Models;
using UserManagement.Web.Services.Interfaces;

namespace UserManagement.Web.Services
{
    public class UserHttpService : IUserHttpService
    {
        private readonly HttpClient http;
        private readonly ILogger<UserHttpService> logger;

        public UserHttpService(HttpClient http, ILogger<UserHttpService> logger)
        {
            this.http = http;
            this.logger = logger;
        }

        public async Task<ApiResponse> Add(UserViewModel user)
        {
            try
            {
                Validate(user);
                var response = await http.PostAsJsonAsync("api/User", user);
                return await ResponseMessage(response);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public async Task<ApiResponse> Delete(int id)
        {
            try
            {
                var response = await http.DeleteAsync($"api/User?id={id}");
                return await ResponseMessage(response);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                throw new Exception(ex.Message);
            }

        }

        public async Task<IEnumerable<UserViewModel>> GetAll()
        {
            try
            {
                var users = await http.GetFromJsonAsync<IEnumerable<UserViewModel>>("api/User");
                return users;
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public async Task<ApiResponse> Update(UserViewModel user)
        {
            try
            {
                Validate(user);
                var response = await http.PutAsJsonAsync("api/User", user);
                return await ResponseMessage(response);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                throw new Exception(ex.Message);
            }
        }
        private async Task<ApiResponse> ResponseMessage(HttpResponseMessage response)
        {
            var json = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<DeserializeResponse>(json);

            return new ApiResponse
            {
                Message = result?.message,
                Status = response.IsSuccessStatusCode ? StatusResponse.Success : StatusResponse.Error
            };
        }

        private void Validate(UserViewModel user)
        {
            if (user.Gender == "Seleccione genero")
            {
                logger.LogWarning($"No se selecciono un genero");
                throw new ValidationException($"Seleccionar un genero");
            }

            if (user.Name.Length > 100)
            {
                logger.LogWarning($"limite de 100 caracte, se supero el máximo aceptado para el campo nombre.");
                throw new ValidationException($"limite de 100 caracte, se supero el máximo aceptado para el campo nombre.");
            }
        }
    }
}