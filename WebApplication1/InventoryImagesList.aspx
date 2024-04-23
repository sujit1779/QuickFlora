<%@ Page MasterPageFile="~/MainMaster.master"  AutoEventWireup="false" CodeFile="InventoryImagesList.aspx.vb" Inherits="InventoryImagesList" %>
 
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" type="text/css" href="assets/global/plugins/select2/select2.css" />
 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" runat="Server">
    Items Images List
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder" runat="Server">
 
<center>
   
    <table class="table table-striped table-bordered table-hover"  border="0" width="100%" id="table1">
 
	<tr>
		<td style="width: 180px">
            <asp:DropDownList ID="drpFieldName" CssClass="form-control input-sm select2me" runat="server" >
                  <asp:ListItem Value="ItemID">Item ID</asp:ListItem>
                    <asp:ListItem Value="ItemName">Item Name</asp:ListItem>
              
            </asp:DropDownList>

		</td>
		<td style="width: 180px">
            <asp:DropDownList ID="drpCondition"   CssClass="form-control input-sm select2me" runat="server">
		
		    <asp:ListItem Value="=">=</asp:ListItem>
             
                <asp:ListItem Value="Like">Like</asp:ListItem>
            </asp:DropDownList>

		</td>
		<td  > <asp:TextBox ID="txtSearchExpression" runat="server" CssClass="form-control input-sm"></asp:TextBox>  </td>
		<td>
            <asp:Button ID="btnSearch" CssClass="btn btn-success btn-xs" runat="server" Text="Search" /> </td>
	</tr>
	 
</table>
     <asp:Label ID="lblErr" runat="server" Text=" " Width="200px"></asp:Label>
    <div style='z-index: 90; height: 100%' class="table-responsive" >
    
    <asp:GridView ID="grdInventory" DataKeyNames="ItemID"  AutoGenerateColumns="false"  AllowPaging="true" runat="server" CssClass="table table-striped table-bordered table-hover"
        
        PageSize="10">
                <Columns>
                 
            
                 <asp:BoundField HeaderText="ItemID"  DataField="ItemID" >
                     
                 </asp:BoundField>
       <asp:BoundField HeaderText="ItemName"  DataField="ItemName" >
                    
                 </asp:BoundField>
              
                 
                    <asp:TemplateField    HeaderText="Thumbnail Image">
                       <ItemTemplate >
                           <a  onclick="Javascript:AddStocknew('<%# DataBinder.Eval(Container, "DataItem.ItemID").ToString() %>','thm');"   href="Javascript:;"  class="btn default btn-xs green">
                         <img id="thm<%# DataBinder.Eval(Container, "DataItem.ItemID").ToString() %>"  width="100" height="100" src="<%# returl(DataBinder.Eval(Container, "DataItem.ThumbnailImage").ToString()) %>" /> 
                          </a> 
                       </ItemTemplate>
                           
                       </asp:TemplateField>
                 
                    <asp:TemplateField    HeaderText="Small Image">
                       <ItemTemplate >
                           <a  onclick="Javascript:AddStocknew('<%# DataBinder.Eval(Container, "DataItem.ItemID").ToString() %>','sm');"   href="Javascript:;"  class="btn default btn-xs green">
                         <img id="sm<%# DataBinder.Eval(Container, "DataItem.ItemID").ToString() %>"  width="100" height="100" src="<%# returl(DataBinder.Eval(Container, "DataItem.PictureURL").ToString()) %>" /> 
                          </a> 
                       </ItemTemplate>
                           
                       </asp:TemplateField>
                          <asp:TemplateField HeaderText="Medium Image"   >
                       <ItemTemplate >
                            <a  onclick="Javascript:AddStocknew('<%# DataBinder.Eval(Container, "DataItem.ItemID").ToString() %>','md');"   href="Javascript:;"  class="btn default btn-xs green">
                           <img width="100" height="100"  id="md<%# DataBinder.Eval(Container, "DataItem.ItemID").ToString() %>"  src="<%# returl(DataBinder.Eval(Container, "DataItem.MediumPictureURL").ToString()) %>" /> 
                                </a> 
                       </ItemTemplate>
                       </asp:TemplateField>
                 
                  <asp:TemplateField HeaderText="Large Image"   >
                       <ItemTemplate >
                         <a  onclick="Javascript:AddStocknew('<%# DataBinder.Eval(Container, "DataItem.ItemID").ToString() %>','lg');"   href="Javascript:;"  class="btn default btn-xs green">
                            <img  width="100" height="100" id="lg<%# DataBinder.Eval(Container, "DataItem.ItemID").ToString() %>"  src="<%# returl(DataBinder.Eval(Container, "DataItem.LargePictureURL").ToString()) %>" /> 
                        </a> 
                       </ItemTemplate>
    
                </asp:TemplateField>
                 </Columns>
                </asp:GridView>
    
    </div> 
        
        </center>





    
    <!-- /.modal -->
							<div id="responsive" class="modal fade" tabindex="-1" aria-hidden="true">
								<div class="modal-dialog">
									<div class="modal-content">
										<div class="modal-header">
											<button type="button" class="close" data-dismiss="modal" aria-hidden="true"></button>
											<h4 class="modal-title"><b>Update Item Image </b></h4>
										</div>
                                        <%--ItemID,ItemName,Price,BHID,PictureURL,BinId--%>
										<div class="modal-body">
											<div class="scroller" style="height:550px" data-always-visible="1" data-rail-visible1="1">
												<div class="row">
													<div class="col-md-6">
														<h4><b>Item Image</b></h4>
                                                        <hr />
                                                        <p>
                                                            <img class="img-thumbnail" id="imgmitem" style="max-width: 90%;" src="https://secure.quickflora.com/itemimages/no_image.gif" />
                                                        </p>
														<p>
                                                            <strong>Item ID</strong>
															<input type="text" readonly="true" id="txtmItemID" class="col-md-12 form-control">
														</p>
														<p>
                                                            <strong>Item Name</strong>
															<input type="text" readonly="true" id="txtmItemName" class="col-md-12 form-control">
														</p>
														 
														<p>
                                                            <strong>Price</strong>
															<input type="text" readonly="true" id="txtmPrice" class="col-md-12 form-control">
														</p>
                                                        
														 
													</div>
													<div class="col-md-6">
														<h4><b>Upload New Image <span id="imgtype"></span>  </b></h4>
														   <hr />
														<div>
                                                              
                                                       <input type="file" class="dropzone-select btn btn-light-primary font-weight-bold btn-sm dz-clickable" id="homePhoto">
                                                    <br />
                                                    <img   id="imgspin" />
                                                    <br />
													<a  href="Javascript:;"  id="but_upload" class="btn btn-primary text-uppercase font-weight-bolder px-15 py-3">Upload</a>
                                                    <br />
													   
														 
													</div>
												</div>
											</div>
										</div>
										<div class="modal-footer">
											<button type="button" data-dismiss="modal" class="btn default">Close</button>
											 <div id="dvtype" style="display:none;"></div>
										</div>
									</div>
								</div>
							</div>
							<div class="modal fade" id="ajax" tabindex="-1" role="basic" aria-hidden="true">
								<img src="assets/img/ajax-modal-loading.gif" alt="" class="loading">
							</div>
							<!-- /.modal -->

</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
      
    <!-- END PAGE LEVEL SCRIPTS -->
<script>
     // Format the price above to USD, INR, EUR using their locales.
let dollarUS = Intl.NumberFormat("en-US", {
    style: "currency",
    currency: "USD",
    });

    jQuery(document).ready(function () {
 
          $("#but_upload").click(function () {
               // alert('hi');
                
               document.getElementById('imgspin').src = 'https://secure.quickflora.com/images/wait.gif';
              var fd = new FormData();
              var files = $('#homePhoto')[0].files[0];
              fd.append('file', files);
                var filename = "";
                filename = files.name;  
                //alert(filename);
              $.ajax({
                  url: 'upload.aspx?itemid=' + $('#txtmItemID').val() + '&type=' + $('#dvtype').html(),
                  type: 'post',
                  data: fd,
                  contentType: false,
                  processData: false,
                  success: function (response) {
                      if (response != 0) {
                          document.getElementById('imgmitem').src = 'https://secure.quickflora.com/itemimages/' + response; 
                          document.getElementById($('#dvtype').html() + $('#txtmItemID').val()).src = 'https://secure.quickflora.com/itemimages/' + response; 
                          document.getElementById('imgspin').src = '';
                      }
                      else {
                          alert('file not uploaded');
                      }
                  },
              });
          });




    });      
        
    function AddStock(data,type) {
       // alert('AddStock');
        $.each(data, function (key, value) {
            $('#txtmItemID').val(value.ItemID);
            $('#txtmItemName').val(value.ItemName);
            $('#txtmPrice').val(value.Price);
            $('#dvtype').html(type);

            if (type == "sm")
                $("#imgmitem").attr("src", "https://secure.quickflora.com/itemimages/" + value.PictureURL);

             if(type=="md")
                $("#imgmitem").attr("src", "https://secure.quickflora.com/itemimages/" + value.MediumPictureURL);

             if(type=="lg")
                $("#imgmitem").attr("src", "https://secure.quickflora.com/itemimages/" + value.LargePictureURL);

             if(type=="thm")
                $("#imgmitem").attr("src", "https://secure.quickflora.com/itemimages/" + value.ThumbnailImage);
        });
       
        //$("#responsive").modal('show');
    }

    function AddStocknew(Itemid,type) {
       // alert('AddStocknew');
        if(type=="sm")
            $("#imgtype").html("Small");

         if(type=="md")
            $("#imgtype").html("Medium");

         if(type=="lg")
            $("#imgtype").html("Large");

         if(type=="thm")
            $("#imgtype").html("Thumbnail");

        $("#responsive").modal('show');
       // return;
        $.getJSON("Ajaxitemsimgdetails.aspx?Itemid=" + Itemid, function (data) {
                 AddStock(data,type);
        });
         
    }
   
     
</script>
<!-- END JAVASCRIPTS -->
   
    <script type="text/javascript" >
 
       
</script>
</asp:Content>

