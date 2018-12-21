using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace ProductTracking.Hubs
{
    public class LiveHub : Hub
    {
        public static string TAN_CURATED = "<script type='text/javascript'>tansCurated();todaysCuration();hourWise();avgTime();</script>";
        public static string TAN_ACCEPTED = "<script type='text/javascript'>qcAccepted();todaysQC();avgTime();</script>";
        public static string ONLINE = "<script type='text/javascript'>online();</script>";

        public void Curated()
        {
            Clients.All.curated(TAN_CURATED);
        }

        public void Accepted()
        {
            Clients.All.accepted(TAN_ACCEPTED);
        }

        public void Online()
        {
            Clients.All.online(ONLINE);
        }
        public void Progress()
        {
            Clients.All.online("Processing . .");
        }
        public void Reload()
        {
            Clients.All.reload();
        }
        public void Notification(string msg)
        {
            Clients.All.notification(msg);
        }
    }
}