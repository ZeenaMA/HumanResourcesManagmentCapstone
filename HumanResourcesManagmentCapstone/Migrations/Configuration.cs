namespace HumanResourcesManagmentCapstone.Migrations
{
    using Microsoft.AspNet.Identity;
    using HumanResourcesManagmentCapstone.Models;
    using System;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            string[] roles = { "Admin", "CEO" };

            string adminEmail = "admin@dah.edu";
            string adminUserName = "admin";
            string adminPassword = "admin123";

            // Create roles
            var roleStore = new CustomRoleStore(context);
            var roleManager = new RoleManager<CustomRole, int>(roleStore);

            foreach (var role in roles)
            {
                if (!roleManager.RoleExists(role))
                {
                    roleManager.Create(new CustomRole { Name = role });
                }
            }

            // Define admin user
            var userStore = new CustomUserStore(context);
            var userManager = new ApplicationUserManager(userStore);

            var admin = new ApplicationUser
            {
                UserName = adminUserName,
                Email = adminEmail,
                EmailConfirmed = true,
                LockoutEnabled = false
            };

            // Create admin user
            if (userManager.FindByName(admin.UserName) == null)
            {
                userManager.Create(admin, adminPassword);
            }

            // Add admin user to admin role
            // roles[0] is "Admin"
            var user = userManager.FindByName(admin.UserName);
            if (!userManager.IsInRole(user.Id, roles[0]))
            {
                userManager.AddToRole(admin.Id, roles[0]);
            }

            // Create Employee. 
            var TM1 = new Employee
            {
                UserName = "Member1",
                Email = "email@gmail.com",
                FirstName = "First",
                MiddleName = "Middle",
                LastName = "Last",
                EmployeeType = EmployeeType.TeamMember,
                HiredDate = DateTime.ParseExact("15/06/2015 13:45:00", "dd/MM/yyyy HH:mm:ss", null),
                NationalIqamaID = 1231231,
                BankAccountNumber = "1",
                Nationality = "A",
                DateOfBirth = DateTime.ParseExact("15/06/2015 13:45:00", "dd/MM/yyyy HH:mm:ss", null)
            };
            userManager.Create(TM1, "123123");

            var TM2 = new Employee
            {
                UserName = "Member2",
                Email = "email@gmail.com",
                FirstName = "Human",
                MiddleName = "Middle",
                LastName = "Last",
                EmployeeType = EmployeeType.TeamMember,
                HiredDate = DateTime.ParseExact("15/06/2015 13:45:00", "dd/MM/yyyy HH:mm:ss", null),
                NationalIqamaID = 1231231,
                BankAccountNumber = "1",
                Nationality = "A",
                DateOfBirth = DateTime.ParseExact("15/06/2015 13:45:00", "dd/MM/yyyy HH:mm:ss", null)
            };
            userManager.Create(TM2, "123123");

            var TM3 = new Employee
            {
                UserName = "Member3",
                Email = "email@gmail.com",
                FirstName = "Human",
                MiddleName = "Middle",
                LastName = "Last",
                EmployeeType = EmployeeType.TeamMember,
                HiredDate = DateTime.ParseExact("15/06/2015 13:45:00", "dd/MM/yyyy HH:mm:ss", null),
                NationalIqamaID = 1231231,
                BankAccountNumber = "1",
                Nationality = "A",
                DateOfBirth = DateTime.ParseExact("15/06/2015 13:45:00", "dd/MM/yyyy HH:mm:ss", null)
            };
            userManager.Create(TM3, "123123");


            //// Certification
            //var Certification1 = new Certification
            //{
            //    StartDate = DateTime.ParseExact("15/06/2015 13:45:00", "dd/MM/yyyy HH:mm:ss", null),
            //    EndDate = DateTime.ParseExact("15/06/2015 13:45:00", "dd/MM/yyyy HH:mm:ss", null),
            //    UniversityRank = 1,
            //    Major = "MIS",
            //    GPA = 1,
            //    Extracurricular = "NO",
            //    EmployeeId = 2,
            //};
        }

    }
}
  
