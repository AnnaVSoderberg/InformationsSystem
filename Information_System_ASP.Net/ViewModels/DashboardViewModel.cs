using System;
using System.Collections.Generic;

namespace Information_System_ASP.Net.Models
{
    public class DashboardViewModel
    {
        public List<Event> Notifications { get; set; }  
        public Dictionary<int, Driver> Drivers { get; set; }

        public List<Driver> RecentDrivers { get; set; }
    }
}
