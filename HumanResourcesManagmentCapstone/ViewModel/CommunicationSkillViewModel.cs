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
    public class CommunicationSkillViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Skill Type")]
        public string SkillType { get; set; }

        [Display(Name = "Skill Level")]
        public LevelLevel? SkillLevel { get; set; }

        public int EmployeeId { get; set; }

        public string Employee { get; set; }
    }
}