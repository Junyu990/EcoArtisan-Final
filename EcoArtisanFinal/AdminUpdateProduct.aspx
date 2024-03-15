<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="AdminUpdateProduct.aspx.cs" Inherits="EcoArtisanFinal.AdminUpdateProduct" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
     <script src="/Content/Product.js?v=<%=DateTime.Now.Ticks %>" type="text/javascript"></script>
    <link href="/Content/Product.css?v=<%=DateTime.Now.Ticks %>" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Edit Product ID: <asp:Label ID="lbl_prodID" runat="server" Text=""></asp:Label></h2>
    <table class="nav-justified addProdForm">
        <tr>
            <td class="auto-style2">Product Name: </td>
            <td class="auto-style6">
                <asp:TextBox ID="tb_updProdName" runat="server"></asp:TextBox>
                <asp:Label CssClass="lbl_prodNameCheck" ID="lbl_prodNameCheck" runat="server" Text=""></asp:Label>

                <br />
                <asp:RequiredFieldValidator ID="RequiredFieldValidatorPName" runat="server" ControlToValidate="tb_updProdName" ErrorMessage="Empty Product Name" ForeColor="Red"></asp:RequiredFieldValidator>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style9">Product Description: </td>
            <td class="auto-style10">
                <asp:TextBox ID="tb_updProdDesc" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="4"></asp:TextBox>
                <br />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="tb_updProdDesc" ErrorMessage="Empty Description" ForeColor="Red"></asp:RequiredFieldValidator>
                <br />
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style2">Price: </td>
            <td class="auto-style6">
                <asp:TextBox ID="tb_updProdPrice" runat="server"></asp:TextBox>
                <br />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="tb_updProdPrice" ErrorMessage="Empty Price" ForeColor="Red"></asp:RequiredFieldValidator>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style3">Upload Image: </td>
            <td class="auto-style7">
                <asp:FileUpload CssClass="fileclass_ProdImg" ID="file_updProdImg" runat="server" onchange="displayImage();" />
                <br />
            </td>
            <td>
                <p>Uploaded Image: </p>
                <asp:Image ID="img_updProdimg" runat="server" Width="148px" Height="142px" CssClass="imgclass_ProdImg auto-style13" />

            </td>
        </tr>

        <script type="text/javascript">
            // Call your function when appropriate
            function displayImage() {
                var fileInput = $(".fileclass_ProdImg");
                var files = fileInput[0].files;

                // Check if files are selected
                if (files.length > 0) {
                    var fileName = files[0].name;

                    // Only update if there is a change in the file name
                    if (fileName !== fileInput.data('lastFileName')) {
                        console.log("Selected file: " + fileName);

                        // Display the selected image in the img_Prodimg element
                        var imgPreview = $(".imgclass_ProdImg");

                        // Check if createObjectURL is supported
                        if (URL.createObjectURL) {
                            imgPreview.attr("src", URL.createObjectURL(files[0]));
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
        <tr>
            <td class="auto-style4">Product Quantity: </td>
            <td class="auto-style8">
                <asp:TextBox ID="tb_updProdQty" runat="server"></asp:TextBox>
                <br />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="tb_updProdQty" ErrorMessage="No Quantity Input" ForeColor="Red"></asp:RequiredFieldValidator>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style11">Product Impact: </td>
            <td class="auto-style12">
                <asp:TextBox ID="tb_updProdImpact" runat="server"></asp:TextBox>
                
                <br />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="tb_updProdImpact" ErrorMessage="No Impact Input" ForeColor="Red"></asp:RequiredFieldValidator>
                
            </td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
    <asp:Button CssClass="btn btn-warning" ID="btn_UpdateProduct" runat="server" Text="Update" OnClick="btn_UpdateProduct_Click"/>
    <asp:Button CssClass="btn btn-danger btn_delProd" ID="btn_DeleteProd" runat="server" Text="Delete" CausesValidation="false"  OnClientClick="<script>confirm('Are You Sure You Want To Delete This Product?');</script>" OnClick="btn_DeleteProd_Click"/>
    <asp:Button CssClass="btn btn-secondary" ID="btn_CancelUpdate" runat="server" Text="Cancel" CausesValidation="false" OnClick="btn_CancelUpdate_Click"/>

    <script>
        function confirmDelete() {
            if (confirm("Are you sure you want to delete this product?")) {
                document.getElementsByClassName('.btn_delProd').classList.add('yesDel');
                 return true;
             } else {
                 return false;
             }
         }
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent2" runat="server">
</asp:Content>
