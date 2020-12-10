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
    public class client
    {
        //Getters and setters for client class properties 
        public int clientID { get; set; }

        public string clientFName { get; set; }

        public string clientLName { get; set; }

        public string clientPhonenumber { get; set; }

        public string clientEmail { get; set; }

        //class function used to validate inputted data
        public bool vaildateInfo(string clientFName, string clientLName, string clientPhonenumber, string clientEmail)
        {
            
            if ( string.IsNullOrEmpty(clientFName) || string.IsNullOrEmpty(clientLName) || string.IsNullOrEmpty(clientPhonenumber)||string.IsNullOrEmpty(clientEmail))
            {

                return false;
            }
            else
            {
                long num = 0;
                if (clientPhonenumber.Length == 10 && Int64.TryParse(clientPhonenumber, out num) && clientFName.Length > 1 && clientLName.Length > 1 && IsValidEmail(clientEmail))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        //class function used to validate emails
        public bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}
