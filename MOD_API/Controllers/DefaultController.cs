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

        // Get All - API

        //api/getAllRegistered

        [Route("api/getAllRegistered")]
        public IHttpActionResult getAllRegistered()
        {
            return Ok(ctrl.getAllRegistered());
        }

        [Route("api/getAllSkills")]
        [HttpGet]
        public IHttpActionResult getAllSkills()
        {
            return Ok(ctrl.getAllSkills());
        }

        [Route("api/getAllTraining")]
        [HttpGet]
        public IHttpActionResult getAllTraining()
        {
            return Ok(ctrl.getAllTraining());
        }

        [Route("api/getAllPayment")]
        [HttpGet]
        public IHttpActionResult getAllPayment()
        {
            return Ok(ctrl.getAllPayment());
        }


        // Get All By ID - API

        // GET: api/getUserById/5
        

        [Route("api/getUserById/{id}")]
        public IHttpActionResult getUserById(int id)
        {
            return Ok(ctrl.getUserById(id));
        }

        [Route("api/getSkillById/{id}")]
        [HttpGet]
        public IHttpActionResult getSkillById(int id)
        {
            return Ok(ctrl.getSkillById(id));
        }

        [Route("api/getTrainingById/{id}")]
        [HttpGet]
        public IHttpActionResult getTrainingById(int id)
        {
            return Ok(ctrl.getTrainingById(id));
        }

        [Route("api/getPaymentById/{id}")]
        [HttpGet]
        public IHttpActionResult getPaymentById(int id)
        {
            return Ok(ctrl.getPaymentById(id));
        }

        // Get Search Data - API

        [Route("api/getSearchData")]
        [HttpGet]
        public IHttpActionResult getSearchData(string trainerTechnology)
        {
            if (trainerTechnology != null)
            {
                var result = ctrl.getSearchData(trainerTechnology);
                return Ok(result);
            }
            else
            {
                return Ok("Error finding data");
            }
        }

        // Post Data In Db - API

        // POST: api/Register

        [Route("api/saveUser")]
        public IHttpActionResult saveUser(UserDtl userDtl)
        {
            var result = ctrl.saveUser(userDtl);

            if (result.message == "Registered Successfully")
            {
                return Ok(result);
            }
            else if(result.message == "Email Already Exists")
            {
                return Ok(result);
            }
            else
            {
                return Ok("Error Saving Data");
            }
        }

        
        [Route("app/saveSkill")]
        [HttpPost]
        public IHttpActionResult saveSkill(SkillDtl skill)
        {
            ctrl.saveSkill(skill);
            return Ok("Technology Added");
        }

        [Route("api/saveTraining")]
        [HttpPost]
        public IHttpActionResult saveTraining(TrainingDtl trainingDtl)
        {
            TrainingDtl result;
            result = ctrl.saveTraining(trainingDtl);
            return Ok(result);
        }

        [Route("api/savePayment")]
        [HttpPost]
        public IHttpActionResult savePayment(PaymentDtl payment)
        {
            if(payment != null)
            {
                PaymentDtl result;
                result = ctrl.savePayment(payment);
                return Ok(result);
            }
            else
            {
                return Ok("Data Not Saved");
            }
        }

        [Route("api/login")]
        [HttpPost]
        public IHttpActionResult login([FromBody] UserDtl user)
        {
            var result = ctrl.login(user);

            if (result.message == "Invalid Password" || result.message == "Email Not Registered")
            {
                return Ok(result);
            }
            else
            {
                result.token = createToken(user.email);
                return Ok(result);
            }
        }

        private string createToken(string email)
        {
            //Set issued at date
            DateTime issuedAt = DateTime.UtcNow;
            //set the time when it expires
            DateTime expires = DateTime.UtcNow.AddDays(7);

            var tokenHandler = new JwtSecurityTokenHandler();

            //create a identity and add claims to the user which we want to log in
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Email , email)
            }); ;

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

        // Put Data - API

        // PUT: api/updatePaymentAndCommisionById/5

        [Route("api/updatePaymentAndCommisionById/{id}")]
        [HttpPut]
        public IHttpActionResult updatePaymentAndCommisionById(int id, PaymentDtl payment)
        {
            ctrl.updatePaymentAndCommisionById(id, payment);
            return Ok("updated");
        }

        [Route("api/updateUserProfileById/{id}")]
        [HttpPut]
        public IHttpActionResult updateUserProfileById(int id, UserDtl user)
        {
            ctrl.updateUserProfileById(id, user);
            return Ok("updated");
        }

        [Route("api/updateTrainerProfileById/{id}")]
        [HttpPut]
        public IHttpActionResult updateTrainerProfileById(int id, UserDtl user)
        {
            ctrl.updateTrainerProfileById(id, user);
            return Ok("updated");
        }

       
        [Route("api/updateTrainingAndPaymentStatusById/{id}")]
        [HttpPut]
        public IHttpActionResult updateTrainingAndPaymentStatusById(int id)
        {
            ctrl.updateTrainingAndPaymentStatusById(id);
            return Ok("updated");
        }

        [Route("api/updateTrainingStatusById/{id}")]
        [HttpPut]
        public IHttpActionResult updateTrainingStatusById(int id)
        {
            ctrl.updateTrainingStatusById(id);
            return Ok("updated");
        }

        [Route("api/updateTrainingProgressById")]
        [HttpPut]
        public IHttpActionResult updateTrainingProgressById(int id, int progressValue)
        {
            ctrl.updateTrainingProgressById(id, progressValue);
            return Ok("Progress Updated");
        }

        [Route("api/updateTrainingRatingById")]
        [HttpPut]
        public IHttpActionResult updateTrainingRatingById(int id, int rating)
        {
            ctrl.updateTrainingRatingById(id, rating);
            return Ok("Progress Updated");
        }

        [Route("api/blockById/{id}")]
        [HttpPut]
        public IHttpActionResult blockById(int id)
        {
            ctrl.blockById(id);
            return Ok("Blocked");
        }

        [Route("api/unBlockById/{id}")]
        [HttpPut]
        public IHttpActionResult unBlockById(int id)
        {
            ctrl.unBlockById(id);
            return Ok("Unblocked");
        }

        [Route("api/acceptTrainingRequestById/{id}")]
        [HttpPut]
        public IHttpActionResult acceptTrainingRequestById(int id)
        {
            ctrl.acceptTrainingRequestById(id);
            return Ok("Accepted");
        }

        [Route("api/rejectTrainingRequestById/{id}")]
        [HttpPut]
        public IHttpActionResult rejectTrainingRequestById(int id)
        {
            ctrl.rejectTrainingRequestById(id);
            return Ok("Rejected");
        }

        // Delete Data By Id - API 

        // DELETE: api/DeleteSkillById/5

        [Route("api/DeleteSkillById/{id}")]
        public IHttpActionResult DeleteSkillById(int id)
        {
            ctrl.DeleteSkillById(id);
            return Ok("Skill Deleted");
        }

    }
}

