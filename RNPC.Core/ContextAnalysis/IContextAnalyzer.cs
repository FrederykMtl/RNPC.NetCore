namespace RNPC.Core.ContextAnalysis
{
    public interface IContextAnalyzer
    {
        ContextInformation EvaluateEventContext(Action.Action action);
    }
}