﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using MOD_BAL;
using MOD_DAL;

namespace MOD_TEST
{
    [TestFixture]
    public class TestUser
    {
        [Test]
        public void GetById()
        {
            user user = new user();
            UserDtl user1 = user.getUserById(1);
            Assert.IsNotNull(user1);
        }

        [Test]
        public void Register()
        {
            user user = new user();
            UserDtl user1 = new UserDtl()
            {
                firstName = "sruti",
                lastName = "r",
                userName = "sruthi",
                password = "sruthi",
                email = "sruti@gmail.com",
                contactNumber = 9808970688,
                linkedinUrl = "sruthi",
                yearOfExperience = 15,
                trainerTechnology = "Python",
                active = true,
                role = 2,
            };
            user.saveUser(user1);
            UserDtl user2 = user.getUserById(4);
            Assert.IsNotNull(user2);
        }

        [Test]
        public void GetAllUser()
        {
            user user = new user();
            IList<UserDtl> p = user.getAllRegistered();
            Assert.IsNotNull(p);
        }

        [Test]
        public void BlockUser()
        {
            user user = new user();
            user.blockById(2);
            UserDtl user1 = user.getUserById(2);
            Assert.IsTrue(user1.active == false);

        }

        [Test]
        public void UnBlockUser()
        {
            user user = new user();
            user.unBlockById(2);
            UserDtl user1 = user.getUserById(2);
            Assert.IsTrue(user1.active == true);
        }


       


    }
}
