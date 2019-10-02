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

        //POST: api/login
        [Route("api/login")]
        [HttpPost]
        public IHttpActionResult LogIn(UserDtl userDtl)
        {
            var result = ctrl.Login(userDtl);
            return Ok(result);
        }

        // POST: api/Register
        [Route("api/register")]
        public IHttpActionResult Post(UserDtl userDtl)
        {
            ctrl.Register(userDtl);
            return Ok("User Registered");
        }

        [Route("app/addTech")]
        [HttpPost]
        public IHttpActionResult addTechno(SkillDtl skill)
        {
            ctrl.addTechnology(skill);
            return Ok("Technology Added");
        }

        // PUT: api/user/5
        [Route("api/block/{id}")]
        [HttpPut]
        public IHttpActionResult Block(int id)
        {
            ctrl.Block(id);
            return Ok("Blocked");
        }

        [Route("api/unblock/{id}")]
        [HttpPut]
        public IHttpActionResult Unblock(int id)
        {
            ctrl.UnBlock(id);
            return Ok("Unblocked");
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
