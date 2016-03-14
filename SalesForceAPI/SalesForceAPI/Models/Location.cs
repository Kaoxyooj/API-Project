﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SalesForceAPI.Models
{
    public class Location
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
    }
}