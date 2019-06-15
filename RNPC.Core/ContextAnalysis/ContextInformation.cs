using RNPC.Core.Enums;

namespace RNPC.Core.ContextAnalysis
{
    public class ContextInformation
    {
        public bool EventHasBeenTranslated;
        public int RepetitionNumber;
        public Action.Action InitialAction;
        public Action.Action TranslatedAction;
        public LocationalContext LocationalContext;
    }
}