using System.Collections.Generic;
using FubuTestingSupport;
using NUnit.Framework;
using Rhino.Mocks;

namespace FubuLocalization.Tests
{
    [TestFixture]
    public class StringTokenTester
    {
        [Test]
        public void equals_is_namespace_aware()
        {
            var token1 = StringToken.FromKeyString("something");
            var token2 = StringToken.FromKeyString("something");
            var token3 = StringToken.FromKeyString("else");

            // FakeToken is namespaced
            var token4 = new FakeToken("something");
            var token5 = new FakeToken("something");
            var token6 = new FakeToken("else");

            token1.ShouldEqual(token2);
            token2.ShouldEqual(token1);
            token3.ShouldNotEqual(token1);
            token1.ShouldNotEqual(token3);

            token4.ShouldEqual(token5);
            token5.ShouldEqual(token4);
            token6.ShouldNotEqual(token4);
            token4.ShouldNotEqual(token6);

            // Namespace matters here
            token1.ShouldNotEqual(token4);
            token3.ShouldNotEqual(token6);
        }

        [Test]
        public void GetHashCode_depends_on_the_localization_key()
        {
            var token1 = StringToken.FromKeyString("something");
            var token2 = StringToken.FromKeyString("something");
            var token3 = StringToken.FromKeyString("else");

            // FakeToken is namespaced
            var token4 = new FakeToken("something");
            var token5 = new FakeToken("something");
            var token6 = new FakeToken("else");

            token1.GetHashCode().ShouldEqual(token2.GetHashCode());
            token2.GetHashCode().ShouldEqual(token1.GetHashCode());
            token3.GetHashCode().ShouldNotEqual(token1.GetHashCode());
            token1.GetHashCode().ShouldNotEqual(token3.GetHashCode());

            token4.GetHashCode().ShouldEqual(token5.GetHashCode());
            token5.GetHashCode().ShouldEqual(token4.GetHashCode());
            token6.GetHashCode().ShouldNotEqual(token4.GetHashCode());
            token4.GetHashCode().ShouldNotEqual(token6.GetHashCode());

            // Namespace matters here
            token1.GetHashCode().ShouldNotEqual(token4.GetHashCode());
            token3.GetHashCode().ShouldNotEqual(token6.GetHashCode());
        }

        [Test]
        public void from_type_just_uses_type_name()
        {
            var token = StringToken.FromType<StringTokenTester>();
            token.Key.ShouldEqual(GetType().Name);
            token.DefaultValue.ShouldEqual(GetType().Name);
        }

        [Test]
        public void from_type_just_uses_type_name_2()
        {
            var token = StringToken.FromType(GetType());
            token.Key.ShouldEqual(GetType().Name);
            token.DefaultValue.ShouldEqual(GetType().Name);
        }

        [Test]
        public void two_instances_with_the_same_key_should_equal_equal_each_other()
        {
            var x = buildCommonToken();
            var y = buildCommonToken();

            x.ShouldEqual(y);
        }

        [Test]
        public void two_instances_with_the_same_key_should_be_considered_the_same_for_hashing_purposes()
        {
            var x = buildCommonToken();
            var y = buildCommonToken();

            var dict = new Dictionary<StringToken, int> { { x, 0 } };

            dict.ContainsKey(y).ShouldBeTrue();
        }

        [Test]
        public void should_render_to_string_from_localization_when_condition_is_true()
        {
            var mocks = new MockRepository();
            var provider = mocks.StrictMock<ILocalizationDataProvider>();
            var token = buildCommonToken();
            const string retVal = "TheText";

            LocalizationManager.Stub(provider);

            using (mocks.Record())
            {
                Expect.Call(provider.GetTextForKey(token)).Return(retVal);
            }

            using (mocks.Playback())
            {
                token
                    .ToString(true)
                    .ShouldEqual(retVal);
            }
        }

        [Test]
        public void should_render_to_string_as_empty_when_condition_is_false()
        {
            buildCommonToken()
                .ToString(false)
                .ShouldEqual(string.Empty);
        }

        private StringToken buildCommonToken()
        {
            const string key = "test";
            return StringToken.FromKeyString(key, "default");
        }
    }

    public class FakeToken : StringToken
    {
        public FakeToken(string key) : base(key, null, namespaceByType:true)
        {
        }
    }
}