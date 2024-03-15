<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="AdminProduct.aspx.cs" Inherits="EcoArtisanFinal.AdminProduct" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
     <link href="<%= ResolveUrl("~/Content/Product.css") %>?v<%=DateTime.Now.Ticks %>" rel="stylesheet" type="text/css" />
    <script src="<%= ResolveUrl("~/Content/Product.js") %>?v<%=DateTime.Now.Ticks %>" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <div class="container">
        <h2>Add New Product</h2>
        <div class="row">
            <div class="col-md-6">
                <div class="form-group">
                    <label for="tb_ProdName">Product Name:</label>
                    <asp:TextBox ID="tb_ProdName" runat="server" CssClass="form-control"></asp:TextBox>
                    <asp:Label CssClass="lbl_prodNameCheck" ID="lbl_prodNameCheck" runat="server" Text=""></asp:Label>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="tb_ProdName" ErrorMessage="Empty Product Name" ForeColor="Red"></asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="col-md-6"></div>
        </div>
        <div class="row">
            <div class="col-md-6">
                <div class="form-group">
                    <label for="tb_ProdDesc">Product Description:</label>
                    <asp:TextBox ID="tb_ProdDesc" runat="server" CssClass="form-control"  TextMode="MultiLine" Rows="4"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="tb_ProdDesc" ErrorMessage="Empty Description" ForeColor="Red"></asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="col-md-6"></div>
        </div>
        <div class="row">
            <div class="col-md-6">
                <div class="form-group">
                    <label for="tb_ProdPrice">Price:</label>
                    <asp:TextBox ID="tb_ProdPrice" runat="server" CssClass="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="tb_ProdPrice" ErrorMessage="Empty Price" ForeColor="Red"></asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="col-md-6"></div>
        </div>
        <div class="row">
            <div class="col-md-6">
                <div class="form-group">
                    <label for="file_ProdImg">Upload Image:</label>
                    <asp:FileUpload CssClass="form-control-file fileclass_ProdImg" ID="file_ProdImg" runat="server" onchange="displayImage();" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="file_ProdImg" ErrorMessage="No Image Uploaded" ForeColor="Red"></asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="col-md-6">
                <p>Uploaded Image:</p>
                <asp:Image CssClass="img_Prodimg img-thumbnail" ID="img_Prodimg" runat="server" Width="148px" Height="142px"/>
            </div>
        </div>
        <div class="row">
            <div class="col-md-6">
                <div class="form-group">
                    <label for="tb_ProdQty">Product Quantity:</label>
                    <asp:TextBox ID="tb_ProdQty" runat="server" CssClass="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="tb_ProdQty" ErrorMessage="No Quantity Input" ForeColor="Red"></asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="col-md-6"></div>
        </div>
        <div class="row">
            <div class="col-md-6">
                <div class="form-group">
                    <label for="tb_ProdImpact">Product Impact:</label>
                    <asp:TextBox ID="tb_ProdImpact" runat="server" CssClass="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="tb_ProdImpact" ErrorMessage="No Impact Input" ForeColor="Red"></asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="col-md-6"></div>
        </div>
        <div class="row">
            <div class="col-md-6">
                <asp:Button ID="btn_AddProduct" runat="server" Text="Add Product" CssClass="btn btn-primary" OnClick="btn_AddProduct_Click" />
                <asp:Button ID="btn_CancelProduct" runat="server" Text="Return" OnClientClick="window.location='AdminViewProduct.aspx'; return false;" CssClass="btn btn-secondary" CausesValidation="false" OnClick="btn_CancelProduct_Click" />
            </div>
            <div class="col-md-6"></div>
        </div>
    </div>
   
        <script type="text/javascript">
            // Call your function when appropriate
            function displayImage() {
                console.log('in display image');
                var fileInput = $(".fileclass_ProdImg");
                var files = fileInput[0].files;

                // Check if files are selected
                if (files.length > 0) {
                    var fileName = files[0].name;

                    // Only update if there is a change in the file name
                    if (fileName !== fileInput.data('lastFileName')) {
                        console.log("Selected file: " + fileName);

                        // Display the selected image in the img_Prodimg element
                        var imgPreview = $(".img_Prodimg");
                        console.log(imgPreview);

                        // Check if createObjectURL is supported
                        if (URL.createObjectURL) {
                            imgPreview.attr("src", URL.createObjectURL(files[0]));
                            console.log(imgPreview.attr);
                        } else {
                            // Fallback for browsers that do not support createObjectURL
                            // You may need to implement your own fallback mechanism
                            console.error("createObjectURL is not supported");
                        }

                        // Update the hidden field value

                        // Save the current file name to data attribute
                        fileInput.data('lastFileName', fileName);
                    }
                } else {
                    // No file selected, handle as needed
                    console.log("No file selected");

                    // Optionally, you may want to reset the image preview or do something else
                    var imgPreview = $(".imgclass_ProdImg");
                    imgPreview.attr("src", ""); // Clear the image preview
                    fileInput.data('lastFileName', ''); // Reset the lastFileName data attribute
                }
            }
        </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent2" runat="server">
</asp:Content>
