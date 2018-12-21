using ProductTracking.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProductTracking.Controllers
{
    public class AnalyticsController : Controller
    {
        // GET: Analytics
        public ActionResult Index()
        {

            return View();
        }
        public ActionResult HabitesDashboard()
        {

            using (var ctx = new ApplicationDbContext())
            {
                var fromString = DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00.000";
                var toStrings = DateTime.Now.ToString("yyyy-MM-dd") + " 09:00:00.000";
                var fromGraceString = DateTime.Now.ToString("yyyy-MM-dd") + " 09:00:01.000";
                var GraceTime= DateTime.Now.ToString("yyyy-MM-dd") + " 09:30:00.000";
                var Latefromtime= DateTime.Now.ToString("yyyy-MM-dd") + " 09:30:01.000";
                var LateEndtimetime = DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:00.000";

              
                int totalEmployeesBeingontime = ctx.Database.SqlQuery<Int32>(new QueryStrings().GetDateTimeQuery(DateTime.Now.ToString("dd-MM-yyyy"), "00:00:00", "09:00:00", "00:00:00")).FirstOrDefault();
                ViewBag.OntimeCount = totalEmployeesBeingontime;
                
                int totalEmployeeslate = ctx.Database.SqlQuery<Int32>(new QueryStrings().GetDateTimeQuery(DateTime.Now.ToString("dd-MM-yyyy"), "09:00:01", "09:30:00", "09:00:00")).FirstOrDefault();
                ViewBag.latetimeCount = totalEmployeeslate;

                int GraceTimePunch = ctx.Database.SqlQuery<Int32>(new QueryStrings().GetDateTimeQuery(DateTime.Now.ToString("dd-MM-yyyy"), "09:30:01", "23:59:59", "09:30:00")).FirstOrDefault();
                ViewBag.Gracetime = GraceTimePunch;

                string TodayDate = DateTime.Now.ToString("yyyy/MM/dd");
                ViewBag.TotalEmployees = ctx.Database.SqlQuery<Int32>(new QueryStrings().GetPresentCount(TodayDate.Replace('-', '/'))).FirstOrDefault();
               


            }
            return View();
        }
        public ActionResult HabitesDashboardPie()
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



                //Countdata.Add(ctx.Database.SqlQuery<Int32>(new QueryStrings().GetTimeQuery("2018-02-22" + " 00:00:00.000", DateTime.Now.ToString("yyyy-MM-dd") + " 08:00:00.000")).FirstOrDefault());
                //Countdata.Add(ctx.Database.SqlQuery<Int32>(new QueryStrings().GetTimeQuery("2018-02-22" + " 08:00:01.000", DateTime.Now.ToString("yyyy-MM-dd") + " 08:30:00.000")).FirstOrDefault());
                //Countdata.Add(ctx.Database.SqlQuery<Int32>(new QueryStrings().GetTimeQuery("2018-02-22" + " 08:30:01.000", DateTime.Now.ToString("yyyy-MM-dd") + " 09:00:00.000")).FirstOrDefault());
                //Countdata.Add(ctx.Database.SqlQuery<Int32>(new QueryStrings().GetTimeQuery("2018-02-22" + " 09:00:01.000", DateTime.Now.ToString("yyyy-MM-dd") + " 09:30:00.000")).FirstOrDefault());
                //Countdata.Add(ctx.Database.SqlQuery<Int32>(new QueryStrings().GetTimeQuery("2018-02-22" + " 09:30:01.000", DateTime.Now.ToString("yyyy-MM-dd") + " 10:00:00.000")).FirstOrDefault());
                //Countdata.Add(ctx.Database.SqlQuery<Int32>(new QueryStrings().GetTimeQuery("2018-02-22" + " 10:00:01.000", DateTime.Now.ToString("yyyy-MM-dd") + " 10:30:00.000")).FirstOrDefault());
                //Countdata.Add(ctx.Database.SqlQuery<Int32>(new QueryStrings().GetTimeQuery("2018-02-22" + " 10:30:01.000", DateTime.Now.ToString("yyyy-MM-dd") + " 11:00:00.000")).FirstOrDefault());
                //Countdata.Add(ctx.Database.SqlQuery<Int32>(new QueryStrings().GetTimeQuery("2018-02-22" + " 11:00:01.000", DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59.000")).FirstOrDefault());
                #endregion
                Countdata.Add(ctx.Database.SqlQuery<Int32>(new QueryStrings().GetDateTimeQuery(DateTime.Now.ToString("dd-MM-yyyy"), "00:00:00", "08:00:00", "00:00:00")).FirstOrDefault());
                Countdata.Add(ctx.Database.SqlQuery<Int32>(new QueryStrings().GetDateTimeQuery(DateTime.Now.ToString("dd-MM-yyyy"), "08:00:01", "08:30:00", "08:00:00")).FirstOrDefault());
                Countdata.Add(ctx.Database.SqlQuery<Int32>(new QueryStrings().GetDateTimeQuery(DateTime.Now.ToString("dd-MM-yyyy"), "08:30:01", "09:00:00", "08:30:00")).FirstOrDefault());
                Countdata.Add(ctx.Database.SqlQuery<Int32>(new QueryStrings().GetDateTimeQuery(DateTime.Now.ToString("dd-MM-yyyy"), "09:00:01", "09:30:00", "09:00:00")).FirstOrDefault());
                Countdata.Add(ctx.Database.SqlQuery<Int32>(new QueryStrings().GetDateTimeQuery(DateTime.Now.ToString("dd-MM-yyyy"), "09:30:01", "10:00:00", "09:30:00")).FirstOrDefault());
                Countdata.Add(ctx.Database.SqlQuery<Int32>(new QueryStrings().GetDateTimeQuery(DateTime.Now.ToString("dd-MM-yyyy"), "10:00:01", "10:30:00", "10:00:00")).FirstOrDefault());
                Countdata.Add(ctx.Database.SqlQuery<Int32>(new QueryStrings().GetDateTimeQuery(DateTime.Now.ToString("dd-MM-yyyy"), "10:30:01", "11:00:00", "10:30:00")).FirstOrDefault());
                Countdata.Add(ctx.Database.SqlQuery<Int32>(new QueryStrings().GetDateTimeQuery(DateTime.Now.ToString("dd-MM-yyyy"), "11:00:01", "23:59:59", "11:00:00")).FirstOrDefault());


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
        public ActionResult HabitesDashboardOntimevsDelay()
        {
            ArrayList OntimevsDelay = new ArrayList();
            string[] months = new string[] { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
            int[] planned = new int[] { 19, 71, 106, 129, 144, 176, 135, 148, 216, 194, 95, 54 };
            int[] unplanned = new int[] { 83, 78, 98, 93, 106, 84, 105, 104, 91, 83, 106, 92 };
            OntimevsDelay.Add(months);
            OntimevsDelay.Add(planned);
            OntimevsDelay.Add(unplanned);
            return Json(OntimevsDelay, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EffortDashboard()
        {
            return View();
        }
        public ActionResult EffortDashboardPie()
        {
            ArrayList Effortstatistics = new ArrayList();

            string[] data = new string[] { "Employee Working Hours", "Employee Leave hours", "Employee Work from Home Hours", "2 hours permission Include Hours" };

            string[] count = new string[] { "4000", "200", "500", "50" };

            for (int i = 0; i < data.Count(); i++)
            {

                string time_cat = data[i].ToString();
                string emp_num = count[i].ToString();
                Effortstatistics.Add(new
                {
                    time_cat,
                    emp_num
                });
            }

            return Json(Effortstatistics, JsonRequestBehavior.AllowGet);
        }
        public ActionResult EffortDashboardWorkingVsLeavehours()
        {
            ArrayList WorkingVsLeavehours = new ArrayList();
            string[] months = new string[] { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
            int[] planned = new int[] { 4900, 7100, 1006, 1029, 1044, 1076, 1305, 2148, 2106, 3194, 2395, 4554 };
            int[] unplanned = new int[] { 83, 78, 98, 93, 106, 84, 105, 104, 691, 483, 306, 502 };
            WorkingVsLeavehours.Add(months);
            WorkingVsLeavehours.Add(planned);
            WorkingVsLeavehours.Add(unplanned);
            return Json(WorkingVsLeavehours, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ComparisonDashboard()
        {
            return View();
        }
        public ActionResult OnTimeComparisonDashboardBUs()
        {
            ArrayList OnTimestatistics = new ArrayList();
            string[] time = new string[] { "7:00", "7:30", "8:00", "8:30", "9:00", "9:30", "10:00", "10:30", "11:00", "11:30", "12:00", "13:00" };
            double[] data = new double[] { 10, 53, 5, 6, 9, 11, 13, 15, 20 };
            double[] data1 = new double[] { 24, 24, 29, 29, 32, 32, 38, 40 };
            double[] data2 = new double[] { 14, 17, 16, 19, 25, 24, 32, 39 };
            int?[] data3 = new int?[] { null, null, 7, 1, 1, 2, 3, 34 };
            OnTimestatistics.Add(time);
            OnTimestatistics.Add(data);
            OnTimestatistics.Add(data1);
            OnTimestatistics.Add(data2);
            OnTimestatistics.Add(data3);
            return Json(OnTimestatistics, JsonRequestBehavior.AllowGet);
        }
        public ActionResult HoursComparisonDashboardBUs()
        {
            ArrayList OnTimestatistics = new ArrayList();
            string[] time = new string[] { "7:00", "7:30", "8:00", "8:30", "9:00", "9:30", "10:00", "10:30", "11:00", "11:30", "12:00", "13:00" };
            double[] data = new double[] { 10, 53, 5, 6, 9, 11, 13, 15, 20 };
            double[] data1 = new double[] { 24, 24, 29, 29, 32, 32, 38, 40 };
            double[] data2 = new double[] { 14, 17, 16, 19, 25, 24, 32, 39 };
            int?[] data3 = new int?[] { null, null, 7, 1, 1, 2, 3, 34 };
            OnTimestatistics.Add(time);
            OnTimestatistics.Add(data);
            OnTimestatistics.Add(data1);
            OnTimestatistics.Add(data2);
            OnTimestatistics.Add(data3);
            return Json(OnTimestatistics, JsonRequestBehavior.AllowGet);
        }
        public ActionResult OrganizationEffortDashboardBUs()
        {
            ArrayList WorkingVsLeavehours = new ArrayList();
            string[] months = new string[] { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
            int[] bu1 = new int[] { 4900, 7100, 1006, 1029, 1044, 1076, 1305, 2148, 2106, 3194, 2395, 4554 };
            int[] bu2 = new int[] { 83, 78, 998, 903, 106, 84, 1805, 104, 6091, 483, 306, 502 };
            int[] bu3 = new int[] { 2900, 7100, 1006, 1029, 1544, 1076, 1305, 2908, 2106, 3194, 2395, 4554 };
            int[] bu4 = new int[] { 4900, 7100, 1706, 1029, 1044, 1076, 1305, 2148, 2106, 3194, 2395, 4554 };
            int[] bu5 = new int[] { 4970, 7100, 1006, 1029, 1544, 1076, 1305, 2148, 2106, 3194, 2395, 4554 };
            int[] bu6 = new int[] { 4900, 7100, 1686, 1029, 1044, 1076, 1305, 2148, 2106, 3194, 2395, 4554 };
            WorkingVsLeavehours.Add(months);
            WorkingVsLeavehours.Add(bu1);
            WorkingVsLeavehours.Add(bu2);
            WorkingVsLeavehours.Add(bu3);
            WorkingVsLeavehours.Add(bu4);
            WorkingVsLeavehours.Add(bu5);
            WorkingVsLeavehours.Add(bu6);
            return Json(WorkingVsLeavehours, JsonRequestBehavior.AllowGet);
        }
        public ActionResult TotalLeaveDashboardBUs()
        {
            ArrayList WorkingVsLeavehours = new ArrayList();
            string[] months = new string[] { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
            int[] bu1 = new int[] { 4900, 7100, 1006, 1029, 1044, 1076, 1305, 2148, 2106, 3194, 2395, 4554 };
            int[] bu2 = new int[] { 83, 78, 998, 903, 106, 84, 1805, 104, 6091, 483, 306, 502 };
            int[] bu3 = new int[] { 2900, 7100, 1006, 1029, 1544, 1076, 1305, 2908, 2106, 3194, 2395, 4554 };
            int[] bu4 = new int[] { 4900, 7100, 1706, 1029, 1044, 1076, 1305, 2148, 2106, 3194, 2395, 4554 };
            int[] bu5 = new int[] { 4970, 7100, 1006, 1029, 1544, 1076, 1305, 2148, 2106, 3194, 2395, 4554 };
            int[] bu6 = new int[] { 4900, 7100, 1686, 1029, 1044, 1076, 1305, 2148, 2106, 3194, 2395, 4554 };
            WorkingVsLeavehours.Add(months);
            WorkingVsLeavehours.Add(bu1);
            WorkingVsLeavehours.Add(bu2);
            WorkingVsLeavehours.Add(bu3);
            WorkingVsLeavehours.Add(bu4);
            WorkingVsLeavehours.Add(bu5);
            WorkingVsLeavehours.Add(bu6);
            return Json(WorkingVsLeavehours, JsonRequestBehavior.AllowGet);

        }
        public ActionResult OrganizationEffortGroupbyLevels()
        {
            ArrayList WorkingVsLeavehours = new ArrayList();
            string[] months = new string[] { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
            int[] bu1 = new int[] { 4900, 7100, 1006, 1029, 1044, 1076, 1305, 2148, 2106, 3194, 2395, 4554 };
            int[] bu2 = new int[] { 83, 78, 998, 903, 106, 84, 1805, 104, 6091, 483, 306, 502 };
            int[] bu3 = new int[] { 2900, 7100, 1006, 1029, 1544, 1076, 1305, 2908, 2106, 3194, 2395, 4554 };
            int[] bu4 = new int[] { 4900, 7100, 1706, 1029, 1044, 1076, 1305, 2148, 2106, 3194, 2395, 4554 };
            int[] bu5 = new int[] { 4970, 7100, 1006, 1029, 1544, 1076, 1305, 2148, 2106, 3194, 2395, 4554 };
            int[] bu6 = new int[] { 4900, 7100, 1686, 1029, 1044, 1076, 1305, 2148, 2106, 3194, 2395, 4554 };
            WorkingVsLeavehours.Add(months);
            WorkingVsLeavehours.Add(bu1);
            WorkingVsLeavehours.Add(bu2);
            WorkingVsLeavehours.Add(bu3);
            WorkingVsLeavehours.Add(bu4);
            WorkingVsLeavehours.Add(bu5);
            WorkingVsLeavehours.Add(bu6);
            return Json(WorkingVsLeavehours, JsonRequestBehavior.AllowGet);

        }
        public ActionResult TotalLeaveGroupbyLevels()
        {
            ArrayList WorkingVsLeavehours = new ArrayList();
            string[] months = new string[] { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
            int[] bu1 = new int[] { 4900, 7100, 1006, 1029, 1044, 1076, 1305, 2148, 2106, 3194, 2395, 4554 };
            int[] bu2 = new int[] { 83, 78, 998, 903, 106, 84, 1805, 104, 6091, 483, 306, 502 };
            int[] bu3 = new int[] { 2900, 7100, 1006, 1029, 1544, 1076, 1305, 2908, 2106, 3194, 2395, 4554 };
            int[] bu4 = new int[] { 4900, 7100, 1706, 1029, 1044, 1076, 1305, 2148, 2106, 3194, 2395, 4554 };
            int[] bu5 = new int[] { 4970, 7100, 1006, 1029, 1544, 1076, 1305, 2148, 2106, 3194, 2395, 4554 };
            int[] bu6 = new int[] { 4900, 7100, 1686, 1029, 1044, 1076, 1305, 2148, 2106, 3194, 2395, 4554 };
            WorkingVsLeavehours.Add(months);
            WorkingVsLeavehours.Add(bu1);
            WorkingVsLeavehours.Add(bu2);
            WorkingVsLeavehours.Add(bu3);
            WorkingVsLeavehours.Add(bu4);
            WorkingVsLeavehours.Add(bu5);
            WorkingVsLeavehours.Add(bu6);
            return Json(WorkingVsLeavehours, JsonRequestBehavior.AllowGet);

        }
        public ActionResult TotalWFHEffortvsBUs()
        {
            ArrayList WorkingVsLeavehours = new ArrayList();
            string[] months = new string[] { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
            int[] bu1 = new int[] { 4900, 7100, 1006, 1029, 1044, 1076, 1305, 2148, 2106, 3194, 2395, 4554 };
            int[] bu2 = new int[] { 83, 78, 998, 903, 106, 84, 1805, 104, 6091, 483, 306, 502 };
            int[] bu3 = new int[] { 2900, 7100, 1006, 1029, 1544, 1076, 1305, 2908, 2106, 3194, 2395, 4554 };
            int[] bu4 = new int[] { 4900, 7100, 1706, 1029, 1044, 1076, 1305, 2148, 2106, 3194, 2395, 4554 };
            int[] bu5 = new int[] { 4970, 7100, 1006, 1029, 1544, 1076, 1305, 2148, 2106, 3194, 2395, 4554 };
            int[] bu6 = new int[] { 4900, 7100, 1686, 1029, 1044, 1076, 1305, 2148, 2106, 3194, 2395, 4554 };
            WorkingVsLeavehours.Add(months);
            WorkingVsLeavehours.Add(bu1);
            WorkingVsLeavehours.Add(bu2);
            WorkingVsLeavehours.Add(bu3);
            WorkingVsLeavehours.Add(bu4);
            WorkingVsLeavehours.Add(bu5);
            WorkingVsLeavehours.Add(bu6);
            return Json(WorkingVsLeavehours, JsonRequestBehavior.AllowGet);

        }
        public ActionResult TotalWFHEffortvsLevels()
        {
            ArrayList WorkingVsLeavehours = new ArrayList();
            string[] months = new string[] { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
            int[] bu1 = new int[] { 4900, 7100, 1006, 1029, 1044, 1076, 1305, 2148, 2106, 3194, 2395, 4554 };
            int[] bu2 = new int[] { 83, 78, 998, 903, 106, 84, 1805, 104, 6091, 483, 306, 502 };
            int[] bu3 = new int[] { 2900, 7100, 1006, 1029, 1544, 1076, 1305, 2908, 2106, 3194, 2395, 4554 };
            int[] bu4 = new int[] { 4900, 7100, 1706, 1029, 1044, 1076, 1305, 2148, 2106, 3194, 2395, 4554 };
            int[] bu5 = new int[] { 4970, 7100, 1006, 1029, 1544, 1076, 1305, 2148, 2106, 3194, 2395, 4554 };
            int[] bu6 = new int[] { 4900, 7100, 1686, 1029, 1044, 1076, 1305, 2148, 2106, 3194, 2395, 4554 };
            WorkingVsLeavehours.Add(months);
            WorkingVsLeavehours.Add(bu1);
            WorkingVsLeavehours.Add(bu2);
            WorkingVsLeavehours.Add(bu3);
            WorkingVsLeavehours.Add(bu4);
            WorkingVsLeavehours.Add(bu5);
            WorkingVsLeavehours.Add(bu6);
            return Json(WorkingVsLeavehours, JsonRequestBehavior.AllowGet);

        }



        public ActionResult LeaveEncashDashboard()
        {
            return View();
        }
        public ActionResult LeaveEncashDashboardPie()
        {
            ArrayList LeaveEncashDashboard = new ArrayList();
            string[] data = new string[] { "Pharma Analytics", "Chemistry Data Services", "Clinical Data Services", "Finance and Legal" };
            string[] count = new string[] { "4000", "200", "500", "50" };
            for (int i = 0; i < data.Count(); i++)
            {
                string time_cat = data[i].ToString();
                string emp_num = count[i].ToString();
                LeaveEncashDashboard.Add(new
                {
                    time_cat,
                    emp_num
                });
            }
            return Json(LeaveEncashDashboard, JsonRequestBehavior.AllowGet);
        }
        public ActionResult LeaveEncashDashboardEncashmentTrend()
        {
            ArrayList LeaveEncashDashboar = new ArrayList();
            string[] months = new string[] { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
            int[] planned = new int[] { 90, 2, 3, 7, 8, 9, 9, 9, 8, 70, 80, 90 };
            int[] unplanned = new int[] { 900, 40, 120, 140, 180, 180, 200, 220, 250, 100, 1200, 1800 };
            LeaveEncashDashboar.Add(months);
            LeaveEncashDashboar.Add(planned);
            LeaveEncashDashboar.Add(unplanned);
            return Json(LeaveEncashDashboar, JsonRequestBehavior.AllowGet);
        }

        public ActionResult MyEmployeeDashboard()
        {
            DataTable EmployeeNames = new QueryStrings().GetUserNames();
            var data= from dt in EmployeeNames.AsEnumerable()

                      select dt.Field<string>("users");

            ViewBag.Names = data.ToList();
            return View();
        }
        public ActionResult MyEmployeeDashboardPie()
        {
            ArrayList EmployeeDashboardstatistics = new ArrayList();

            string[] data = new string[] { "Paid Leave", "On Duty", "Compensatory", "Paternity Leave", "Optional Holiday"};

            string[] count = new string[] { "18", "20", "10", "0", "1"};

            for (int i = 0; i < data.Count(); i++)
            {

                string time_cat = data[i].ToString();
                string emp_num = count[i].ToString();
                EmployeeDashboardstatistics.Add(new
                {
                    time_cat,
                    emp_num
                });
            }

            return Json(EmployeeDashboardstatistics, JsonRequestBehavior.AllowGet);
        }
        public ActionResult MyEmployeeDashboardBeingOntime(string Date, string Name)
        {
            string FDate = Date.Split(':').First();
            string LastDate = Date.Split(':').Last();
            string EMpId = Name.Split('(').Last().Split(')').First();


            int UserId = Convert.ToInt32(EMpId);// 60814;
            DateTime Fromdate = Convert.ToDateTime(FDate);
            DateTime ToDate = Convert.ToDateTime(LastDate);
            DataTable BeingOntimeData = new QueryStrings().GetInOutTotalPunchData(FDate, LastDate, UserId);

            ArrayList EmployeeDashboardontimestatistics = new ArrayList();

            List<DateTime> DateData = new List<DateTime>();

            List<string> CountData = new List<string>();

            for (DateTime date = Fromdate; date <= ToDate; date = date.AddDays(1.0))
            {
                DateData.Add(date);
            }
            for (DateTime date = DateData[0]; date <= DateData[DateData.Count - 1]; date = date.AddDays(1.0))
            {
                CountData.Add(GetTimePunchValue(BeingOntimeData, date));
            }
            for (int i = 0; i < CountData.Count(); i++)
            {

                string att_time = DateData[i].ToString(("dd-MMM-yy"));
                string attendance = CountData[i].ToString();
                EmployeeDashboardontimestatistics.Add(new
                {
                    att_time,
                    attendance
                });
            }

            return Json(EmployeeDashboardontimestatistics, JsonRequestBehavior.AllowGet);
        }

        private string GetTimePunchValue(DataTable beingOntimeData, DateTime date)
        {
            try
            {
                var hourValuevalue = (from lst in beingOntimeData.AsEnumerable()
                                      where lst.Field<DateTime>("Datadate").Equals(date)
                                      select lst.Field<TimeSpan>("Inpunch").ToString()).FirstOrDefault();
                return hourValuevalue.Equals(null) ? "00:00:00" : Convert.ToString(hourValuevalue);
            }
            catch (Exception ex)
            {

                return "00:00:00";

            }
        }

        public ActionResult MyEmployeeDashboardSpendingHours(string Date, string Name)
        {
            
            string FDate = Date.Split(':').First();
            string LastDate = Date.Split(':').Last();
            string EMpId = Name.Split('(').Last().Split(')').First();


            int UserId = Convert.ToInt32(EMpId);// 60814;
            DateTime Fromdate = Convert.ToDateTime(FDate);
            DateTime ToDate = Convert.ToDateTime(LastDate);
            DataTable BeingOntimeData = new QueryStrings().GetBeingOntimeData(FDate, LastDate, UserId);

            ArrayList EmployeeDashboardSpendingstatistics = new ArrayList();

            #region commneted
            //string[] time = new string[] { "7:00", "7:30", "8:00", "8:30", "9:00", "9:30", "10:00", "10:30", "11:00", "11:30", "12:00", "12:30", "13:00", };

            //int[] count = new int[] { 40, 10, 94, 272, 320, 10, 50, 2, 3, 7, 1, 2, 6 };

            // var time = BeingOntimeData.AsEnumerable().Select(r => r[1]).ToList();
            //  var count = BeingOntimeData.AsEnumerable().Select(r => r[2]).ToList();
            #endregion

            List<DateTime> DateData = new List<DateTime>();

            List<string> CountData = new List<string>();

            for (DateTime date = Fromdate; date <= ToDate; date = date.AddDays(1.0))
            {
                DateData.Add(date);
            }
            for (DateTime date = DateData[0]; date <= DateData[DateData.Count - 1]; date = date.AddDays(1.0))
            {
                CountData.Add(GetTimeValue(BeingOntimeData, date));
            }
            for (int i = 0; i < CountData.Count(); i++)
            {

                string att_time = DateData[i].ToString("dd-MMM-yy");
                string attendance = CountData[i].ToString();
                EmployeeDashboardSpendingstatistics.Add(new
                {
                    att_time,
                    attendance
                });
            }
            return Json(EmployeeDashboardSpendingstatistics, JsonRequestBehavior.AllowGet);
           
            
        }
        private string GetTimeValue(DataTable beingOntimeData, DateTime date)
        {
            try
            {
                var hourValuevalue = (from lst in beingOntimeData.AsEnumerable()
                                      where lst.Field<DateTime>("Datadate").Equals(date)
                                      select lst.Field<TimeSpan>("Datatime").ToString()).FirstOrDefault();
                return hourValuevalue.Equals(null) ? "00:00:00" :Convert.ToString(hourValuevalue);
            }
            catch (Exception ex)
            {

                return "00:00:00";

            }
        }
        public ActionResult MyEmployeeDashboardSpendingHoursEmployeeBUOrganization(string Date, string Name)
        {

            string FDate = Date.Split(':').First();
            string LastDate = Date.Split(':').Last();
            string EMpId = Name.Split('(').Last().Split(')').First();


            int UserId = Convert.ToInt32(EMpId);// 60814;
            DateTime Fromdate = Convert.ToDateTime(FDate);
            DateTime ToDate = Convert.ToDateTime(LastDate);
            DataTable BeingOntimeData = new QueryStrings().GetInOutTotalPunchData(FDate, LastDate, UserId);

            List<DateTime> DateData = new List<DateTime>();
            List<string> Datestringdata = new List<string>();
            List<double> FirstPuch = new List<double>();
            List<double> LastPuch = new List<double>();
            List<double> Totalhoursspent = new List<double>();

            for (DateTime date = Fromdate; date <= ToDate; date = date.AddDays(1.0))
            {
                DateData.Add(date);
                Datestringdata.Add(date.ToString("dd-MMM")); 
            }
            for (DateTime date = DateData[0]; date <= DateData[DateData.Count - 1]; date = date.AddDays(1.0))
            {
                string firstpunch= GetFirstPunchTimeValue(BeingOntimeData, date);
                var data = firstpunch.Split(':');
                string time = data[0] + "." + data[1];
                FirstPuch.Add(Convert.ToDouble(time));
                    
            }
            for (DateTime date = DateData[0]; date <= DateData[DateData.Count - 1]; date = date.AddDays(1.0))
            {
                string firstpunch=GetLastPuchTimeValue(BeingOntimeData, date);
                var data = firstpunch.Split(':');
                string time = data[0] + "." + data[1];
                LastPuch.Add(Convert.ToDouble(time));
            }
            for (DateTime date = DateData[0]; date <= DateData[DateData.Count - 1]; date = date.AddDays(1.0))
            {
                string firstpunch = GetTotalhoursTimeValue(BeingOntimeData, date);
                var data = firstpunch.Split(':');
                string time = data[0] + "." + data[1];
                Totalhoursspent.Add(Convert.ToDouble(time));
                
            }



            ArrayList EmployeeDashboardSpendingstatistics = new ArrayList();
            //string[] categories = new string[] { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
            //double[] Selecteduserdata = new double[] { 8.0, 6.9, 9.5, 8.5, 10.4, 9.5, 9.2, 8.5, 9.3, 7.3, 6.9, 9.6 };
            //double[] BUdata = new double[] { 8.9, 2.2, 5.7, 8.5, 11.9, 15.2, 17.0, 12.6, 12.2, 10.3, 6.6, 4.8 };
            //double[] Organizationdata = new double[] { 8.9, 8.2, 5.7, 8.5, 7.9, 8.2, 17.0, 9.6, 10.2, 9.3, 8.6, 10.8 };
            EmployeeDashboardSpendingstatistics.Add(Datestringdata.ToArray());
            EmployeeDashboardSpendingstatistics.Add(FirstPuch.ToArray());
            EmployeeDashboardSpendingstatistics.Add(LastPuch.ToArray());
            EmployeeDashboardSpendingstatistics.Add(Totalhoursspent.ToArray());
            return Json(EmployeeDashboardSpendingstatistics, JsonRequestBehavior.AllowGet);
        }

        private string GetFirstPunchTimeValue(DataTable beingOntimeData, DateTime date)
        {

            try
            {
                var hourValuevalue = (from lst in beingOntimeData.AsEnumerable()
                                      where lst.Field<DateTime>("Datadate").Equals(date)
                                      select lst.Field<TimeSpan>("Inpunch").ToString()).FirstOrDefault();
                return hourValuevalue.Equals(null) ? "00:00:00" : Convert.ToString(hourValuevalue);
            }
            catch (Exception ex)
            {

                return "00:00:00";

            }
        }

        private string GetTotalhoursTimeValue(DataTable beingOntimeData, DateTime date)
        {
            try
            {
                var hourValuevalue = (from lst in beingOntimeData.AsEnumerable()
                                      where lst.Field<DateTime>("Datadate").Equals(date)
                                      select lst.Field<TimeSpan>("Outpunch").ToString()).FirstOrDefault();
                return hourValuevalue.Equals(null) ? "00:00:00" : Convert.ToString(hourValuevalue);
            }
            catch (Exception ex)
            {

                return "00:00:00";

            }
        }

        private string GetLastPuchTimeValue(DataTable beingOntimeData, DateTime date)
        {
            try
            {
                var hourValuevalue = (from lst in beingOntimeData.AsEnumerable()
                                      where lst.Field<DateTime>("Datadate").Equals(date)
                                      select lst.Field<TimeSpan>("TotalHours").ToString()).FirstOrDefault();
                return hourValuevalue.Equals(null) ? "00:00:00" : Convert.ToString(hourValuevalue);
            }
            catch (Exception ex)
            {

                return "00:00:00";

            }
        }

        public ActionResult DelayedSignInDashboard()
        {
            return View();
        }

        public ActionResult OrganizationDashboard()
        {
            return View();
        }
        public ActionResult OrganizationDashboardBandLevels()
        {
            ArrayList LeaveEncashDashboard = new ArrayList();
            string[] data = new string[] { "E1", "M1", "M2", "M3", "G1", "G2", "G3", "P1", "P2", "P3" };
            string[] count = new string[] { "3","10","5", "0", "3", "6", "16", "6", "8", "18" };
            for (int i = 0; i < data.Count(); i++)
            {
                string time_cat = data[i].ToString();
                string emp_num = count[i].ToString();
                LeaveEncashDashboard.Add(new
                {
                    time_cat,
                    emp_num
                });
            }
            return Json(LeaveEncashDashboard, JsonRequestBehavior.AllowGet);
        }
        public ActionResult OrganizationDashboardBusinessUnits()
        {
            ArrayList LeaveEncashDashboard = new ArrayList();
            string[] data = new string[] { "Pharma Analytics", "Chemistry Data Services", "Clinical Data Services", "Finance and Legal" };
            string[] count = new string[] { "40", "10", "15", "20" };
            for (int i = 0; i < data.Count(); i++)
            {
                string time_cat = data[i].ToString();
                string emp_num = count[i].ToString();
                LeaveEncashDashboard.Add(new
                {
                    time_cat,
                    emp_num
                });
            }
            return Json(LeaveEncashDashboard, JsonRequestBehavior.AllowGet);
        }
        public ActionResult OrganizationDashboardHoursTrend()
        {
            ArrayList EmployeeDashboardSpendingstatistics = new ArrayList();

            string[] time = new string[] { "7:00", "7:30", "8:00", "8:30", "9:00", "9:30", "10:00", "10:30", "11:00", "11:30", "12:00", "12:30", "13:00", };

            int[] count = new int[] { 49, 10, 94, 272, 320, 10, 50, 2, 3, 7, 1, 2, 6 };

            for (int i = 0; i < count.Count(); i++)
            {

                string att_time = time[i].ToString();
                string attendance = count[i].ToString();
                EmployeeDashboardSpendingstatistics.Add(new
                {
                    att_time,
                    attendance
                });
            }
            return Json(EmployeeDashboardSpendingstatistics, JsonRequestBehavior.AllowGet);
        }
        public ActionResult OrganizationDashboardEmployeevsBUvsOrganization()
        {
            ArrayList EmployeeDashboardSpendingstatistics = new ArrayList();
            string[] categories = new string[] { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
            double[] Selecteduserdata = new double[] { 8.0, 6.9, 9.5, 8.5, 10.4, 9.5, 9.2, 8.5, 9.3, 7.3, 6.9, 9.6 };
            double[] BUdata = new double[] { 8.9, 2.2, 5.7, 8.5, 11.9, 15.2, 17.0, 12.6, 12.2, 10.3, 6.6, 4.8 };
            double[] Organizationdata = new double[] { 8.9, 8.2, 5.7, 8.5, 7.9, 8.2, 17.0, 9.6, 10.2, 9.3, 8.6, 10.8 };
            EmployeeDashboardSpendingstatistics.Add(categories);
            EmployeeDashboardSpendingstatistics.Add(Selecteduserdata);
            EmployeeDashboardSpendingstatistics.Add(BUdata);
            EmployeeDashboardSpendingstatistics.Add(Organizationdata);
            return Json(EmployeeDashboardSpendingstatistics, JsonRequestBehavior.AllowGet);
        }

    }
}