<%@ Page Language="C#" AutoEventWireup="true" CodeFile="showfile.aspx.cs" Inherits="showfile" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>WebOffice演示</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="css/style.css" rel="stylesheet" type="text/css" />
    <script src="js/main.js" type="text/javascript"></script>
</head>
<body>
    <%--<uc:top runat="server" ID="top1" />--%>
    <%
        string DocType = "docx";
        string filepath = "/Doc/XX项目应用实施解决方案模板.docx";

    %>
    <!--banner end-->
    <input type="hidden" id="hfdoctype" value="<%=DocType %>" />
    <input type="hidden" id="hffilepath" value="<%=filepath.Substring(1) %>" />
    <div class="w1002 container clearfix" style="min-height: 560px;">

        <div>
            <input type="button" id="btnSave" onclick="SaveDocToServer()" style="width: 61px; height: 25px; line-height: 25px; cursor: pointer; background: url(images/denglu2.jpg) no-repeat; float: right; border: none;" />

        </div>

        <!-- -----------------------------== 装载weboffice控件 ==----------------------------------->

        <script src="js/LoadWebOffice.js"></script>

        <!-- --------------------------------==  结束装载控件 ==------------------------------------->
    </div>
    <!--container end-->
</body>
</html>
<script id="clientEventHandlersJS" language="javascript">

    function WebOffice1_NotifyCtrlReady() {
    //LoadOriginalFile接口装载文件,
    //如果是编辑已有文件，则文件路径传给LoadOriginalFile的第一个参数
    document.all.WebOffice1.LoadOriginalFile("<%=filepath %>", "<%=DocType %>");
    //屏蔽标准工具栏的前几个按钮
    document.all.WebOffice1.SetToolBarButton2("Standard", 1, 1);
    document.all.WebOffice1.SetToolBarButton2("Standard", 2, 1);
    document.all.WebOffice1.SetToolBarButton2("Standard", 3, 1);
    document.all.WebOffice1.SetToolBarButton2("Standard", 6, 1);

   <%if (DocType == "doc" || DocType == "docx")
    {%>
    //屏蔽文件菜单项
    document.all.WebOffice1.SetToolBarButton2("Menu Bar", 1, 1);
    //屏蔽 保存快捷键(Ctrl+S) 
    document.all.WebOffice1.SetKeyCtrl(595, -1, 0);
    //屏蔽 打印快捷键(Ctrl+P) 
    document.all.WebOffice1.SetKeyCtrl(592, -1, 0);
    <%}
    else if (DocType == "xls" || DocType == "xlsx")
    {%>
    //屏蔽文件菜单项
    document.all.WebOffice1.SetToolBarButton2("Worksheet Menu Bar", 1, 1);
    <%} %>
}

</script>

<!-- --------------------===  Weboffice初始化完成事件--------------------- -->

<script language="javascript" for="WebOffice1" event="NotifyCtrlReady">
<!--
    WebOffice1_NotifyCtrlReady() // 在装载完Weboffice(执行<object>...</object>)控件后自动执行WebOffice1_NotifyCtrlReady方法
    //-->
</script>
</script>

<script language="javascript" event="NotifyToolBarClick(iIndex)" for="WebOffice1">
<!--
    WebOffice_Event_Flash("NotifyToolBarClick");
    WebOffice1_NotifyToolBarClick(iIndex);
    //-->
</script>
<script type="text/javascript">
    bToolBar_onclick();
    //    okSave();

    function SaveDocToServer() {
        //初始化Http引擎
        document.all.WebOffice1.HttpInit();
        //添加相应的Post元素
        //document.all.WebOffice1.HttpAddPostString("DocTitle", encodeURI(title));
        document.all.WebOffice1.HttpAddPostString("DocType", encodeURI(document.getElementById("hfdoctype").value));
        document.all.WebOffice1.HttpAddPostString("DocPath", encodeURI(document.getElementById("hffilepath").value));
        //把当前文档添加到Post元素列表中，文件的标识符?DocContent
        document.all.WebOffice1.HttpAddPostCurrFile("DocContent", "");
        var url = window.location.href.substr(0, window.location.href.lastIndexOf('/'));
        var vtRet = document.all.WebOffice1.HttpPost(url + "/upload.aspx");
        if ("succeed" == vtRet) {
            alert("文件保存成功");
        } else {
            alert("文件保存失败");
        }
    }
</script>

