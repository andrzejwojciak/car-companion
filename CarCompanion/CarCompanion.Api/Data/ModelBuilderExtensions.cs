using System;
using carcompanion.Models;
using Microsoft.EntityFrameworkCore;

namespace carcompanion.Data
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ExpenseCategory>().HasData(
                new ExpenseCategory {ExpenseCategoryId = "other", Description = null, ParentId = null},
                new ExpenseCategory {ExpenseCategoryId = "insurance", Description = null, ParentId = null},
                new ExpenseCategory {ExpenseCategoryId = "repair", Description = null, ParentId = null},
                new ExpenseCategory {ExpenseCategoryId = "fuel", Description = null, ParentId = null},
                new ExpenseCategory {ExpenseCategoryId = "utilization", Description = null, ParentId = null}
            );

            modelBuilder.Entity<UserCarRole>().HasData(
                new UserCarRole {UserCarRoleId = "owner"},
                new UserCarRole {UserCarRoleId = "editor"},
                new UserCarRole {UserCarRoleId = "viewer"}
            );

            modelBuilder.Entity<Role>().HasData(
                new Role {RoleId = "user"},
                new Role {RoleId = "superUser"},
                new Role {RoleId = "admin"}
            );

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    UserId = Guid.NewGuid(), Email = "admin@example.com",
                    Password = "b7F7UYpUwpYZBbIsp0McRFp+/LAtS9e8zZsDF1sYKbw=", //zaq1@WSX
                    AddedDate = DateTime.Now,
                    RoleId = "admin"
                },
                new User
                {
                    UserId = Guid.NewGuid(), Email = "superuser@example.com",
                    Password = "b7F7UYpUwpYZBbIsp0McRFp+/LAtS9e8zZsDF1sYKbw=", //zaq1@WSX
                    AddedDate = DateTime.Now,
                    RoleId = "superUser"
                },
                new User
                {
                    UserId = Guid.NewGuid(), Email = "firstuser@example.com",
                    Password = "b7F7UYpUwpYZBbIsp0McRFp+/LAtS9e8zZsDF1sYKbw=", //zaq1@WSX
                    AddedDate = DateTime.Now, RoleId = "user"
                },
                new User
                {
                    UserId = Guid.NewGuid(), Email = "secounduser@example.com",
                    Password = "b7F7UYpUwpYZBbIsp0McRFp+/LAtS9e8zZsDF1sYKbw=", //zaq1@WSX
                    AddedDate = DateTime.Now, RoleId = "user"
                }
            );
        }
    }
}