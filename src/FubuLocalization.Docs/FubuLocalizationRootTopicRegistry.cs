namespace FubuLocalization.Docs
{
    public class FubuLocalizationRootTopicRegistry : FubuDocs.TopicRegistry
    {
        public FubuLocalizationRootTopicRegistry()
        {
            For<FubuLocalization.Docs.FubuLocalizationRoot>().Append<FubuLocalization.Docs.Architecture>();

        }
    }
}
