/*
* Description: Custom date validation attribute used for validating attendace is not created in the future.
* Author: Zee
* Due date: 17/04/2018
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace HumanResourcesManagmentCapstone.Common
{
    public class CurrentDateAttribute : ValidationAttribute
    {
        /// <summary>
        ///  Validate attribute is not more than current date.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool IsValid(object value)
        {
            DateTime dateTime = Convert.ToDateTime(value);

            return dateTime <= DateTime.Now;
        }
    }
}