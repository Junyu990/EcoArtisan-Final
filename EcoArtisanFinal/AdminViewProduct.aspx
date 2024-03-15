<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="AdminViewProduct.aspx.cs" Inherits="EcoArtisanFinal.AdminViewProduct" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
      <link href="<%= ResolveUrl("~/Content/Product.css") %>?v<%=DateTime.Now.Ticks %>" rel="stylesheet" type="text/css" />
    <link href="<%= ResolveUrl("~/Content/AdminProduct.css") %>?v<%=DateTime.Now.Ticks %>" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <div style="display: flex; justify-content: space-between;">
        <h3>Total Products: <asp:Label ID="lbl_totalProduct" runat="server" Text=""></asp:Label></h3>
        <asp:Button CssClass="btnAddProd" ID="btn_AddProduct" runat="server" Text="Add New Product" OnClick="btn_AddProduct_Click"/>
    </div>
    
    <div class="cardcontainer">
        <asp:Repeater ID="repeater_AdminProduct" runat="server">
           <ItemTemplate>
                 <div class="card">
                     <a href='<%# "AdminProductDetails.aspx?prodID=" + Eval("ProdID") %>' style="text-align:center;">
                    <div class="card-details">

                        <img src='../Content/Products/<%# Eval("ProdImg") %>' alt='<%# Eval("ProdName") %>'>
                        <div class="card-title" style="text-align:center; text-decoration: none;"><%# Eval("ProdName") %></div>
                        <div class="card-price price" style="text-align:center; color:black;">$<%# Eval("ProdPrice") %></div>
                        <div class="card-qty qty" style="text-align:center; color: darkgrey;"><%# Eval("ProdQty") %> left in stock.</div>
                        
                        <span style="text-decoration:underline; color: darkgrey;">Click to View Product Details</span>
                    
                    </div>
                    </a>
            </div>
         </ItemTemplate>
    </asp:Repeater>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent2" runat="server">
</asp:Content>
