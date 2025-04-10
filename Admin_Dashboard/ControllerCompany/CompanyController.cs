using Admin_Dashboard.DAL;
using Admin_Dashboard.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NuGet.Common;
using NuGet.Protocol;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Admin_Dashboard.ControllerCompany
{
    public class CompanyController : Controller
    {
        private readonly DataLayer _layer;
        private readonly IConfiguration _config;

        public CompanyController(DataLayer layer, IConfiguration config)
        {
            _layer = layer;
            _config = config;
        }
        public IActionResult Login() 
        { 
            return View(); 
        }
        [HttpPost]
        public IActionResult Login(LoginClass login)
        {
            var res =_layer.Companyusers.SingleOrDefault(x=>x.UserName == login.UserName && x.Password==login.Password);
            if (res!=null)
            {
                var token = GenerateToken(res);
                TempData["Userid"] = res.FirstName+" "+res.LastName;
                HttpContext.Session.SetString("Token", token);
                return RedirectToAction("Index",new {Token=token});
            }
            else
            {
                ViewBag.Message = "<Span style='color:red;'> User Not Found!!! Username or password incorrect</span>";
                return View();
            }

        }
        public IActionResult CreateUser()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateUser(CompanyUsers user)
        {
            bool res = _layer.Companyusers.Any(x => x.UserName == user.UserName);
            if (!res)
            {
                _layer.Companyusers.Add(user);
                _layer.SaveChanges();
                return RedirectToAction("Login");
            }
            else
                return View();
        }
      
        private string GenerateToken(CompanyUsers user)
        {
             

            var sec = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Key"]));
            var cred= new SigningCredentials(sec,SecurityAlgorithms.HmacSha256);
            var payload = new List<Claim>
            {
                new Claim("ID",user.UserId.ToString()),
                new Claim("UserObject",user.ToJson()),
               
            };
            if (user.AdminAccess == true)
            {
                HttpContext.Session.SetString("SuperAdmin", "Super Admin");
                 payload.Add( new Claim(ClaimTypes.Role, "admin"));
                payload.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            }

            var token = new JwtSecurityToken(_config["JWT:Issuer"], _config["JWT:Audience"],
                claims:payload,expires:DateTime.Now.AddMinutes(10),signingCredentials:cred);
            var Token = new JwtSecurityTokenHandler().WriteToken(token);
            return Token;
        }
        [HttpGet]
       
        public IActionResult Index()
        {
            if(HttpContext.Session.GetString("SuperAdmin") != null)
            {
                return View();
            }else 
                return BadRequest();
           
            
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            
            return RedirectToAction("Login");
        }
        [HttpGet]
        public JsonResult GetUserName(string name)
        {
            bool isexists = _layer.Companyusers.Any(x => x.UserName == name);
            if (isexists==false)
                return Json(isexists);
            else
                return Json(isexists);
            
        }
        //public IActionResult GetToken()
        //{
        //    var to
        //}
    }
}
