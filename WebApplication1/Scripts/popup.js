iframes = new Array();

function getAbsPos(el)
{
    var p = { x: el.offsetLeft, y: el.offsetTop };
    if (el.style.position != 'absolute' && el.offsetParent &&
        el.offsetParent != el) {
        var p2 = getAbsPos(el.offsetParent);
        p.x += p2.x;
        p.y += p2.y;
    }
    return p;
}

function setEventHandler(el, eventName, f)
{
    if (el.attachEvent)// IE
        el.attachEvent('on' + eventName, f);
    else if (el.addEventListener)// Gecko / W3C
        el.addEventListener(eventName, f, true);
    else // Opera (or old browsers)
        el['on' + eventName] = f;
}

function getEventElement(ev)
{
    if (ev.currentTarget)
        return ev.currentTarget;
    else
        return window.event.srcElement;
}

function getIFrameDoc(iframe)
{
    var iframedoc;
    if (iframe.contentDocument)
        return iframe.contentDocument; // NS6
    else if (iframe.contentWindow)
        return iframe.contentWindow.document; // IE5.5 & IE6
    else if (iframe.document)
        return iframe.document; // IE5
    else
        return null;
}

function showPopup(text_element_id, value_element_id, src, h, w, leftTopBorder, rightBottomBorder)
{
    if (src == "")
        return;
    var text_el = document.getElementById(text_element_id);
    var anchor_el = text_el;
    while (anchor_el.nodeName.toUpperCase() != 'TABLE') {
        anchor_el = anchor_el.parentNode;
        if (!anchor_el) {
            anchor_el = text_el;
            break;
        }
    }
    var iframe = text_el.popupIFrame;

  	if (document.popupActive && document.popupActive != iframe) {
	    document.popupActive.style.visibility = 'hidden';
        document.popupActive.style.display = 'none';
	}

    if (!iframe) {
        iframe = text_el.popupIFrame = document.createElement('iframe');
		iframe.style.height = h + 'px';
		iframe.style.position = 'absolute';
		iframe.style.zIndex = 3000;
		iframe.style.borderTop = iframe.style.borderLeft = leftTopBorder;
		iframe.style.borderBottom = iframe.style.borderRight = rightBottomBorder;
		iframe.frameBorder = 0;
		iframe.src = src;
		document.body.appendChild(iframe);
		setEventHandler(iframe, 'load', iframeLoaded);
		if (!document.popupInitialized)
		    setEventHandler(document, 'click', checkClick);
	} else
	    getIFrameDoc(iframe).location.replace(src);
	if(w==0)
		w = (anchor_el.offsetWidth - 4);
	iframe.style.width = w + 'px';

	iframe.popupShow = true;
	var p = getAbsPos(anchor_el);
	p.y += anchor_el.offsetHeight;
	if (p.y < document.body.scrollTop)
	    p.y = document.body.scrollTop;
	else if (p.y + h > document.body.scrollTop + document.body.clientHeight - 5) {
		if ((p.y-anchor_el.offsetHeight-document.body.scrollTop)>(document.body.scrollTop + document.body.clientHeight-p.y) && p.y>h+anchor_el.offsetHeight )
			p.y-=h+anchor_el.offsetHeight;	
    if (p.y < 0)
	        p.y = 0;
	}
	if (p.x + w > document.body.clientWidth-document.body.scrollLeft-5)
	{
	    p.x = p.x -(w - anchor_el.offsetWidth);
	    if (p.x < 0)
	        p.x = 0;
	}
	iframe.style.left = p.x;
	iframe.style.top = p.y;
	iframe.text_el = text_el;
	iframe.value_el = document.getElementById(value_element_id);
    iframes.push(iframe);
}

function iframeLoaded(ev)
{
    var iframe = getEventElement(ev);
    if (iframe.popupShow) {
    	iframe.style.visibility = 'visible';
	    iframe.style.display = 'block';
		iframe.popupShow = false;
		document.popupActive = iframe;
	}
}

function checkClick(ev)
{
    if (!document.popupActive)
	    return;

    var el = ev.target;
    if (!el)
        el = window.event.srcElement;

	var exc = null;
    for (; el; el = el.parentNode) {
        if (el == document.popupActive)
		    return;
    }

  	document.popupActive.style.visibility = 'hidden';
    document.popupActive.style.display = 'none';
	document.popupActive = null;
}

function getIFrame(doc)
{
    for (var i = 0; i < iframes.length; ++i) {
        var iframe = iframes[i];
        if (getIFrameDoc(iframe) == doc)
            return iframe;
    }

    return null;
}

function hideIFrame(iframe)
{
    iframe.style.visibility = 'hidden';
	iframe.style.display = 'none';
	document.popupActive = null;
}

function setFieldData(doc, text, value)
{
    var iframe = getIFrame(doc);
    if (iframe) {
	    iframe.text_el.value = text;
	    iframe.value_el.value = value;
	    var onchange = iframe.text_el.getAttribute('onchange');
        if(onchange)
        {
            if (onchange instanceof Function)
                onchange();
            else
                eval(onchange);
        }
	    hideIFrame(iframe);
	}
}

function closePopup(doc)
{
    var iframe = getIFrame(doc);
    if (iframe)
	    hideIFrame(iframe);
}

function adjustFieldWidth(element_id)
{
    var el = document.getElementById(element_id);
    el.style.width = el.parentNode.offsetWidth + 'px';
}
