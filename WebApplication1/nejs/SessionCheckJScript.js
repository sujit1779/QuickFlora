// JScript File

var startSession = 0;
var SessionCheckajax = new Ajax();
function doSessionPollTime() {



    startSession = new Date();
    startSession = startSession.getTime();

    var dvObject;
    dvObject = document.getElementById("hdroot");
    //alert(dvObject.value);

    if (dvObject != null && dvObject.value!="") {
        SessionCheckajax.doGet(dvObject.value + 'SessionCheck.aspx?start=' + startSession, showpopSessionTime);
    }



}

//window.onload = doPollTime;
var pollHandSession = setTimeout(doSessionPollTime, 10000);

function showpopSessionTime(str) {

    var dvObject0;
    dvObject0 = document.getElementById("hdroot");
    //alert(dvObject0.value);

    var dvObject;
    dvObject = document.getElementById("JsCompanyID");
    var dvObject2;
    dvObject2 = document.getElementById("JSDivisionID");
    var dvObject3;
    dvObject3 = document.getElementById("JSDepartmentID");

    //alert("Value from page = " + str);
    if (dvObject != null) {
        if (dvObject.value != str) {
            alert("For your security due to inactivity, you have been logged out of the system. You will need to log back in.\nIf you need to adjust your timeout settings, please contact your system administrator.");
            window.location = dvObject0.value + 'loginform.aspx?companyID=' + dvObject.value + '&DivisionID=' + dvObject2.value + '&DepartmentID=' + dvObject3.value

        }
        
    }

    var pollHandSession = setTimeout(doSessionPollTime, 30000);
}