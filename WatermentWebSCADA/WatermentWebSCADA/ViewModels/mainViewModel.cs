﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WatermentWebSCADA.Models;

namespace WatermentWebSCADA.ViewModels
{
    public class mainViewModel
    {

        //public facilities facilitiess { get; set; }
        
        
        //facilities
        public string Name { get; set; }
        public string IP { get; set; }
        public string Domain { get; set; }
        //locations model
        public string Country { get; set; }
        public int Postcode { get; set; }
        public string County { get; set; }
        public string LastCalibted { get; set; }

    }
}