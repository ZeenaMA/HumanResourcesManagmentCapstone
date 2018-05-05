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
                UserName = "member1",
                Email = "email@gmail.com",
                FirstName = "Layla",
                MiddleName = "M.",
                LastName = "Adam",
                EmployeeType = EmployeeType.TeamMember,
                HiredDate = new DateTime(2015, 6, 20),
                NationalIqamaID = 1231231,
                BankAccountNumber = "1",
                Nationality = "A",
                DateOfBirth = new DateTime(2016, 8, 6)
            };
            userManager.Create(TM1, "123123");

            var TM2 = new Employee
            {
                UserName = "member2",
                Email = "email@gmail.com",
                FirstName = "Sami",
                MiddleName = "K.",
                LastName = "Palm",
                EmployeeType = EmployeeType.TeamMember,
                HiredDate = new DateTime(2014, 1, 1),
                NationalIqamaID = 1231231,
                BankAccountNumber = "1",
                Nationality = "Saudi Arabia",
                DateOfBirth = new DateTime(2018, 6, 6),
            };
            userManager.Create(TM2, "123123");

            var TM3 = new Employee
            {
                UserName = "member3",
                Email = "email@gmail.com",
                FirstName = "Carmen",
                MiddleName = "B.",
                LastName = "Inican",
                EmployeeType = EmployeeType.TeamMember,
                HiredDate = new DateTime(2017, 9, 10),
                NationalIqamaID = 1231231,
                BankAccountNumber = "1",
                Nationality = "Dominican Republic",
                DateOfBirth = new DateTime(2016, 11, 1),
            };
            userManager.Create(TM3, "123123");

            var TM4 = new Employee
            {
                UserName = "member4",
                Email = "email@gmail.com",
                FirstName = "Ahmad",
                MiddleName = "M.",
                LastName = "Mali",
                EmployeeType = EmployeeType.TeamMember,
                HiredDate = new DateTime(2016, 9, 10),
                NationalIqamaID = 1231231,
                BankAccountNumber = "1",
                Nationality = "Dominican Republic",
                DateOfBirth = new DateTime(2016, 11, 1),
            };
            userManager.Create(TM4, "123123");

            //Achievement
            var achievements = new List<Achievement>
            {
                new Achievement
                {
                AchievementType = AchievementType.Business,
                Discription = "Presented the company at the KMPUL conference",
                EmployeeId = context.Employees.Single(c => c.UserName == "member1").Id
                },

                new Achievement
                {
                AchievementType = AchievementType.NonProfit,
                Discription = "Fundraiser for orphans",
                EmployeeId = context.Employees.Single(c => c.UserName == "member2").Id
                },

                new Achievement
                {
                AchievementType = AchievementType.Business,
                Discription = "Trained 10 new employees",
                EmployeeId = context.Employees.Single(c => c.UserName == "member3").Id
                },

                new Achievement
                {
                AchievementType = AchievementType.Personal,
                Discription = "Wrote a book",
                EmployeeId = context.Employees.Single(c => c.UserName == "member4").Id
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
                EndDate = new DateTime(2018,6,6),
                Description = "Managed sales employees.",
                OrgnizationType = OrgnizationType.Corporate,
                EmployeeId = context.Employees.Single(c => c.UserName == "member1").Id
                },

                new Experience
                {
                EmploymentPlace = "Dah",
                EmploymentType = "TA",
                StartDate = new DateTime(2015,7,6),
                EndDate = new DateTime(2016,9,1),
                Description = "Teaching assestant for MIS major.",
                OrgnizationType = OrgnizationType.Corporate,
                EmployeeId = context.Employees.Single(c => c.UserName == "member2").Id
                },

                new Experience
                {
                EmploymentPlace = "MIT",
                EmploymentType = "Techicnition",
                StartDate = new DateTime(2011,7,6),
                EndDate = new DateTime(2016,9,1),
                Description = "IT Techicnition.",
                OrgnizationType = OrgnizationType.Corporate,
                EmployeeId = context.Employees.Single(c => c.UserName == "member3").Id
                },

                new Experience
                {
                EmploymentPlace = "Dah",
                EmploymentType = "Manager",
                StartDate = new DateTime(2015,7,6),
                EndDate = new DateTime(2016,9,1),
                Description = "IT department manager",
                OrgnizationType = OrgnizationType.Corporate,
                EmployeeId = context.Employees.Single(c => c.UserName == "member4").Id
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
                EmployeeId = context.Employees.Single(c => c.UserName == "member1").Id
                },

                new CommunicationSkill
                {
                SkillType = "Arabic",
                SkillLevel = SkillLevel.Professional,
                EmployeeId = context.Employees.Single(c => c.UserName == "member1").Id
                },

                new CommunicationSkill
                {
                SkillType = "Arabic",
                SkillLevel = SkillLevel.Professional,
                EmployeeId = context.Employees.Single(c => c.UserName == "member2").Id
                },

                new CommunicationSkill
                {
                SkillType = "English",
                SkillLevel = SkillLevel.Professional,
                EmployeeId = context.Employees.Single(c => c.UserName == "member2").Id
                },


                 new CommunicationSkill
                {
                SkillType = "German",
                SkillLevel = SkillLevel.Intermediate,
                EmployeeId = context.Employees.Single(c => c.UserName == "member3").Id
                },

                 new CommunicationSkill
                {
                SkillType = "English",
                SkillLevel = SkillLevel.Professional,
                EmployeeId = context.Employees.Single(c => c.UserName == "member3").Id
                },


                 new CommunicationSkill
                {
                SkillType = "Arabic",
                SkillLevel = SkillLevel.Professional,
                EmployeeId = context.Employees.Single(c => c.UserName == "member3").Id
                },

                 new CommunicationSkill
                {
                SkillType = "German",
                SkillLevel = SkillLevel.Intermediate,
                EmployeeId = context.Employees.Single(c => c.UserName == "member3").Id
                },

                new CommunicationSkill
                {
                SkillType = "English",
                SkillLevel = SkillLevel.Professional,
                EmployeeId = context.Employees.Single(c => c.UserName == "member4").Id
                }

            };
            communicationSkills.ForEach(s => context.CommunicationSkills.AddOrUpdate(m => m.CommunicationSkillId, s));
            context.SaveChanges();

            //Attendance
            var attendances = new List<Attendance>
            {
                new Attendance
                {
                StartDate = new DateTime(2017,1,1),
                EndDate = new DateTime(2014,6,6),
                TargetWorkingHours = 30,
                PresentDays = 30,
                AbsentDays = 1,
                EmployeeWorkingHours = 30,
                FeedBack = "",
                EmployeeId = context.Employees.Single(c => c.UserName == "member1").Id
                },

                new Attendance
                {
                StartDate = new DateTime(2017,1,2),
                EndDate = new DateTime(2014,6,6),
                TargetWorkingHours = 30,
                PresentDays = 30,
                AbsentDays = 1,
                EmployeeWorkingHours = 30,
                FeedBack = "",
                EmployeeId = context.Employees.Single(c => c.UserName == "member1").Id
                },


                new Attendance
                {
                StartDate = new DateTime(2017,1,3),
                EndDate = new DateTime(2014,6,6),
                TargetWorkingHours = 30,
                PresentDays = 30,
                AbsentDays = 1,
                EmployeeWorkingHours = 30,
                FeedBack = "",
                EmployeeId = context.Employees.Single(c => c.UserName == "member1").Id
                },

                new Attendance
                {
                StartDate = new DateTime(2016,6,7),
                EndDate = new DateTime(2016,6,6),
                TargetWorkingHours = 30,
                PresentDays = 30,
                AbsentDays = 1,
                EmployeeWorkingHours = 30,
                FeedBack = "",
                EmployeeId = context.Employees.Single(c => c.UserName == "member2").Id

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
                EmployeeId = context.Employees.Single(c => c.UserName == "member1").Id
                },

                new Network
                {
                PlatformType = PlatformType.Instagram,
                ContactsNumber = 3000,
                EmployeeId = context.Employees.Single(c => c.UserName == "member2").Id
                },

                new Network
                {
                PlatformType = PlatformType.Facebook,
                ContactsNumber = 5620,
                EmployeeId = context.Employees.Single(c => c.UserName == "member2").Id
                },

                new Network
                {
                PlatformType = PlatformType.Twitter,
                ContactsNumber = 7923,
                EmployeeId = context.Employees.Single(c => c.UserName == "member2").Id
                }
            };
            networks.ForEach(s => context.Networks.AddOrUpdate(m => m.NetworkId, s));
            context.SaveChanges();

            // Performances
            var performances = new List<Performance>
            {
                new Performance
                {
                KPI = 66,
                Discipline = 100,
                Status = "",
                IssueDate = new DateTime(2018,1,1),
                Comment = "",
                Decision = Decision.Approved,
                CreationDate = new DateTime(2016,6,6),
                EmployeeId = context.Employees.Single(c => c.UserName == "member1").Id
                },

                new Performance
                {
                KPI = 90,
                Discipline = 75,
                Status = "",
                IssueDate = new DateTime(2018,2,1),
                Comment = "",
                Decision = Decision.Approved,
                CreationDate = new DateTime(2016,6,6),
                EmployeeId = context.Employees.Single(c => c.UserName == "member2").Id
                },

                new Performance
                {
                KPI = 60,
                Discipline = 75,
                Status = "",
                IssueDate = new DateTime(2018,2,1),
                Comment = "",
                Decision = Decision.Approved,
                CreationDate = new DateTime(2016,6,6),
                EmployeeId = context.Employees.Single(c => c.UserName == "member3").Id
                },

                new Performance
                {
                KPI = 50,
                Discipline = 100,
                Status = "",
                IssueDate = new DateTime(2018,2,1),
                Comment = "",
                Decision = Decision.Approved,
                CreationDate = new DateTime(2016,6,6),
                EmployeeId = context.Employees.Single(c => c.UserName == "member4").Id
                }
            };
            performances.ForEach(s => context.Performances.AddOrUpdate(m => m.PerformanceId, s));
            context.SaveChanges();

            // Certifications 
            var certifications = new List<Certification>
            {

                new Certification
                {
                StartDate = new DateTime(2004,9,6),
                EndDate = new DateTime(2008,1,1),
                UniversityRank = 654,
                Major = "MIS",
                GPA = 3.5M,
                Extracurricular = "Astronomy Club",
                EmployeeId = context.Employees.Single(c => c.UserName == "member1").Id

                },

                new Certification
                {
                StartDate = new DateTime(2004,9,6),
                EndDate = new DateTime(2008,1,1),
                UniversityRank = 654,
                Major = "MIS",
                GPA = 4.0M,
                Extracurricular = "Astronomy Club",
                EmployeeId = context.Employees.Single(c => c.UserName == "member2").Id

                },

                 new Certification
                {
                StartDate = new DateTime(2006,1,1),
                EndDate = new DateTime(2010,1,1),
                UniversityRank = 1000,
                Major = "CS",
                GPA = 3.9M,
                Extracurricular = "Literature Club",
                EmployeeId = context.Employees.Single(c => c.UserName == "member3").Id
                },

                new Certification
                {
                StartDate = new DateTime(2004,9,6),
                EndDate = new DateTime(2008,1,1),
                UniversityRank = 1,
                Major = "Marketing",
                GPA = 4.5M,
                Extracurricular = "Peer Tutoring",
                EmployeeId = context.Employees.Single(c => c.UserName == "member4").Id

                },
            };
            certifications.ForEach(s => context.Certifications.AddOrUpdate(m => m.CertificationId, s));
            context.SaveChanges();

            // Criteria 
            var criteria = new List<Criterion>
            {
                new Criterion { Criteria = "(Proactivity) Positive Energy"},
                new Criterion { Criteria = "(Proactivity) Taking Initiatives "},
                new Criterion { Criteria = "(Proactivity) Action "},
                new Criterion { Criteria = "(Organized) Project Managment"},
                new Criterion { Criteria = "(Organized) Time Management "},
                new Criterion { Criteria = "(Support Others) Functionally"},
                new Criterion { Criteria = "(Support Others) Personally"},
                new Criterion { Criteria = "(Communication) Listening Skills"},
                new Criterion { Criteria = "(Communication) Skills"},
                new Criterion { Criteria = "(Development) Knowladge "},
                new Criterion { Criteria = "(Dedication) Attitudes "},
                new Criterion { Criteria = "(Dedication) Doing their best" },
                new Criterion { Criteria = "(Leadership) Having a vision"},
                new Criterion { Criteria = "(Leadership) Moving others"},
                new Criterion { Criteria = "(Creativity) Doning Something deferent"}
            };
            criteria.ForEach(s => context.Criteria.AddOrUpdate(m => m.Criteria, s));
            context.SaveChanges();
        }
    }
}
