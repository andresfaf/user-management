using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using UserManagement.Data.Context;
using UserManagement.Data.Entities;
using UserManagement.Data.Interfaces;

namespace UserManagement.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext applicationDbContext;
        private readonly ILogger<UserRepository> logger;
        public UserRepository(ApplicationDbContext applicationDbContext, ILogger<UserRepository> logger)
        {
            this.applicationDbContext = applicationDbContext;
            this.logger = logger;
        }

        public async Task Add(User user)
        {
            try
            {
                await applicationDbContext.Database
                    .ExecuteSqlInterpolatedAsync($"EXEC usp_InsertUser {user.Name}, {user.BirthDate}, {user.Gender}"
                );
            }
            catch (SqlException ex)
            {
                logger.LogError(ex, "Error al crear usuario.");
                throw;
            }
        }

        public async Task Delete(int id)
        {
            try
            {
                await applicationDbContext.Database
                    .ExecuteSqlInterpolatedAsync($"EXEC usp_DeleteUser {id}"
                );
            }
            catch (SqlException ex)
            {
                logger.LogError(ex, "Error al eliminar usuario.");
                throw;
            }
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            try
            {
                return await applicationDbContext.Users
               .FromSqlInterpolated($"EXEC usp_GetAllUsers")
               .ToListAsync();
            }
            catch (SqlException ex)
            {
                logger.LogError(ex, "Error al obtener listado de usuarios.");
                throw;
            }
        }

        public async Task Update(User user)
        {
            try
            {
                await applicationDbContext.Database
                    .ExecuteSqlInterpolatedAsync($"EXEC usp_UpdateUser {user.Id}, {user.Name}, {user.BirthDate}, {user.Gender}"
                );
            }
            catch (SqlException ex)
            {
                logger.LogError(ex, "Error al actualizar usuario.");
                throw;
            }
        }
    }
}
