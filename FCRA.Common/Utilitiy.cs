using FCRA.Common;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FCRA
{
    public static class Utilitiy
    {
        public static string ToTwoDecimal(this decimal? value)
        {
            if (!value.HasValue)
                return string.Empty;
            return string.Format("{0:#,0.00}", value.Value);
        }
        public static string ToTwoDecimal(this decimal value)
        {
            return string.Format("{0:#,0.00}", value);
        }
        public static string ToNoDecimal(this decimal? value)
        {
            if (!value.HasValue)
                return string.Empty;
            return string.Format("{0:#,0}", decimal.Round(value.Value, 0, MidpointRounding.ToEven));
        }
        public static string ToNoDecimal(this decimal value)
        {
            return string.Format("{0:#,0}", decimal.Round(value, 0, MidpointRounding.ToEven));
        }

        public static string ToMoney(this decimal? value, string? currencySymbol = null)
        {
            if (!value.HasValue)
                return string.Empty;
            return string.Format("{1}{0:#,0.00}", value.Value, GetMoneySymbol(currencySymbol));
        }
        public static string ToMoney(this decimal value, string? currencySymbol = null)
        {
            return string.Format("{1}{0:#,0.00}", value, GetMoneySymbol(currencySymbol));
        }
        public static string ToMoneyNoDecimal(this decimal? value, string? currencySymbol = null)
        {
            if (!value.HasValue)
                return string.Empty;
            return string.Format("{1}{0:#,0}", decimal.Round(value.Value, 0, MidpointRounding.ToEven), GetMoneySymbol(currencySymbol));
        }
        public static string ToMoneyNoDecimal(this decimal value, string? currencySymbol = null)
        {
            return string.Format("{1}{0:#,0}", decimal.Round(value, 0, MidpointRounding.ToEven), GetMoneySymbol(currencySymbol));
        }

        public static string ToMoney4(this decimal? value, string? currencySymbol = null)
        {
            if (!value.HasValue)
                return string.Empty;
            return string.Format("{1}{0:#,0.0000}", value.Value, GetMoneySymbol(currencySymbol));
        }
        public static string ToMoney4(this decimal value, string? currencySymbol = null)
        {
            return string.Format("{1}{0:#,0.0000}", value, GetMoneySymbol(currencySymbol));
        }

        public static string ToMoneyUnicode(this decimal? value, string? currencySymbol = null)
        {
            if (!value.HasValue)
                return string.Empty;
            return string.Format("{1}{0:#,0.00}", value.Value, GetMoneySymbolUnicode(currencySymbol));
        }
        public static string ToMoneyUnicode(this decimal value, string? currencySymbol = null)
        {
            return string.Format("{1}{0:#,0.00}", value, GetMoneySymbolUnicode(currencySymbol));
        }
        public static string ToMoneyUnicodeNoDecimal(this decimal? value, string? currencySymbol = null)
        {
            if (!value.HasValue)
                return string.Empty;
            return string.Format("{1}{0:#,0}", decimal.Round(value.Value, 0, MidpointRounding.ToEven), GetMoneySymbolUnicode(currencySymbol));
        }
        public static string ToMoneyUnicodeNoDecimal(this decimal value, string? currencySymbol = null)
        {
            return string.Format("{1}{0:#,0}", decimal.Round(value, 0, MidpointRounding.ToEven), GetMoneySymbolUnicode(currencySymbol));
        }

        public static string GetMoneySymbol(string? currencySymbol = null)
        {
            return string.Format("{0}{1}", !string.IsNullOrWhiteSpace(currencySymbol) ? currencySymbol : "₹", "&nbsp;");
        }
        public static string GetMoneySymbolUnicode(string? currencySymbol = null)
        {
            return string.Format("{0}{1}", !string.IsNullOrWhiteSpace(currencySymbol) ? currencySymbol : "&#8377;", "&#x0020;");
        }
        public static string GetDaySuffix(int day)
        {
            return day switch
            {
                1 or 21 or 31 => "st",
                2 or 22 => "nd",
                3 or 23 => "rd",
                _ => "th",
            };
        }

        public static string GetRatingText(ScaleType scaleType, int rating)
        {
            if (rating < 1)
                return "-";
            if (scaleType == ScaleType.ThreePoint || scaleType == ScaleType.FourPoint)
            {
                if (rating == 1)
                    return "Low";
                if (rating == 2)
                    return "Medium";
                if (rating == 3)
                    return "High";
                if (rating == 4)
                    return "Critical";
            }
            if (scaleType == ScaleType.FivePoint)
            {
                if (rating == 1)
                    return "Low";
                if (rating == 2)
                    return "Medium";
                if (rating == 3)
                    return "Medium-High";
                if (rating == 4)
                    return "High";
                if (rating == 5)
                    return "Critical";

            }
            return "Critical";
        }

        public static string GetRatingCssClass(int rating)
        {
            if (rating < 1)
                return "";
            if (rating == 1)
                return "riskScore1";
            if (rating == 2)
                return "riskScore2";
            if (rating == 3)
                return "riskScore3";
            if (rating == 4)
                return "riskScore4";
            return "riskScore5";
        }

        public static Color GetRatingColor(int rating)
        {
            if (rating < 1)
                return Color.White;
            if (rating == 1)
                return Color.Green;
            if (rating == 2)
                return Color.DarkOrange;
            if (rating == 3)
                return Color.Red;
            if (rating == 4)
                return Color.Brown;
            return Color.SaddleBrown;
        }

        public static int GetScoreRating(decimal ratingScore, decimal scoreRange2, decimal scoreRange3, decimal? scoreRange4, decimal? scoreRange5)
        {
            decimal score = Math.Round(ratingScore, 2);
            if (score < scoreRange2)
                return 1;
            if (score < scoreRange3)
                return 2;
            if (score > scoreRange3 && !scoreRange4.HasValue)
                return 3;
            if (score < scoreRange4)
                return 3;
            if (score > scoreRange4 && !scoreRange5.HasValue)
                return 4;
            if (score < scoreRange5)
                return 4;
            if (score > scoreRange5)
                return 5;
            return 1;
        }

        public static Tuple<string, string> GetResudualRating(ScaleType scaleType, int rating1, int rating2)
        {
            if (scaleType == ScaleType.ThreePoint)
            {
                if ((rating1 == 1 && rating2 == 1)
                    || (rating1 == 1 && rating2 == 2))
                    return new Tuple<string, string>("Low", "riskScore1");

                if ((rating1 == 1 && rating2 == 3)
                    || (rating1 == 2 && rating2 == 1)
                    || (rating1 == 2 && rating2 == 2)
                    || (rating1 == 3 && rating2 == 1))
                    return new Tuple<string, string>("Medium", "riskScore2");

                if ((rating1 == 2 && rating2 == 3)
                    || (rating1 == 3 && rating2 == 2)
                    || (rating1 == 3 && rating2 == 3))
                    return new Tuple<string, string>("High", "riskScore3");
            }

            if (scaleType == ScaleType.FourPoint)
            {
                if ((rating1 == 1 && rating2 == 1)
                    || (rating1 == 1 && rating2 == 2)
                    || (rating1 == 1 && rating2 == 3)
                    || (rating1 == 2 && rating2 == 1))
                    return new Tuple<string, string>("Low", "riskScore1");

                if ((rating1 == 1 && rating2 == 4)
                    || (rating1 == 2 && rating2 == 2)
                    || (rating1 == 2 && rating2 == 3)
                    || (rating1 == 3 && rating2 == 1)
                    || (rating1 == 3 && rating2 == 2))
                    return new Tuple<string, string>("Medium", "riskScore2");

                if ((rating1 == 2 && rating2 == 4)
                    || (rating1 == 3 && rating2 == 3)
                    || (rating1 == 3 && rating2 == 4)
                    || (rating1 == 4 && rating2 == 1)
                    || (rating1 == 4 && rating2 == 2))
                    return new Tuple<string, string>("High", "riskScore3");

                if ((rating1 == 4 && rating2 == 3)
                   || (rating1 == 4 && rating2 == 4))
                    return new Tuple<string, string>("Critical", "riskScore4");
            }

            if (scaleType == ScaleType.FivePoint)
            {
                if ((rating1 == 1 && rating2 == 1)
                    || (rating1 == 1 && rating2 == 2)
                    || (rating1 == 1 && rating2 == 3)
                    || (rating1 == 2 && rating2 == 1)
                    || (rating1 == 2 && rating2 == 2)
                    || (rating1 == 2 && rating2 == 3)
                    || (rating1 == 3 && rating2 == 1))
                    return new Tuple<string, string>("Low", "riskScore1");

                if ((rating1 == 1 && rating2 == 4)
                    || (rating1 == 1 && rating2 == 5)
                    || (rating1 == 2 && rating2 == 4)
                    || (rating1 == 3 && rating2 == 2)
                    || (rating1 == 3 && rating2 == 3)
                    || (rating1 == 4 && rating2 == 1)
                    || (rating1 == 4 && rating2 == 2))
                    return new Tuple<string, string>("Medium", "riskScore2");

                if ((rating1 == 2 && rating2 == 4)
                    || (rating1 == 3 && rating2 == 4)
                    || (rating1 == 3 && rating2 == 5)
                    || (rating1 == 4 && rating2 == 3)
                    || (rating1 == 4 && rating2 == 4)
                    || (rating1 == 4 && rating2 == 5)
                    || (rating1 == 5 && rating2 == 1)
                    || (rating1 == 5 && rating2 == 2))
                    return new Tuple<string, string>("High", "riskScore3");

                if ((rating1 == 5 && rating2 == 3)
                   || (rating1 == 5 && rating2 == 4)
                   || (rating1 == 5 && rating2 == 5))
                    return new Tuple<string, string>("Critical", "riskScore4");
            }




            return new Tuple<string, string>("Low", "riskScore1");
        }
    }
}
