namespace FubuLocalization.Tests
{
    // SAMPLE: ExampleKeys
    
    public class ExampleKeys : StringToken
    {
        public static readonly ExampleKeys InvalidFormat = new ExampleKeys("Data is formatted incorrectly");
        public static readonly ExampleKeys Required = new ExampleKeys("Required Field");

        public ExampleKeys(string text)
            : base(null, text)
        {
        }
    }

    // ENDSAMPLE

    // SAMPLE: ExampleNamespacing

    public class ExampleNamespacing : StringToken
    {
        public static readonly ExampleNamespacing One = new ExampleNamespacing("One");
        public static readonly ExampleNamespacing Two = new ExampleNamespacing("Two");

        public ExampleNamespacing(string text)
            : base(null, text, namespaceByType: true)
        {
        }
    }

    // ENDSAMPLE
}