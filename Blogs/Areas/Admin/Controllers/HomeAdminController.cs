using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Blogs.EF;
using PagedList;

namespace Blogs.Areas.Admin.Controllers
{
    public class HomeAdminController : Controller
    {
        private MyJustBlogDbContext db = new MyJustBlogDbContext();
        // GET: Admin/Posts
        public ActionResult Index(string currentFilter, string SearchString, int? page)
        {
            List<Post> posts = new List<Post>();
            if (Session["ID"] != null)
            {
                if (SearchString != null)
                {
                    page = 1;
                }
                else
                    SearchString = currentFilter;
                if (page == null) page = 1;

                
                if (!string.IsNullOrEmpty(SearchString))
                {
                    posts = db.Posts.Include(p => p.Category).Where(p => p.Title.Contains(SearchString)).ToList();
                    ViewBag.FindCount = posts.Count();
                }
                else
                {
                    posts = db.Posts.Include(p => p.Category).OrderByDescending(p => p.Title).ToList();
                    
                }
                ViewBag.CurrentFilter = SearchString;

                int pageSize = 5;
                int pageNumber = (page ?? 1);
                posts = posts.OrderByDescending(p => p.Title).ToList();
                return View(posts.ToPagedList(pageNumber, pageSize));
            }

            else
            {
                return RedirectToAction("Login", "Login");
            }
        }

        // GET: Admin/Posts/Details/5

        public ActionResult LastedPosts()
        {
            var posts = db.Posts.Include(p => p.Category).OrderByDescending(p => p.Posted_On).Take(5);
            return View(posts.ToList());

        }

        public ActionResult MostsViewed()
        {
            var posts = db.Posts.Include(p => p.Category).OrderByDescending(p => p.ViewCount).Take(5);
            return View(posts.ToList());
        }

        public ActionResult MostInterestingPosts()
        {
            var posts = db.Posts.Include(p => p.Category).OrderByDescending(p => p.RateCount).Take(5);
            return View(posts.ToList());
        }

        [HttpPost]
        public PartialViewResult SearchPost(Post post)
        {

            if (!String.IsNullOrEmpty(post.Short_Description))
            {
                return PartialView("_List", post);
            }
            else
                return PartialView("_List", db.Posts.Include(p => p.Category));
        }













        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }



        public ActionResult List()
        {
            var posts = db.Posts.Include(p => p.Category).OrderByDescending(p => p.ViewCount);
            return View(posts.ToList());
        }

        // GET: Admin/Posts/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name");
            return View();
        }

        // POST: Admin/Posts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Short_Description,Meta,UrlSlug,Published,Posted_On,Modified,CategoryId,ViewCount,RateCount,TotalRate,PostContent")] Post post)
        {
            if (ModelState.IsValid)
            {
                db.Posts.Add(post);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", post.CategoryId);
            return View(post);
        }

        // GET: Admin/Posts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", post.CategoryId);
            return View(post);
        }

        // POST: Admin/Posts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Short_Description,Meta,UrlSlug,Published,Posted_On,Modified,CategoryId,ViewCount,RateCount,TotalRate,PostContent")] Post post)
        {
            if (ModelState.IsValid)
            {
                db.Entry(post).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", post.CategoryId);
            return View(post);
        }

        // GET: Admin/Posts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: Admin/Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Post post = db.Posts.Find(id);
            db.Posts.Remove(post);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
