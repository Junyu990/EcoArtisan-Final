<%@ Page Title="Contact" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="EcoArtisanFinal.Contact" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
      <div style="display: flex;">
   <div style="width:40%;">
    <h2><%: Title %>.</h2>
    <h3>Your contact page.</h3>
    <address>
        One Microsoft Way<br />
        Redmond, WA 98052-6399<br />
        <abbr title="Phone">P:</abbr>
        425.555.0100
    </address>

    <address>
        <strong>Support:</strong>   <a href="mailto:Support@example.com">Support@example.com</a><br />
        <strong>Marketing:</strong> <a href="mailto:Marketing@example.com">Marketing@example.com</a>
    </address>
    
    <!-- Bootstrap Form -->
     <h3>Send Us A Message!</h3>
    <div class="container">
        <div class="row">
            <div class="col-md-6">
                <asp:TextBox ID="messageSubject" CssClass="form-control" runat="server" placeholder="Subject" required></asp:TextBox><br />
                <asp:TextBox ID="txtMessage" CssClass="form-control" runat="server" TextMode="MultiLine" Rows="4" placeholder="Your Message" required></asp:TextBox><br />
                <asp:Button ID="btnSubmit" CssClass="btn btn-primary" runat="server" Text="Submit" OnClick="btnSubmit_Click" />
            </div>
        </div>
    </div>
  </div>
    <div class="col-md-6">
        <iframe src="https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d3988.6624356559787!2d103.84730307430277!3d1.379204961482177!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x31da16eb92a1ba23%3A0x8f5f35132b90361f!2sNanyang%20Polytechnic%20Block%20L!5e0!3m2!1sen!2ssg!4v1707740181535!5m2!1sen!2ssg" width="600" height="450" style="border:0;" allowfullscreen="" loading="lazy" referrerpolicy="no-referrer-when-downgrade"></iframe>
    </div>
</div>
</asp:Content>
