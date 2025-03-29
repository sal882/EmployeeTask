using EmployeeTask.Abstractions.Const;
using EmployeeTask.VerticalSlicing.Data.Entities;
using EmployeeTask.VerticalSlicing.Features.Auth.Common;
using Microsoft.EntityFrameworkCore;

namespace EmployeeTask.VerticalSlicing.Data.Context
{
    public class ApplicationContextSeed
    {
        public static async Task seedAsync(ApplicationDBContext dbcontext)
        {

            // seed roles
            var adminRole = await EnsureRoleExistsAsync(dbcontext, DefaultRoles.Admin);
            var employeeRole = await EnsureRoleExistsAsync(dbcontext, DefaultRoles.Employee);
            var userRole = await EnsureRoleExistsAsync(dbcontext, DefaultRoles.User);

            // seed Admin data
            if (!dbcontext.Set<User>().Any(u => u.FullName == DefaultAdmins.AdminUserName))
            {
                var admin = new User
                {

                    Email = DefaultAdmins.AdminEmail,
                    FullName = DefaultAdmins.AdminUserName,
                    IsVerified = true,
                    PhoneNumber = DefaultAdmins.AdminPhoneNumber,
                    VerificationOTP = null,
                    PasswordHash = PasswordHasher.HashPassword(DefaultAdmins.AdminPassword),
                    RoleId = adminRole.Id

                };

                await dbcontext.Set<User>().AddAsync(admin);
                await dbcontext.SaveChangesAsync();


            }
        }


        private static async Task<Role?> EnsureRoleExistsAsync(ApplicationDBContext dbContext, string roleName)
        {
            var role = await dbContext.Set<Role>().FirstOrDefaultAsync(r => r.Name == roleName);
            if (role == null)
            {
                role = new Role
                {
                    Name = roleName
                };
                await dbContext.Set<Role>().AddAsync(role);
                await dbContext.SaveChangesAsync();
            }
            return role;
        }
    }
}
