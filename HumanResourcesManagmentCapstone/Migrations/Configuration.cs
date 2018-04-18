namespace HumanResourcesManagmentCapstone.Migrations
{
    using Microsoft.AspNet.Identity;
    using HumanResourcesManagmentCapstone.Models;
    using System;
    using System.Data.Entity.Migrations;
    using System.Collections.Generic;
    using System.Linq;

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
                HiredDate = new DateTime(2016, 6, 6),
                NationalIqamaID = 1231231,
                BankAccountNumber = "1",
                Nationality = "A",
                DateOfBirth = new DateTime(2016, 6, 6)
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
                HiredDate = new DateTime(2016, 6, 6),
                NationalIqamaID = 1231231,
                BankAccountNumber = "1",
                Nationality = "A",
                DateOfBirth = new DateTime(2016, 6, 6),
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
                HiredDate = new DateTime(2016, 6, 6),
                NationalIqamaID = 1231231,
                BankAccountNumber = "1",
                Nationality = "A",
                DateOfBirth = new DateTime(2016, 6, 6),                
            };
            userManager.Create(TM3, "123123");

            //Achievement
            var achievements = new List<Achievement>
            {
                new Achievement
                {
                AchievementType = AchievementType.Business,
                Discription = "",
                EmployeeId = context.Employees.Single(c => c.UserName == "Member1").Id
                },

                new Achievement
                {
                AchievementType = AchievementType.Business,
                Discription = "",
                EmployeeId = context.Employees.Single(c => c.UserName == "Member2").Id
                }
            };

            achievements.ForEach(s => context.Achievements.AddOrUpdate(m => m.AchievementId, s));
            context.SaveChanges();

            //Experience
            var experiences = new List<Experience>
            {
                new Experience
                {
                EmploymentPlace = "Google",
                EmploymentType = "Manager",
                StartDate = new DateTime(2016,6,6),
                EndDate = new DateTime(2016,6,6),
                Description = "",
                OrgnizationType = OrgnizationType.Corporate,
                EmployeeId = context.Employees.Single(c => c.UserName == "Member1").Id
                },

                new Experience
                {
                EmploymentPlace = "Dah",
                EmploymentType = "Manager",
                StartDate = new DateTime(2016,6,6),
                EndDate = new DateTime(2016,6,6),
                Description = "",
                OrgnizationType = OrgnizationType.Corporate,
                EmployeeId = context.Employees.Single(c => c.UserName == "Member2").Id
                },
            };
            experiences.ForEach(s => context.Experiences.AddOrUpdate(m => m.ExperienceId, s));
            context.SaveChanges();

            //CommunicationSkill
            var communicationSkills = new List<CommunicationSkill>
            {
                new CommunicationSkill
                {
                SkillType = "English",
                SkillLevel = SkillLevel.Basic,
                EmployeeId = context.Employees.Single(c => c.UserName == "Member1").Id
                },

                new CommunicationSkill
                {
                SkillType = "Arabic",
                SkillLevel = SkillLevel.Intermediate,
                EmployeeId = context.Employees.Single(c => c.UserName == "Member2").Id
                }
            };
            communicationSkills.ForEach(s => context.CommunicationSkills.AddOrUpdate(m => m.CommunicationSkillId, s));
            context.SaveChanges();

            //Attendance
            var attendances = new List<Attendance>
            {
                new Attendance
                {
                StartDate = new DateTime(2016,6,6),
                EndDate = new DateTime(2016,6,6),
                TargetWorkingHours = 30,
                PresentDays = 30,
                AbsentDays = 1,
                EmployeeWorkingHours = 30,
                FeedBack = "",
                EmployeeId = context.Employees.Single(c => c.UserName == "Member1").Id
                },

                new Attendance
                {
                StartDate = new DateTime(2016,6,6),
                EndDate = new DateTime(2016,6,6),
                TargetWorkingHours = 30,
                PresentDays = 30,
                AbsentDays = 1,
                EmployeeWorkingHours = 30,
                FeedBack = "",
                EmployeeId = context.Employees.Single(c => c.UserName == "Member2").Id

                }
            };
            attendances.ForEach(s => context.Attendances.AddOrUpdate(m => m.AttendanceId, s));
            context.SaveChanges();

            //Network
            var networks = new List<Network>
            {
                new Network
                {
                PlatformType = PlatformType.Instagram,
                ContactsNumber = 300,
                EmployeeId = context.Employees.Single(c => c.UserName == "Member1").Id
                },

                new Network
                {
                PlatformType = PlatformType.Instagram,
                ContactsNumber = 300,
                EmployeeId = context.Employees.Single(c => c.UserName == "Member2").Id
                }
            };
            networks.ForEach(s => context.Networks.AddOrUpdate(m => m.NetworkId, s));
            context.SaveChanges();

            //Network
            var performances = new List<Performance>
            {
                new Performance
                {
                KPI = 66,
                Discipline = 100,
                Status = "",
                IssueDate = new DateTime(2016,6,6),
                Comment = "",
                Decision = Decision.Approved,
                CreationDate = new DateTime(2016,6,6),
                EmployeeId = context.Employees.Single(c => c.UserName == "Member1").Id
                },

                new Performance
                {
                KPI = 66,
                Discipline = 100,
                Status = "",
                IssueDate = new DateTime(2016,6,6),
                Comment = "",
                Decision = Decision.Approved,
                CreationDate = new DateTime(2016,6,6),
                EmployeeId = context.Employees.Single(c => c.UserName == "Member2").Id
                }
            };
            performances.ForEach(s => context.Performances.AddOrUpdate(m => m.PerformanceId, s));
            context.SaveChanges();

            // Certifications 
            var certifications = new List<Certification>
            {
                new Certification
                {
                StartDate = new DateTime(2016,6,6),
                EndDate = new DateTime(2016,6,6),
                UniversityRank = 1,
                Major = "MIS",
                GPA = 1,
                Extracurricular = "NO",
                EmployeeId = context.Employees.Single(c => c.UserName == "Member2").Id

                },

                 new Certification
                {
                StartDate = new DateTime(2016,6,6),
                EndDate = new DateTime(2016,6,6),
                UniversityRank = 1,
                Major = "MIS",
                GPA = 1,
                Extracurricular = "NO",
                EmployeeId = context.Employees.Single(c => c.UserName == "Member1").Id

                },

            };
            certifications.ForEach(s => context.Certifications.AddOrUpdate(m => m.CertificationId, s));
            context.SaveChanges();


            // Criteria 
            var criteria = new List<Criterion>
            {
                new Criterion { Criteria = "Positive Energy"},
                new Criterion { Criteria = "Taking Initiatives "},
                new Criterion { Criteria = "Action "},
                new Criterion { Criteria = "Project Managment"},
                new Criterion { Criteria = "Time Management "},
                new Criterion { Criteria = "Functionallys"},
                new Criterion { Criteria = "Personally"},
                new Criterion { Criteria = "Listening Skills"},
                new Criterion { Criteria = "Skills"},
                new Criterion { Criteria = "Knowladge "},
                new Criterion { Criteria = "Attitudes "},
                new Criterion { Criteria = "Doing their best" },
                new Criterion { Criteria = "Having a vision"},
                new Criterion { Criteria = "Moving others"},
                new Criterion { Criteria = "Doning Something deferent"}
            };
            criteria.ForEach(s => context.Criteria.AddOrUpdate(m => m.Criteria, s));
            context.SaveChanges();
        }
    }
}
  
