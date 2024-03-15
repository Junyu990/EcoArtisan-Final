<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="AdminViewUsers.aspx.cs" Inherits="EcoArtisanFinal.AdminViewUsers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Admin View Users</h2>
    <hr />
    <div class="row">
        <div class="col-md-12">
            <h3>Customers</h3>
            <asp:GridView ID="GridViewCustomers" runat="server" CssClass="table table-striped table-bordered" AutoGenerateColumns="False" DataKeyNames="UserID" OnRowCommand="GridViewCustomers_RowCommand">
                <Columns>
                    <asp:BoundField DataField="UserID" HeaderText="User ID" ItemStyle-Width="100px" />
                    <asp:BoundField DataField="UserName" HeaderText="User Name" ItemStyle-Width="150px"/>
                    <asp:BoundField DataField="UserEmail" HeaderText="Email" ItemStyle-Width="200px" />
                    <asp:BoundField DataField="Points" HeaderText="Points" ItemStyle-Width="100px" />
                    <asp:BoundField DataField="SignUpDate" HeaderText="Sign Up Date" ItemStyle-Width="200px" />
                    <asp:ButtonField Text="Give Admin" ControlStyle-CssClass="btn btn-primary" CommandName="GiveAdmin" ItemStyle-Width="150px"/>
                    <asp:ButtonField Text="Delete" ControlStyle-CssClass="btn btn-danger" CommandName="DeleteUser" ItemStyle-Width="150px" />
                </Columns>
            </asp:GridView>
        </div>
        <div class="col-md-12">
            <h3>Admins</h3> 
            <asp:GridView ID="GridViewAdmins" runat="server" CssClass="table table-striped table-bordered" AutoGenerateColumns="False"  DataKeyNames="UserID" OnRowCommand="GridViewAdmins_RowCommand">
                <Columns>
                    <asp:BoundField DataField="UserID" HeaderText="User ID" ItemStyle-Width="100px" />
                    <asp:BoundField DataField="UserName" HeaderText="User Name" ItemStyle-Width="150px"/>
                    <asp:BoundField DataField="UserEmail" HeaderText="Email" ItemStyle-Width="200px" />
                    <asp:BoundField DataField="Points" HeaderText="Points" ItemStyle-Width="100px" />
                    <asp:BoundField DataField="SignUpDate" HeaderText="Sign Up Date" ItemStyle-Width="200px" />
                    <asp:ButtonField Text="Remove Admin Role" ControlStyle-CssClass="btn btn-danger" CommandName="RemoveAdmin" ItemStyle-Width="150px" />
                    <asp:ButtonField Text="Delete" ControlStyle-CssClass="btn btn-danger" CommandName="DeleteAdmin" ItemStyle-Width="150px" />
                </Columns>
            </asp:GridView>
        </div>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent2" runat="server">
</asp:Content>
