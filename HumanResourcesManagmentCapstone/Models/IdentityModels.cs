using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using HumanResourcesManagmentCapstone.Models;

namespace HumanResourcesManagmentCapstone.Models
{
    public class ApplicationUser : IdentityUser<int, CustomUserLogin, CustomUserRole,
    CustomUserClaim>
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser, int> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, CustomRole,
    int, CustomUserLogin, CustomUserRole, CustomUserClaim>
    {
        public ApplicationDbContext()
            : base("DefaultConnection")
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public virtual DbSet<Achievement> Achievements { get; set; }
        public virtual DbSet<Attendance> Attendances { get; set; }
        public virtual DbSet<Certification> Certifications { get; set; }
        public virtual DbSet<CommunicationSkill> CommunicationSkills { get; set; }
        public virtual DbSet<Constituent> Constituents { get; set; }
        public virtual DbSet<Criterion> Criteria { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Evaluation> Evaluations { get; set; }
        public virtual DbSet<Experience> Experiences { get; set; }
        public virtual DbSet<Network> Networks { get; set; }
        public virtual DbSet<Performance> Performances { get; set; }
        public virtual DbSet<Salary> Salaries { get; set; }
        ///public virtual DbSet<Score> Scores { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Attendance>()
                .Property(e => e.TargetWorkingHours)
                .HasPrecision(4, 2);

            modelBuilder.Entity<Attendance>()
                .Property(e => e.EmployeeWorkingHours)
                .HasPrecision(4, 2);

            modelBuilder.Entity<Certification>()
                .Property(e => e.GPA)
                .HasPrecision(3, 2);

            //modelBuilder.Entity<Criterion>()
            //    .Property(e => e.CriteriaScore);
            //    //.HasPrecision(3, 1);

            //modelBuilder.Entity<Criterion>()
            //    .HasMany(e => e.Evaluations)
            //    .WithRequired(e => e.Criterion)
            //    .WillCascadeOnDelete(false);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.Achievements)
                .WithRequired(e => e.Employee)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.Attendances)
                .WithRequired(e => e.Employee)
                .HasForeignKey(e => e.EmployeeId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.AdminAttendances)
                .WithOptional(e => e.Administrator)
                .HasForeignKey(e => e.AdministratorId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.Certifications)
                .WithRequired(e => e.Employee)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.CommunicationSkills)
                .WithRequired(e => e.Employee)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.Constituents)
                .WithRequired(e => e.Employee)
                .HasForeignKey(e => e.EPEmployeeId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.Evaluations)
                .WithRequired(e => e.Employee)
                .HasForeignKey(e => e.EmployeeId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.EmployeeEvaluation)
                .WithRequired(e => e.EmployeeEvaluation)
                .HasForeignKey(e => e.EvaluatorId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.Experiences)
                .WithRequired(e => e.Employee)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.Networks)
                .WithRequired(e => e.Employee)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.Performances)
                .WithRequired(e => e.Employee)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.Salaries)
                .WithRequired(e => e.Employee)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Evaluation>()
                .Property(e => e.GradeAttained)
                .HasPrecision(3, 1);

            modelBuilder.Entity<Network>()
                .Property(e => e.ContactsNumber);
                //.HasPrecision(6, 0);

            modelBuilder.Entity<Performance>()
                .Property(e => e.KPI)
                .HasPrecision(5, 2);

            modelBuilder.Entity<Performance>()
                .Property(e => e.Discipline)
                .HasPrecision(5, 2);

            modelBuilder.Entity<Performance>()
                .HasMany(e => e.Constituents)
                .WithRequired(e => e.Performance)
                .HasForeignKey(e => e.EPPerformanceId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Salary>()
                .Property(e => e.BasicSalary)
                .HasPrecision(7, 2);

            modelBuilder.Entity<Salary>()
                .Property(e => e.PerformanceBasedSalary)
                .HasPrecision(7, 2);
        }

        //public System.Data.Entity.DbSet<HumanResourcesManagmentCapstone.ViewModel.AttendanceViewModel> AttendanceViewModels { get; set; }

        //public System.Data.Entity.DbSet<HumanResourcesManagmentCapstone.ViewModel.EvaluationViewModel> EvaluationViewModels { get; set; }
    }

    public class CustomUserRole : IdentityUserRole<int> { }
    public class CustomUserClaim : IdentityUserClaim<int> { }
    public class CustomUserLogin : IdentityUserLogin<int> { }

    public class CustomRole : IdentityRole<int, CustomUserRole>
    {
        public CustomRole() { }
        public CustomRole(string name) { Name = name; }
    }

    public class CustomUserStore : UserStore<ApplicationUser, CustomRole, int,
        CustomUserLogin, CustomUserRole, CustomUserClaim>
    {
        public CustomUserStore(ApplicationDbContext context)
            : base(context)
        {
        }
    }

    public class CustomRoleStore : RoleStore<CustomRole, int, CustomUserRole>
    {
        public CustomRoleStore(ApplicationDbContext context)
            : base(context)
        {
        }
    }

}
