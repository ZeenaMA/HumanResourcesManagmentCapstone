/*
* Description: .
* Author: Zee
* Due date: 20/03/2018
*/
using HumanResourcesManagmentCapstone.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HumanResourcesManagmentCapstone.ViewModel
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Middl Name")]
        public string MiddleName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        //[Required]
        //[StringLength(10)]
        [Display(Name ="Employee Type")]
        public EmployeeType? EmployeeType { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name ="Hire Date")]
        public DateTime HiredDate { get; set; }

        [Display(Name ="National/Iqama ID")]
        public int NationalIqamaID { get; set; }

        [Required]
        [StringLength(13)]
        [Display(Name ="Bank Account Number")]
        public string BankAccountNumber { get; set; }

        [Required]
        [StringLength(50)]
        public string Nationality { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name ="Date of Birth")]
        public DateTime DateOfBirth { get; set; }

        // Used to diaplay the list of roles
        public string Roles { get; set; }

        // List of attendances ????
        public EmployeeViewModel()
        {
            Attendances = new List<Attendance>();
        }
        public List<Attendance> Attendances { get; set; }

    }
}