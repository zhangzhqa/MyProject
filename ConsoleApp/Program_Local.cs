using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace ConsoleApp
{
    class Program_Local
    {
        static void Main(string[] args)
        {
            CallErpServices();
        }

        static void CallErpServices()
        {
            //<!--本机上下文-->
            //<!--<Key Name="EnterpriseID" value="001" />
            //<Key Name="OrgID" value="1001410310010024" />
            //<Key Name="OrgCode" value="10" />
            //<Key Name="UserID" value="1001410310010245" />
            //<Key Name="UserCode" value="admin" />
            //<Key Name="UserName" value="管理员" />
            //<Key Name="CultureName" value="zh-CN" />-->

            string strURL= "http://localhost/U9/CustRestServices/YY.U9.Cust.CommonSV.ICommonOperateSV.svc/Do";

            System.Net.HttpWebRequest request = (HttpWebRequest)WebRequest.Create(strURL);
            request.Method = "POST";
            request.ContentType = "application/json; charset=utf-8";
            string requestInfos = string.Empty;
            string context = "{";
            context = context + "\"EntCode\":\"02\",";
            context = context + "\"OrgCode\":\"100\",";
            context = context + "\"UserCode\":\"admin\",";
            context = context + "\"CultureName\":\"zh-CN\"";
            context = context + "}";

            //业务类型：物料-ItemMaster 杂发-MiscShip
            string businessType = "TransferForm";
            //Action：新增-Add 修改：Modify  删除-Delete  审核-Approve
            string actionType = "Approve";
            //组织ID
            string org = "100";
            //单号：单号1,单号2,单号3
            //string docNos = "ZF20181120001,ZF20181120002";
            string docIDs = "1001904090000020";

            

            StringBuilder requestXml = new StringBuilder();
            requestXml.Append("<body>");
            requestXml.Append("<item>");
            requestXml.Append("<businessType>").Append(businessType).Append("</businessType>");
            requestXml.Append("<actionType>").Append(actionType).Append("</actionType>");
            requestXml.Append("<org>").Append(org).Append("</org>");
            requestXml.Append("<docID>").Append(docIDs).Append("</docID>");
            requestXml.Append("</item>");
            requestXml.Append("</body>");


            string data = "{\"context\":" + context + ",\"requestInfos\":\"" + requestXml.ToString() + "\"}";
            byte[] param;
            param = System.Text.Encoding.UTF8.GetBytes(data.Trim());
            request.ContentLength = param.Length;
            System.IO.Stream writer = request.GetRequestStream();
            writer.Write(param, 0, param.Length);
            writer.Close();
            System.Net.HttpWebResponse response;

            try
            {
                response = (System.Net.HttpWebResponse)request.GetResponse();
            }
            catch (WebException ex)
            {
                response = (HttpWebResponse)ex.Response;
            }

            System.IO.StreamReader myreader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
            string responseText = myreader.ReadToEnd();
            myreader.Close();
        }


        //创建请求信息Xml
        static string CreateRequestXml()
        {
            string businessType = "Rcv";
            string actionType = "Modify";

            StringBuilder xmlBuilder = new StringBuilder();

            xmlBuilder.Append("<body>");
            xmlBuilder.Append("<item>");
            xmlBuilder.Append("<businessType>").Append(businessType).Append("</businessType>");
            xmlBuilder.Append("<actionType>").Append(actionType).Append("</actionType>");

            //单据头部信息
            xmlBuilder.Append("<head>");
            xmlBuilder.Append("<PrivateDescSeg1>PO2018001</PrivateDescSeg1>");//ERP单号
            xmlBuilder.Append("<PrivateDescSeg2>2018-08-06</PrivateDescSeg2>");//ERP单据日期
            xmlBuilder.Append("<PrivateDescSeg3></PrivateDescSeg3>");//其他字段
            xmlBuilder.Append("<PrivateDescSeg4></PrivateDescSeg4>");//其他字段
            xmlBuilder.Append("<PrivateDescSeg5></PrivateDescSeg5>");
            xmlBuilder.Append("</head>");

            //单据明细信息
            xmlBuilder.Append("<details>");

            /***第一行开始***/
            xmlBuilder.Append("<detail>");
            xmlBuilder.Append("<PrivateDescSeg1>10</PrivateDescSeg1>");//ERP单据行号
            xmlBuilder.Append("<PrivateDescSeg2>ItemAAA</PrivateDescSeg2>");//ERP单据行号
            xmlBuilder.Append("<PrivateDescSeg3>品名1</PrivateDescSeg3>");//ERP单据行号
            xmlBuilder.Append("<PrivateDescSeg4>Lot001</PrivateDescSeg4>");//ERP单据行号
            xmlBuilder.Append("<PrivateDescSeg5>PJ1801</PrivateDescSeg5>");//项目号
            xmlBuilder.Append("<PrivateDescSeg6>100</PrivateDescSeg6>");//数量
            xmlBuilder.Append("<PrivateDescSeg7></PrivateDescSeg7>");//其他字段
            xmlBuilder.Append("<PrivateDescSeg8></PrivateDescSeg8>");//其他字段
            xmlBuilder.Append("</detail>");
            /***第一行结束***/

            /***第二行开始***/
            xmlBuilder.Append("<detail>");
            xmlBuilder.Append("<PrivateDescSeg1>20</PrivateDescSeg1>");//ERP单据行号
            xmlBuilder.Append("<PrivateDescSeg2>ItemBBB</PrivateDescSeg2>");//ERP单据行号
            xmlBuilder.Append("<PrivateDescSeg3>品名2</PrivateDescSeg3>");//ERP单据行号
            xmlBuilder.Append("<PrivateDescSeg4>Lot002</PrivateDescSeg4>");//ERP单据行号
            xmlBuilder.Append("<PrivateDescSeg5>PJ1802</PrivateDescSeg5>");//项目号
            xmlBuilder.Append("<PrivateDescSeg6>200</PrivateDescSeg6>");//数量
            xmlBuilder.Append("<PrivateDescSeg7></PrivateDescSeg7>");//其他字段
            xmlBuilder.Append("<PrivateDescSeg8></PrivateDescSeg8>");//其他字段
            xmlBuilder.Append("</detail>");
            /***第二行结束***/

            xmlBuilder.Append("</details>");

            xmlBuilder.Append("</item>");
            xmlBuilder.Append("</body>");

            return xmlBuilder.ToString();
        }

        //单据Head
        public static string CreateHeadXml()
        {
            string docType = "001";//单据类型编码
            string reimburseDate = DateTime.Now.ToString("yyyy-MM-dd");
            string taskCode = string.Empty;//任务
            string reimBurseOrg = "10";//报销组织编码
            string reimBurseDept = "33";//报销部门编码
            string projectCode = string.Empty;//项目 XS-DG-15-156-SY-Y
            string expenseUse = "报销款";//用途
            string reimBurseByOrg = "100";//报销人组织
            string reimBurseBy = "03013";//报销人
            string employeeBankCardType = string.Empty;//银行卡类型
            string employeeBankCardNo = string.Empty;//银行卡号
            string reimBurseCurrency = string.Empty;//币种符号：人民币：CNY,美元：USD
            decimal sumReimburseMoney = 800;//报销金额

            StringBuilder xmlDetails = new StringBuilder(@"<Body>");
            //只能有一行数据
            xmlDetails.Append("<Item>");
            xmlDetails.Append("<PrivateDescSeg1>").Append(docType).Append("</PrivateDescSeg1>");
            xmlDetails.Append("<PrivateDescSeg2>").Append(reimburseDate).Append("</PrivateDescSeg2>");
            xmlDetails.Append("<PrivateDescSeg3>").Append(taskCode).Append("</PrivateDescSeg3>");
            xmlDetails.Append("<PrivateDescSeg4>").Append(reimBurseOrg).Append("</PrivateDescSeg4>");
            xmlDetails.Append("<PrivateDescSeg5>").Append(reimBurseDept).Append("</PrivateDescSeg5>");
            xmlDetails.Append("<PrivateDescSeg6>").Append(projectCode).Append("</PrivateDescSeg6>");
            xmlDetails.Append("<PrivateDescSeg7>").Append(expenseUse).Append("</PrivateDescSeg7>");
            xmlDetails.Append("<PrivateDescSeg8>").Append(reimBurseByOrg).Append("</PrivateDescSeg8>");
            xmlDetails.Append("<PrivateDescSeg9>").Append(reimBurseBy).Append("</PrivateDescSeg9>");
            xmlDetails.Append("<PrivateDescSeg10>").Append(employeeBankCardType).Append("</PrivateDescSeg10>");
            xmlDetails.Append("<PrivateDescSeg11>").Append(employeeBankCardNo).Append("</PrivateDescSeg11>");
            xmlDetails.Append("<PrivateDescSeg12>").Append(reimBurseCurrency).Append("</PrivateDescSeg12>");
            xmlDetails.Append("<PrivateDescSeg13>").Append(sumReimburseMoney.ToString()).Append("</PrivateDescSeg13>");
            xmlDetails.Append("</Item>");
            xmlDetails.Append("</Body>");
            return xmlDetails.ToString();
        }

        //明细
        public static string CreateDetailXml()
        {
            string reimburseCurrency = "C001";//币种编码
            string expenseItem = "SJ-0018";//费用项目编码
            string expensePayDept = "5103";//列支部门(和头上的一样)
            string expensePayBy = "03013";//列支人员(和头上的一样)
            decimal reimburseMoney = 800;//报销金额
            decimal invoiceMoney = 800;
            string project = "";//项目
            string businessUnit = "0301";//事业部
            string feeDept = "33";//费用所属部门编码

            StringBuilder xmlDetails = new StringBuilder(@"<Body>");
            //第一行数据
            xmlDetails.Append("<Item>");
            xmlDetails.Append("<PrivateDescSeg1>").Append(reimburseCurrency).Append("</PrivateDescSeg1>");
            xmlDetails.Append("<PrivateDescSeg2>").Append(expenseItem).Append("</PrivateDescSeg2>");
            xmlDetails.Append("<PrivateDescSeg3>").Append(expensePayDept).Append("</PrivateDescSeg3>");
            xmlDetails.Append("<PrivateDescSeg4>").Append(expensePayBy).Append("</PrivateDescSeg4>");
            xmlDetails.Append("<PrivateDescSeg5>").Append(reimburseMoney).Append("</PrivateDescSeg5>");
            xmlDetails.Append("<PrivateDescSeg6>").Append(invoiceMoney).Append("</PrivateDescSeg6>");
            xmlDetails.Append("<PrivateDescSeg7>").Append(project).Append("</PrivateDescSeg7>");
            xmlDetails.Append("<PrivateDescSeg8>").Append(businessUnit).Append("</PrivateDescSeg8>");
            xmlDetails.Append("<PrivateDescSeg9>").Append(feeDept).Append("</PrivateDescSeg9>");
            xmlDetails.Append("<PrivateDescSeg10></PrivateDescSeg10>");
            xmlDetails.Append("<PrivateDescSeg11></PrivateDescSeg11>");
            xmlDetails.Append("<PrivateDescSeg12></PrivateDescSeg12>");
            xmlDetails.Append("<PrivateDescSeg13></PrivateDescSeg13>");
            xmlDetails.Append("<PrivateDescSeg14></PrivateDescSeg14>");
            xmlDetails.Append("<PrivateDescSeg15></PrivateDescSeg15>");
            xmlDetails.Append("<PrivateDescSeg16></PrivateDescSeg16>");
            xmlDetails.Append("<PrivateDescSeg17></PrivateDescSeg17>");
            xmlDetails.Append("<PrivateDescSeg18></PrivateDescSeg18>");
            xmlDetails.Append("<PrivateDescSeg19></PrivateDescSeg19>");
            xmlDetails.Append("<PrivateDescSeg20></PrivateDescSeg20>");
            xmlDetails.Append("<PrivateDescSeg21></PrivateDescSeg21>");
            xmlDetails.Append("<PrivateDescSeg22></PrivateDescSeg22>");
            xmlDetails.Append("<PrivateDescSeg23></PrivateDescSeg23>");
            xmlDetails.Append("<PrivateDescSeg24></PrivateDescSeg24>");
            xmlDetails.Append("<PrivateDescSeg25></PrivateDescSeg25>");
            xmlDetails.Append("<PrivateDescSeg26></PrivateDescSeg26>");
            xmlDetails.Append("<PrivateDescSeg27></PrivateDescSeg27>");
            xmlDetails.Append("<PrivateDescSeg28></PrivateDescSeg28>");
            xmlDetails.Append("<PrivateDescSeg29></PrivateDescSeg29>");
            xmlDetails.Append("<PrivateDescSeg30></PrivateDescSeg30>");
            xmlDetails.Append("<PubDescSeg1></PubDescSeg1>");
            xmlDetails.Append("<PubDescSeg2></PubDescSeg2>");
            xmlDetails.Append("<PubDescSeg3></PubDescSeg3>");
            xmlDetails.Append("<PubDescSeg4></PubDescSeg4>");
            xmlDetails.Append("<PubDescSeg5></PubDescSeg5>");
            xmlDetails.Append("<PubDescSeg6></PubDescSeg6>");
            xmlDetails.Append("<PubDescSeg7></PubDescSeg7>");
            xmlDetails.Append("<PubDescSeg8></PubDescSeg8>");
            xmlDetails.Append("<PubDescSeg9></PubDescSeg9>");
            xmlDetails.Append("<PubDescSeg10></PubDescSeg10>");
            xmlDetails.Append("<PubDescSeg11></PubDescSeg11>");
            xmlDetails.Append("<PubDescSeg12></PubDescSeg12>");
            xmlDetails.Append("<PubDescSeg13></PubDescSeg13>");
            xmlDetails.Append("<PubDescSeg14></PubDescSeg14>");
            xmlDetails.Append("<PubDescSeg15></PubDescSeg15>");
            xmlDetails.Append("<PubDescSeg16></PubDescSeg16>");
            xmlDetails.Append("<PubDescSeg17></PubDescSeg17>");
            xmlDetails.Append("<PubDescSeg18></PubDescSeg18>");
            xmlDetails.Append("<PubDescSeg19></PubDescSeg19>");
            xmlDetails.Append("<PubDescSeg20></PubDescSeg20>");
            xmlDetails.Append("<PubDescSeg21></PubDescSeg21>");
            xmlDetails.Append("<PubDescSeg22></PubDescSeg22>");
            xmlDetails.Append("<PubDescSeg23></PubDescSeg23>");
            xmlDetails.Append("<PubDescSeg24></PubDescSeg24>");
            xmlDetails.Append("<PubDescSeg25></PubDescSeg25>");
            xmlDetails.Append("<PubDescSeg26></PubDescSeg26>");
            xmlDetails.Append("<PubDescSeg27></PubDescSeg27>");
            xmlDetails.Append("<PubDescSeg28></PubDescSeg28>");
            xmlDetails.Append("<PubDescSeg29></PubDescSeg29>");
            xmlDetails.Append("<PubDescSeg30></PubDescSeg30>");
            xmlDetails.Append("<PubDescSeg31></PubDescSeg31>");
            xmlDetails.Append("<PubDescSeg32></PubDescSeg32>");
            xmlDetails.Append("<PubDescSeg33></PubDescSeg33>");
            xmlDetails.Append("<PubDescSeg34></PubDescSeg34>");
            xmlDetails.Append("<PubDescSeg35></PubDescSeg35>");
            xmlDetails.Append("<PubDescSeg36></PubDescSeg36>");
            xmlDetails.Append("<PubDescSeg37></PubDescSeg37>");
            xmlDetails.Append("<PubDescSeg38></PubDescSeg38>");
            xmlDetails.Append("<PubDescSeg39></PubDescSeg39>");
            xmlDetails.Append("<PubDescSeg40></PubDescSeg40>");
            xmlDetails.Append("<PubDescSeg41></PubDescSeg41>");
            xmlDetails.Append("<PubDescSeg42></PubDescSeg42>");
            xmlDetails.Append("<PubDescSeg43></PubDescSeg43>");
            xmlDetails.Append("<PubDescSeg44></PubDescSeg44>");
            xmlDetails.Append("<PubDescSeg45></PubDescSeg45>");
            xmlDetails.Append("<PubDescSeg46></PubDescSeg46>");
            xmlDetails.Append("<PubDescSeg47></PubDescSeg47>");
            xmlDetails.Append("<PubDescSeg48></PubDescSeg48>");
            xmlDetails.Append("<PubDescSeg49></PubDescSeg49>");
            xmlDetails.Append("<PubDescSeg50></PubDescSeg50>");
            xmlDetails.Append("</Item>");
            //第二行数据
            xmlDetails.Append("</Body>");
            return xmlDetails.ToString();
        }
    }
}
