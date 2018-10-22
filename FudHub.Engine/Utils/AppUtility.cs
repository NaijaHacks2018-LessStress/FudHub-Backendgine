﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Xml;
using System.Xml.Linq;

public class AppUtility
{
    public static string ReturnMessage;    

    public static DateTime UTC()
    {
        return DateTime.UtcNow.AddHours(1); //(AppConfig.UTCOffsetAdd);
    }

    public static string CleanUrlSlug(string Str, int MaxLength = 50)
    {
        Str = Str.Substring(0, Str.Length >= MaxLength ? MaxLength : Str.Length).ToLowerInvariant();
        //remove all accent
        dynamic bytes = Encoding.GetEncoding("Cyrillic").GetBytes(Str);
        Str = Encoding.ASCII.GetString(bytes);
        //replace whitespaces
        Str = Regex.Replace(Str, "\\s", "-", RegexOptions.Compiled);
        //remove invalid characters
        Str = Regex.Replace(Str, "[^\\w\\s\\p{Pd}]", "", RegexOptions.Compiled);
        //trim dashes from start and end
        Str = Str.Trim('-', '-');
        //replace double occurence of - or \_
        Str = Regex.Replace(Str, "([-_]){2,}", "$1", RegexOptions.Compiled);

        return Str;
    }

    public static bool IsNumeric(object Expression)
    {
        if (Expression == null || Expression is DateTime)
            return false;

        if (Expression is Int16 || Expression is Int32 || Expression is Int64 || Expression is Decimal || Expression is Single || Expression is Double || Expression is Boolean)
            return true;

        try
        {
            if (Expression is string)
                Double.Parse(Expression as string);
            else
                Double.Parse(Expression.ToString());
            return true;
        }
        catch { } // just dismiss errors but return false
        return false;
    }

    public static string CleanDomain(string Domain)
    {
        Domain = Domain.Replace("http://", "").Replace("https://", "").Replace("http://www.", "").Replace("https://www.", "").Replace("www.", "").Replace("/", "").Replace("\\", "");
        Domain = Domain.Split(':')[0];
        return Domain.ToLower();
    }


    public static string GenerateAlphaNumeric(int Length)
    {
        Random random = new Random();
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string(Enumerable.Repeat(chars, Length).Select(s => s[random.Next(s.Length)]).ToArray());
    }

    public static string GenerateNumeric(int Length)
    {
        Random random = new Random();
        const string chars = "0123456789";
        return new string(Enumerable.Repeat(chars, Length).Select(s => s[random.Next(s.Length)]).ToArray());
    }

    public static bool ValidateEmail(string Email)
    {
        Regex EmailRegex = new Regex("^(?<Username>[^@]+)@(?<host>.+)$");
        Match EmailMatch = EmailRegex.Match(Email.Trim());
        return EmailMatch.Success;
    }

    public static int NameMatcher(string Name1, string Name2)
    {
        var nameList1 = new List<string>();
        var nameList2 = new List<string>();
        var nameCol = new Dictionary<string, string>();
        int matchCount = 0;

        nameList1 = Name1.ToLower()
                            .Trim()
                            .Replace("  ", " ")
                            .Replace("   ", " ")
                            .Split(' ')
                            .ToList();
        nameList2 = Name2.ToLower()
                            .Trim()
                            .Replace("  ", " ")
                            .Replace("   ", " ")
                            .Split(' ')
                            .ToList();

        for (int i = 0; i < nameList1.Count(); i++)
        {
            nameCol.Add(nameList1[i], "0");
        }

        for (int i = 0; i < nameList2.Count(); i++)
        {
            if (nameCol.ContainsKey(nameList2[i]))
            {
                matchCount += 1;
            }
        }

        return matchCount;
    }
    
    public static string DateTimeToWord(DateTime DateTimeString)
    {
        if ((DateTimeString == null)) return "...";
        long value; string str = "";

        if (DateDifference.DateDiff(DateDifference.DateInterval.Minute, DateTimeString, UTC()) <= 2)
        {
            str = "now";
        }
        else if (DateDifference.DateDiff(DateDifference.DateInterval.Minute, DateTimeString, UTC()) <= 60)
        {
            value = DateDifference.DateDiff(DateDifference.DateInterval.Minute, DateTimeString, UTC());
            str = value + " min" + (value > 1 ? "s " : " ") + "ago";
        }
        else if (DateDifference.DateDiff(DateDifference.DateInterval.Hour, DateTimeString, UTC()) <= 24)
        {
            value = DateDifference.DateDiff(DateDifference.DateInterval.Hour, DateTimeString, UTC());
            str = value + " hr" + (value > 1 ? "s " : " ") + "ago";
        }
        else if (DateDifference.DateDiff(DateDifference.DateInterval.Day, DateTimeString, UTC()) == 1)
        {
            value = DateDifference.DateDiff(DateDifference.DateInterval.Day, DateTimeString, UTC());
            str = " yst";
        }
        else if (DateDifference.DateDiff(DateDifference.DateInterval.Day, DateTimeString, UTC()) <= 7)
        {
            value = DateDifference.DateDiff(DateDifference.DateInterval.Day, DateTimeString, UTC());
            str = value + " day" + (value > 1 ? "s " : " ") + "ago";
        }
        else if (DateDifference.DateDiff(DateDifference.DateInterval.Weekday, DateTimeString, UTC()) <= 4)
        {
            value = DateDifference.DateDiff(DateDifference.DateInterval.Weekday, DateTimeString, UTC());
            str = value + " wk" + (value > 1 ? "s " : " ") + "ago";
        }
        else if (DateDifference.DateDiff(DateDifference.DateInterval.Month, DateTimeString, UTC()) <= 12)
        {
            value = DateDifference.DateDiff(DateDifference.DateInterval.Month, DateTimeString, UTC());
            str = value + " mth" + (value > 1 ? "s " : " ") + "ago";
        }
        else if (DateDifference.DateDiff(DateDifference.DateInterval.Year, DateTimeString, UTC()) >= 1)
        {
            value = DateDifference.DateDiff(DateDifference.DateInterval.Year, DateTimeString, UTC());
            str = value + " yr" + (value > 1 ? "s " : " ") + "ago";
        }

        return str;
    }

    public static string DigitStyle(dynamic Value)
    {
        double value = Convert.ToDouble(Value), before, after;
        string str = value.ToString();
        switch (value.ToString().Length)
        {
            case 4:
            case 5:
            case 6:
                before = Convert.ToDouble(str) / 1000;
                after = Math.Round(before, 1, MidpointRounding.ToEven);
                if (after == 100)
                {
                    str = 1 + "m";
                }
                str = after + "k";
                break;
            case 7:
            case 8:
            case 9:
                before = Convert.ToDouble(str) / 1000000;
                after = Math.Round(before, 1, MidpointRounding.ToEven);
                if (after == 1000)
                {
                    str = 1 + "b";
                }
                str = after + "m";
                break;
            case 10:
            case 11:
            case 12:
                before = Convert.ToDouble(str) / 1000000000;
                after = Math.Round(before, 1, MidpointRounding.ToEven);
                if (after == 1000)
                {
                    str = 1 + "t";
                }
                str = after + "b";
                break;
            case 13:
            case 14:
            case 15:
                before = Convert.ToDouble(str) / 1000000000000L;
                after = Math.Round(before, 1, MidpointRounding.ToEven);
                str = after + "t";
                break;
            default:
                return value.ToString();
        }

        return str;
    }

    public enum ConvertType
    {
        B = 0,
        KB = 1,
        MB = 2,
        GB = 3,
        TB = 4,
        PB = 5,
        EB = 6,
        ZI = 7,
        YI = 8
    }

    public static double ConvertSize(long Bytes, ConvertType ConvertTo)
    {
        return Math.Round(Bytes / (Math.Pow(1024, (int)ConvertTo)));
    }

    public class Country
    {
        public static string Get(string Code)
        {
            if (Code != "")
            {
                var countryXml = new XmlDocument();
                countryXml.Load(AppDomain.CurrentDomain.BaseDirectory + "Countries.xml");
                XmlNode node = countryXml.SelectSingleNode("/countries/country[@code='" + Code + "']");
                return node.InnerText;
            }
            return "";
        }

        public static string Flag(string Code)
        {
            return "~/img/Flags/" + Code + ".png";
        }
    }

    public static string DelimitedStringListFromFile(string FilePath, string Delimiter)
    {
        string path = FilePath;
        if (File.Exists(path))
        {
            string str = File.ReadAllText(path);
            string[] str2 = str.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            string strList = "";
            for (int i = 0; i < str2.Count() + 1 - 1; i++)
            {
                //Create string array in js format to use in jquery autocomplete
                strList += "'" + str2[i].Trim() + "'" + (i != str2.Count() - 1 ? Delimiter : "");
            }

            return strList;
        }

        return string.Empty;
    }

    public static List<string> GetNigeriaStates()
    {
        dynamic doc = XDocument.Load(AppDomain.CurrentDomain.BaseDirectory + ("~/App_Data/NGStatesLGA.xml"));
        List<string> list = new List<string>();
        foreach (XElement e in doc.Root.Elements)
        {
            list.Add(e.Attribute("name").ToString());
        }
        return list;
    }

    public static bool ExternalFileExist(string Url)
    {
        dynamic uri = new Uri(Url);
        if (uri.IsFile)
        {
            return File.Exists(uri.LocalPath);
        }
        else
        {
            try
            {
                HttpWebRequest r = WebRequest.Create(uri);
                r.Method = "HEAD";
                HttpWebResponse rsp = (HttpWebResponse)r.GetResponse();
                return rsp.StatusCode == HttpStatusCode.OK;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
    
    //public class CustomErrorLogger
    //{
    //    public DateTime DateStamp { get; set; }
    //    public string Code { get; set; }
    //    public string Message { get; set; }
    //    public string Page { get; set; }
    //    public string IP { get; set; }

    //    public static void Log(string Message, string Code = "", string Page = "")
    //    {
    //        try
    //        {
    //            string file = HttpContext.Current.Server.MapPath("~/App_Data/ErrorLog.xml");
    //            XDocument errorLog = XDocument.Load(file);
    //            if (string.IsNullOrEmpty(Code))
    //                Code = HttpContext.Current.Response.StatusCode.ToString();
    //            if (string.IsNullOrEmpty(Page))
    //                Page = HttpContext.Current.Request.Url.AbsolutePath.ToString();

    //            errorLog.Root.Add(new XElement("log", new XAttribute("code", Code), 
    //                new XAttribute("message", Message), 
    //                new XAttribute("datetime", AppUtility.UTC().ToShortDateString() + " " + AppUtility.UTC().ToShortTimeString()),
    //                new XAttribute("page", Page), 
    //                new XAttribute("ip", AppUtility.GetUserIPAddress())));
    //            errorLog.Save(file);
    //        }
    //        catch (Exception ex)
    //        {
    //            string msg = ex.Message;
    //        }
    //    }

    //    public static List<CustomErrorLogger> LoadLog()
    //    {
    //        string file = HttpContext.Current.Server.MapPath("~/App_Data/ErrorLog.xml");
    //        XDocument errorLog = XDocument.Load(file);
    //        List<CustomErrorLogger> list = new List<CustomErrorLogger>();

    //        CustomErrorLogger log;
    //        foreach (var e in errorLog.Root.Elements())
    //        {
    //            log = new CustomErrorLogger
    //            {
    //                DateStamp = DateTime.Parse(e.Attribute("datetime").Value),
    //                Code = e.Attribute("code").Value,
    //                Message = e.Attribute("message").Value,
    //                Page = e.Attribute("page").Value,
    //                IP = e.Attribute("ip").Value
    //            };
    //            list.Add(log);
    //        }
    //        return list;
    //        //.OrderBy(Function(p) p.Key)
    //    }

    //    public static bool Clear()
    //    {
    //        string file = HttpContext.Current.Server.MapPath("~/App_Data/ErrorLog.xml");
    //        XDocument errorLog = XDocument.Load(file);
    //        errorLog.Root.Elements().Remove();
    //        errorLog.Save(file);

    //        return true;
    //    }
    //}

    public static string UploadToBase64(Stream file)
    {
        if (file != null)
        {
            Stream fs = file;
            var br = new BinaryReader(fs);
            byte[] bytes = br.ReadBytes((int)fs.Length);
            string base64Str = Convert.ToBase64String(bytes, 0, bytes.Length);

            return "data:image/png;base64," + base64Str;
        }
        return string.Empty;
    }

    //public static bool Base64ImageToFile(string Base64String, string SaveAs, System.Drawing.Imaging.ImageFormat Format)
    //{
    //    try
    //    {
    //        //Remove this part "data:image/jpeg;base64,"
    //        Base64String = Base64String.Split(',')[1];
    //        byte[] bytes = Convert.FromBase64String(Base64String);

    //        Image image;
    //        using (var ms = new MemoryStream(bytes))
    //        {
    //            image = Image.FromStream(ms);
    //        }
    //        image.Save(SaveAs, Format);
    //        return true;
    //    }
    //    catch (Exception ex)
    //    {
    //        ReturnMessage = ex.Message;
    //        return false;
    //    }
    //}
}

public static partial class DateDifference
{
    public enum DateInterval
    {
        Day,
        DayOfYear,
        Hour,
        Minute,
        Month,
        Quarter,
        Second,
        Weekday,
        WeekOfYear,
        Year
    }

    public static long DateDiff(DateInterval intervalType, System.DateTime dateOne, System.DateTime dateTwo)
    {
        switch (intervalType)
        {
            case DateInterval.Day:
            case DateInterval.DayOfYear:
                System.TimeSpan spanForDays = dateTwo - dateOne;
                return (long)spanForDays.TotalDays;
            case DateInterval.Hour:
                System.TimeSpan spanForHours = dateTwo - dateOne;
                return (long)spanForHours.TotalHours;
            case DateInterval.Minute:
                System.TimeSpan spanForMinutes = dateTwo - dateOne;
                return (long)spanForMinutes.TotalMinutes;
            case DateInterval.Month:
                return ((dateTwo.Year - dateOne.Year) * 12) + (dateTwo.Month - dateOne.Month);
            case DateInterval.Quarter:
                long dateOneQuarter = (long)System.Math.Ceiling(dateOne.Month / 3.0);
                long dateTwoQuarter = (long)System.Math.Ceiling(dateTwo.Month / 3.0);
                return (4 * (dateTwo.Year - dateOne.Year)) + dateTwoQuarter - dateOneQuarter;
            case DateInterval.Second:
                System.TimeSpan spanForSeconds = dateTwo - dateOne;
                return (long)spanForSeconds.TotalSeconds;
            case DateInterval.Weekday:
                System.TimeSpan spanForWeekdays = dateTwo - dateOne;
                return (long)(spanForWeekdays.TotalDays / 7.0);
            case DateInterval.WeekOfYear:
                System.DateTime dateOneModified = dateOne;
                System.DateTime dateTwoModified = dateTwo;
                while (dateTwoModified.DayOfWeek != System.Globalization.DateTimeFormatInfo.CurrentInfo.FirstDayOfWeek)
                {
                    dateTwoModified = dateTwoModified.AddDays(-1);
                }
                while (dateOneModified.DayOfWeek != System.Globalization.DateTimeFormatInfo.CurrentInfo.FirstDayOfWeek)
                {
                    dateOneModified = dateOneModified.AddDays(-1);
                }
                System.TimeSpan spanForWeekOfYear = dateTwoModified - dateOneModified;
                return (long)(spanForWeekOfYear.TotalDays / 7.0);
            case DateInterval.Year:
                return dateTwo.Year - dateOne.Year;
            default:
                return 0;
        }
    }
}