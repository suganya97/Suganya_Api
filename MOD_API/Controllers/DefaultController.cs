using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MOD_DAL;
using MOD_BAL;
using System.Data.Entity;


namespace MOD_API.Controllers
{
    public class DefaultController : ApiController
    {

        MOD_BAL.user ctrl = new MOD_BAL.user();
         //without header
        //user ctrl = new user();


        // GET: api/getallusersandmentors
        [Route("api/getAll")]
        public IHttpActionResult Get()
        {
            return Ok(ctrl.GetAll()); 
        }

        // GET: api/user/5
        [Route("api/user/{id}")]
        public IHttpActionResult Get(int id)
        {
            return Ok(ctrl.Get(id));
        }

        // POST: api/Register
        [Route("api/register")]
        public IHttpActionResult Post(UserDtl userDtl)
        {
            ctrl.Add(userDtl);
            return Ok("Record Added");
        }

        // PUT: api/user/5
        [Route("api/user/edit/{id}")]
        public IHttpActionResult Put(UserDtl userDtl)
        {
            ctrl.Update(userDtl);
            return Ok("Record Updated");
        }

        // DELETE: api/Product/5
        [Route("api/Delete/{id}")]
        public IHttpActionResult Delete(int id)
        {
            ctrl.Delete(id);
            return Ok("Record Deleted");
        }


       // [Route("api/get")]
       // [HttpGet]
       // public IHttpActionResult GetUser()
       // {
       //     var result = ctrl.GetAll();
       //     return Ok(result);
       // }
       // [Route("api/get/{ID}")]
       // public IHttpActionResult GetById(int id)
       // {
       //     var result = ctrl.GetUserById(id);
       
       //     return Ok(result);
       // }
        
       // [Route("api/register")]
       //[HttpPost]
       // public UserDtl POST(UserDtl userDtl)
       // {
       //     ctrl.Register(userDtl);
       //     return ctrl.GetUserById(userDtl.id);
       // }
    }
}
