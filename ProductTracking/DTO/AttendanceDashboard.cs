using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProductTracking.DTO
{
    public class AttendanceDashboard
    {
        public int NineAM { get; set; }
        public int TenAM { get; set; }
        public int ElevenAM { get; set;}
        public int AfterElevenAM { get; set; }

        public AttendanceDashboard()
        {

        }
    }
}