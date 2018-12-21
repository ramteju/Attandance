using ProductTracking.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web;

namespace ProductTracking
{
    public class QueryStrings
    {
        public string GetTimeQuery(string fromdatetime, string todatetime)
        {
           
            return @"SELECT COUNT(*) g FROM (SELECT MIN(IndexNo) as d  FROM [COSEC].[dbo].[Mx_VEW_APIUserAttendanceEvents] where IDateTime BETWEEN '" + fromdatetime + @"' and '" + todatetime + @"'  GROUP BY USERid) g";


        }
        public string GetPresentCount(string currentdate)
        {
            
            return @" SELECT COUNT(dISTINCT(USERID)) FROM[COSEC].[dbo].[Mx_VEW_APIUserAttendanceEvents]  where CAST(IDateTime as date) = '" + currentdate.Replace('/','-') + @"'";
        }

       
        public string GetTimes(string fromdatetime, string todatetime)
        {
           
             return @"SELECT count(distinct (UserID))  FROM[COSEC].[dbo].[Mx_VEW_APIUserAttendanceEvents] where IDateTime BETWEEN '" + fromdatetime + @"' and '" + todatetime + @"'";
        }

        public string GetDateTimeQuery(string fromdatetime, string fromtime , string totime, string betweentime)
        {
            return @"  SELECT COUNT(*) g FROM(SELECT MIN(IndexNo) as d  FROM[COSEC].[dbo].[Mx_VEW_APIUserAttendanceEvents] where Edate = '" + fromdatetime.Replace('-', '/') + @"' and
                      ETime BETWEEN '" + fromtime + @"' and '" + totime + @"' and UserID not in
                      (
                      SELECT  distinct UserID  FROM[COSEC].[dbo].[Mx_VEW_APIUserAttendanceEvents] where Edate = '" + fromdatetime.Replace('-', '/') + @"' and
                      ETime BETWEEN '00:00:00' and '" + betweentime + @"'
                      )
                     GROUP BY USERid) g";
        }

        public DataTable GetInpunhData(string Date)
        {

            try
            {
                string orderQueryString = @"SELECT CAST(IDateTime as date) AS ForDate, DATEPART(hour,IDateTime) AS OnHour, COUNT(*) AS Totals FROM [COSEC].[dbo].[Mx_VEW_APIUserAttendanceEvents] where
                       IndexNo in (
                                     SELECT MIN(IndexNo) FROM [COSEC].[dbo].[Mx_VEW_APIUserAttendanceEvents] where CAST(IDateTime as date)='" + Date + @"' GROUP BY USERid 
                                  )
                       GROUP BY CAST(IDateTime as date),
                       DATEPART(hour,IDateTime)
                       order by CAST(IDateTime as date), DATEPART(hour,IDateTime)  asc";

                var dt = DataTable(orderQueryString);
                return dt;

            }
            catch (Exception )
            {

              
            }
            return null;
        }

        public DataTable GetOutPunchData(string Date)
        {
            try
            {

                string orderQueryString = @"SELECT CAST(IDateTime as date) AS ForDate, DATEPART(hour, IDateTime) AS OnHour, COUNT(*) AS Totals FROM[COSEC].[dbo].[Mx_VEW_APIUserAttendanceEvents] where
                                            IndexNo in (
                                            SELECT MAX(IndexNo) FROM[COSEC].[dbo].[Mx_VEW_APIUserAttendanceEvents] where CAST(IDateTime as date) = '" + Date + @"'  GROUP BY USERid having count (*)>1
                                                     )
                                            GROUP BY CAST(IDateTime as date),
                                            DATEPART(hour, IDateTime)
                                           order by CAST(IDateTime as date), DATEPART(hour, IDateTime)  asc";
                var dt = DataTable(orderQueryString);
                return dt;

            }
            catch (Exception)
            {


            }


            return null;
        }

        public DataTable DataTable( string sqlQuery)
        {
            ApplicationDbContext context = new ApplicationDbContext();
            DbProviderFactory dbFactory = DbProviderFactories.GetFactory(context.Database.Connection);

            using (var cmd = dbFactory.CreateCommand())
            {
                cmd.Connection = context.Database.Connection;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = sqlQuery;
                using (DbDataAdapter adapter = dbFactory.CreateDataAdapter())
                {
                    adapter.SelectCommand = cmd;

                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    return dt;
                }
            }
        }

        public DataTable GetUserNames()
        {
            try
            {

                string orderQueryString = @"SELECT DISTINCT UserName+' ('+UserID +') ' as users FROM [Mx_VEW_UserDetails] WHERE ActiveFlag=1";
                var dt = DataTable(orderQueryString);
                return dt;

            }
            catch (Exception)
            {


            }


            return null;
        }

        public  DataTable GetBeingOntimeData(string Fromdate, string Todate, int userId)
        {
            try
            {

                string orderQueryString = @"select G.UserName,CAST(IDateTime as date)as Datadate,CAST(MAX (IDateTime)-MIN(IDateTime) AS TIME(0)) as Datatime  FROM [COSEC].[dbo].[Mx_VEW_APIUserAttendanceEvents] t
					JOIN [dbo].[Mx_VEW_UserDetails] G ON t.UserID=G.UserID 
					where t.UserID='" + userId + @"' and CAST(IDateTime as date) BETWEEN '" + Fromdate + @"' AND '" + Todate + @"'
					GROUP BY G.UserName,CAST(IDateTime as date)";
                var dt = DataTable(orderQueryString);
                return dt;

            }
            catch (Exception)
            {


            }


            return null;
        }

        public DataTable GetInOutTotalPunchData(string Fromdate, string Todate, int userId)
        {
            try
            {

                string orderQueryString = @"select G.UserName,CAST(IDateTime as date)  as Datadate,CAST(MIN(IDateTime) AS TIME(0)) as Inpunch,CAST(MAX(IDateTime) 
                                            AS TIME(0)) as Outpunch,CAST(MAX(IDateTime)-MIN(IDateTime) AS TIME(0)) as TotalHours FROM [COSEC].[dbo].[Mx_VEW_APIUserAttendanceEvents] t
                                            JOIN [dbo].[Mx_VEW_UserDetails] G ON t.UserID=G.UserID where t.UserID='" + userId + @"' and CAST(IDateTime as date) BETWEEN '" + Fromdate + @"' AND '" + Todate + @"'
                                            GROUP BY G.UserName,CAST(IDateTime as date)";
                var dt = DataTable(orderQueryString);
                return dt;

            }
            catch (Exception)
            {


            }


            return null;
        }

        
    }
}