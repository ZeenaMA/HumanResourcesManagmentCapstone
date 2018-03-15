using HumanResourcesManagmentCapstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HumanResourcesManagmentCapstone.ViewModel
{
    public class CommunicationSkillViewModel
    {
        public int Id { get; set; }

        public string SkillType { get; set; }

        public LevelLevel? SkillLevel { get; set; }
    }
}