<%@ Page Title="Login / Sign Up" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Final_Project._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">


    <link href="../Styles/Styles.css" rel="stylesheet" type="text/css"/>
    <script src="../Scripts/Javascript/LoginScript.js" defer></script>

    <main>
        <div class="container">
        <div class="info-side">
            <img src="../Media/cresteclogo_whitebg.jpg" alt="Company Logo" class="logo">
            <h1>Welcome to Crestec Philippines</h1>
            <p>Access your account to manage design requests, track orders, and more. New users can create an account to get started.</p>
        </div>
        <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
        <div class="form-side">
            <div class="tabs">
                <div class="tab active" id="login-tab">Login</div>
                <div class="tab" id="signup-tab">Sign Up</div>
            </div>
            
            <div class="form-container" id="login-form">
                <div class="form-group">
                    <label for="email">Email Address</label>
                    <input type="email" id="txt_login_email" runat="server" placeholder="Enter your email address">
                </div>
                
                <div class="form-group">
                    <label for="password">Password</label>
                    <input type="password" id="txt_login_password" runat="server" placeholder="Enter your password">
                    <div class="forgot-password">
                        <a href="#">Forgot password?</a>
                    </div>
                </div>
                
                 <asp:Button CssClass="custom-button" ID="btn_login" runat="server" OnClick="btnLogin_Click" Text="Login"/>
                
                <!-- Add extra spacing at the bottom -->
                <div style="margin-top: 30px;"></div>
                
                <div class="form-footer">
                    Don't have an account? <a href="#" id="show-signup">Sign up now</a>
                </div>
            </div>
            
            <div class="form-container" id="signup-form">
                <div class="form-group">
                    <label for="last-name">Last Name</label>
                    <input type="text" id="txt_create_last_name" runat="server" placeholder="Enter your Last Name">
                </div>
                <div class="form-group">
                    <label for="first-name">First Name</label>
                    <input type="text" id="txt_create_first_name" runat="server" placeholder="Enter your First Name">
                </div>
                
                <div class="form-group">
                    <label for="signup-email">Email Address</label>
                    <input type="email" id="txt_create_signup_email" runat="server" placeholder="Enter your email address">
                </div>
                
                <div class="form-group">
                    <label for="signup-password">Password</label>
                    <input type="password" id="txt_create_signup_password" runat="server" placeholder="Create a password">
                </div>
                
                <div class="form-group">
                    <label for="confirm-password">Confirm Password</label>
                    <input type="password" id="txt_create_confirm_password" runat="server" placeholder="Confirm your password">
                </div>
                
                <asp:Button runat="server" text="Create Account" OnClick="btnCreate_Click"></asp:Button>
                
                <div class="form-footer">
                    Already have an account? <a href="#" id="show-login">Login instead</a>
                </div>
            </div>
        </div>
    </div>
    </main>

</asp:Content>
