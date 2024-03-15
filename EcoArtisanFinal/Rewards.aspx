<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Rewards.aspx.cs" Inherits="EcoArtisanFinal.Rewards" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
     <link rel="stylesheet" type="text/css" href='<%= ResolveUrl("~/Content/rewards.css") %>?v=<%= DateTime.Now.Ticks %>' />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.4/css/all.min.css" integrity="sha512-dTtYVYExNlKd8GnDTYAbw9onCjKqSYeQ+IJUz+UXCgIh55hCbF8qde1GPJ70+I8r9HnJx4Ue0NPTm3aBV5gMRw==" crossorigin="anonymous" referrerpolicy="no-referrer" />
    <script src="Content/rewards.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <div class="rewards">
                    <div class="reward-point-container">
                        <div class="leaf-and-reward">
                            
                            <h1>🏆 Rewards</h1>
                            
                        </div>
                        <div class="my-rewards">
                             <asp:Label ID="pointsLabel" runat="server"></asp:Label><img class="threeleaf" src="../Content/Images/ecocoin.png" alt="">
                        </div>
                    </div>
                            <h4>Use <img class="ecocoin" src="../Content/Images/ecocoin.png" alt=""> ecocoins to redeem your rewards!</h4>

        <section id="category" class="category">
                    <div class="rewards-by-category">
                        <ul id="category-filters" class="d-flex justify-content-center">
                          <li data-filter="*" class="filter-active">
                              <div class="rewards-by-category-card">
                                <span>🎁</span><span>All</span>
                              </div>
                          </li>
                          <li data-filter=".filter-vouchers">
                              <div class="rewards-by-category-card">
                                 <span>🎫</span><span>Vouchers</span>
                             </div>
                          </li>
                            <li data-filter=".filter-limited">
                                <div class="rewards-by-category-card">
                                  <span>⏰</span><span>Limited Time Items</span>
                                </div>
                          </li>
                          <li data-filter=".filter-products">
                              <div class="rewards-by-category-card">
                                <span>🥂</span><span>Products</span>
                                </div>
                          </li>
                        </ul>
                    </div>
        

            <div class="rewards-item rewards-container">
                <asp:Repeater ID="rptRewards" runat="server" OnItemCommand="rptRewards_ItemCommand">
                    <ItemTemplate>
                        <div class="rewards-item-card filter-rewards <%# Eval("FilterClass") %>">
                            <div class="rewards-item-card-picture-container">
                                <img class="rewards-item-card-picture" src="../Content/Rewards/<%# Eval("ImageUrl") %>" alt='<%# Eval("ItemName") %>'/>
                            </div>
                            <div class="rewards-item-card-description">
                                <div class="rewards-item-card-name-and-amount">
                                    <div class="rewards-item-card-name"><%# Eval("ItemName") %></div>
                                    
                                </div>
                                <div class="rewards-item-card-points-and-reedeem">
                                    <div class="rewards-item-card-points">
                                        <img class="ecocoin" src="../Content/Images/ecocoin.png" alt=""><span class="yellow-points"><%# Eval("Points") %></span></div>
                                    <div class="rewards-item-card-reedeem">
                                        <asp:LinkButton ID="Redeem" CssClass="btn btn-redeem" runat="server" CommandName="Redeem" CommandArgument='<%# Eval("ItemID") %>'>Redeem</asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </section>
     </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent2" runat="server">
</asp:Content>
