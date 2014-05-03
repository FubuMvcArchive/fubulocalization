using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FubuCore;

namespace FubuLocalization
{
    public class StringToken<T> : StringToken
    {
        public StringToken(string defaultValue) : this(null, defaultValue, namespaceByType: true)
        {
        }

        protected StringToken(string key, string defaultValue, string localizationNamespace = null, bool namespaceByType = false) : base(key, defaultValue, localizationNamespace, namespaceByType)
        {
        }

        protected override LocalizationKey buildKey(Type type, string localizationNamespace, bool namespaceByType)
        {
            return base.buildKey(typeof(T), localizationNamespace, namespaceByType);
        }
    }
}
