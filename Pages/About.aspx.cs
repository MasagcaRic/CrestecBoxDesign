using Final_Project.Data;
using Final_Project.Data.Entities;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Final_Project
{
    public partial class About : Page
    {
        //protected void Page_Load(object sender, EventArgs e)
        //{
        //    if (Request.Cookies["SESSIONID"] == null)
        //    {
        //        Response.Redirect("Login.aspx");
        //        return;
        //    }
        //
        //    string jwtToken = Request.Cookies["SESSIONID"].Value;
        //    string role = ValidateJwtToken(jwtToken);
        //
        //    if (role == null) // Token is invalid
        //    {
        //        LogoutAndRedirect();
        //        return;
        //    }
        //
        //    if (role != "User") // Prevent admin from accessing user pages if necessary
        //    {
        //        Response.Redirect("Contact.aspx");
        //    }
        //    Response.Cache.SetCacheability(HttpCacheability.NoCache);
        //    Response.Cache.SetNoStore();
        //    Response.AppendHeader("Pragma", "no-cache");
        //    Response.AppendHeader("Expires", "0");
        //
        //}
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Cookies["SESSIONID"] == null)
            {
                Response.Redirect("Login.aspx");
                return;
            }

            string jwtToken = Request.Cookies["SESSIONID"].Value;
            string role = ValidateJwtToken(jwtToken);

            if (role == null)
            {
                LogoutAndRedirect();
                return;
            }

            if (!IsPostBack)
            {
                if (Request.QueryString["RequestID"] != null)
                {
                    int requestId;
                    if (int.TryParse(Request.QueryString["RequestID"], out requestId))
                    {
                        LoadRequestDetails(requestId);
                    }
                }
            }
        }

        private void LoadRequestDetails(int requestId)
        {
            using (var db = new AppDbContext())
            {
                var request = (from req in db.BoxRequests
                               join cust in db.Customers on req.CustomerID equals cust.CustomerID
                               join reqType in db.RequestTypes on req.RequestTypeID equals reqType.RequestTypeID
                               join print in db.PrintingDetails on req.RequestID equals print.RequestID into printJoin
                               from print in printJoin.DefaultIfEmpty()
                               where req.RequestID == requestId
                               select new
                               {
                                   req.RequestID,
                                   reqType.TypeName,
                                   cust.Name,
                                   cust.ContactPerson,
                                   req.PartCode,
                                   req.ItemDescription,
                                   req.Material,
                                   req.Quantity,
                                   req.DateNeeded,
                                   req.SpecialInstructions,
                                   req.Illustration,
                                   PrintColorCount = print != null ? print.PrintColorCount : 0,
                                   PrintingTolerance = print != null ? print.PrintingTolerance : null,
                                   PrintProcess = print != null ? print.PrintProcess : null,
                                   Sales = db.SalesInCharges.Where(s => s.RequestID == req.RequestID).Select(s => s.Name).ToList(),
                                   TestRequirements = db.TestRequirements.Where(t => t.RequestID == req.RequestID).ToList(),
                                   OtherTests = db.OtherTests.Where(ot => ot.RequestID == req.RequestID).ToList(),
                                   NatureOfProject = db.NatureOfProjects.FirstOrDefault(n => n.RequestID == req.RequestID)
                               }).FirstOrDefault();

                if (request != null)
                {
                    // Fill text fields
                    txt_CustomerName.Text = request.Name;
                    txt_ContactPerson.Text = request.ContactPerson;
                    txt_PartCode.Text = request.PartCode;
                    txt_ItemDescription.Text = request.ItemDescription;
                    txt_OtherMaterial.Text = request.Material;
                    txt_Quantity.Text = request.Quantity.ToString();
                    txt_DateNeeded.Text = request.DateNeeded.ToString("yyyy-MM-dd");
                    txt_SpecialInstructions.Text = request.SpecialInstructions;
                    txt_PrintColorCount.Text = request.PrintColorCount.ToString();
                    txt_PrintingTolerance.Text = request.PrintingTolerance;  // FIXED: Assign correct Printing Tolerance

                    // Type of Request Checkboxes
                    chk_IDFreq.Checked = request.TypeName == "IDF REQUEST";
                    chk_FDFreq.Checked = request.TypeName == "FDF REQUEST";
                    chk_SAMPLEreq.Checked = request.TypeName == "SAMPLE REQUEST";

                    // Sales in Charge Fields
                    if (request.Sales.Count >= 4)
                    {
                        txt_SalesInCharge.Text = request.Sales[0];
                        txt_SalesJapanDesk.Text = request.Sales[1];
                        txt_SalesQaLtc.Text = request.Sales[2];
                        txt_SalesQaLisp3.Text = request.Sales[3];
                    }

                    // Print Process Checkboxes

                    if (!string.IsNullOrEmpty(request.PrintProcess))
                    {
                        var printProcesses = request.PrintProcess.Split(new[] { ", " }, StringSplitOptions.RemoveEmptyEntries);

                        chk_NOprint.Checked = printProcesses.Contains("NO PRINT");
                        chk_SILKprint.Checked = printProcesses.Contains("SILK SCREEN");
                        chk_OFFSETprint.Checked = printProcesses.Contains("OFFSET");
                        chk_ROTOGRAVURE.Checked = printProcesses.Contains("ROTOGRAVURE");
                        chk_HYBRIDprint.Checked = printProcesses.Contains("HYBRID PRINTING");

                        // Fill the text box if there was a custom print process
                        txt_PrintProcess.Text = string.Join(", ", printProcesses.Except(new[]
                        {
                            "NO PRINT", "SILK SCREEN", "OFFSET", "ROTOGRAVURE", "HYBRID PRINTING"
                        }));
                    }



                    // Test Requirements Checkboxes & Values
                    foreach (var test in request.TestRequirements)
                    {
                        switch (test.TestType)
                        {
                            case "BCT": chk_BCT.Checked = true; txt_BCT.Text = test.TestValue; break;
                            case "BURSTING": chk_BURSTING.Checked = true; txt_Bursting.Text = test.TestValue; break;
                            case "ECT": chk_ECT.Checked = true; txt_ECT.Text = test.TestValue; break;
                            case "DROPTEST": chk_DROPTEST.Checked = true; txt_DropTest.Text = test.TestValue; break;
                            case "PAPERCOM": chk_PAPERCOM.Checked = true; txt_PaperCompression.Text = test.TestValue; break;
                            case "RCT": chk_RCT.Checked = true; txt_RCT.Text = test.TestValue; break;
                            case "COBB TEST": chk_COBBTEST.Checked = true; txt_CobbTest.Text = test.TestValue; break;
                        }
                    }

                    // Other Test Checkboxes & Values
                    foreach (var test in request.OtherTests)
                    {
                        switch (test.TestType)
                        {
                            case "ROHS 1": chk_ROHS1.Checked = true; txt_ROHS1.Text = test.SpecialRequest; break;
                            case "ROHS 2": chk_ROHS2.Checked = true; txt_ROHS2.Text = test.SpecialRequest; break;
                            case "XRF INTERNAL": chk_XRFINTERNAL.Checked = true; txt_XRFInternal.Text = test.SpecialRequest; break;
                            case "OTHER TEST": chk_OtherTest.Checked = true; txt_OtherTest.Text = test.SpecialRequest; break;
                        }
                    }

                    // Nature of Project Checkboxes
                    if (request.NatureOfProject != null)
                    {
                        chk_NewItem.Checked = request.NatureOfProject.NewItem;
                        chk_ExistingItem.Checked = request.NatureOfProject.ExistingItem;
                        chk_CustomerSuppliedDrawing.Checked = request.NatureOfProject.CustomerSuppliedDrawing;
                        chk_CustomerSuppliedSample.Checked = request.NatureOfProject.CustomerSuppliedSample;
                        chk_CustomerSuppliedProduct.Checked = request.NatureOfProject.CustomerSuppliedProduct;
                        txt_RevisionNumber.Text = request.NatureOfProject.RevisionNumber;
                    }
                }
            }
        }




        private string ValidateJwtToken(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("H7FWGsdsbd7nwpGbSpENafAPz104hOf8")),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                };

                var principal = tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);

                if (validatedToken is JwtSecurityToken jwtToken && jwtToken.ValidTo >= DateTime.UtcNow)
                {
                    var roleClaim = principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);
                    return roleClaim?.Value;
                }
            }
            catch { }
            return null;
        }

        private void LogoutAndRedirect()
        {
            HttpCookie expiredCookie = new HttpCookie("SESSIONID")
            {
                Expires = DateTime.UtcNow.AddDays(-1),
                HttpOnly = true,
                Secure = Request.IsSecureConnection
            };
            Response.Cookies.Add(expiredCookie);
            Response.Redirect("Login.aspx");
        }

        private int GetUserIDFromToken()
        {
            string jwtToken = Request.Cookies["SESSIONID"]?.Value;
            if (string.IsNullOrEmpty(jwtToken)) return 0;

            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("H7FWGsdsbd7nwpGbSpENafAPz104hOf8")),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                };

                var principal = tokenHandler.ValidateToken(jwtToken, validationParameters, out SecurityToken validatedToken);

                if (validatedToken is JwtSecurityToken jwtTokenObj && jwtTokenObj.ValidTo >= DateTime.UtcNow)
                {
                    var userIdClaim = principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
                    return userIdClaim != null ? int.Parse(userIdClaim.Value) : 0;
                }
            }
            catch { }

            return 0;
        }
        protected void btn_submit_Click(object sender, EventArgs e)
        {
            using (var db = new AppDbContext())
            {
                try
                {
                    // Retrieve RequestType ID based on selected request
                    int requestTypeId = 0;

                    if (chk_IDFreq.Checked)
                        requestTypeId = db.RequestTypes.FirstOrDefault(r => r.TypeName == "IDF REQUEST")?.RequestTypeID ?? 0;
                    else if (chk_FDFreq.Checked)
                        requestTypeId = db.RequestTypes.FirstOrDefault(r => r.TypeName == "FDF REQUEST")?.RequestTypeID ?? 0;
                    else if (chk_SAMPLEreq.Checked)
                        requestTypeId = db.RequestTypes.FirstOrDefault(r => r.TypeName == "SAMPLE REQUEST")?.RequestTypeID ?? 0;

                    // Ensure a valid ID is selected
                    if (requestTypeId == 0)
                    {
                        lblMessage.Text = "Error: Invalid request type selected.";
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                        return;
                    }

                    // Validate fields
                    //if (requestTypeId == 0 || !DateTime.TryParse(txt_DateNeeded.Text, out DateTime dateNeeded) ||
                    //    !int.TryParse(txt_Quantity.Text, out int quantity) || quantity <= 0 ||
                    //    string.IsNullOrEmpty(txt_CustomerName.Text.Trim()) ||
                    //    string.IsNullOrEmpty(txt_ContactPerson.Text.Trim()))
                    //{
                    //    lblMessage.Text = "Please fill in all required fields.";
                    //    lblMessage.ForeColor = System.Drawing.Color.Red;
                    //    return;
                    //}

                    // Attempt to parse DateNeeded; if it fails, set a default value
                    if (!DateTime.TryParse(txt_DateNeeded.Text, out DateTime dateNeeded))
                    {
                        dateNeeded = DateTime.UtcNow; // Set default value
                    }


                    // Attempt to parse Quantity; if it fails, set a default value
                    if (!int.TryParse(txt_Quantity.Text, out int quantity) || quantity <= 0)
                    {
                        quantity = 1; // Default value
                    }


                    // Ensure requestTypeId is set (default to a valid type if none is selected)
                    if (requestTypeId == 0)
                    {
                        requestTypeId = db.RequestTypes.FirstOrDefault()?.RequestTypeID ?? 1;
                    }

                    // Proceed with submission


                    // Find or create customer
                    var customer = db.Customers.FirstOrDefault(c => c.Name == txt_CustomerName.Text.Trim()) ??
                                   new Customer
                                   {
                                       Name = txt_CustomerName.Text.Trim(),
                                       ContactPerson = txt_ContactPerson.Text.Trim()
                                   };

                    if (customer.CustomerID == 0)
                        db.Customers.Add(customer);

                    db.SaveChanges();

                    // Create new BoxRequest
                    var newRequest = new BoxRequest
                    {
                        CustomerID = customer.CustomerID,
                        RequestTypeID = requestTypeId,
                        RequestBy = GetUserIDFromToken(),
                        NotedBy = txt_NotedBy.Text.Trim(),
                        DateNeeded = dateNeeded,
                        DateSubmitted = DateTime.UtcNow,
                        Quantity = quantity,
                        PartCode = txt_PartCode.Text.Trim(),
                        ItemDescription = txt_ItemDescription.Text.Trim(),
                        Material = ddl_Material.SelectedValue == "Other" ? txt_OtherMaterial.Text.Trim() : ddl_Material.SelectedValue,
                        SpecialInstructions = txt_SpecialInstructions.Text.Trim(),
                        Illustration = txt_Illustration.Text.Trim()
                    };

                    db.BoxRequests.Add(newRequest);
                    db.SaveChanges();

                    // Insert Printing Details
                    string printProcess = string.Join(", ", new[]
                    {
                        chk_NOprint.Checked ? "NO PRINT" : null,
                        chk_SILKprint.Checked ? "SILK SCREEN" : null,
                        chk_OFFSETprint.Checked ? "OFFSET" : null,
                        chk_ROTOGRAVURE.Checked ? "ROTOGRAVURE" : null,
                        chk_HYBRIDprint.Checked ? "HYBRID PRINTING" : null
                    }.Where(p => p != null));

                    db.PrintingDetails.Add(new PrintingDetails
                    {
                        RequestID = newRequest.RequestID,
                        PrintColorCount = int.TryParse(txt_PrintColorCount.Text, out int printQty) ? printQty : 0,
                        PrintingTolerance = txt_PrintingTolerance.Text.Trim(),
                        PrintProcess = string.IsNullOrWhiteSpace(txt_PrintProcess.Text.Trim())
    ?                       printProcess
    :                       printProcess + ", " + txt_PrintProcess.Text.Trim()
                    });

                    db.SaveChanges();

                    // Insert Test Requirements
                    var testRequirements = new[]
                    {
                        (chk_BCT.Checked, "BCT", txt_BCT.Text.Trim()),
                        (chk_BURSTING.Checked, "BURSTING", txt_Bursting.Text.Trim()),
                        (chk_ECT.Checked, "ECT", txt_ECT.Text.Trim()),
                        (chk_DROPTEST.Checked, "DROPTEST", txt_DropTest.Text.Trim()),
                        (chk_PAPERCOM.Checked, "PAPERCOM", txt_PaperCompression.Text.Trim()),
                        (chk_RCT.Checked, "RCT", txt_RCT.Text.Trim()),
                        (chk_COBBTEST.Checked, "COBB TEST", txt_CobbTest.Text.Trim())
                    };

                    foreach (var (checkedBox, type, value) in testRequirements)
                    {
                        if (checkedBox || !string.IsNullOrWhiteSpace(value)) // Save if checked OR has text
                        {
                            db.TestRequirements.Add(new TestRequirement
                            {
                                RequestID = newRequest.RequestID,
                                TestType = type,
                                TestValue = value
                            });
                        }
                    }

                    db.SaveChanges();
                    // Insert Other Test Requirements
                    var otherTests = new[]
                    {
                        (chk_ROHS1.Checked, "ROHS 1", txt_ROHS1.Text.Trim()),
                        (chk_ROHS2.Checked, "ROHS 2", txt_ROHS2.Text.Trim()),
                        (chk_XRFINTERNAL.Checked, "XRF INTERNAL", txt_XRFInternal.Text.Trim()),
                        (chk_OtherTest.Checked, "OTHER TEST", txt_OtherTest.Text.Trim())
                    };

                    foreach (var (shouldSave, type, value) in otherTests)
                    {
                        if (shouldSave) // Save only if checkbox is checked OR text exists
                        {
                            db.OtherTests.Add(new OtherTest
                            {
                                RequestID = newRequest.RequestID,
                                TestType = type,
                                SpecialRequest = value
                            });
                        }
                    }

                    db.SaveChanges();

                    // Insert Nature of Project
                    db.NatureOfProjects.Add(new NatureOfProject
                    {
                        RequestID = newRequest.RequestID,
                        NewItem = chk_NewItem.Checked,
                        ExistingItem = chk_ExistingItem.Checked,
                        CustomerSuppliedDrawing = chk_CustomerSuppliedDrawing.Checked,
                        CustomerSuppliedSample = chk_CustomerSuppliedSample.Checked,
                        CustomerSuppliedProduct = chk_CustomerSuppliedProduct.Checked,
                        RevisionNumber = txt_RevisionNumber.Text.Trim()
                    });

                    db.SaveChanges();

                    lblMessage.Text = "Request submitted successfully!";
                    lblMessage.ForeColor = System.Drawing.Color.Green;
                    // Insert Sales In Charge details in a single batch operation
                    db.SalesInCharges.AddRange(new List<SalesInCharge>
                    {
                        new SalesInCharge { RequestID = newRequest.RequestID, Name = txt_SalesInCharge.Text.Trim() },
                        new SalesInCharge { RequestID = newRequest.RequestID, Name = txt_SalesJapanDesk.Text.Trim() },
                        new SalesInCharge { RequestID = newRequest.RequestID, Name = txt_SalesQaLtc.Text.Trim() },
                        new SalesInCharge { RequestID = newRequest.RequestID, Name = txt_SalesQaLisp3.Text.Trim() }
                    });

                    db.SaveChanges();

                    // Insert Special Instructions                
                    newRequest.SpecialInstructions = txt_SpecialInstructions.Text.Trim();
                    db.SaveChanges();

                    // Also insert into SpecialInstruction table if provided
                    if (!string.IsNullOrEmpty(txt_SpecialInstructions.Text.Trim()))
                    {
                        db.SpecialInstructions.Add(new SpecialInstruction
                        {
                            RequestID = newRequest.RequestID,
                            Description = txt_SpecialInstructions.Text.Trim()
                        });

                        db.SaveChanges();
                    }

                    // Insert default request status (Pending)
                    db.RequestStatuses.Add(new RequestStatus
                    {
                        RequestID = newRequest.RequestID,
                        Status = "Pending",
                        ReviewedBy = null,  // Not yet reviewed
                        ReviewDate = DateTime.UtcNow
                    });

                    db.SaveChanges();

                    Debug.WriteLine("Request Type ID: " + requestTypeId);


                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Error submitting request: " + ex.Message);

                    Exception inner = ex.InnerException;
                    while (inner != null)  // Loop through inner exceptions
                    {
                        Debug.WriteLine("Inner Exception: " + inner.Message);
                        inner = inner.InnerException;
                    }

                    lblMessage.Text = "An error occurred while submitting the request.";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                }


            }
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            HttpCookie expiredCookie = new HttpCookie("SESSIONID")
            {
                Expires = DateTime.UtcNow.AddDays(-1),
                HttpOnly = true,
                Secure = Request.IsSecureConnection
            };
            Response.Cookies.Add(expiredCookie);
            Response.Redirect("Login.aspx");
        }
        protected void btnDash_Click(object sender, EventArgs e)
        {
            Response.Redirect("UserDashboard.aspx");
        }

    }
}
