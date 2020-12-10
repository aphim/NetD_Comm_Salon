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
    public class appointment
    {
        //Getters and Setters for appointment class properties
        public int appointmentID { get; set; }

        public DateTime appointmentDate { get; set; }


        public int stylistID { get; set; }

        public virtual Stylist stylist{ get; set; }

        public string stylistFName { get; set; }

        public string stylistLName { get; set; }

        public string fullname { get { return this.stylistFName + " " + this.stylistFName; } }

        public int clientID { get; set; }

        public virtual client client { get; set; }

    }
}
