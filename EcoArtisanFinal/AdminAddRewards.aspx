<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="AdminAddRewards.aspx.cs" Inherits="EcoArtisanFinal.AdminAddRewards" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <nav aria-label="breadcrumb">
            <ol class="breadcrumb">
                <li class="breadcrumb-item"><a href="/AdminHome">Home</a></li>
                <li class="breadcrumb-item"><a href="/AdminRewards">Rewards</a></li>
                <li class="breadcrumb-item active" aria-current="page">Add Rewards</li>
            </ol>
        </nav>
        <h2>Add Rewards</h2>
        <div class="row">
            <div class="col-md-6">
                <div class="form-group">
                    <label for="txtItemName">Item Name:</label>
                    <asp:TextBox ID="txtItemName" runat="server" CssClass="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvItemName" runat="server" ControlToValidate="txtItemName" ErrorMessage="Item Name is required." Display="Dynamic" CssClass="text-danger" />
                </div>
                <div class="form-group">
                    <label for="txtDescription">Description:</label>
                    <asp:TextBox ID="txtDescription" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="4"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvDescription" runat="server" ControlToValidate="txtDescription" ErrorMessage="Description is required." Display="Dynamic" CssClass="text-danger" />
                </div>
                <div class="form-group">
                    <label for="ddlFilterClass">Filter Class:</label>
                    <asp:DropDownList ID="ddlFilterClass" runat="server" CssClass="form-control">
                        <asp:ListItem Text="-- Select --" Value=""></asp:ListItem>
                        <asp:ListItem Text="Voucher" Value="filter-vouchers"></asp:ListItem>
                        <asp:ListItem Text="Limited Time Item" Value="filter-limited"></asp:ListItem>
                        <asp:ListItem Text="Product" Value="filter-products"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvFilterClass" runat="server" ControlToValidate="ddlFilterClass" ErrorMessage="Filter Class is required." Display="Dynamic" CssClass="text-danger" InitialValue=""></asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="col-md-6">
                <div class="form-group">
                    <label for="fuImage">Select Image:</label>
                    <asp:FileUpload CssClass="fuImage form-control" ID="fuImage" runat="server" onchange="displayImage();" />
                    <asp:RequiredFieldValidator ID="rfvImage" runat="server" ControlToValidate="fuImage" ErrorMessage="Image is required." Display="Dynamic" CssClass="text-danger" />
                </div>
                    <p>Uploaded Image:</p>
                    <asp:Image CssClass="img_Prodimg img-thumbnail" ID="img_Prodimg" runat="server" Width="148px" Height="142px"/>
                <div class="form-group">
                    <label for="txtPoints">Points:</label>
                    <asp:TextBox ID="txtPoints" runat="server" CssClass="form-control" type="number"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvPoints" runat="server" ControlToValidate="txtPoints" ErrorMessage="Points is required." Display="Dynamic" CssClass="text-danger" />
                </div>
                <div class="form-group">
                    <label for="txtDiscount">Discount:</label>
                    <asp:TextBox ID="txtDiscount" runat="server" CssClass="form-control" type="number" step="0.01"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvDiscount" runat="server" ControlToValidate="txtDiscount" ErrorMessage="Discount is required." Display="Dynamic" CssClass="text-danger" />
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                <div class="form-group">
                    <asp:Button ID="btnSubmit" runat="server" Text="Add Reward" CssClass="btn btn-primary" OnClick="btnSubmit_Click" />
                </div>
            </div>
        </div>
    </div>
     <script type="text/javascript">
            // Call your function when appropriate
            function displayImage() {
                console.log('in display image');
                var fileInput = $(".fuImage");
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
