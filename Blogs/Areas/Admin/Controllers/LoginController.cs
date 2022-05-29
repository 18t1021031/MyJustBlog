using Blogs.EF;
using Blogs.Models;
using BotDetect.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using CaptchaMvc;
using System.Net;
using Newtonsoft.Json;

namespace Blogs.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        private MyJustBlogDbContext db = new MyJustBlogDbContext();
        // GET: Admin/Login
        public ActionResult Index()
        {
            return View();
        }
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Index(LoginModel model)
        //{
        //    var result = new AccountModels().Login(model.UserName, model.Password);
        //    if(result && ModelState.IsValid)
        //    {
        //        SessionHelper.SetSession(new UserSession() { UserName = model.UserName});
        //        return RedirectToAction("Index", "HomeAdmin");
        //    }
        //    else
        //    {
        //        ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu không đúng");

        //    }
        //    return View(model);
        //}

        //public ActionResult Logout()
        //{
        //    FormsAuthentication.SignOut();
        //    return RedirectToAction("Index", "HomeAdmin");
        //}

        //GET: Register

        public ActionResult Register()
        {
            return View();
        }


        /// <summary>  
        /// Validate Captcha  
        /// </summary>  
        /// <param name="response"></param>  
        /// <returns></returns>  
        public static CaptchaResponse ValidateCaptcha(string response)
        {
            string secret = System.Web.Configuration.WebConfigurationManager.AppSettings["recaptchaPrivateKey"];
            var client = new WebClient();
            var jsonResult = client.DownloadString(string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", secret, response));
            return JsonConvert.DeserializeObject<CaptchaResponse>(jsonResult.ToString());
        }


        //POST: Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(LoginModel _user)
        {
            CaptchaResponse response = ValidateCaptcha(Request["g-recaptcha-response"]);
            if (response.Success && ModelState.IsValid)
            {
                var check = db.Users.FirstOrDefault(s => s.UserName == _user.UserName);
                if (check == null)
                {
                    if(_user.Password == _user.RePassword)
                    {
                        _user.Password = GetMD5(_user.Password);
                        db.Configuration.ValidateOnSaveEnabled = false;
                        User user = new User();
                        user.UserName = _user.UserName;
                        user.Password = _user.Password;
                        var client = new WebClient();

                        db.Users.Add(user);
                        db.SaveChanges();
                        TempData["AlertMessage"] = "Register Successfully...";
                        return RedirectToAction("Login", "Login");
                    }
                    else
                    {
                        ModelState.AddModelError("", "The RePassword is incorrect!");
                        return View(_user);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "User name already exists");
                    return View(_user);
                }
            }
            else
            {
                ModelState.AddModelError("", "Error From Google ReCaptchas");
                return View(_user);
            }
            return View();


        }

        //create a string MD5
        public static string GetMD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(str);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;

            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");

            }
            return byte2String;
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login([Bind(Include = "UserName, Password")] User user)
        {
            CaptchaResponse response = ValidateCaptcha(Request["g-recaptcha-response"]);
            if (response.Success && ModelState.IsValid)
            {
                var f_password = GetMD5(user.Password);
                var data = db.Users.Where(s => s.UserName.Equals(user.UserName) && s.Password.Equals(f_password)).ToList();
                if (data.Count() > 0)
                {
                    //add session
                    Session["UserName"] = data.FirstOrDefault().UserName;
                    Session["ID"] = data.FirstOrDefault().ID;
                    return RedirectToAction("Index", "HomeAdmin");
                }
                else
                {
                    ModelState.AddModelError("", "The Username or Password is incorrect!");
                    return View(user);
                }
            }
            else
            {
                ModelState.AddModelError("", "Error From Google ReCaptchas");
                return View(user);
            }
            return View(user);

        }

        //Logout
        public ActionResult Logout()
        {
            Session.Clear();//remove session
            return RedirectToAction("Index", "Home", new { area = ""});
        }
    }
}