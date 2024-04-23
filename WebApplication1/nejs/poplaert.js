// JScript File

var start = 0;
var ajax = new Ajax();
function  doPollTime() 
    {
    
    
    start = new Date();
    start = start.getTime();
    ajax.doGet('AjaxTime.aspx?start=' + start, showpopTime);
        
    }


    
doPollTime();

     
function showpopTime(str)
{

    

    var dvObject;
    dvObject=document.getElementById("dvtime");

 if(dvObject!=null)
        {
    dvObject.innerHTML=str;
    }
    
    var ctl00_StatusBar_lbCurrentDate;
    ctl00_StatusBar_lbCurrentDate=document.getElementById("ctl00_StatusBar_lbCurrentDate");

 if(ctl00_StatusBar_lbCurrentDate!=null)
        {
    ctl00_StatusBar_lbCurrentDate.innerHTML=str;    
    
    }
    var pollHand = setTimeout(doPollTime, 30000);
}

 



