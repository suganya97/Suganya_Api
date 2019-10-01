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

        //public MODDBEntities1 db = new MODDBEntities1();
        //office
        public MOD_DBEntities db = new MOD_DBEntities();
        public List<UserDtl> GetAll()
        {
            
                return db.UserDtls.ToList();
         
        }
        public UserDtl Get(int id)
        {
           
                return db.UserDtls.Find(id);
           
        }
        public void Add(UserDtl userDtl)
        {
            if(userDtl.role == 2)
            {
                var user = new UserDtl()
                {
                    userName = userDtl.userName,
                    email = userDtl.email,
                    firstName = userDtl.firstName,
                    lastName = userDtl.lastName,
                    contactNumber = userDtl.contactNumber,
                    yearOfExperience = userDtl.yearOfExperience,
                    linkdinUrl = userDtl.linkdinUrl,
                    role = userDtl.role,
                    password = userDtl.password,
                    confirmedSignUp = userDtl.confirmedSignUp,
                    active = userDtl.active
                };

                db.UserDtls.Add(user);
            }

            db.SaveChanges();
           
        }
        public void Delete(int id)
        {
           
                db.UserDtls.Remove(db.UserDtls.Find(id));
                db.SaveChanges();
            
        }
        public void Update(UserDtl item)
        {
         
                db.Entry(item).State = System.Data.Entity.EntityState.Modified;
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
