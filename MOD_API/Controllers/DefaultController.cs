using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MOD_DAL;
using MOD_BAL;
using System.Data.Entity;
using System.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Security.Claims;
using System.Web;


namespace MOD_API.Controllers
{
    public class DefaultController : ApiController
    {

        MOD_BAL.user ctrl = new MOD_BAL.user();

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

        // Get Search Data
        [Route("api/getSearchData")]
        [HttpGet]
        public IHttpActionResult getSearchData(string trainerTechnology)
        {
            var result = ctrl.GetSearch(trainerTechnology);
            return Ok(result);
        }

        [Route("api/allTrainingDetails")]
        [HttpGet]
        public IHttpActionResult GetAllTraining()
        {
            return Ok(ctrl.GetTrainingDetails());
        }

        [Route("api/allPayment")]
        [HttpGet]
        public IHttpActionResult getAllPayment()
        {
            return Ok(ctrl.getAllPayment());
        }


        [Route("api/training/{id}")]
        [HttpGet]
        public IHttpActionResult GetTrainingById(int id)
        {
            return Ok(ctrl.getById(id));
        }

        [Route("api/enrollTraining")]
        [HttpPost]
        public IHttpActionResult enrollTrainings(TrainingDtl trainingDtl)
        {
            TrainingDtl result;
            result = ctrl.saveTraining(trainingDtl);
            return Ok(result);
        }

        [Route("api/savePayment")]
        [HttpPost]
        public IHttpActionResult savePayment(PaymentDtl payment)
        {
            PaymentDtl result;
            result = ctrl.savePayment(payment);
            return Ok(result);
        }

        //POST: api/login
        [Route("api/login")]
        [HttpPost]
        public IHttpActionResult LogIn([FromBody] UserDtl user)
        {
            var result = ctrl.Login(user);

            result.token = createToken(user.email);

            return Ok(result);
        }

        private string createToken(string username)
        {
            //Set issued at date
            DateTime issuedAt = DateTime.UtcNow;
            //set the time when it expires
            DateTime expires = DateTime.UtcNow.AddDays(7);

            var tokenHandler = new JwtSecurityTokenHandler();

            //create a identity and add claims to the user which we want to log in
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, username)
            });

            const string sec = "401b09eab3c013d4ca54922bb802bec8fd5318192b0a75f201d8b3727429090fb337591abd3e44453b954555b7a0812e1081c39b740293f765eae731f5a65ed1";
            var now = DateTime.UtcNow;
            var securityKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(System.Text.Encoding.Default.GetBytes(sec));
            var signingCredentials = new Microsoft.IdentityModel.Tokens.SigningCredentials(securityKey, Microsoft.IdentityModel.Tokens.SecurityAlgorithms.HmacSha256Signature);


            //create the jwt
            var token =
                (JwtSecurityToken)
                    tokenHandler.CreateJwtSecurityToken(issuer: "http://localhost:44307", audience: "http://localhost:44307",
                        subject: claimsIdentity, notBefore: issuedAt, expires: expires, signingCredentials: signingCredentials);
            var tokenString = tokenHandler.WriteToken(token);

            return tokenString;
        }

        // POST: api/Register
        [Route("api/register")]
        public IHttpActionResult Post(UserDtl userDtl)
        {
            int result = ctrl.Register(userDtl);
            if (result == 0)
            {
                return Ok("User Registered");
            }
            else
            {
                return Ok("Email already Exists");
            }
        }


        //[Authorize]
        [Route("app/addTech")]
        [HttpPost]
        public IHttpActionResult addTechno(SkillDtl skill)
        {
            ctrl.addTechnology(skill);
            return Ok("Technology Added");
        }

        [Route("api/getTech")]
        [HttpGet]
        public IHttpActionResult getTechno()
        {
            return Ok(ctrl.GetAllTechnology());
        }



        [Route("api/skill/{id}")]
        [HttpGet]
        public IHttpActionResult getSkillById(int id)
        {
            return Ok(ctrl.getSkillById(id));
        }


        // DELETE: api/Product/5
        [Route("api/DeleteSkillById/{id}")]
        public IHttpActionResult DeleteSkill(int id)
        {
            ctrl.DeleteTechnology(id);
            return Ok("Skill Deleted");
        }

        

        [Route("api/getPaymentById/{id}")]
        [HttpGet]
        public IHttpActionResult getPaymentById(int id)
      {
            return Ok(ctrl.getPaymentById(id));
        }


        [Route("api/updatePaymentAndCommision/{id}")]
        [HttpPut]
        public IHttpActionResult updatePaymentAndCommisionmentById(int id,PaymentDtl payment)
        {
            ctrl.updatePaymentAndCommisionmentById(id,payment);
            return Ok("updated");
        }


        [Route("api/trainingPaymentStatus/{id}")]
        [HttpPut]
        public IHttpActionResult trainingPaymentStatus(int id)
        {
            ctrl.trainingPaymentStatus(id);
            return Ok("updated");
        }

        [Route("api/trainingStatus/{id}")]
        [HttpPut]
        public IHttpActionResult trainingStatus(int id)
        {
            ctrl.trainingStatus(id);
            return Ok("updated");
        }

        [Route("api/changeProgress")]
        [HttpPut]
        public IHttpActionResult changeProgress(int id, int progressValue)
        {
            ctrl.changeProgress(id, progressValue);
            return Ok("Progress Updated");
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

        [Route("api/acceptStatus/{id}")]
        [HttpPut]
        public IHttpActionResult acceptStatus(int id)
        {
            ctrl.changeAccept(id);
            return Ok("Accepted");
        }

        [Route("api/rejectStatus/{id}")]
        [HttpPut]
        public IHttpActionResult rejectStatus(int id)
        {
            ctrl.changeReject(id);
            return Ok("Rejected");
        }


    }
}

