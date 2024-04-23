// JScript File

var start = 0;
var ajax = new Ajax();

function logoff(str) 
    {
    //alert("closing....");
    start = new Date();
    start = start.getTime();
    
    //alert('PopLogoffemployee.aspx?start=' + start + ', showpop');
    
    ajax.doGet('PopLogoffemployee.aspx?'+ str  + '&start=' + start, showpop);
        
    }
    
 function showpop(str)
 {
 
    //alert(str); 
    
 }