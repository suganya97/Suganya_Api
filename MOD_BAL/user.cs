using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MOD_DAL;
using System.Data.Entity;
using System.Net.Http;
using System.Data;
using System.Security.Cryptography;

namespace MOD_BAL
{
  public  class user
    {

        public MyEntity db = new MyEntity();
        
        //office
        //public MOD_DBEntities db = new MOD_DBEntities();
        public List<UserDtl> GetAll()
        {
                return db.UserDtls.ToList();
        }

        public UserDtl Get(int id)
        {
           return db.UserDtls.Find(id);  
        }

        public TrainingDtl getById(int id)
        {
            return db.TrainingDtls.Find(id);
        }

        public TrainingDtl saveTraining(TrainingDtl trainingDtl)
        {
            TrainingDtl result;
            result = db.TrainingDtls.Add(trainingDtl);
            db.SaveChanges();
            return result;
        }
        public List<TrainingDtl> GetTrainingDetails()
        {
            return db.TrainingDtls.ToList();
        }


        public List<UserDtl> GetSearch(string trainerTechnology)
        {
            List<UserDtl> mentors;
            mentors = db.UserDtls.Where(x => x.trainerTechnology == trainerTechnology).ToList();
            return mentors;
        }

        public UserDetails Login(string email,string password)
        {

            UserDtl authLogin;
            string message = null;

            authLogin = db.UserDtls.SingleOrDefault(x => x.email == email);

            if (authLogin != null)
            {
                var pass = EncodePasswordToBase64(password);
                authLogin = db.UserDtls.SingleOrDefault(x => x.email == email && x.password == pass);
                if (authLogin != null)
                {
                    message = "Logged In Successfully";
                }
                else
                {
                    message = "Invalid Password";
                }
            }
            else
            {
                message = "Email Not Registered";
            }
            return new UserDetails()
            {
                message = message,
                token = null,
                userInfo = authLogin
            };
        }


        public static string EncodePasswordToBase64(string password)
        {
            try
            {
                byte[] encData_byte = new byte[password.Length];
                encData_byte = System.Text.Encoding.UTF8.GetBytes(password);
                string encodedData = Convert.ToBase64String(encData_byte);
                return encodedData;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in base64Encode" + ex.Message);
            }
        }
        public int Register(UserDtl userDtl)
        {
            int flag=0;
            UserDtl authLogin = db.UserDtls.FirstOrDefault(x => x.email == userDtl.email);
            if(authLogin == null)
            {

                var pass = EncodePasswordToBase64(userDtl.password);

            if (userDtl.role == 1)
            {
                userDtl.active = true;
                    var user = new UserDtl()
                    {
                        active = true,
                        userName = userDtl.userName.Trim(),
                        email = userDtl.email.Trim(),
                        firstName = userDtl.firstName.Trim(),
                        lastName = userDtl.lastName.Trim(),
                        role = userDtl.role,
                        password = pass
                };

                db.UserDtls.Add(user);

                db.SaveChanges();
                    flag= 0;
            }
            else if(userDtl.role == 2)
            {
                
                var user = new UserDtl();

                user.active = true;
                user.role = userDtl.role;
                user.userName = userDtl.userName.Trim();
                user.email = userDtl.email.Trim();
                user.firstName = userDtl.firstName.Trim();
                user.lastName = userDtl.lastName.Trim();
                user.contactNumber = userDtl.contactNumber;
                user.yearOfExperience = userDtl.yearOfExperience;
                user.linkdinUrl = userDtl.linkdinUrl;
                user.trainerTimings = userDtl.trainerTimings;
                user.trainerTechnology = userDtl.trainerTechnology;
                user.password = pass;
                user.confirmedSignUp = userDtl.confirmedSignUp;
              
                db.UserDtls.Add(user);

                db.SaveChanges();
                    flag = 0;

            }
            else if(userDtl.role == 3)
            {
                var user = new UserDtl();

                user.active = true;
                user.role = userDtl.role;
                user.userName = userDtl.userName.Trim();
                user.email = userDtl.email.Trim();
                user.firstName = userDtl.firstName.Trim();
                user.lastName = userDtl.lastName.Trim();
                user.contactNumber = userDtl.contactNumber;
                user.password = pass;
                user.confirmedSignUp = userDtl.confirmedSignUp;

                db.UserDtls.Add(user);

                db.SaveChanges();
                    flag=0;
            }

            }  
            else
            {
                flag = 1;
            }
            return flag;
        }

        public void addTechnology(SkillDtl skill)
        {
            db.SkillDtls.Add(skill);
            db.SaveChanges();
        }

        public List<SkillDtl> GetAllTechnology()
        {
            return db.SkillDtls.ToList();
        }

        public void DeleteTechnology(int id)
        {
            db.SkillDtls.Remove(db.SkillDtls.Find(id));
            db.SaveChanges();
        }

        public void Block(int id)
        {
            UserDtl user = db.UserDtls.Find(id);
            user.active = false;
            db.Configuration.ValidateOnSaveEnabled = false;
            db.Entry(user).State = System.Data.Entity.EntityState.Modified;
            db.Configuration.ValidateOnSaveEnabled = true;
            db.SaveChanges();
            
        }
        public void UnBlock(int id)
        {
            UserDtl user = db.UserDtls.Find(id);
            user.active = true;
            db.Configuration.ValidateOnSaveEnabled = false;
            db.Entry(user).State = System.Data.Entity.EntityState.Modified;
            db.Configuration.ValidateOnSaveEnabled = true;
            db.SaveChanges();
        }

        //=========        
        //public IList<UserDtl> GetAll()
        //{
        //    return db.UserDtls.ToList();
        //}

        //public UserDtl GetUserById(int id)
        //{
        //    return db.UserDtls.Find(id);
        //}

        //public void Register(UserDtl userDtl)
        //{
        //    //mentor
        //    if (userDtl.role == 2)
        //    {
        //      var user  = new UserDtl()
        //        {
        //            userName = userDtl.userName,
        //            email = userDtl.email,
        //            firstName = userDtl.firstName,
        //            lastName = userDtl.lastName,
        //            contactNumber = userDtl.contactNumber,
        //            yearOfExperience = userDtl.yearOfExperience,
        //            linkdinUrl = userDtl.linkdinUrl,
        //            role = userDtl.role,
        //            password = userDtl.password,
        //            confirmedSignUp = userDtl.confirmedSignUp,
        //            active = userDtl.active
        //        };

        //        db.UserDtls.Add(user);
        //    }
        //    // for mentor
        //    else if(userDtl.role == 3)
        //    {
        //        var user = new UserDtl()
        //        {
        //            userName = userDtl.userName,
        //            email = userDtl.email,
        //            password = userDtl.password,
        //            firstName = userDtl.firstName,
        //            lastName = userDtl.lastName,
        //            role = userDtl.role,
        //            contactNumber = userDtl.contactNumber,
        //            confirmedSignUp = userDtl.confirmedSignUp,
        //            active = userDtl.active
        //        };

        //        db.UserDtls.Add(user);
        //    }

        //    db.SaveChanges();
        //}

        //public void Delete(int id)
        //{
        //    UserDtl user = db.UserDtls.Find(id);
        //    db.UserDtls.Remove(user);
        //    db.SaveChanges();
        //}

        //public void update(UserDtl userDtl)
        //{
        //    db.Entry(userDtl).State = EntityState.Modified;
        //    db.Configuration.ValidateOnSaveEnabled = false;
        //    db.SaveChanges();
        //    db.Configuration.ValidateOnSaveEnabled = true;
        //}

    }
}
