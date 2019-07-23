using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCore_facebook.Library
{
    public class _Extensions
    {
        public static string datetime_parse(DateTime datetime)
        {
            string return_date = "";
            int ngay = DateTime.Now.Day,
                thang = DateTime.Now.Month,
                nam = DateTime.Now.Year;
            int ngay_d = datetime.Day,
                thang_d = datetime.Month,
                nam_d = datetime.Year;
            if (nam != nam_d)
            {
                return_date = datetime.ToString("dd/MM/yyyy HH:mm");
            }
            else
            {
                if (thang != thang_d)
                {
                    return_date = datetime.ToString("dd/MM/yyyy HH:mm");
                }
                else if (ngay != ngay_d)
                {
                    int xem_ngay = ngay - ngay_d;
                    if (xem_ngay == 1)
                    {
                        return_date = "Hôm qua " + datetime.ToString("HH:mm");

                    }
                    else if (xem_ngay == 2)
                    {
                        return_date = "Hôm kia " + datetime.ToString("HH:mm");
                    }
                    else
                    {
                        return_date = datetime.ToString("dd/MM/yyyy HH:mm");
                    }
                }
                else
                {
                    return_date = "Hôm nay " + datetime.ToString("HH:mm");
                }
            }
            return return_date;

        }
    }
}
