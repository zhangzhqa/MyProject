using System;
using System.Data;
using System.Configuration;
using System.Collections; 
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Net;
using System.IO;
using System.Text;
using System.Net.Mail;
using System.Text.RegularExpressions;

public class PublicFunction
{
    private static object lockWrite = new object();

    public PublicFunction()
    { }


    public static string EncryptPassword(string pass)
    {
        return FormsAuthentication.HashPasswordForStoringInConfigFile(pass + "睿智网络 Site V2.0", "MD5");
    }
}
