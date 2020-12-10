//Program:      Netd 3202 Lab 5 Salon Webpage
//Created by:   Jacky Yuan
//Date:         Dec 04, 2020
//Purpose:      Basic website for a hair salon
//Change log:   N/A
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Netd_Comm_Salon.Models
{
    public class Stylist
    {
        //getters and setters for the stylist properties
        public int stylistID { get; set; }

        public string stylistFName { get; set; }

        public string stylistLName { get; set; }

        public string stylistExt { get; set; }

        public string fullname { get { return this.stylistFName + " " + this.stylistLName; } }

        //class function used for validating the stylist inputs
        public bool vaildatestylst(string stylistFName, string stylistLName, string stylistExt)
        {
            if (string.IsNullOrEmpty(stylistFName) || string.IsNullOrEmpty(stylistLName) || string.IsNullOrEmpty(stylistExt))
            {

                return false;
            }
            else
            {
                long num = 0;
                if (stylistExt.Length == 3 && Int64.TryParse(stylistExt, out num) && stylistFName.Length > 1 && stylistLName.Length > 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }




    }
}
