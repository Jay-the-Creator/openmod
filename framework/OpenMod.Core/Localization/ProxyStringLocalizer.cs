using Microsoft.Extensions.Localization;
using OpenMod.API;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace OpenMod.Core.Localization
{
    [OpenModInternal]
    public class ProxyStringLocalizer : IStringLocalizer
    {
        private readonly IStringLocalizer m_StringLocalizer;

        public ProxyStringLocalizer(IStringLocalizer stringLocalizer)
        {
            m_StringLocalizer = stringLocalizer;
        }

        public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
        {
            return m_StringLocalizer.GetAllStrings(includeParentCultures);
        }

        [Obsolete("IStringLocalizer.WithCulture is obsolete, use `CurrentCulture` instead.", error: true)]
        public IStringLocalizer WithCulture(CultureInfo culture)
        {
            throw new NotImplementedException(
                "IStringLocalizer.WithCulture is obsolete, use `CurrentCulture` instead.");
        }

        public LocalizedString this[string name]
        {
            get { return m_StringLocalizer[name]; }
        }

        public LocalizedString this[string name, params object[] arguments]
        {
            get { return m_StringLocalizer[name, arguments]; }
        }
    }
}