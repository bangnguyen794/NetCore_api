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
                    return_date = datetime.ToString("dd/MM HH:mm");
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
                        return_date = datetime.ToString("dd/MM HH:mm");
                    }
                }
                else
                {
                    return_date = datetime.ToString("HH:mm");
                }
            }
            return return_date;

        }
        public static string NullToString(object Value)
        {

            // Value.ToString() allows for Value being DBNull, but will also convert int, double, etc.
            return Value == null ? "" : Value.ToString();

            // If this is not what you want then this form may suit you better, handles 'Null' and DBNull otherwise tries a straight cast
            // which will throw if Value isn't actually a string object.
            //return Value == null || Value == DBNull.Value ? "" : (string)Value;


        }

    }
}
