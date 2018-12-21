using ProductTracking.Models;
using System;
using System.Web.Mvc;
using Microsoft.AspNet.SignalR;
using ProductTracking.Hubs;
using System.Net;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ProductTracking.DTO;
using System.Data.Entity.Core.Objects;
using System.Data;

namespace ProductTracking.Controllers
{
    public class HomeController : Controller
    {
        private static readonly int MAX_REPORT_DAYS = 10;
        private static readonly int MIN_HOUR = 8;
        private static readonly int MAX_HOUR = 11;
        private static readonly float STEP = 0.15f;

        public ActionResult Index()
        {
            using (var ctx = new ApplicationDbContext())
            {
                int totalEmployees = ctx.Database.SqlQuery<Int32>("SELECT DISTINCT COUNT(UserID) FROM [Mx_VEW_UserDetails] WHERE ActiveFlag=1").FirstOrDefault();
                string TodayDate = DateTime.Now.ToString("yyyy/MM/dd");
                int totalAttendants = ctx.Database.SqlQuery<Int32>(new QueryStrings().GetPresentCount(TodayDate.Replace('-','/'))).FirstOrDefault();
                int Totalabsentees = totalEmployees - totalAttendants;
                float Percentage = ((float)totalAttendants / (float)totalEmployees) * 100;
                ViewBag.totalEmp = totalEmployees;
                ViewBag.totalAttendants = totalAttendants;
                ViewBag.PL = Totalabsentees;
                ViewBag.UPL = Math.Round((Decimal)Percentage, 2, MidpointRounding.AwayFromZero) +"%";
                ViewBag.CurrentDate = DateTime.Now.ToString("dd-MMM-yyyy");
            }
            return View();
        }

        public ActionResult Reload()
        {
            var context = GlobalHost.ConnectionManager.GetHubContext<LiveHub>();
            context.Clients.All.reload();
            return Content("SUCCCESS");
        }

        public ActionResult PastFewDaysAttendance()
        {
            ArrayList pastFewDaysCount = new ArrayList();
            ArrayList series = new ArrayList();
            ArrayList ticks = new ArrayList();
            DateTime today = DateTime.Now;

            DateTime pastDay = DateTime.Now.AddDays(-MAX_REPORT_DAYS);
            Random rnd = new Random();

            for (int i = MAX_REPORT_DAYS; i >= 0; i--)
            {
                DateTime beforeDate = DateTime.Now.AddDays(-i).Date;
                int count = rnd.Next(250, 600);
                series.Add(count);
                ticks.Add(beforeDate.ToString("dd-MM"));
            }
            pastFewDaysCount.Add(series);
            pastFewDaysCount.Add(ticks);
            return Json(pastFewDaysCount, JsonRequestBehavior.AllowGet);
        }

        public ActionResult HourWise()
        {
           
            ArrayList hourWiseData = new ArrayList();
            ArrayList series = new ArrayList();

            ArrayList ticks = new ArrayList();
            DateTime today = DateTime.Now;
            for (float i = MIN_HOUR; i <= MAX_HOUR; i += STEP)
                ticks.Add(i.ToString());

            var seriesMeta = new ArrayList();

            foreach (var date in new List<DateTime> { DateTime.Now })
            {
                var dateSeries = HourWiseOfDate(date);
                series.Add(dateSeries);
                var label = date.ToString("dd-MM-yy");
                var sum = dateSeries.Sum();
                seriesMeta.Add(new { label = label + " - " + sum, highlighter = new { formatString = label } });
            }

            hourWiseData.Add(ticks);
            hourWiseData.Add(seriesMeta);
            hourWiseData.Add(series);

            return Json(hourWiseData, JsonRequestBehavior.AllowGet);
        }

        List<int> HourWiseOfDate(DateTime date)
        {
            List<int> series = new List<int>();


            Dictionary<int, int> hourWiseCount = new Dictionary<int, int>();
            Random rnd = new Random();

            for (float i = MIN_HOUR; i <= MAX_HOUR; i += STEP)
                series.Add(rnd.Next(50, 100));

            return series;
        }

        public ActionResult DashboardTrend()
        {
            ArrayList pastFewDaysCount = new ArrayList();
            DateTime today = DateTime.Now;

            DateTime pastDay = DateTime.Now.AddDays(-MAX_REPORT_DAYS);
            Random rnd = new Random();
            using (var ctx = new ApplicationDbContext())
            {

                for (int i = MAX_REPORT_DAYS; i >= 0; i--)
                {
                    DateTime beforeDate = DateTime.Now.AddDays(-i).Date;
                    var fromString = beforeDate.ToString("yyyy-MM-dd") + " 00:00:00.000";
                    var LateEndtimetime = beforeDate.ToString("yyyy-MM-dd") + " 23:59:59.000";

                    //var fromString = beforeDate.ToString("2018-02-22" + " 00:00:00.000");
                    //var LateEndtimetime = beforeDate.ToString("2018-02-22" + " 23:59:59.000");
                    int attendance = ctx.Database.SqlQuery<Int32>(new QueryStrings().GetTimes(fromString, LateEndtimetime)).FirstOrDefault();
                    //int attendance = rnd.Next(250, 600);
                    var att_time = beforeDate.ToString("dd-MMM-yyyy");
                    pastFewDaysCount.Add(new
                    {
                        att_time,
                        attendance
                    });
                }
            }
          
            return Json(pastFewDaysCount, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DashboardPie()
        {
            
            ArrayList OnTimestatistics = new ArrayList();
            using (var ctx = new ApplicationDbContext())
            {
                string[] data = new string[] { "Before 8:00 AM", " 8:00 AM to 8:30 AM", " 8:30 AM to 9:00 AM", " 9:00 AM to 9:30 AM", " 9:30 AM to 10:00 AM", " 10:00 AM to 10:30 AM", " After 11:00 AM " };

                List<Int32> Countdata = new List<Int32>();


                #region commented by Varaprasad
                //Countdata.Add(ctx.Database.SqlQuery<Int32>(new QueryStrings().GetTimeQuery(DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:01.000", DateTime.Now.ToString("yyyy-MM-dd") + " 08:00:00.000")).FirstOrDefault());
                //Countdata.Add(ctx.Database.SqlQuery<Int32>(new QueryStrings().GetTimeQuery(DateTime.Now.ToString("yyyy-MM-dd") + " 08:00:01.000", DateTime.Now.ToString("yyyy-MM-dd") + " 08:30:00.000")).FirstOrDefault());
                //Countdata.Add(ctx.Database.SqlQuery<Int32>(new QueryStrings().GetTimeQuery(DateTime.Now.ToString("yyyy-MM-dd") + " 08:30:01.000", DateTime.Now.ToString("yyyy-MM-dd") + " 09:00:00.000")).FirstOrDefault());
                //Countdata.Add(ctx.Database.SqlQuery<Int32>(new QueryStrings().GetTimeQuery(DateTime.Now.ToString("yyyy-MM-dd") + " 09:00:01.000", DateTime.Now.ToString("yyyy-MM-dd") + " 09:30:00.000")).FirstOrDefault());
                //Countdata.Add(ctx.Database.SqlQuery<Int32>(new QueryStrings().GetTimeQuery(DateTime.Now.ToString("yyyy-MM-dd") + " 09:30:01.000", DateTime.Now.ToString("yyyy-MM-dd") + " 10:00:00.000")).FirstOrDefault());
                //Countdata.Add(ctx.Database.SqlQuery<Int32>(new QueryStrings().GetTimeQuery(DateTime.Now.ToString("yyyy-MM-dd") + " 10:00:01.000", DateTime.Now.ToString("yyyy-MM-dd") + " 10:30:00.000")).FirstOrDefault());
                //Countdata.Add(ctx.Database.SqlQuery<Int32>(new QueryStrings().GetTimeQuery(DateTime.Now.ToString("yyyy-MM-dd") + " 10:30:01.000", DateTime.Now.ToString("yyyy-MM-dd") + " 11:00:00.000")).FirstOrDefault());
                //Countdata.Add(ctx.Database.SqlQuery<Int32>(new QueryStrings().GetTimeQuery(DateTime.Now.ToString("yyyy-MM-dd") + " 11:00:01.000", DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00.000")).FirstOrDefault());


                //Countdata.Add(ctx.Database.SqlQuery<Int32>(new QueryStrings().GetTimes("2018-02-22" + " 00:00:00.000", DateTime.Now.ToString("yyyy-MM-dd") + " 08:00:00.000")).FirstOrDefault());
                //Countdata.Add(ctx.Database.SqlQuery<Int32>(new QueryStrings().GetTimes("2018-02-22" + " 08:00:01.000", DateTime.Now.ToString("yyyy-MM-dd") + " 08:30:00.000")).FirstOrDefault());
                //Countdata.Add(ctx.Database.SqlQuery<Int32>(new QueryStrings().GetTimes("2018-02-22" + " 08:30:01.000", DateTime.Now.ToString("yyyy-MM-dd") + " 09:00:00.000")).FirstOrDefault());
                //Countdata.Add(ctx.Database.SqlQuery<Int32>(new QueryStrings().GetTimes("2018-02-22" + " 09:00:01.000", DateTime.Now.ToString("yyyy-MM-dd") + " 09:30:00.000")).FirstOrDefault());
                //Countdata.Add(ctx.Database.SqlQuery<Int32>(new QueryStrings().GetTimes("2018-02-22" + " 09:30:01.000", DateTime.Now.ToString("yyyy-MM-dd") + " 10:00:00.000")).FirstOrDefault());
                //Countdata.Add(ctx.Database.SqlQuery<Int32>(new QueryStrings().GetTimes("2018-02-22" + " 10:00:01.000", DateTime.Now.ToString("yyyy-MM-dd") + " 10:30:00.000")).FirstOrDefault());
                //Countdata.Add(ctx.Database.SqlQuery<Int32>(new QueryStrings().GetTimes("2018-02-22" + " 10:30:01.000", DateTime.Now.ToString("yyyy-MM-dd") + " 11:00:00.000")).FirstOrDefault());
                //Countdata.Add(ctx.Database.SqlQuery<Int32>(new QueryStrings().GetTimes("2018-02-22" + " 11:00:01.000", DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59.000")).FirstOrDefault());
                #endregion


                Countdata.Add(ctx.Database.SqlQuery<Int32>(new QueryStrings().GetDateTimeQuery(DateTime.Now.ToString("dd-MM-yyyy"),  "00:00:00",  "08:00:00", "00:00:00")).FirstOrDefault());
                Countdata.Add(ctx.Database.SqlQuery<Int32>(new QueryStrings().GetDateTimeQuery(DateTime.Now.ToString("dd-MM-yyyy"),  "08:00:01",  "08:30:00", "08:00:00")).FirstOrDefault());
                Countdata.Add(ctx.Database.SqlQuery<Int32>(new QueryStrings().GetDateTimeQuery(DateTime.Now.ToString("dd-MM-yyyy"),  "08:30:01",  "09:00:00", "08:30:00")).FirstOrDefault());
                Countdata.Add(ctx.Database.SqlQuery<Int32>(new QueryStrings().GetDateTimeQuery(DateTime.Now.ToString("dd-MM-yyyy"),  "09:00:01",  "09:30:00", "09:00:00")).FirstOrDefault());
                Countdata.Add(ctx.Database.SqlQuery<Int32>(new QueryStrings().GetDateTimeQuery(DateTime.Now.ToString("dd-MM-yyyy"),  "09:30:01",  "10:00:00", "09:30:00")).FirstOrDefault());
                Countdata.Add(ctx.Database.SqlQuery<Int32>(new QueryStrings().GetDateTimeQuery(DateTime.Now.ToString("dd-MM-yyyy"),  "10:00:01",  "10:30:00", "10:00:00")).FirstOrDefault());
                Countdata.Add(ctx.Database.SqlQuery<Int32>(new QueryStrings().GetDateTimeQuery(DateTime.Now.ToString("dd-MM-yyyy"),  "10:30:01",  "11:00:00", "10:30:00")).FirstOrDefault());
                Countdata.Add(ctx.Database.SqlQuery<Int32>(new QueryStrings().GetDateTimeQuery(DateTime.Now.ToString("dd-MM-yyyy"),  "11:00:01",  "23:59:59", "11:00:00")).FirstOrDefault());


                int[] count = Countdata.ToArray();


                for (int i = 0; i < data.Count(); i++)
                {

                    string time_cat = data[i].ToString();
                    string emp_num = count[i].ToString();
                    OnTimestatistics.Add(new
                    {
                        time_cat,
                        emp_num
                    });
                }
            }

            return Json(OnTimestatistics, JsonRequestBehavior.AllowGet);
        }
       
        public ActionResult DashboardLeaveStatistics()
        {
            
            ArrayList Leavestatistics = new ArrayList();

            string[] data= new string[] { "Paid Leave", "On duty", "Compensatory", "Paternity Leave", "Optional Holiday" };

            string[] count = new string[] { "100", "29.16", "24.10", "14.79", "5.03" };

            for (int i =0; i < data.Count(); i++)
            {

                string time_cat = data[i].ToString();
                string emp_num = count[i].ToString();
                Leavestatistics.Add(new
                {
                    time_cat,
                    emp_num
                });
            }

            return Json(Leavestatistics, JsonRequestBehavior.AllowGet);

        }

        public ActionResult PlannedvsUnlpanned()
        {
          

            ArrayList plannedvsunplanned = new ArrayList();

            string[] months = new string[] {  "Jan","Feb","Mar","Apr","May","Jun","Jul","Aug","Sep","Oct","Nov","Dec"};
            int[] planned = new int[] { 59, 71, 106, 129, 144, 176, 135, 148, 216, 194, 95, 54 };
            int[] unplanned = new int[] { 83, 78, 98, 93, 106, 84, 105, 104, 91, 83, 106, 92 };
            plannedvsunplanned.Add(months);
            plannedvsunplanned.Add(planned);
            plannedvsunplanned.Add(unplanned);

            return Json(plannedvsunplanned, JsonRequestBehavior.AllowGet);


        }

        public ActionResult Inpunch()
        {
           DataTable hourwiseData = new QueryStrings().GetInpunhData(DateTime.Now.ToString("yyyy-MM-dd"));
            //DataTable hourwiseData = new QueryStrings().GetInpunhData("2018-02-22");
            ArrayList plannedvsunplanned = new ArrayList();

            var Time = Enumerable.Range(0, 24).ToArray();
            List<int> Data = new List<int>();


            for (int hour = 0; hour < 24; hour++)
            {
               // plannedvsunplanned.Add(hour.ToString());
                Data.Add(GetHourValue(hourwiseData, hour));
                
            }
            plannedvsunplanned.Add(Time);
            plannedvsunplanned.Add(Data.ToArray());

            return Json(plannedvsunplanned, JsonRequestBehavior.AllowGet);
                       
        }

        private int GetHourValue(DataTable hourwiseData,int hour)
        {
            try
            {
                var hourValuevalue = (from lst in hourwiseData.AsEnumerable()
                                      where lst.Field<int>("OnHour").Equals(hour)
                                      select lst.Field<int>("Totals")).FirstOrDefault();
                return hourValuevalue.Equals(null) ? 0 : hourValuevalue;
            }
            catch (Exception ex)
            {

                return 0;
                
            }
        }

        public ActionResult OutPunch()
        {
            DataTable hourwiseData = new QueryStrings().GetOutPunchData(DateTime.Now.ToString("yyyy-MM-dd"));
           // DataTable hourwiseData = new QueryStrings().GetOutPunchData("2018-02-22");


            ArrayList plannedvsunplanned = new ArrayList();

            var Time = Enumerable.Range(0, 24).ToArray();
            List<int> Data = new List<int>();


            for (int hour = 0; hour < 24; hour++)
            {
                // plannedvsunplanned.Add(hour.ToString());
                Data.Add(GetHourValue(hourwiseData, hour));

            }
            plannedvsunplanned.Add(Time);
            plannedvsunplanned.Add(Data.ToArray());

            return Json(plannedvsunplanned, JsonRequestBehavior.AllowGet);


        }

    }

    

}