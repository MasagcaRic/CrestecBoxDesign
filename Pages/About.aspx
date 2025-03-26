<%@ Page Title="About" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="Final_Project.About" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    
    <link href="../Styles/FormsStyles.css" rel="stylesheet" type="text/css"/>
    
    
    <div class="header">
        <div class="logo"><img src="../Media/cresteclogo_whitebg.jpg" style="width: 150px; height: auto;"></div>
        <div class="company-name">CRESTEC PHILIPPINES, INC.</div>
        <asp:Button ID="btnLogout" runat="server" CssClass="logout-button" Text="Logout" OnClientClick="return confirmLogout();" OnClick="btnLogout_Click" Height="38px" Width="89px" />
        <asp:Button ID="btnDsh" runat="server" CssClass="Dash-button" Text="Dashboard" OnClick="btnDash_Click" Height="38px" Width="89px" />
    </div>
    <div class="form-title center">
        <h2>DESIGN REQUEST FORM</h2>
    </div>
    
    <!-- START OF FORM -->
    <div id="design-request-form">
        <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
        <!-- First Section: Request, Account, Customer -->
        <div class="form-section">
            <div class="flex-container">
                <!-- Type of Request -->
                <div class="request-section">
                    <h3 class="section-header">TYPE OF REQUEST</h3>
                    
                    <div class="checkbox-group">
                        <label class="checkbox-label">
                            <asp:CheckBox ID="chk_IDFreq" runat="server" Text=" IDF REQUEST" />
                        </label>
                        <label class="checkbox-label">
                            <asp:CheckBox ID="chk_FDFreq" runat="server" Text=" FDF REQUEST" />
                        </label>
                        <label class="checkbox-label">
                            <asp:CheckBox ID="chk_SAMPLEreq" runat="server" Text=" SAMPLE REQUEST" />
                        </label>
                    </div>
                    
                    <label>Date Needed:</label>
                    <asp:TextBox ID="txt_DateNeeded" runat="server" TextMode="Date"></asp:TextBox>
                    
                    <div class="qty-row">
                        <label>QTY:</label>
                        <asp:TextBox ID="txt_Quantity" runat="server" TextMode="Number" min="1"></asp:TextBox>
                    </div>
                </div>
        
                <!-- Account in Charge -->
                <div class="account-section">
                    <h3 class="section-header">ACCOUNT IN CHARGE</h3>
                    
                    <div class="field-row">
                        <label>Sales in Charge:</label>
                        <asp:TextBox ID="txt_SalesInCharge" runat="server"></asp:TextBox>
                    </div>
                    
                    <div class="field-row">
                        <label>Sales Japan Desk:</label>
                        <asp:TextBox ID="txt_SalesJapanDesk" runat="server"></asp:TextBox>
                    </div>
        
                    <div class="field-row">
                        <label>Sales/QA (LTC):</label>
                        <asp:TextBox ID="txt_SalesQaLtc" runat="server"></asp:TextBox>
                    </div>
        
                    <div class="field-row">
                        <label>Sales/QA (LISP3):</label>
                        <asp:TextBox ID="txt_SalesQaLisp3" runat="server"></asp:TextBox>
                    </div>
                </div>
        
                <!-- Customer Details -->
                <div class="customer-section">
                    <h3 class="section-header">CUSTOMER DETAILS</h3>
                    
                    <div class="field-row">
                        <label>Customer Name:</label>
                        <asp:TextBox ID="txt_CustomerName" runat="server"></asp:TextBox>
                    </div>
                    
                    <div class="field-row">
                        <label>Contact Person:</label>
                        <asp:TextBox ID="txt_ContactPerson" runat="server"></asp:TextBox>
                    </div>
                </div>
            </div>
        </div>


       
        <!-- Second Section: Print Process and Product Details -->
        <div class="form-section">
            <div class="flex-container">
                <!-- Print Process -->
                <div class="print-section">
                    <h3 class="section-header">PRINT PROCESS</h3>
                    
                    <div class="checkbox-group">
                        <label class="checkbox-label">
                            <asp:CheckBox ID="chk_NOprint" runat="server" Text=" NO PRINT" />
                        </label>
                        <label class="checkbox-label">
                            <asp:CheckBox ID="chk_SILKprint" runat="server" Text=" SILK SCREEN" />
                        </label>
                        <label class="checkbox-label">
                            <asp:CheckBox ID="chk_OFFSETprint" runat="server" Text=" OFFSET" />
                        </label>
                        <label class="checkbox-label">
                            <asp:CheckBox ID="chk_ROTOGRAVURE" runat="server" Text=" ROTOGRAVURE" />
                        </label>
                        <label class="checkbox-label">
                            <asp:CheckBox ID="chk_HYBRIDprint" runat="server" Text=" HYBRID PRINTING" />
                        </label>
                    </div>
                    
                    <label>Other:</label>
                    <asp:TextBox ID="txt_PrintProcess" runat="server"></asp:TextBox>
                </div>
        
                <!-- Product Details -->
                <div class="product-section">
                    <h3 class="section-header">PRODUCT DETAILS</h3>
                    
                    <div class="field-row">
                        <label>Partcode/Number:</label>
                        <asp:TextBox ID="txt_PartCode" runat="server"></asp:TextBox>
                    </div>
                    
                    <div class="field-row">
                        <label>Item Description:</label>
                        <asp:TextBox ID="txt_ItemDescription" runat="server"></asp:TextBox>
                    </div>
                    
                    <div class="field-row">
                        <label>Material:</label>
                        <asp:DropDownList ID="ddl_Material" runat="server">
                            <asp:ListItem Text="Material Type" Value="" />
                            <asp:ListItem Text="CORRUGATED" Value="CORRUGATED" />
                            <asp:ListItem Text="PLASTIC/BUBBLE BAG" Value="PLASTIC" />
                            <asp:ListItem Text="FOAM" Value="FOAM" />
                            <asp:ListItem Text="ESPS" Value="ESPS" />
                            <asp:ListItem Text="ESD" Value="ESD" />
                            <asp:ListItem Text="WOOD" Value="WOOD" />
                            <asp:ListItem Text="Other" Value="Other" />
                        </asp:DropDownList>
                    </div>
                    
                    <div class="field-row">
                        <label>Other Material:</label>
                        <asp:TextBox ID="txt_OtherMaterial" runat="server" Enabled="false"></asp:TextBox>
                    </div>
                    
                    <div class="product-detail-row">
                        <div class="field-group">
                            <label>Size/Dimension:</label>
                            <asp:DropDownList ID="ddl_SizeDimension" runat="server">
                                <asp:ListItem Text="ID/OD" Value="" />
                                <asp:ListItem Text="Inner Dimension" Value="InnerDimension" />
                                <asp:ListItem Text="Outer Dimension" Value="OuterDimension" />
                            </asp:DropDownList>
                        </div>
                        
                        <div class="field-group">
                            <label>Tolerance:</label>
                            <asp:TextBox ID="txt_Tolerance" runat="server"></asp:TextBox>
                        </div>
                        
                        <div class="field-group">
                            <label>No. of Print Color:</label>
                            <asp:TextBox ID="txt_PrintColorCount" runat="server" TextMode="Number" min="1"></asp:TextBox>
                        </div>
                    </div>
                    
                    <div class="field-row">
                        <label>Printing Tolerance:</label>
                        <asp:TextBox ID="txt_PrintingTolerance" runat="server"></asp:TextBox>
                    </div>
                </div>
            </div>
        </div>


        <!-- Test Requirements Section -->
        <div class="form-section">
            <div class="flex-container">
                <!-- Test Requirements -->
                <div class="test-section">
                    <h3 class="section-header">TEST REQUIREMENTS BY CLIENT</h3>
                    
                    <div class="checkbox-group">
                        <div class="checkbox-with-value">
                            <asp:CheckBox ID="chk_BCT" runat="server" />
                            <span>BCT;</span>
                            <span class="value-label">Value:</span>
                            <asp:TextBox ID="txt_BCT" runat="server" CssClass="value-input"></asp:TextBox>
                        </div>
                            
                        <div class="checkbox-with-value">
                            <asp:CheckBox ID="chk_BURSTING" runat="server" />
                            <span>BURSTING;</span>
                            <span class="value-label">Value:</span>
                            <asp:TextBox ID="txt_Bursting" runat="server" CssClass="value-input"></asp:TextBox>
                        </div>
                        
                        <div class="checkbox-with-value">
                            <asp:CheckBox ID="chk_ECT" runat="server" />
                            <span>ECT;</span>
                            <span class="value-label">Value:</span>
                            <asp:TextBox ID="txt_ECT" runat="server" CssClass="value-input"></asp:TextBox>
                        </div>
                        
                        <div class="checkbox-with-value">
                            <asp:CheckBox ID="chk_DROPTEST" runat="server" />
                            <span>DROPTEST;</span>
                            <span class="value-label">Value:</span>
                            <asp:TextBox ID="txt_DropTest" runat="server" CssClass="value-input"></asp:TextBox>
                        </div>
                        
                        <div class="checkbox-with-value">
                            <asp:CheckBox ID="chk_PAPERCOM" runat="server" />
                            <span>PAPERCOM;</span>
                            <span class="value-label">Value:</span>
                            <asp:TextBox ID="txt_PaperCompression" runat="server" CssClass="value-input"></asp:TextBox>
                        </div>
                        
                        <div class="checkbox-with-value">
                            <asp:CheckBox ID="chk_RCT" runat="server" />
                            <span>RCT;</span>
                            <span class="value-label">Value:</span>
                            <asp:TextBox ID="txt_RCT" runat="server" CssClass="value-input"></asp:TextBox>
                        </div>
                        
                        <div class="checkbox-with-value">
                            <asp:CheckBox ID="chk_COBBTEST" runat="server" />
                            <span>COBB TEST;</span>
                            <span class="value-label">Value:</span>
                            <asp:TextBox ID="txt_CobbTest" runat="server" CssClass="value-input"></asp:TextBox>
                        </div>
                    </div>
                    
                    <label>Other:</label>
                    <asp:TextBox ID="txt_OtherTestRequirement" runat="server"></asp:TextBox>
                </div>
                
                <!-- Other Test -->
                <div class="test-section">
                    <h3 class="section-header">OTHER TEST</h3>
                    
                    <div class="checkbox-group">
                        <div class="checkbox-with-value">
                            <asp:CheckBox ID="chk_ROHS1" runat="server" />
                            <span>ROHS 1;</span>
                            <span class="value-label">Value:</span>
                            <asp:TextBox ID="txt_ROHS1" runat="server" CssClass="value-input"></asp:TextBox>
                        </div>
                            
                        <div class="checkbox-with-value">
                            <asp:CheckBox ID="chk_ROHS2" runat="server" />
                            <span>ROHS 2;</span>
                            <span class="value-label">Value:</span>
                            <asp:TextBox ID="txt_ROHS2" runat="server" CssClass="value-input"></asp:TextBox>
                        </div>
                        
                        <div class="checkbox-with-value">
                            <asp:CheckBox ID="chk_XRFINTERNAL" runat="server" />
                            <span>XRF INTERNAL;</span>
                            <span class="value-label">Value:</span>
                            <asp:TextBox ID="txt_XRFInternal" runat="server" CssClass="value-input"></asp:TextBox>
                        </div>
                        
                        <label class="checkbox-label">
                            <asp:CheckBox ID="chk_OtherTest" runat="server" Text=" OTHER TEST" />
                        </label>
                    </div>
                    
                    <label>Other:</label>
                    <asp:TextBox ID="txt_OtherTest" runat="server"></asp:TextBox>
                </div>
                
                <!-- Nature of Project -->
                <div class="test-section">
                    <h3 class="section-header">NATURE OF PROJECT</h3>
                    
                    <div class="checkbox-group">
                        <label class="checkbox-label">
                            <asp:CheckBox ID="chk_NewItem" runat="server" Text=" New Item" />
                        </label>
                            
                        <label class="checkbox-label">
                            <asp:CheckBox ID="chk_ExistingItem" runat="server" Text=" Existing Item" />
                        </label>
                        
                        <label class="checkbox-label">
                            <asp:CheckBox ID="chk_CustomerSuppliedDrawing" runat="server" Text=" Customer Supplied Drawing" />
                        </label>
                        
                        <label class="checkbox-label">
                            <asp:CheckBox ID="chk_CustomerSuppliedSample" runat="server" Text=" Customer Supplied Sample" />
                        </label>
                        
                        <label class="checkbox-label">
                            <asp:CheckBox ID="chk_CustomerSuppliedProduct" runat="server" Text=" Customer Supplied Product" />
                        </label>
                        
                        <div class="checkbox-with-value">
                            <asp:CheckBox ID="chk_Revision" runat="server" />
                            <span>Revision No.:</span>
                            <span class="value-label">Value:</span>
                            <asp:TextBox ID="txt_RevisionNumber" runat="server" CssClass="value-input"></asp:TextBox>
                        </div>
                    </div>
                </div>
            </div>
        </div>


        <!-- Special Instructions Section -->
        <div class="form-section">
            <div class="flex-container">
                <!-- Product Description -->
                <div class="print-section">
                    <h3 class="section-header">Product Description / Special Instructions</h3>
                    <asp:TextBox ID="txt_SpecialInstructions" runat="server" TextMode="MultiLine"
                        placeholder="Enter any additional product description or special instructions here..."></asp:TextBox>
                    
                    <div class="file-upload">
                        <label>Upload File (Optional):</label>
                        <asp:FileUpload ID="file_SpecialInstructions" runat="server" />
                    </div>
                </div>
        
                <!-- Illustration/Sketch -->
                <div class="product-section">
                    <h3 class="section-header">Illustration / Sketch</h3>
                    <div class="file-upload">
                        <label>Upload Illustration / Sketch (Optional):</label>
                        <asp:FileUpload ID="file_Illustration" runat="server" />
                    </div>
                    
                    <div class="field-row">
                        <label>Notes on Illustration:</label>
                        <asp:TextBox ID="txt_Illustration" runat="server" TextMode="MultiLine"
                            placeholder="Enter any notes about the illustration or sketch..."></asp:TextBox>
                    </div>
                </div>
            </div>
        </div>
        
        <!-- Requested by / Noted by Section -->
        <div class="form-section">
            <div class="flex-container">
                <div style="width: 50%;">
                    <h3 class="section-header">Requested by:</h3>
                    <asp:TextBox ID="txt_RequestBy" runat="server" placeholder="Enter name"></asp:TextBox>
                </div>
                <div style="width: 50%;">
                    <h3 class="section-header">Noted by:</h3>
                    <asp:TextBox ID="txt_NotedBy" runat="server" placeholder="Enter name"></asp:TextBox>
                </div>
            </div>
        </div>
        
        <!-- Submit Button -->
        <div class="center">
            <asp:Button ID="btn_submit" runat="server" Text="Submit Request" 
                OnClick="btn_submit_Click" CssClass="custom-button" />
        </div>
    </div>
<script src="../Scripts/Javascript/FormScript.js" defer></script>
</asp:Content>
