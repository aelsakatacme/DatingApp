using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatingApp.API.Extensions
{
    public static class DatingAppExtension
    {
        public static void AddApplicationError(this HttpResponse httpResponse, string message)
        {
            httpResponse.Headers.Add("Application-Error", message);
            httpResponse.Headers.Add("Access-Control-Exposes-Headers", "Application-Error");
            httpResponse.Headers.Add("Access-Control-Allow-Origin", "*");
        }


        #region DATETIME
        public static string CalculateFullAge(this DateTime? birthDate)
        {
            if (birthDate != null)
            {
                return CalculateFullAge((DateTime)birthDate);
            }
            return "";
        }

        public static string CalculateFullAge(this DateTime birthDate)
        {
            if (birthDate != null)
            {
                DateTime Now = DateTime.Now;
                DateTime Dob = (DateTime)birthDate;
                int Years = new DateTime(DateTime.Now.Subtract(Dob).Ticks).Year - 1;
                DateTime PastYearDate = Dob.AddYears(Years);
                int Months = 0;
                for (int i = 1; i <= 12; i++)
                {
                    if (PastYearDate.AddMonths(i) == Now)
                    {
                        Months = i;
                        break;
                    }
                    else if (PastYearDate.AddMonths(i) >= Now)
                    {
                        Months = i - 1;
                        break;
                    }
                }
                int Days = Now.Subtract(PastYearDate.AddMonths(Months)).Days;
                int Hours = Now.Subtract(PastYearDate).Hours;
                int Minutes = Now.Subtract(PastYearDate).Minutes;
                int Seconds = Now.Subtract(PastYearDate).Seconds;
                return String.Format("{0} Year(s) {1} Month(s) {2} Day(s) {3} Hour(s) {4} Minute(s) {5} Second(s)", Years, Months, Days, Hours, Minutes, Seconds);
            }
            return "";
        }

        public static int CalculateAge(this DateTime? birthDate)
        {
            if (birthDate != null)
            {
                return CalculateAge((DateTime)birthDate);
            }
            return 0;
        }

        public static int CalculateAge(this DateTime birthDate)
        {
            return new DateTime(DateTime.Now.Subtract(birthDate).Ticks).Year - 1;
        }
        #endregion

    }
}
