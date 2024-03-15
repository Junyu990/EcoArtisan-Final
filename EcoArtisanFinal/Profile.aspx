<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="EcoArtisanFinal.Profile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
     <link rel="stylesheet" type="text/css" href='<%= ResolveUrl("~/Content/profile.css") %>?v=<%= DateTime.Now.Ticks %>' />
    <link rel="stylesheet" type="text/css" href='<%= ResolveUrl("~/Content/rewards.css") %>?v=<%= DateTime.Now.Ticks %>' />
    <script src="../Content/Profile.js"></script>
    <script src="Content/rewards.js"></script>
    <script src="https://code.jquery.com/jquery-3.6.4.min.js"></script>

    <style>
        /* CSS */
        .btn-outline-primary:hover {
            color: #fff; /* Text color on hover */
            background-color: #6db67d; /* Background color on hover */
            border-color: #007bff; /* Border color on hover */
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
       <div class="container-fluid">
  <div class="row">
    <div class="col-md-2">
      <ul class="nav flex-column" id="myTab" role="tablist">
        <li class="nav-item">
            <div style="text-align:center;">
                <asp:Image ID="img_User" runat="server" class="rounded-circle mt-5" width="150px" />
                <br />
                <span class="font-weight-bold"><%: Session["UserName"] %></span>
                <br />
                <span class="text-black-50"><%: Session["UserEmail"] %></span>
                <br />
            </div>
        </li>
                <li class="nav-item active">
                  <a class="nav-link" id="personalinfo-tab" data-toggle="tab" href="#personalinfo" role="tab" aria-controls="personalinfo" aria-selected="false">Personal Information</a>
                </li>
                <li class="nav-item">
                  <a class="nav-link" id="myrewards-tab" data-toggle="tab" href="#myrewards" role="tab" aria-controls="myrewards" aria-selected="true">My Rewards</a>
                </li>
                <li class="nav-item">
                  <a class="nav-link" id="orders-tab" data-toggle="tab" href="#orders" role="tab" aria-controls="orders" aria-selected="false">My Orders</a>
                </li>
                <li class="nav-item">
                  <a class="nav-link" id="reviews-tab" data-toggle="tab" href="#reviews" role="tab" aria-controls="reviews" aria-selected="false">My Reviews</a>
                </li>
                <li class="nav-item">
                  <a class="nav-link" id="rewards-tab" data-toggle="tab" href="#rewards" role="tab" aria-controls="rewards" aria-selected="false">Rewards</a>
                </li>
                <li class="nav-item">
                  <a class="nav-link" id="payment-tab" data-toggle="tab" href="#payment" role="tab" aria-controls="payment" aria-selected="false">Payment Methods</a>
                </li>
                <li class="nav-item">
                  <a class="nav-link" id="settings-tab" data-toggle="tab" href="#settings" role="tab" aria-controls="settings" aria-selected="false">Settings</a>
                </li>
      </ul>
    </div>
    <div class="col-md-9">
      <!-- Tab panes -->
      <div class="tab-content">
          <!--My Rewards tab-->
        <div class="tab-pane" id="myrewards" role="tabpanel" aria-labelledby="myrewards-tab">
          <div class="rewards">
                    <div class="reward-point-container">
                        <div class="leaf-and-reward">
                            <h1>My Rewards</h1>
                        </div>
                    </div>

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
                            <asp:Repeater ID="rptRewards" runat="server">
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
                                                    <asp:LinkButton ID="Use" CssClass="btn btn-redeem" runat="server" CommandName="Use" CommandArgument='<%# Eval("ItemID") %>'>Use</asp:LinkButton>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                    </section>
                 </div>
        </div>
          <!--My Orders tab-->
        <div class="tab-pane" id="orders" role="tabpanel" aria-labelledby="orders-tab">
          <h3>My Orders</h3>
          <p>Content for My Orders tab goes here.</p>
          <asp:Button ID="viewhistory" runat="server" Text="Order History" CssClass="btn btn-outline-primary" OnClick="Redirect_order_history"/>
        </div>
          <!--Reviews tab-->
        <div class="tab-pane" id="reviews" role="tabpanel" aria-labelledby="reviews-tab">
            <h3>My Reviews</h3>
            <p>Content for My Reviews tab goes here.</p>
            <asp:Button ID="viewreviews" runat="server" Text="View My Reviews" CssClass="btn btn-outline-primary" OnClick="Redirect_reviews"/>
        </div>
          <!--Personal Information-->
        <div class="tab-pane active" id="personalinfo" role="tabpanel" aria-labelledby="personalinfo-tab">
          <h3>Personal Information</h3>
                <div class="p-3 py-5">
                    <div class="d-flex justify-content-between align-items-center mb-3">
                        <h4 class="text-left">Profile Settings</h4>
                    </div>
                    
                    <div class="row mt-2">
                        <div class="col-md-12">
                            <label for="profile-picture">Profile Picture</label>
                            <!-- Input for uploading or changing profile picture -->
                            <asp:FileUpload ID="profilePicture" runat="server" CssClass="form-control-file" Enabled="false"/>
                        </div>
                        <div class="col-md-6">
                            <label class="labels">Username:</label>
                            <asp:TextBox ID="usernameTextBox" runat="server" CssClass="form-control" ReadOnly="true" />
                        </div>
                        <div class="col-md-6">
                            <label class="labels">Email: </label>
                            <asp:TextBox ID="emailTextBox" runat="server" CssClass="form-control" ReadOnly="true" />
                        </div>
                        <div class="col-md-6">
                            <label class="labels">First Name:</label>
                            <asp:TextBox ID="firstNameTextBox" runat="server" CssClass="form-control" ReadOnly="true" Placeholder="Enter first name"/>
                        </div>
                        <div class="col-md-6">
                            <label class="labels">Last Name:</label>
                            <asp:TextBox ID="lastNameTextBox" runat="server" CssClass="form-control" ReadOnly="true" Placeholder="Enter last name" />
                        </div>
                    </div>
                    <div class="row mt-3">
                        <div class="col-md-6">
                            <label class="labels">Mobile Number: </label>
                            <asp:TextBox ID="mobileNumberTextBox" runat="server" CssClass="form-control" Placeholder="Enter phone number" ReadOnly="true" />
                        </div>
                        <div class="col-md-6">
                            <label class="labels">Birthdate:</label>
                            <asp:TextBox ID="birthdateTextBox" runat="server" CssClass="form-control" ReadOnly="true" TextMode="Date" placeholder="Not Selected" />
                        </div>
                        <div class="col-md-12">
                            <label class="labels">Gender:</label>
                            <asp:RadioButtonList ID="genderRBL" runat="server" Enabled="false">
                                <asp:ListItem>Male</asp:ListItem>
                                <asp:ListItem>Female</asp:ListItem>
                            </asp:RadioButtonList>
                        </div>

                        <div class="col-md-12">
                            <label class="labels">Country</label>
                            <asp:DropDownList ID="countryDDL" CssClass="form-control" Enabled="False" runat="server">
                                <asp:ListItem>--COUNTRY--</asp:ListItem>
                                <asp:ListItem>Afghanistan</asp:ListItem>
                                <asp:ListItem>Albania</asp:ListItem>
                                <asp:ListItem>Algeria</asp:ListItem>
                                <asp:ListItem>Andorra</asp:ListItem>
                                <asp:ListItem>Angola</asp:ListItem>
                                <asp:ListItem>Antigua & Deps</asp:ListItem>
                                <asp:ListItem>Argentina</asp:ListItem>
                                <asp:ListItem>Armenia</asp:ListItem>
                                <asp:ListItem>Australia</asp:ListItem>
                                <asp:ListItem>Austria</asp:ListItem>
                                <asp:ListItem>Azerbaijan</asp:ListItem>
                                <asp:ListItem>Bahamas</asp:ListItem>
                                <asp:ListItem>Bahrain</asp:ListItem>
                                <asp:ListItem>Bangladesh</asp:ListItem>
                                <asp:ListItem>Barbados</asp:ListItem>
                                <asp:ListItem>Belarus</asp:ListItem>
                                <asp:ListItem>Belgium</asp:ListItem>
                                <asp:ListItem>Belize</asp:ListItem>
                                <asp:ListItem>Benin</asp:ListItem>
                                <asp:ListItem>Bhutan</asp:ListItem>
                                <asp:ListItem>Bolivia</asp:ListItem>
                                <asp:ListItem>Bosnia Herzegovina</asp:ListItem>
                                <asp:ListItem>Botswana</asp:ListItem>
                                <asp:ListItem>Brazil</asp:ListItem>
                                <asp:ListItem>Brunei</asp:ListItem>
                                <asp:ListItem>Bulgaria</asp:ListItem>
                                <asp:ListItem>Burkina</asp:ListItem>
                                <asp:ListItem>Burundi</asp:ListItem>
                                <asp:ListItem>Cambodia</asp:ListItem>
                                <asp:ListItem>Cameroon</asp:ListItem>
                                <asp:ListItem>Canada</asp:ListItem>
                                <asp:ListItem>Cape Verde</asp:ListItem>
                                <asp:ListItem>Central African Rep</asp:ListItem>
                                <asp:ListItem>Chad</asp:ListItem>
                                <asp:ListItem>Chile</asp:ListItem>
                                <asp:ListItem>China</asp:ListItem>
                                <asp:ListItem>Colombia</asp:ListItem>
                                <asp:ListItem>Comoros</asp:ListItem>
                                <asp:ListItem>Congo</asp:ListItem>
                                <asp:ListItem>Congo {Democratic Rep}</asp:ListItem>
                                <asp:ListItem>Costa Rica</asp:ListItem>
                                <asp:ListItem>Croatia</asp:ListItem>
                                <asp:ListItem>Cuba</asp:ListItem>
                                <asp:ListItem>Cyprus</asp:ListItem>
                                <asp:ListItem>Czech Republic</asp:ListItem>
                                <asp:ListItem>Denmark</asp:ListItem>
                                <asp:ListItem>Djibouti</asp:ListItem>
                                <asp:ListItem>Dominica</asp:ListItem>
                                <asp:ListItem>Dominican Republic</asp:ListItem>
                                <asp:ListItem>East Timor</asp:ListItem>
                                <asp:ListItem>Ecuador</asp:ListItem>
                                <asp:ListItem>Egypt</asp:ListItem>
                                <asp:ListItem>El Salvador</asp:ListItem>
                                <asp:ListItem>Equatorial Guinea</asp:ListItem>
                                <asp:ListItem>Eritrea</asp:ListItem>
                                <asp:ListItem>Estonia</asp:ListItem>
                                <asp:ListItem>Ethiopia</asp:ListItem>
                                <asp:ListItem>Fiji</asp:ListItem>
                                <asp:ListItem>Finland</asp:ListItem>
                                <asp:ListItem>France</asp:ListItem>
                                <asp:ListItem>Gabon</asp:ListItem>
                                <asp:ListItem>Gambia</asp:ListItem>
                                <asp:ListItem>Georgia</asp:ListItem>
                                <asp:ListItem>Germany</asp:ListItem>
                                <asp:ListItem>Ghana</asp:ListItem>
                                <asp:ListItem>Greece</asp:ListItem>
                                <asp:ListItem>Grenada</asp:ListItem>
                                <asp:ListItem>Guatemala</asp:ListItem>
                                <asp:ListItem>Guinea</asp:ListItem>
                                <asp:ListItem>Guinea-Bissau</asp:ListItem>
                                <asp:ListItem>Guyana</asp:ListItem>
                                <asp:ListItem>Haiti</asp:ListItem>
                                <asp:ListItem>Honduras</asp:ListItem>
                                <asp:ListItem>Hungary</asp:ListItem>
                                <asp:ListItem>Iceland</asp:ListItem>
                                <asp:ListItem>India</asp:ListItem>
                                <asp:ListItem>Indonesia</asp:ListItem>
                                <asp:ListItem>Iran</asp:ListItem>
                                <asp:ListItem>Iraq</asp:ListItem>
                                <asp:ListItem>Ireland {Republic}</asp:ListItem>
                                <asp:ListItem>Israel</asp:ListItem>
                                <asp:ListItem>Italy</asp:ListItem>
                                <asp:ListItem>Ivory Coast</asp:ListItem>
                                <asp:ListItem>Jamaica</asp:ListItem>
                                <asp:ListItem>Japan</asp:ListItem>
                                <asp:ListItem>Jordan</asp:ListItem>
                                <asp:ListItem>Kazakhstan</asp:ListItem>
                                <asp:ListItem>Kenya</asp:ListItem>
                                <asp:ListItem>Kiribati</asp:ListItem>
                                <asp:ListItem>Korea North</asp:ListItem>
                                <asp:ListItem>Korea South</asp:ListItem>
                                <asp:ListItem>Kosovo</asp:ListItem>
                                <asp:ListItem>Kuwait</asp:ListItem>
                                <asp:ListItem>Kyrgyzstan</asp:ListItem>
                                <asp:ListItem>Laos</asp:ListItem>
                                <asp:ListItem>Latvia</asp:ListItem>
                                <asp:ListItem>Lebanon</asp:ListItem>
                                <asp:ListItem>Lesotho</asp:ListItem>
                                <asp:ListItem>Liberia</asp:ListItem>
                                <asp:ListItem>Libya</asp:ListItem>
                                <asp:ListItem>Liechtenstein</asp:ListItem>
                                <asp:ListItem>Lithuania</asp:ListItem>
                                <asp:ListItem>Luxembourg</asp:ListItem>
                                <asp:ListItem>Macedonia</asp:ListItem>
                                <asp:ListItem>Madagascar</asp:ListItem>
                                <asp:ListItem>Malawi</asp:ListItem>
                                <asp:ListItem>Malaysia</asp:ListItem>
                                <asp:ListItem>Maldives</asp:ListItem>
                                <asp:ListItem>Mali</asp:ListItem>
                                <asp:ListItem>Malta</asp:ListItem>
                                <asp:ListItem>Marshall Islands</asp:ListItem>
                                <asp:ListItem>Mauritania</asp:ListItem>
                                <asp:ListItem>Mauritius</asp:ListItem>
                                <asp:ListItem>Mexico</asp:ListItem>
                                <asp:ListItem>Micronesia</asp:ListItem>
                                <asp:ListItem>Moldova</asp:ListItem>
                                <asp:ListItem>Monaco</asp:ListItem>
                                <asp:ListItem>Mongolia</asp:ListItem>
                                <asp:ListItem>Montenegro</asp:ListItem>
                                <asp:ListItem>Morocco</asp:ListItem>
                                <asp:ListItem>Mozambique</asp:ListItem>
                                <asp:ListItem>Myanmar, {Burma}</asp:ListItem>
                                <asp:ListItem>Namibia</asp:ListItem>
                                <asp:ListItem>Nauru</asp:ListItem>
                                <asp:ListItem>Nepal</asp:ListItem>
                                <asp:ListItem>Netherlands</asp:ListItem>
                                <asp:ListItem>New Zealand</asp:ListItem>
                                <asp:ListItem>Nicaragua</asp:ListItem>
                                <asp:ListItem>Niger</asp:ListItem>
                                <asp:ListItem>Nigeria</asp:ListItem>
                                <asp:ListItem>Norway</asp:ListItem>
                                <asp:ListItem>Oman</asp:ListItem>
                                <asp:ListItem>Pakistan</asp:ListItem>
                                <asp:ListItem>Palau</asp:ListItem>
                                <asp:ListItem>Panama</asp:ListItem>
                                <asp:ListItem>Papua New Guinea</asp:ListItem>
                                <asp:ListItem>Paraguay</asp:ListItem>
                                <asp:ListItem>Peru</asp:ListItem>
                                <asp:ListItem>Philippines</asp:ListItem>
                                <asp:ListItem>Poland</asp:ListItem>
                                <asp:ListItem>Portugal</asp:ListItem>
                                <asp:ListItem>Qatar</asp:ListItem>
                                <asp:ListItem>Romania</asp:ListItem>
                                <asp:ListItem>Russian Federation</asp:ListItem>
                                <asp:ListItem>Rwanda</asp:ListItem>
                                <asp:ListItem>St Kitts & Nevis</asp:ListItem>
                                <asp:ListItem>St Lucia</asp:ListItem>
                                <asp:ListItem>Saint Vincent & the Grenadines</asp:ListItem>
                                <asp:ListItem>Samoa</asp:ListItem>
                                <asp:ListItem>San Marino</asp:ListItem>
                                <asp:ListItem>Sao Tome & Principe</asp:ListItem>
                                <asp:ListItem>Saudi Arabia</asp:ListItem>
                                <asp:ListItem>Senegal</asp:ListItem>
                                <asp:ListItem>Serbia</asp:ListItem>
                                <asp:ListItem>Seychelles</asp:ListItem>
                                <asp:ListItem>Sierra Leone</asp:ListItem>
                                <asp:ListItem>Singapore</asp:ListItem>
                                <asp:ListItem>Slovakia</asp:ListItem>
                                <asp:ListItem>Slovenia</asp:ListItem>
                                <asp:ListItem>Solomon Islands</asp:ListItem>
                                <asp:ListItem>Somalia</asp:ListItem>
                                <asp:ListItem>South Africa</asp:ListItem>
                                <asp:ListItem>South Sudan</asp:ListItem>
                                <asp:ListItem>Spain</asp:ListItem>
                                <asp:ListItem>Sri Lanka</asp:ListItem>
                                <asp:ListItem>Sudan</asp:ListItem>
                                <asp:ListItem>Suriname</asp:ListItem>
                                <asp:ListItem>Swaziland</asp:ListItem>
                                <asp:ListItem>Sweden</asp:ListItem>
                                <asp:ListItem>Switzerland</asp:ListItem>
                                <asp:ListItem>Syria</asp:ListItem>
                                <asp:ListItem>Taiwan</asp:ListItem>
                                <asp:ListItem>Tajikistan</asp:ListItem>
                                <asp:ListItem>Tanzania</asp:ListItem>
                                <asp:ListItem>Thailand</asp:ListItem>
                                <asp:ListItem>Togo</asp:ListItem>
                                <asp:ListItem>Tonga</asp:ListItem>
                                <asp:ListItem>Trinidad & Tobago</asp:ListItem>
                                <asp:ListItem>Tunisia</asp:ListItem>
                                <asp:ListItem>Turkey</asp:ListItem>
                                <asp:ListItem>Turkmenistan</asp:ListItem>
                                <asp:ListItem>Tuvalu</asp:ListItem>
                                <asp:ListItem>Uganda</asp:ListItem>
                                <asp:ListItem>Ukraine</asp:ListItem>
                                <asp:ListItem>United Arab Emirates</asp:ListItem>
                                <asp:ListItem>United Kingdom</asp:ListItem>
                                <asp:ListItem>United States</asp:ListItem>
                                <asp:ListItem>Uruguay</asp:ListItem>
                                <asp:ListItem>Uzbekistan</asp:ListItem>
                                <asp:ListItem>Vanuatu</asp:ListItem>
                                <asp:ListItem>Vatican City</asp:ListItem>
                                <asp:ListItem>Venezuela</asp:ListItem>
                                <asp:ListItem>Vietnam</asp:ListItem>
                                <asp:ListItem>Yemen</asp:ListItem>
                                <asp:ListItem>Zambia</asp:ListItem>
                                <asp:ListItem>Zimbabwe</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-6">
                            <label class="labels">Address Line 1</label>
                            <asp:TextBox ID="addressLine1TextBox" runat="server" CssClass="form-control" Placeholder="Enter address line 1" ReadOnly="true" />
                        </div>
                        <div class="col-md-6">
                            <label class="labels">Address Line 2</label>
                            <asp:TextBox ID="addressLine2TextBox" runat="server" CssClass="form-control" Placeholder="Enter address line 2" ReadOnly="true" />
                        </div>
                        <div class="col-md-6">
                            <label class="labels">Postcode</label>
                            <asp:TextBox ID="postcodeTextBox" runat="server" CssClass="form-control" Placeholder="Enter postcode" ReadOnly="true" />
                        </div>
                        <div class="col-md-6">
                            <label class="labels">State</label>
                            <asp:TextBox ID="stateTextBox" runat="server" CssClass="form-control" Placeholder="Enter state" ReadOnly="true" />
                        </div>
                        <div class="col-md-6">
                            <label class="labels">Area</label>
                            <asp:TextBox ID="areaTextBox" runat="server" CssClass="form-control" Placeholder="Enter area" ReadOnly="true" />
                        </div>
                    </div>
                    <div class="row mt-2">
                        <br />
                            <div class="col-md-1"><asp:Button ID="editButton" runat="server" Text="Edit" CssClass="btn btn-primary" OnClick="editButton_Click" OnClientClick="return enableEditMode();"/></div>
                            <div class="col-md-1">
                                <asp:Button ID="saveButton" runat="server" Text="Save" CssClass="btn btn-success" Style="display: none;" OnClick="saveButton_Click" />
                            </div>

                            <div class="col-md-1"><asp:Button ID="cancelButton" runat="server" Text="Cancel" CssClass="btn btn-secondary" OnClientClick="return cancelEditMode();" Style="display: none;" OnClick="cancelButton_Click" /></div>
                            <div class="col-md-1">
                                <asp:Button ID="update" runat="server" Text="Update" CssClass="btn btn-success" Style="display: none;" OnClick="update_Click" />
                            </div>
                    </div>
               </div>
        </div>
          <!--Claim Rewards Tab-->
        <div class="tab-pane" id="rewards" role="tabpanel" aria-labelledby="rewards-tab">
          <h3>Rewards</h3>
          <p>Content for Rewards tab goes here.</p>
            <asp:Button ID="viewrewards" runat="server" Text="Redeem Rewards Here" CssClass="btn btn-outline-primary" OnClick="Redirect_rewards"/>
        </div>
          <!--Payment tab-->
        <div class="tab-pane" id="payment" role="tabpanel" aria-labelledby="payment-tab">
          <h3>Payment Methods</h3>
          <p>Content for Payment Methods tab goes here.</p>
        </div>
          <!--Settings tab-->
        <div class="tab-pane" id="settings" role="tabpanel" aria-labelledby="settings-tab">
          <h3>Settings</h3>
            <div class="p-3 py-5">
                    <div class="d-flex justify-content-between align-items-center mb-3">
                        <p class="text-left">Content for settings goes here</p>
                    </div>
                    
                    <div class="row mt-2">
                        
                        
                        
                    </div>
                    <div class="row mt-3">
                        
                    </div>
                    <div class="row mt-2">
                        <br />
                            <div class="col-md-1"><asp:Button ID="delete_btn" runat="server" Text="Delete Account" CssClass="btn btn-primary"  OnClientClick="return confirm('Are you sure you want to delete your account?');" OnClick="delete_btn_Click" /></div>
                            
                    </div>
               </div>
            </div>
      </div>
    </div>
  </div>
</div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent2" runat="server">
</asp:Content>
