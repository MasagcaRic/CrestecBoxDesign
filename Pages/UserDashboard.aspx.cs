using Final_Project.Data;
using Final_Project.Data.Entities;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Final_Project
{
    public partial class UserDashboard : Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Cookies["SESSIONID"] == null)
            {
                Response.Redirect("Login.aspx");
                return;
            }

            string jwtToken = Request.Cookies["SESSIONID"].Value;
            string role = ValidateJwtToken(jwtToken);

            if (role != "User")
            {
                Response.Redirect("Contact.aspx");
            }

            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();
            Response.AppendHeader("Pragma", "no-cache");
            Response.AppendHeader("Expires", "0");

            if (!IsPostBack) // Load data only on first page load
            {
                LoadRequests();
            }
        }
        private void LoadRequests()
        {
            using (var db = new AppDbContext())
            {
                var requestList = (from req in db.BoxRequests
                                   join cust in db.Customers on req.CustomerID equals cust.CustomerID
                                   join reqType in db.RequestTypes on req.RequestTypeID equals reqType.RequestTypeID
                                   join status in db.RequestStatuses on req.RequestID equals status.RequestID into statusJoin
                                   from status in statusJoin.DefaultIfEmpty()
                                   select new
                                   {
                                       RequestID = req.RequestID,
                                       DateSubmitted = req.DateSubmitted,
                                       Type = reqType.TypeName,
                                       Customer = cust.Name,
                                       PartCode = req.PartCode,
                                       DateNeeded = req.DateNeeded,
                                       Status = status != null ? status.Status : "Pending"
                                   }).ToList();

                RequestsTable.DataSource = requestList;
                RequestsTable.DataBind();
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
        protected string GetStatusColor(string status)
        {
            switch (status.ToLower())
            {
                case "approved":
                    return "status-approved"; // Green
                case "pending":
                    return "status-pending"; // Orange
                case "rejected":
                    return "status-rejected"; // Red
                default:
                    return ""; // No color
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

        protected void btnViewDetails_Click(object sender, EventArgs e)
        {

        }


        protected void btnFilter_Click(object sender, EventArgs e)
        {
            string searchTerm = SearchInput.Text.Trim();
            string filterType = FilterType.SelectedValue;
            string filterStatus = FilterStatus.SelectedValue;

            using (var db = new AppDbContext())
            {
                var query = from req in db.BoxRequests
                            join cust in db.Customers on req.CustomerID equals cust.CustomerID
                            join reqType in db.RequestTypes on req.RequestTypeID equals reqType.RequestTypeID
                            join status in db.RequestStatuses on req.RequestID equals status.RequestID into statusJoin
                            from status in statusJoin.DefaultIfEmpty()
                            select new
                            {
                                RequestID = req.RequestID,
                                DateSubmitted = req.DateSubmitted,
                                Type = reqType.TypeName,
                                Customer = cust.Name,
                                PartCode = req.PartCode,
                                DateNeeded = req.DateNeeded,
                                Status = status != null ? status.Status : "Pending"
                            };

                // Apply filters
                if (!string.IsNullOrEmpty(searchTerm))
                {
                    query = query.Where(r => r.Customer.Contains(searchTerm) || r.PartCode.Contains(searchTerm));
                }

                if (!string.IsNullOrEmpty(filterType))
                {
                    query = query.Where(r => r.Type == filterType);
                }

                if (!string.IsNullOrEmpty(filterStatus))
                {
                    query = query.Where(r => r.Status == filterStatus);
                }

                RequestsTable.DataSource = query.ToList();
                RequestsTable.DataBind();
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            SearchInput.Text = "";
            FilterType.SelectedIndex = 0;
            FilterStatus.SelectedIndex = 0;
            LoadRequests(); // Reload all data
        }

        protected void RequestsTable_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            RequestsTable.PageIndex = e.NewPageIndex;
            LoadRequests(); // Reload data
        }

        protected void RequestsTable_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //if (e.CommandName == "ViewDetails")
            //{
            //    int requestId = Convert.ToInt32(e.CommandArgument);
            //
            //    using (var db = new AppDbContext())
            //    {
            //        var request = (from req in db.BoxRequests
            //                       join cust in db.Customers on req.CustomerID equals cust.CustomerID
            //                       join reqType in db.RequestTypes on req.RequestTypeID equals reqType.RequestTypeID
            //                       where req.RequestID == requestId
            //                       select new
            //                       {
            //                           req.RequestID,
            //                           reqType.TypeName,
            //                           cust.Name,
            //                           req.PartCode,
            //                           req.DateNeeded,
            //                           req.SpecialInstructions
            //                       }).FirstOrDefault();
            //
            //        if (request != null)
            //        {
            //            DetailRequestId.Text = request.RequestID.ToString();
            //            DetailRequestType.Text = request.TypeName;
            //            // Add more fields if needed
            //
            //            RequestModal.Style["display"] = "block"; // Show modal
            //        }
            //    }
            //}
        }
        protected void RequestsTable_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow) // Ensure it's a data row
            {
                Label lblStatus = (Label)e.Row.FindControl("lblStatus");
                if (lblStatus != null)
                {
                    lblStatus.CssClass = GetStatusColor(lblStatus.Text);
                }
            }
        }

        //private void LogoutAndRedirect()
        //{
        //    HttpCookie expiredCookie = new HttpCookie("SESSIONID")
        //    {
        //        Expires = DateTime.UtcNow.AddDays(-1),
        //        HttpOnly = true,
        //        Secure = Request.IsSecureConnection
        //    };
        //    Response.Cookies.Add(expiredCookie);
        //    Response.Redirect("Login.aspx");
        //}
        //
        //
        //
        //
        //
        protected void btnExport_Click(object sender, EventArgs e)
        {
            //using (var db = new AppDbContext())
            //{
            //    var data = from req in db.BoxRequests
            //               join cust in db.Customers on req.CustomerID equals cust.CustomerID
            //               join reqType in db.RequestTypes on req.RequestTypeID equals reqType.RequestTypeID
            //               join status in db.RequestStatuses on req.RequestID equals status.RequestID into statusJoin
            //               from status in statusJoin.DefaultIfEmpty()
            //               select new
            //               {
            //                   req.RequestID,
            //                   req.DateSubmitted,
            //                   reqType.TypeName,
            //                   cust.Name,
            //                   req.PartCode,
            //                   req.DateNeeded,
            //                   Status = status != null ? status.Status : "Pending"
            //               };
            //
            //    StringBuilder sb = new StringBuilder();
            //    sb.AppendLine("RequestID,DateSubmitted,Type,Customer,PartCode,DateNeeded,Status");
            //
            //    foreach (var row in data)
            //    {
            //        sb.AppendLine($"{row.RequestID},{row.DateSubmitted},{row.Type},{row.Customer},{row.PartCode},{row.DateNeeded},{row.Status}");
            //    }
            //
            //    Response.Clear();
            //    Response.Buffer = true;
            //    Response.AddHeader("content-disposition", "attachment;filename=Requests.csv");
            //    Response.ContentType = "text/csv";
            //    Response.Write(sb.ToString());
            //    Response.End();
            //}
        }
        //
        //
        //
        //private int GetUserIDFromToken()
        //{
        //    string jwtToken = Request.Cookies["SESSIONID"]?.Value;
        //    if (string.IsNullOrEmpty(jwtToken)) return 0;
        //
        //    try
        //    {
        //        var tokenHandler = new JwtSecurityTokenHandler();
        //        var validationParameters = new TokenValidationParameters
        //        {
        //            ValidateIssuerSigningKey = true,
        //            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("H7FWGsdsbd7nwpGbSpENafAPz104hOf8")),
        //            ValidateIssuer = false,
        //            ValidateAudience = false,
        //            ClockSkew = TimeSpan.Zero
        //        };
        //
        //        var principal = tokenHandler.ValidateToken(jwtToken, validationParameters, out SecurityToken validatedToken);
        //
        //        if (validatedToken is JwtSecurityToken jwtTokenObj && jwtTokenObj.ValidTo >= DateTime.UtcNow)
        //        {
        //            var userIdClaim = principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
        //            return userIdClaim != null ? int.Parse(userIdClaim.Value) : 0;
        //        }
        //    }
        //    catch
        //    {
        //        return 0; // Return 0 if token is invalid or UserID is not found
        //    }
        //
        //    return 0;
        //}
        //

        




    }
}
