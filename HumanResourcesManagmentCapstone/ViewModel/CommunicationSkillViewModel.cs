/*
* Description: View model for CommunicationSkill passes information between CommunicationSkill views and its controller.
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
    public class CommunicationSkillViewModel
    {
        /// <summary>
        /// View model based on the CommunicationSkill model.
        /// </summary>
        public int Id { get; set; }

        [Display(Name = "Skill Type")]
        public string SkillType { get; set; }

        [Display(Name = "Skill Level")]
        public SkillLevel? SkillLevel { get; set; }

        public int EmployeeId { get; set; }

        [Display(Name = "Employee Name")]
        public string EmployeeName { get; set; }
    }
}