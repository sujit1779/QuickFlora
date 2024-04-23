
<%@ Page Language="VB" AutoEventWireup="false" CodeFile="QFPOSLogin.aspx.vb" Inherits="QFPOSLogin" %>
<%@ Register TagPrefix="stfb" TagName="common" Src="common.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<!--[if IE 7]> <html lang="en" class="ie7 no-js"> <![endif]-->
<!--[if IE 8]> <html lang="en" class="ie8 no-js"> <![endif]-->
<!--[if IE 9]> <html lang="en" class="ie9 no-js"> <![endif]-->
<!--[if !IE]><!-->
<html lang="en">
<!--<![endif]-->
<head runat="server">
<meta charset="utf-8"/>
<title>FLORICA Module Login</title>



<meta http-equiv="X-UA-Compatible" content="IE=edge">
<meta content="width=device-width, initial-scale=1.0" name="viewport"/>
<meta http-equiv="Content-type" content="text/html; charset=utf-8">
<meta content="" name="description"/>
<meta content="" name="author"/>
<meta name="robots" content="noindex">
<!-- BEGIN GLOBAL MANDATORY STYLES -->
<link href="http://fonts.googleapis.com/css?family=Open+Sans:400,300,600,700&subset=all" rel="stylesheet" type="text/css"/>
<link href="assets/global/plugins/font-awesome/css/font-awesome.min.css" rel="stylesheet" type="text/css"/>
<link href="assets/global/plugins/simple-line-icons/simple-line-icons.min.css" rel="stylesheet" type="text/css"/>
<link href="assets/global/plugins/bootstrap/css/bootstrap.min.css" rel="stylesheet" type="text/css"/>
<link href="assets/global/plugins/uniform/css/uniform.default.css" rel="stylesheet" type="text/css"/>
<!-- END GLOBAL MANDATORY STYLES -->
<!-- BEGIN PAGE LEVEL STYLES -->
<link href="assets/admin/pages/css/login.css" rel="stylesheet" type="text/css"/>
<!-- END PAGE LEVEL SCRIPTS -->
<!-- BEGIN THEME STYLES -->
<link href="assets/global/css/components.css" id="style_components" rel="stylesheet" type="text/css"/>
<link href="assets/global/css/plugins.css" rel="stylesheet" type="text/css"/>
<link href="assets/admin/layout/css/layout.css" rel="stylesheet" type="text/css"/>
<link href="assets/admin/layout/css/themes/darkblue.css" rel="stylesheet" type="text/css" id="style_color"/>
<link href="assets/admin/layout/css/custom.css" rel="stylesheet" type="text/css"/>
<!-- END THEME STYLES -->


         <style  type="text/css">
            #fixed1
            {
                position: fixed;
                top: 200px;
                right: 0px;
                width: 25px;
                height: 80px;
                background-color: #7AD624;
            }
            
            #overlay
            {
                visibility: hidden;
                position: absolute;
                left: 0px;
                top: 0px;
                width: 100%;
                height: 100%;
                text-align: center;
                z-index: 1000;
                background-image: url(https://secure.quickflora.com/EnterpriseASP/images/TransContentBackground.png);
                background-repeat: repeat;
            }
            
            #overlay div
            {
                width: 550px;
                margin: 50px auto;
                background-color: #fff;
                border: 4px solid #A0A0A0;
                padding: 15px;
                text-align: center;
            }
        </style>
	


<link rel="shortcut icon" href="favicon.ico"/>

<!-- HTML5 shim and Respond.js for IE8 support of HTML5 elements and media queries -->
<!--[if lt IE 9]>
  <script src="https://oss.maxcdn.com/html5shiv/3.7.2/html5shiv.min.js"></script>
  <script src="https://oss.maxcdn.com/respond/1.4.2/respond.min.js"></script>
<![endif]-->



</head>
<body  class="login">

<!-- BEGIN SIDEBAR TOGGLER BUTTON -->
<div class="menu-toggler sidebar-toggler">
</div>
<!-- END SIDEBAR TOGGLER BUTTON -->


<!-- BEGIN LOGO -->
<div class="logo">

    <%= lblCompany.Text%>
	 <asp:Label Visible="false"  class="form-title" ID="lblCompany" runat="server" Text=""> </asp:Label> 
     <asp:Label Visible="false"  class="form-title" ID="lbllocationid" runat="server" Text=""></asp:Label>
    <asp:Label Visible="false"  class="form-title" ID="lblTerminalID" runat="server" Text=""></asp:Label>

</div>
<!-- END LOGO -->

    <form class="login-form" id="form1" runat="server">
   <!-- BEGIN LOGIN -->
<div class="content">
	<!-- BEGIN LOGIN FORM -->
	   <h3 style=" font-size:20px;" class="form-title"><%=FLORICA %></h3>

		<div class="<%=CSScls %>">
			<button class="close" data-close="alert"></button>
			<span>
				 Enter correct username and password.
			</span>
		</div>
		
        <div class="form-group">
			<!--ie8, ie9 does not support html5 placeholder, so we just show field title for that-->
			<label class="control-label visible-ie8 visible-ie9">Username</label>
			<asp:TextBox ID="TBLogin" class="form-control form-control-solid placeholder-no-fix"   runat="server"></asp:TextBox>
		</div>
		<div class="form-group">
			<label class="control-label visible-ie8 visible-ie9">Password</label>
			<asp:TextBox ID="TBPassword" class="form-control form-control-solid placeholder-no-fix" TextMode="Password"  runat="server"></asp:TextBox>
		</div>


		<div class="form-actions">
        	 
			<button type="submit" class="btn btn-success pull-right">Login</button>
			<label class="rememberme check">
			<input type="checkbox" name="remember" value="1"/>Remember </label>
			
		</div>
	    <div class="form-footer-link">
	    	   
        	<a href="javascript:;" id="forget-password" class="forget-password">Forgot Password?</a>
		</div>
	   
	   <div class="form-footer-link">
            <script type="text/javascript" src="https://sealserver.trustwave.com/seal.js?code=55d9abf642624aea83d3dbaa33ca8123"></script><img style="padding-left:20px; border:0;" src="https://secure.quickflora.com/images/awslog.png" />
             
            <asp:Panel ID="Panel1"   runat="server" Visible="false">

                    
             <br /> 
                <p><b><u> Expanded V Day Support Options for 2024</u></b></p>
             <%--<br />--%> 

<p>We have expanded the support hours and staff to better assist you in resolving all cases for Valentines Day until February 14th at midnight PST. </p>
             <br /> 

<p><u>Reminder:</u></p>
             <%--<br />--%> 

                 <p>
                 Please hit the <b>“HELP”</b> button on the right side of your screen to open new support cases. A technical support may call you directly to follow up by phone in many cases. We do not accept support cases by phone from Feb 12-14th.  
                     </p>
             <%--<br />--%> 
                 <p>
                 You may also email <a href="mailto:support@quickflora.com">support@quickflora.com</a> directly. 
                     </p>
             <%--<br />--%> 
                 <p>
                 The support manual is also online at <a target="_blank" href="http://www.quickfloramanual.com/">http://www.quickfloramanual.com/</a>
                     </p>
             <%--<br />--%> 
                 <p>
The Support Team at QuickFlora 
                     </p>
               
            
             </asp:Panel> 
             
               
        </div> 
	<!-- END LOGIN FORM -->
 
	 
</div>
<!-- END LOGIN -->
    </form>
<div class="copyright">
	 <p>Copyright <%=DateTime.Now.Year %> © <strong>QuickFlora</strong>, All Rights Reserved.</p>
	 
</div>
 <div class="login-footer">
	<p><a href="http://www.quickflora.com" target="_blank" class="fa fa-spin"><img src="assets/admin/layout/img/icon-quick-flora.png" alt="QuickFlora"/></a></p><a href="http://www.quickflora.com" target="_blank"><img src="assets/admin/layout/img/logo-quick-flora-text.png" alt="QuickFlora"/></a>
</div>


<script type="text/javascript">
    function overlay() {
        el = document.getElementById("overlay");
        el.style.visibility = (el.style.visibility == "visible") ? "hidden" : "visible";
    }
</script>


<div  style="padding:5px;position: fixed; top: 280px; right:0px;width: 25px;  background-color: #7AD624;" > <a style="text-decoration:none;" href='#' onclick='overlay()'> <p style="margin:5px;font-family:Arial;color:White;font-weight:bolder; font-size:14px;"> H<br />E<br />L<br />P  </p> </a> </div>

<div id="overlay">
     <div>
           
          
           <IFRAME id="suport" src='https://secure.quickflora.com/EnterpriseASP/scripts/Support.aspx?CompID=<%= JsCompanyID%>&DivID=<%= JSDivisionID%>&DeptID=<%= JSDepartmentID%>&UserName=NEWPOS@<%= JSUserName%>'      frameBorder=no width="540" height="500" ></IFRAME>

          <a href='#' style="margin:5px;font-family:Arial;color:White;font-weight:bolder; font-size:14px; color:Red;" onclick='overlay()'>[Close]</a> 

     </div>
</div> 


<!-- END LOGIN -->
<!-- BEGIN JAVASCRIPTS(Load javascripts at bottom, this will reduce page load time) -->
<!-- BEGIN CORE PLUGINS -->
<!--[if lt IE 9]>
<script src="assets/global/plugins/respond.min.js"></script>
<script src="assets/global/plugins/excanvas.min.js"></script> 
<![endif]-->
<script src="assets/global/plugins/jquery.min.js" type="text/javascript"></script>
<script src="assets/global/plugins/jquery-migrate.min.js" type="text/javascript"></script>
<script src="assets/global/plugins/bootstrap/js/bootstrap.min.js" type="text/javascript"></script>
<script src="assets/global/plugins/jquery.blockui.min.js" type="text/javascript"></script>
<script src="assets/global/plugins/jquery.cokie.min.js" type="text/javascript"></script>
<script src="assets/global/plugins/uniform/jquery.uniform.min.js" type="text/javascript"></script>
<!-- END CORE PLUGINS -->
<!-- BEGIN PAGE LEVEL PLUGINS -->
<script src="assets/global/plugins/jquery-validation/js/jquery.validate.min.js" type="text/javascript"></script>
<!-- END PAGE LEVEL PLUGINS -->
<!-- BEGIN PAGE LEVEL SCRIPTS -->
<script src="assets/global/scripts/metronic.js" type="text/javascript"></script>
<script src="assets/admin/layout/scripts/layout.js" type="text/javascript"></script>
<script src="assets/admin/layout/scripts/demo.js" type="text/javascript"></script>
<script src="assets/admin/pages/scripts/login.js" type="text/javascript"></script>
<!-- END PAGE LEVEL SCRIPTS -->
<script>
    jQuery(document).ready(function () {
        Metronic.init(); // init metronic core components
        Layout.init(); // init current layout
        Login.init();
        Demo.init();
    });
</script>
<!-- END JAVASCRIPTS -->


    <script type="text/javascript">
        function checkbrowser() {
            if ($.browser.mozilla) {
                // alert(2);
            } else {
                var msg = "";
                msg = msg + "This application is optimized for the Firefox Mozilla browser. Use of any other non-compatible browsers will not provide all program functions.";
                msg = msg + "\n\nPlease visit www.firefox.com to download a free copy. ";
                msg = msg + "\n\nIf you need assistance configuring your computer for Firefox, please email support@quickflora.com for further assistance during normal business hours. ";

                alert(msg);
                //alert("This application is compatible with mozilla firefox");
            }

            return 0;
        }

        checkbrowser();

    </script>

</body>
</html>
