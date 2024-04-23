// JavaScript support functions
function addOption(select_el, value, label)
{
    var option = document.createElement("option");
    option.value = value;
    var label = document.createTextNode(label);
    option.appendChild(label);
    select_el.appendChild(option);
}

function populateCombo(select_el, items)
{
    while (select_el.length > 0)
    {
        select_el.remove(0);
    }
    
    for (var i = 0; i < items.length; ++i)
    {
        addOption(select_el, items[i], items[i]);
    }
}

function getUpdateMark()
{
    return document.getElementById("updateMark");
}

function startUpdate()
{
    var mark = getUpdateMark();
    if (mark)
    {
        mark.style.visibility = 'visible';
    }
}

function endUpdate()
{
    var mark = getUpdateMark();
    if (mark)
    {
        mark.style.visibility = 'hidden';
    }
}

function escapeHTML(text)
{
    var divEl = document.createElement('div');
    var textNode = document.createTextNode(text);
    divEl.appendChild(textNode);
    return divEl.innerHTML;
}

// Login form support

var cmbDivisionID = null;
var cmbDepartmentID = null;

function handleCompanyChange(result, context)
{
    var lists = result.split("#");
    var divisionList = lists[0].split(";");
    var departmentList = lists[1].split(";");
    populateCombo(document.getElementById(cmbDivisionID), divisionList);
    populateCombo(document.getElementById(cmbDepartmentID), departmentList);
    endUpdate();
}

function handleDivisionChange(result, context)
{
    var departmentList = result.split(";");
    populateCombo(document.getElementById(cmbDepartmentID), departmentList);
    endUpdate();
}

// onload actions

var onloadActions = new Array();

window.onload = function ()
{
    for(var i = 0; i < onloadActions.length; ++i)
    {
        (onloadActions[i])();
    }
}

function addOnLoadAction(action)
{
    onloadActions[onloadActions.length] = action;
}

function addOnLoadAlert(text)
{
    addOnLoadAction(function() { alert(text); });
}

var lastX;

function registerLayout()
{
	document.body.onresize = applyLayout;
	applyLayout();
}

function applyLayout()
{
	var header = document.getElementById('header');
	var leftPanel = document.getElementById('link-panel');
	var contentPanel = document.getElementById('content-panel');
	var footer = document.getElementById('footer');
	
	var headerHeight = header.offsetHeight;
	var footerHeight = footer.offsetHeight;
	
	var contentHeight = document.body.clientHeight - headerHeight - footerHeight;
	if (contentHeight < 50)
	    return;

	leftPanel.style.height = contentHeight + 'px';
	contentPanel.style.height = contentHeight + 'px';
	
	var leftPanelWidth = leftPanel.offsetWidth;	
	var fullWidth = footer.offsetWidth;	

	contentPanel.style.width = fullWidth - leftPanelWidth - 8 +'px';
	var contentPanelClientWidth = contentPanel.clientWidth;
	contentPanel.style.overflow='auto';
    var children = contentPanel.children;
    if (children != null){
       if (children.length != null){
          for (i = 0; i < children.length; i++){
             if(children(i).tagName=='TABLE' )
                children(i).style.width=contentPanelClientWidth +'px';
          }
       }
       if(contentPanel.offsetWidth !=contentPanelClientWidth)
       {
           contentPanel.style.overflow='auto';
       }
    } 

	if (navigationPanelResize)
	{
	    navigationPanelResize(leftPanel);
	}
}


