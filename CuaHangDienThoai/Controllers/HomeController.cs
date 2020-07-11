using CuaHangMayTinh.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Owin.Security.Facebook;
using Newtonsoft.Json;
using System.Web.Security;
using Model.EF;
using Facebook;

namespace CuaHangMayTinh.Controllers
{
    public class HomeController : Controller
    {
        private MobileStore db = new MobileStore();
        public ActionResult Index()
        {
            HomeModel homeModel = new HomeModel();
            homeModel.LastestProducts = db.SanPhams.OrderByDescending(x => x.ngay_tao).Take(5).ToList();
            homeModel.TopSellerProducts = db.SanPhams.OrderByDescending(x => x.ngay_tao).Take(3).ToList();
            homeModel.HotProducts = db.SanPhams.Where(x => x.hot == true).Take(5).ToList();
            homeModel.TopViewedProducts = db.SanPhams.OrderByDescending(x =>x.so_luot_xem).Take(3).ToList();

            List<ProductCartDTO> cart = (Session["Cart"] as List<ProductCartDTO>);
            if (cart == null) cart = new List<ProductCartDTO>();
            homeModel.RecentlyViewedProducts = cart;

            return View(homeModel);
        }

        // GET: Product/Details/5
        public ActionResult Detail(string id)
        {
            SanPham sanPham = db.SanPhams.Find(id);
            if(sanPham.so_luot_xem == null)
            {
                sanPham.so_luot_xem = 0;
            }

            sanPham.so_luot_xem += 1;
            db.SaveChanges();
            return View(sanPham);
        }

        // GET: Product/Categories/id
        public ActionResult Categories(int id)
        {
            List<SanPham> products = db.SanPhams.Where(x => x.ma_danh_muc == id).ToList();
            return View(products);
        }
        public ActionResult Warranty()
        {
            return View();
        }
        public ActionResult Pay()
        {
            return View();
        }
        public ActionResult DT()
        {
            return View();
        }
        //private Uri RediredtUri
        //{
        //    get
        //    {
        //        var uriBuilder = new UriBuilder(Request.Url);
        //        uriBuilder.Query = null;
        //        uriBuilder.Fragment = null;
        //        uriBuilder.Path = Url.Action("FacebookCallback");
        //        return uriBuilder.Uri;
        //    }
        //}
//        [AllowAnonymous]
//        public ActionResult Facebook()
//        {
//            var fb = new FacebookClient();

//            var loginUrl = fb.GetLoginUrl(new

//            {
//                client_id = "744492899629118",
//                client_secret = "920805a51ac4217b2ddbbf504ec275aa",
//                redirect_uri = RediredtUri.AbsoluteUri,
//                response_type = "code",
//                scope = "email"
//            });
//            return Redirect(loginUrl.AbsoluteUri);
//        }
//        public ActionResult FacebookCallback(string code)
//        {
//            var fb = new FacebookClient();
//            dynamic result = fb.Post("oauth/access_token", new
//            {
//                client_id = "744492899629118",
//                client_secret = "920805a51ac4217b2ddbbf504ec275aa",
//                redirect_uri = RediredtUri.AbsoluteUri,
//                code = code
//            });
//            var accessToken = result.access_token;
//            Session["AccessToken"] = accessToken;
//            fb.AccessToken = accessToken;
//            dynamic me = fb.Get("me?fields=link,first_name,currency,last_name,email,gender,locale,timezone,verified,picture,age_range");
//            string email = me.email;
//            TempData["email"] = me.email;
//            TempData["first_name"] = me.first_name;
//            TempData["lastname"] = me.last_name;
//            TempData["picture"] = me.picture.data.url;
//            FormsAuthentication.SetAuthCookie(email, false);
//            return RedirectToAction("Index", "Home");
//        }
    }
}