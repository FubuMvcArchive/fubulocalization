using System.Globalization;

namespace FubuLocalization
{
    // SAMPLE: MissingHandler
    public interface ILocalizationMissingHandler
    {
        string FindMissingText(StringToken key, CultureInfo culture);
        string FindMissingProperty(PropertyToken property, CultureInfo culture);
    }
    // ENDSAMPLE
}