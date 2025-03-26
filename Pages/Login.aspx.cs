using Final_Project.Data;
using Final_Project.Data.Entities;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;

namespace Final_Project
{
    public partial class _Default : Page
    {
        private const string SecretKey = "H7FWGsdsbd7nwpGbSpENafAPz104hOf8";
        private const string CookieName = "SESSIONID";

        protected void Page_Load(object sender, EventArgs e)
        {
            // Clear previous session cookie immediately
            if (Request.Cookies[CookieName] != null)
            {
                HttpCookie expiredCookie = new HttpCookie(CookieName)
                {
                    Expires = DateTime.UtcNow.AddDays(-1),
                    HttpOnly = true,
                    Secure = Request.IsSecureConnection
                };
                Response.Cookies.Add(expiredCookie);
            }

            // Prevent page caching to ensure logout works properly
            Response.Cache.SetNoStore();
            Response.AppendHeader("Cache-Control", "no-cache, no-store, must-revalidate");
            Response.AppendHeader("Pragma", "no-cache");
            Response.AppendHeader("Expires", "0");

            // Now check if there's a valid session and redirect accordingly
            if (Request.Cookies[CookieName] != null)
            {
                string jwtToken = Request.Cookies[CookieName].Value;
                var role = ValidateJwtToken(jwtToken);

                if (role == null)
                {
                    // Ensure invalid session is removed before reloading
                    HttpCookie expiredCookie = new HttpCookie(CookieName)
                    {
                        Expires = DateTime.UtcNow.AddDays(-1),
                        HttpOnly = true,
                        Secure = Request.IsSecureConnection
                    };
                    Response.Cookies.Add(expiredCookie);
                    Response.Redirect(Request.RawUrl, true);
                    return;
                }

                Response.Redirect(role == "Admin" ? "Contact.aspx" : "About.aspx");
            }
        }


        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string email = txt_login_email.Value;
            string password = txt_login_password.Value;

            var result = Authenticate(email, password);
            var account = result.Item1;
            var isAdmin = result.Item2;

            if (account == null)
            {
                lblMessage.Text = "Invalid email or password.";
                return;
            }

            string token = GenerateJwtToken(account, isAdmin);

            HttpCookie authCookie = new HttpCookie(CookieName, token)
            {
                HttpOnly = true,
                Secure = Request.IsSecureConnection,
                Expires = DateTime.UtcNow.AddHours(2)
            };
            Response.Cookies.Add(authCookie);
            Response.Redirect(isAdmin ? "Contact.aspx" : "About.aspx");
        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            var firstName = txt_create_first_name.Value;
            var lastName = txt_create_last_name.Value;
            var email = txt_create_signup_email.Value;
            var password = txt_create_signup_password.Value;

            var hashedPassword = HashPassword(password);

            using (var db = new AppDbContext())
            {
                //var admin = new Admin
                //{
                //    FirstName = firstName,
                //    LastName = lastName,
                //    Email = email,
                //    Password = hashedPassword
                //};
                //
                //db.Admins.Add(admin);
                //db.SaveChanges();
                //Debug.WriteLine("Admin created successfully.");



                var user = new User
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    Password = hashedPassword
                };
                
                db.Users.Add(user);
                db.SaveChanges();
                Debug.WriteLine("User created successfully.");
            }
        }
        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(password);
                byte[] hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }
        private Tuple<object, bool> Authenticate(string email, string password)
        {
            string hashedPassword = HashPassword(password);
            using (var db = new AppDbContext())
            {
                var admin = db.Admins.FirstOrDefault(a => a.Email == email && a.Password == hashedPassword);
                if (admin != null) return new Tuple<object, bool>(admin, true);

                var user = db.Users.FirstOrDefault(u => u.Email == email && u.Password == hashedPassword);
                if (user != null) return new Tuple<object, bool>(user, false);
            }
            return new Tuple<object, bool>(null, false);
        }

        private string GenerateJwtToken(object account, bool isAdmin)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            string email, firstName, lastName;
            if (isAdmin)
            {
                var admin = (Admin)account;
                email = admin.Email;
                firstName = admin.FirstName;
                lastName = admin.LastName;
            }
            else
            {
                var user = (User)account;
                email = user.Email;
                firstName = user.FirstName;
                lastName = user.LastName;
            }

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, email),
                new Claim(JwtRegisteredClaimNames.UniqueName, firstName + " " + lastName),
                new Claim(ClaimTypes.Role, isAdmin ? "Admin" : "User"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private string ValidateJwtToken(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey)),
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
    }
}
