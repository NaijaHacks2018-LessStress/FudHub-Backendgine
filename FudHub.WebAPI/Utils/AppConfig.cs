using System;
using System.Configuration;

public class AppConfig
{
    public static string Name { get { return ConfigurationManager.AppSettings["app:Name"]; } }
    public static string Description { get { return ConfigurationManager.AppSettings["app:Desc"]; } }

}