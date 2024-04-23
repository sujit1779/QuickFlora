// JScript File
function HelloWorld() 
{
this.message = 'Hello, world!';
this.sayMessage = function() {
                                window.alert(this.message);
                             };
}


function Ajax()
 {
    this.req = null;
    this.url = null;
    this.method = 'GET';
    this.async = true;
    this.status = null;
    this.statusText = '';
    this.postData = null;
    this.readyState = null;
    this.responseText = null;
    this.responseXML = null;
    this.handleResp = null;
    this.responseFormat = 'text'; // 'text', 'xml', or 'object'
    this.mimeType = null;
    this.setMimeType = function(mimeType) 
    {
        this.mimeType = mimeType;
    };
    
    this.init = function() 
    {
        if (!this.req) 
        {
        try {
            // Try to create object for Firefox, Safari, IE7, etc.
            this.req = new XMLHttpRequest();
            }
        catch (e) 
        {
          try 
             {
              // Try to create object for later versions of IE.
              this.req = new ActiveXObject('MSXML2.XMLHTTP');
             }
         catch (e) 
             {
                try 
                {
                // Try to create object for early versions of IE.
                this.req = new ActiveXObject('Microsoft.XMLHTTP');
                }
                catch (e) 
                {
                // Could not create an XMLHttpRequest object.
                return false;
                }
             }
        }
    }
    return this.req;
    };
 
 
    this.doReq = function() 
    {
        if (!this.init()) 
        {
            alert('Could not create XMLHttpRequest object.');
            return;
        }
        this.req.open(this.method, this.url, this.async);
        if (this.mimeType)
         {
            try {
            req.overrideMimeType(this.mimeType);
            }
            catch (e) {
            // couldn't override MIME type -- IE6 or Opera?
            }
        }
        var self = this; // Fix loss-of-scope in inner function
        this.req.onreadystatechange = function() 
            {
                if (self.req.readyState == 4) 
                {
                // Do stuff to handle response
                var resp = null;
                
                 switch (self.responseFormat) 
                  {
                    case 'text':
                    resp = self.req.responseText;
                    //alert(resp);
                    break;
                    case 'xml':
                    resp = self.req.responseXML;
                    break;
                    case 'object':
                    resp = req;
                    break;
                  }
                  if (self.req.status >= 200 && self.req.status <= 299) 
                  {
                    self.handleResp(resp);
                  }
                  else 
                  {
                    self.handleErr(resp);
                  }
                    
                }//end of if
            };
            
       this.req.send(this.postData);
        
    };
    
    
    
    
    
    
    this.handleErr = function() 
    {
        var errorWin;
        try {
        errorWin = window.open('', 'errorWin');
        errorWin.document.body.innerHTML = this.responseText;
        }
        catch (e) {
        alert('An error occurred, but the error message cannot be '
        + 'displayed. This is probably because of your browser\'s '
        + 'pop-up blocker.\n'
        + 'Please allow pop-ups from this web site if you want to '
        + 'see the full error messages.\n'
        + '\n'
        + 'Status Code: ' + this.req.status + '\n'
        + 'Status Description: ' + this.req.statusText);
        }
    }; 
    
    this.abort = function()
    {
    if (this.req) {
    this.req.onreadystatechange = function() { };
    this.req.abort();
    this.req = null;
    }
    };
    
    this.doGet = function(url, hand, format) 
    {
        
        this.url = url;
        this.handleResp = hand;
        this.responseFormat = format || 'text';
        this.doReq();
    };

 }//end of class
 
 
 
 function ScopeTest() 
 {
    this.message = 'Greetings from ScopeTest!';
    this.doTest = function() 
    {
        alert(this.message);
    };
 }