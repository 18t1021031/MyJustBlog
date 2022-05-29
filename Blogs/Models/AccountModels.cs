using Blogs.EF;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Blogs.Models
{
    public class AccountModels
    {
        private MyJustBlogDbContext db = null;
        public AccountModels()
        {
            db = new MyJustBlogDbContext();
        }
        public bool Login(string userName, string password)
        {
            object[] sqlParams =
            {
                new SqlParameter("@UserName", userName),
                new SqlParameter("@Password", password),
            };
            var res = db.Database.SqlQuery<bool>("Sp_Account_Login @UserName, @Password", sqlParams).SingleOrDefault();
            return res;
        }
    }
}