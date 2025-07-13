using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using Mapster;
using Microsoft.Data.SqlClient;
using UserManagement.Api.Dtos;
using UserManagement.Api.Interfaces;
using UserManagement.Data.Entities;
using UserManagement.Data.Interfaces;

namespace UserManagement.Api.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        private readonly ILogger<UserService> logger;

        public UserService(IUserRepository userRepository, ILogger<UserService> logger)
        {
            this.userRepository = userRepository;
            this.logger = logger;
        }

        public async Task Add(UserDto userDto)
        {
            try
            {
                if (!Regex.IsMatch(userDto.Gender, "^[MFOmfo]$"))
                {
                    logger.LogWarning($"El campo genero solo acepta M, F, O. Se ingresó: '{userDto.Gender}'.");
                    throw new ValidationException($"El campo genero solo acepta M, F, O. Se ingresó: '{userDto.Gender}'.");
                }

                if (userDto.Name.Length > 100)
                {
                    logger.LogWarning($"limite de 100 caracte, se supero el máximo aceptado para el campo nombre.");
                    throw new ValidationException($"limite de 100 caracte, se supero el máximo aceptado para el campo nombre.");
                }

                var user = userDto.Adapt<User>();
                await userRepository.Add(user);
            }
            catch (SqlException)
            {
                throw;
            }
        }

        public async Task Delete(int id)
        {
            try
            {
                await userRepository.Delete(id);
            }
            catch (SqlException)
            {
                throw;
            }
        }

        public async Task<IEnumerable<UserDto>> GetAll()
        {
            try
            {
                var users = await userRepository.GetAll();
                var usersDto = users.Adapt<IEnumerable<UserDto>>();
                return usersDto; 
            }
            catch (SqlException)
            {
                throw;
            }
            
        }

        public async Task Update(UserDto userDto)
        {
            try
            {
                if (!Regex.IsMatch(userDto.Gender, "^[MFOmfo]$"))
                {
                    logger.LogWarning($"El campo genero solo acepta M, F, O. Se ingresó: '{userDto.Gender}'.");
                    throw new ValidationException($"El campo genero solo acepta M, F, O. Se ingresó: '{userDto.Gender}'.");
                }

                if (userDto.Name.Length > 100)
                {
                    logger.LogWarning($"limite de 100 caracte, se supero el máximo aceptado para el campo nombre.");
                    throw new ValidationException($"limite de 100 caracte, se supero el máximo aceptado para el campo nombre.");
                }

                var user = userDto.Adapt<User>();
                await userRepository.Update(user);
            }
            catch (SqlException)
            {
                throw;
            }
        }
    }
}
