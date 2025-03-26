<%@ Page Title="Contact" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="Final_Project.Contact" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="../Styles/AdminStyles.css" rel="stylesheet" type="text/css"/>
    
    <div class="header">
        <div class="logo"><img src="../Media/cresteclogo_whitebg.jpg" style="width: 60px; height: auto;" alt="Crestec Logo"></div>
        <div class="company-name">CRESTEC PHILIPPINES, INC. - ADMIN DASHBOARD</div>
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
    OnRowCommand="RequestsTable_RowCommand">
    <Columns>
        <asp:BoundField DataField="RequestID" HeaderText="Request ID" />
        <asp:BoundField DataField="DateSubmitted" HeaderText="Date Submitted" />
        <asp:BoundField DataField="Type" HeaderText="Type" />
        <asp:BoundField DataField="Customer" HeaderText="Customer" />
        <asp:BoundField DataField="PartCode" HeaderText="Part Code" />
        <asp:BoundField DataField="DateNeeded" HeaderText="Date Needed" />

        
            <asp:TemplateField HeaderText="Status">
                <ItemTemplate>
                    <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("Status") %>' 
                        CssClass='<%# GetStatusColor(Eval("Status").ToString()) %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
    
            
            
            <asp:TemplateField HeaderText="Actions">
                <ItemTemplate>
                    <asp:HyperLink ID="lnkViewDetails" runat="server" 
                        NavigateUrl='<%# "About.aspx?RequestID=" + Eval("RequestID") %>' 
                        CssClass="btn btn-info btn-sm">View Details</asp:HyperLink>

                    <asp:Button ID="btnReject" runat="server" Text="Reject" CssClass="btn btn-reject" 
                        CommandName="RejectRequest" CommandArgument='<%# Eval("RequestID") %>' />

                    <asp:Button ID="btnApprove" runat="server" Text="Approve" CssClass="btn btn-approve" 
                        CommandName="ApproveRequest" CommandArgument='<%# Eval("RequestID") %>' />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>

    </div>
    
    <!-- Modal for Request Details -->
    <asp:Panel ID="RequestModal" runat="server" CssClass="modal" Style="display:none;">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h2 class="modal-title">Design Request Details</h2>
                    <asp:Button ID="btnClose2" runat="server" Text="×" CssClass="close" OnClick="btnClose_Click" />
                </div>
                <div class="modal-body">
                    <!-- Sections match the original HTML, using asp:Panel and asp:Label -->
                    <asp:Panel runat="server" CssClass="detail-section">
                        <h3>Request Information</h3>
                        <div class="detail-grid">
                            <asp:Label ID="DetailRequestId" runat="server" CssClass="detail-item" />
                            <asp:Label ID="DetailRequestType" runat="server" CssClass="detail-item" />
                            <!-- Continue with other labels following the same pattern -->
                        </div>
                    </asp:Panel>
                    
                    <!-- Repeat for other sections: Account & Customer, Print & Product Details, etc. -->
                </div>
                
                <div class="modal-footer">
                    <asp:Button ID="btnReject" runat="server" Text="Reject Request" CssClass="btn btn-reject" OnClick="btnReject_Click" />
                    <asp:Button ID="btnApprove" runat="server" Text="Approve Request" CssClass="btn btn-approve" OnClick="btnApprove_Click" />
                    <asp:Button ID="btnClose" runat="server" Text="Close" CssClass="btn btn-view" OnClick="btnClose_Click" />
                </div>
            </div>
        </div>
    </asp:Panel>
<script src="../Scripts/Javascript/AdminScript.js" defer></script>
</asp:Content>