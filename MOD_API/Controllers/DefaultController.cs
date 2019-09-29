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

        MOD_BAL.user use=new MOD_BAL.user();
        [Route("api/get")]
        [HttpGet]
        public IHttpActionResult GetUser()
        {
            var result = use.GetAll();
            return Ok(result);
        }
        [Route("api/get/{ID}")]
        public IHttpActionResult GetById(int id)
        {
            var result = use.GetUserById(id);
       
            return Ok(result);
        }
        
        [Route("api/register")]
       [HttpPost]
        public UserDtl POST(UserDtl userDtl)
        {
            use.Register(userDtl);
            return use.GetUserById(userDtl.id);
        }
    }
}
