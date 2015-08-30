using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using HitchARideService.Models;

namespace HitchARideService.Controllers
{
    public class LoginController : ApiController
    {
        private HitchARideEntities db = new HitchARideEntities();

        // GET api/Login
        public IEnumerable<UserInfo> GetUserInfoes()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.UserInfoes.AsEnumerable();
        }

        // GET api/Login/5
        public UserInfo GetUserInfo(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            UserInfo userinfo = db.UserInfoes.Find(id);
            if (userinfo == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return userinfo;
        }

        // PUT api/Login/5
        public HttpResponseMessage PutUserInfo(int id, UserInfo userinfo)
        {
            if (ModelState.IsValid && id == userinfo.ID)
            {
                db.Entry(userinfo).State = EntityState.Modified;

                try
                {
                    db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // POST api/Login
        public HttpResponseMessage PostUserInfo(UserInfo userinfo)
        {
            if (ModelState.IsValid)
            {
                db.UserInfoes.Add(userinfo);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, userinfo);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = userinfo.ID }));
                return response;
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // DELETE api/Login/5
        public HttpResponseMessage DeleteUserInfo(int id)
        {
            UserInfo userinfo = db.UserInfoes.Find(id);
            if (userinfo == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.UserInfoes.Remove(userinfo);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return Request.CreateResponse(HttpStatusCode.OK, userinfo);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}