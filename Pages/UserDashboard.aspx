<%@ Page Title="UserDashboard" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UserDashboard.aspx.cs" Inherits="Final_Project.UserDashboard" %>

    <asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
    <link href="../Styles/UserDashboardStyles.css" rel="stylesheet" type="text/css"/>
    
    <div class="header">
        <div class="logo"><img src="../Media/cresteclogo_whitebg.jpg" style="width: 60px; height: auto;" alt="Crestec Logo"></div>
        <div class="company-name">CRESTEC PHILIPPINES, INC. - USER DASHBOARD</div>
    </div>
    <div class="container">
        <div class="dashboard-title">
            <h2>Design Request Forms</h2>
             
            <div>
                <asp:Button ID="btnExport" runat="server" Text="Export Data" CssClass="btn btn-view" OnClick="btnExport_Click" />
                <asp:Button ID="btnLogout" runat="server" CssClass="logout-button" Text="Logout" OnClientClick="return confirmLogout();" OnClick="btnLogout_Click" Height="31px" Width="137px" />
            </div>
        </div>
        <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
        <!-- Search and Filter Section -->
        <div class="search-container">
            <asp:TextBox ID="SearchInput" runat="server" CssClass="form-control" PlaceHolder="Search by customer, part code, etc."></asp:TextBox>
            
            <asp:DropDownList ID="FilterType" runat="server" CssClass="form-control">
                <asp:ListItem Text="All Request Types" Value="" />
                <asp:ListItem Text="IDF Request" Value="IDFreq" />
                <asp:ListItem Text="FDF Request" Value="FDFreq" />
                <asp:ListItem Text="Sample Request" Value="SAMPLEreq" />
            </asp:DropDownList>
            
            <asp:DropDownList ID="FilterStatus" runat="server" CssClass="form-control">
                <asp:ListItem Text="All Statuses" Value="" />
                <asp:ListItem Text="Pending" Value="pending" />
                <asp:ListItem Text="Approved" Value="approved" />
                <asp:ListItem Text="Rejected" Value="rejected" />
            </asp:DropDownList>
            
            <asp:Button ID="btnFilter" runat="server" Text="Filter" CssClass="btn btn-primary" OnClick="btnFilter_Click" />
            <asp:Button ID="btnReset" runat="server" Text="Reset" CssClass="btn btn-secondary" OnClick="btnReset_Click" />
        </div>
        
        <!-- Table of Requests -->
        <asp:GridView ID="RequestsTable" runat="server" CssClass="table table-striped" 
    AutoGenerateColumns="false" AllowPaging="true" PageSize="10" 
    OnPageIndexChanging="RequestsTable_PageIndexChanging" 
    OnRowCommand="RequestsTable_RowCommand"
    OnRowDataBound="RequestsTable_RowDataBound">

    <Columns>
        <asp:BoundField DataField="RequestID" HeaderText="Request ID" />
        <asp:BoundField DataField="DateSubmitted" HeaderText="Date Submitted" />
        <asp:BoundField DataField="Type" HeaderText="Type" />
        <asp:BoundField DataField="Customer" HeaderText="Customer" />
        <asp:BoundField DataField="PartCode" HeaderText="Part Code" />
        <asp:BoundField DataField="DateNeeded" HeaderText="Date Needed" />

        
            <asp:TemplateField HeaderText="Status">
    <ItemTemplate>
        <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("Status") %>'></asp:Label>
    </ItemTemplate>
</asp:TemplateField>



    
            
            
            <asp:TemplateField HeaderText="Actions">
                 <ItemTemplate>
                    <asp:HyperLink ID="lnkViewDetails" runat="server" 
                        NavigateUrl='<%# "About.aspx?RequestID=" + Eval("RequestID") %>' 
                        CssClass="btn btn-info btn-sm">View Details</asp:HyperLink>
                 </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>

    </div>
    
   
    
<script src="../Scripts/Javascript/UserDashboardScript.js" defer></script>
</asp:Content>