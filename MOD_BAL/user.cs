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

        public Mydb db = new Mydb();
        
        // Get All 

        public List<UserDtl> getAllRegistered()
        {
            try
            {
                return db.UserDtls.ToList();
            }
            catch
            {
                throw;
            }
        }

        public List<SkillDtl> getAllSkills()
        {
            try
            {
                return db.SkillDtls.ToList();
            }
            catch
            {
                throw;
            }
        }

        public List<TrainingDtl> getAllTraining()
        {
            try
            {
                return db.TrainingDtls.ToList();
            }
            catch
            {
                throw;
            }
        }

        public List<PaymentDtl> getAllPayment()
        {
            try
            {
                return db.PaymentDtls.ToList();
            }
            catch
            {
                throw;
            }
        }

        // Get All By ID 

        public UserDtl getUserById(int id)
        {
           try
            {
                return db.UserDtls.Find(id);
            }
            catch
            {
                throw;
            }
        }

        public SkillDtl getSkillById(int id)
        {
            try
            {
                return db.SkillDtls.Find(id);
            }
            catch
            {
                throw;
            }
        }

        public TrainingDtl getTrainingById(int id)
        {
            try
            {
                return db.TrainingDtls.Find(id);
            }
            catch
            {
                throw;
            }
        }

        public PaymentDtl getPaymentById(int id)
        {
            try
            {
                return db.PaymentDtls.Find(id);
            }
            catch
            {
                throw;
            }
        }

        // Get Search Data 

        public List<UserDtl> getSearchData(string trainerTechnology)
        {
            try
            {
                List<UserDtl> tt;
                tt = db.UserDtls.Where(x => x.trainerTechnology == trainerTechnology).ToList();
                return tt;
            }
            catch
            {
                throw;
            }
        }

        // Post Data In Db 

        public UserDetails saveUser(UserDtl userDtl)
        {
            try
            {
                string message = null;
                UserDtl user1 = db.UserDtls.SingleOrDefault(x => x.email == userDtl.email);
                if (user1 == null)
                {

                    var pass = EncodePasswordToBase64(userDtl.password);

                    if (userDtl.role == 1)
                    {
                        userDtl.active = true;
                        var user = new UserDtl()
                        {
                            active = true,
                            userName = userDtl.userName.Trim().ToLower(),
                            email = userDtl.email.Trim().ToLower(),
                            firstName = userDtl.firstName.Trim(),
                            lastName = userDtl.lastName.Trim(),
                            role = userDtl.role,
                            password = pass
                        };

                        db.UserDtls.Add(user);

                        db.SaveChanges();
                        message = "Registered Successfully";
                    }
                    else if (userDtl.role == 2)
                    {
                        var user = new UserDtl();

                        user.active = true;
                        user.role = userDtl.role;
                        user.userName = userDtl.userName.Trim().ToLower();
                        user.email = userDtl.email.Trim().ToLower();
                        user.firstName = userDtl.firstName.Trim();
                        user.lastName = userDtl.lastName.Trim();
                        user.contactNumber = userDtl.contactNumber;
                        user.yearOfExperience = userDtl.yearOfExperience;
                        user.linkedinUrl = userDtl.linkedinUrl;
                        user.trainerTimings = userDtl.trainerTimings;
                        user.trainerTechnology = userDtl.trainerTechnology;
                        user.password = pass;
                        user.confirmedSignUp = userDtl.confirmedSignUp;

                        db.UserDtls.Add(user);

                        db.SaveChanges();
                        message = "Registered Successfully";

                    }
                    else if (userDtl.role == 3)
                    {
                        var user = new UserDtl();

                        user.active = true;
                        user.role = userDtl.role;
                        user.userName = userDtl.userName.Trim().ToLower();
                        user.email = userDtl.email.Trim().ToLower();
                        user.firstName = userDtl.firstName.Trim();
                        user.lastName = userDtl.lastName.Trim();
                        user.contactNumber = userDtl.contactNumber;
                        user.password = pass;
                        user.confirmedSignUp = userDtl.confirmedSignUp;

                        db.UserDtls.Add(user);

                        db.SaveChanges();
                        message = "Registered Successfully";
                    }
                }
                else
                {
                    message = "Email Already Exists";
                }
                return new UserDetails()
                {
                    message = message
                };
            }
            catch
            {
                throw;
            }
        }

        public void saveSkill(SkillDtl skill)
        {
            try
            {
                db.SkillDtls.Add(skill);
                db.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        public TrainingDtl saveTraining(TrainingDtl trainingDtl)
        {
            try
            {
                TrainingDtl result;
                result = db.TrainingDtls.Add(trainingDtl);
                db.SaveChanges();
                return result;
            }
            catch
            {
                throw;
            }
        }

        public PaymentDtl savePayment(PaymentDtl payment)
        {
            try
            {
                PaymentDtl result;
                result = db.PaymentDtls.Add(payment);
                db.SaveChanges();
                return result;
            }
            catch
            {
                throw;
            }
        }
     
        public UserDetails login(UserDtl user)
        {
            try
            {
                UserDtl authLogin;
                string message = null;

                authLogin = db.UserDtls.SingleOrDefault(x => x.email.ToLower() == user.email.ToLower());

                if (authLogin != null)
                {
                    var pass = EncodePasswordToBase64(user.password);
                    authLogin = db.UserDtls.SingleOrDefault(x => x.email.ToLower() == user.email.ToLower() && x.password == pass);
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
            catch
            {
                throw;
            }
            
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

        // Update Data 

        public void updatePaymentAndCommisionById(int id, PaymentDtl paymentDtl)
        {
            try
            {
                PaymentDtl user = db.PaymentDtls.Find(id);
                user.commision = paymentDtl.commision;
                user.trainerFees = paymentDtl.trainerFees;
                db.Configuration.ValidateOnSaveEnabled = false;
                db.Entry(user).State = System.Data.Entity.EntityState.Modified;
                db.Configuration.ValidateOnSaveEnabled = true;
                db.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        public void updateUserProfileById(int id, UserDtl userDtl)
        {
            try
            {
                UserDtl user = db.UserDtls.Find(id);

                user.userName = userDtl.userName.Trim().ToLower();
                user.email = userDtl.email.Trim().ToLower();
                user.firstName = userDtl.firstName.Trim();
                user.lastName = userDtl.lastName.Trim();
                user.contactNumber = userDtl.contactNumber;
                db.Configuration.ValidateOnSaveEnabled = false;
                db.Entry(user).State = System.Data.Entity.EntityState.Modified;
                db.Configuration.ValidateOnSaveEnabled = true;
                db.SaveChanges();
            }
            catch
            {
                throw;
            }
        }
        public void updateTrainerProfileById(int id, UserDtl userDtl)
        {
            try
            {
                UserDtl user = db.UserDtls.Find(id);
                
                user.userName = userDtl.userName.Trim().ToLower();
                user.email = userDtl.email.Trim().ToLower();
                user.firstName = userDtl.firstName.Trim();
                user.lastName = userDtl.lastName.Trim();
                user.contactNumber = userDtl.contactNumber;
                user.yearOfExperience = userDtl.yearOfExperience;
                user.linkedinUrl = userDtl.linkedinUrl;
                user.trainerTechnology = userDtl.trainerTechnology;
                db.Configuration.ValidateOnSaveEnabled = false;
                db.Entry(user).State = System.Data.Entity.EntityState.Modified;
                db.Configuration.ValidateOnSaveEnabled = true;
                db.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        public void updateTrainingAndPaymentStatusById(int id)
        {
            try
            {
                TrainingDtl user = db.TrainingDtls.Find(id);
                user.trainingPaymentStatus = true;
                db.Configuration.ValidateOnSaveEnabled = false;
                db.Entry(user).State = System.Data.Entity.EntityState.Modified;
                db.Configuration.ValidateOnSaveEnabled = true;
                db.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        public void updateTrainingStatusById(int id)
        {
            try
            {
                TrainingDtl user = db.TrainingDtls.Find(id);
                user.status = "current";
                user.progress = 0;
                db.Configuration.ValidateOnSaveEnabled = false;
                db.Entry(user).State = System.Data.Entity.EntityState.Modified;
                db.Configuration.ValidateOnSaveEnabled = true;
                db.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        public void updateTrainingProgressById(int id, int progressValue)
        {
            try
            {
                TrainingDtl user = db.TrainingDtls.Find(id);
                user.progress = progressValue;
                if (progressValue == 100)
                {
                    user.status = "completed";
                }
                db.Configuration.ValidateOnSaveEnabled = false;
                db.Entry(user).State = System.Data.Entity.EntityState.Modified;
                db.Configuration.ValidateOnSaveEnabled = true;
                db.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        
        public void updateTrainingRatingById(int id, int rating)
        {
            try
            {
                TrainingDtl user = db.TrainingDtls.Find(id);
                user.rating = rating;
                db.Configuration.ValidateOnSaveEnabled = false;
                db.Entry(user).State = System.Data.Entity.EntityState.Modified;
                db.Configuration.ValidateOnSaveEnabled = true;
                db.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        public void blockById(int id)
        {
            try
            {
                UserDtl user = db.UserDtls.Find(id);
                user.active = false;
                db.Configuration.ValidateOnSaveEnabled = false;
                db.Entry(user).State = System.Data.Entity.EntityState.Modified;
                db.Configuration.ValidateOnSaveEnabled = true;
                db.SaveChanges();
            }
            catch
            {
                throw;
            }
        }
        
        public void unBlockById(int id)
        {
            try
            {
                UserDtl user = db.UserDtls.Find(id);
                user.active = true;
                db.Configuration.ValidateOnSaveEnabled = false;
                db.Entry(user).State = System.Data.Entity.EntityState.Modified;
                db.Configuration.ValidateOnSaveEnabled = true;
                db.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        public void acceptTrainingRequestById(int id)
        {
            try
            {
                TrainingDtl trainingDtl = db.TrainingDtls.Find(id);
                trainingDtl.accept = true;
                db.Configuration.ValidateOnSaveEnabled = false;
                db.Entry(trainingDtl).State = System.Data.Entity.EntityState.Modified;
                db.Configuration.ValidateOnSaveEnabled = true;
                db.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        public void rejectTrainingRequestById(int id)
        {
            try
            {
                TrainingDtl trainingDtl = db.TrainingDtls.Find(id);
                trainingDtl.rejectNotify = true;
                db.Configuration.ValidateOnSaveEnabled = false;
                db.Entry(trainingDtl).State = System.Data.Entity.EntityState.Modified;
                db.Configuration.ValidateOnSaveEnabled = true;
                db.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        // Delete Data

        public void DeleteSkillById(int id)
        {
            try
            {
                db.SkillDtls.Remove(db.SkillDtls.Find(id));
                db.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

  }
}
