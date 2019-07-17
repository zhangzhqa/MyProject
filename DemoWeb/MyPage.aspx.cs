using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Outlook = Microsoft.Office.Interop.Outlook;

namespace DemoWeb
{
    public partial class MyPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            SendMail();
        }

        /// <summary>
        /// Microsoft.CSharp.dll
        /// Microsoft.Office.Interop.Outlook.dll
        /// </summary>
        private static void SendMail()
        {
            Outlook.Application olApp = new Outlook.Application();
            Outlook.MailItem mailItem = (Outlook.MailItem)olApp.CreateItem(Outlook.OlItemType.olMailItem);
            mailItem.To = "32783379@qq.com";
            mailItem.Subject = DateTime.Now.ToString("yyyyMMdd") + "_调用Outlook发送邮件测试";
            mailItem.BodyFormat = Outlook.OlBodyFormat.olFormatHTML;

            string content = "附件为" + DateTime.Now.ToString("yyyyMMdd") + " 数据，请查阅，谢谢！";
            content = "各收件人，<br/> <br/>请重点关注以下内容：<br/> <br/>" + content + "<br/> <br/><br/><br/>此邮件为系统自动邮件通知，请不要直接进行回复！谢谢。";
            content = content + "<br/>\r\n                                    <br/>Best Regards!\r\n                                    <br/>\r\n                                    <br/>          \r\n                                    <br/>==============================================\r\n                               \r\n                                    <br/>\r\n                                    <br/>\r\n                \r\n             ===============================================";

            mailItem.HTMLBody = content;

            string fileName = @"E:\U9\U9资料\U9UAP开发2.pdf";
            mailItem.Attachments.Add(fileName);
            ((Outlook._MailItem)mailItem).Send();
            mailItem = null;
            olApp = null;
        }
        private void SendMailWeb()
        {
            Outlook.Application olApp = new Outlook.Application();
            Outlook.MailItem mailItem = (Outlook.MailItem)olApp.CreateItem(Outlook.OlItemType.olMailItem);
            mailItem.To = "32783379@qq.com";
            mailItem.Subject = DateTime.Now.ToString("yyyyMMdd") + "_调用Outlook发送邮件测试";
            mailItem.BodyFormat = Outlook.OlBodyFormat.olFormatHTML;

            string content = "附件为" + DateTime.Now.ToString("yyyyMMdd") + " 数据，请查阅，谢谢！";
            content = "各收件人，<br/> <br/>请重点关注以下内容：<br/> <br/>" + content + "<br/> <br/><br/><br/>此邮件为系统自动邮件通知，请不要直接进行回复！谢谢。";
            content = content + "<br/>\r\n                                    <br/>Best Regards!\r\n                                    <br/>\r\n                                    <br/>          \r\n                                    <br/>==============================================\r\n                               \r\n                                    <br/>\r\n                                    <br/>\r\n                \r\n             ===============================================";

            mailItem.HTMLBody = content;

            string smtpAddress = "mail.yonyou.com";
            mailItem.SendUsingAccount = GetAccountForEmailAddress(olApp, smtpAddress);

            string fileName = @"E:\U9\U9资料\U9UAP开发2.pdf";
            mailItem.Attachments.Add(fileName);

            ((Outlook._MailItem)mailItem).Send();
            mailItem = null;
            olApp = null;

        }

        private static Outlook.Account GetAccountForEmailAddress(Outlook.Application application, string smtpAddress)
        {
            // Loop over the Accounts collection of the current Outlook session.
            Outlook.Accounts accounts = application.Session.Accounts;
            foreach (Outlook.Account account in accounts)
            {
                // When the email address matches, return the account.
                if (account.SmtpAddress == smtpAddress)
                {
                    return account;
                }
            }
            // If you get here, no matching account was found.
            throw new System.Exception(string.Format("No Account with SmtpAddress: {0} exists!",
                smtpAddress));
        }
    }
}