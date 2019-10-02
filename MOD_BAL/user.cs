using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MOD_DAL;
using System.Data.Entity;
using System.Net.Http;
using System.Data;

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

        public UserDtl Login(UserDtl userDtl)
        {
           UserDtl authLogin = db.UserDtls.FirstOrDefault(x => x.email == userDtl.email && x.password == userDtl.password);
           return authLogin;
        }


        public void Register(UserDtl userDtl)
        {

            if(userDtl.role == 1)
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
                    password = userDtl.password
                };

                db.UserDtls.Add(user);

                db.SaveChanges();
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
                user.password = userDtl.password;
                user.confirmedSignUp = userDtl.confirmedSignUp;
              
                db.UserDtls.Add(user);

                db.SaveChanges();

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
                user.password = userDtl.password;
                user.confirmedSignUp = userDtl.confirmedSignUp;

                db.UserDtls.Add(user);

                db.SaveChanges();

            }

        }
        public void Delete(int id)
        {
           
                db.UserDtls.Remove(db.UserDtls.Find(id));
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
