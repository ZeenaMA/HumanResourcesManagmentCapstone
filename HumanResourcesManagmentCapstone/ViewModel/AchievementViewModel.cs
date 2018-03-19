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
    /// <summary>
    /// 
    /// </summary>
    public class AchievementViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Achievement Type")]
        public TypeOfAchievement? AchievementType { get; set; }

        public string Discription { get; set; }

        public int EmployeeId { get; set; }

        public string Employee { get; set; }
    }
}