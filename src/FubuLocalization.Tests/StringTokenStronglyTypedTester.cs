using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace FubuLocalization.Tests
{
    [TestFixture]
    public class StringTokenStronglyTypedTester
    {
        [Test]
        public void simple_localization()
        {
            var token = Loc.Basic;
            var locKey = token.ToLocalizationKey();
            Assert.AreEqual("Loc:Basic", locKey.Key1);
            Assert.AreEqual("Basic_Trans", token.DefaultValue);
        }

        [Test]
        public void nested_class_localization()
        {
            var token = Loc.Account.AccountName;
            var locKey = token.ToLocalizationKey();
            Assert.AreEqual("Loc.Account:AccountName", locKey.Key1);
            Assert.AreEqual("Account_Trans", token.DefaultValue);
        }

        [Test]
        public void parameter_localization()
        {
            var token = Loc.User.NameWithParams;
            var locKey = token.ToLocalizationKey();
            Assert.AreEqual("Loc.User:NameWithParams", locKey.Key1);
            Assert.AreEqual("Name_Sir", token.FormatTokenWith(new NameParams { LastName = "Sir" }));
        }

        [Test]
        public void parameter_localization_raw_key()
        {
            var token = Loc.User.NameWithParams;
            Assert.AreEqual("Name_{LastName}", token.ToRawString());
        }
    }

    public partial class Loc
    {
        public static readonly StringToken Basic = new StringToken<Loc>("Basic_Trans");

        public class User
        {
            public static readonly StringToken Name = new StringToken<User>("Name_Trans");
            public static readonly StringToken<User, NameParams> NameWithParams = new StringToken<User, NameParams>("Name_{LastName}");
        }
    }

    public class NameParams
    {
        public string LastName { get; set; }
    }

    public partial class Loc
    {
        public class Account
        {
            public static readonly StringToken AccountName = new StringToken<Account>("Account_Trans");
        }
    }
}
