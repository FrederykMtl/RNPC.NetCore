namespace RNPC.Core.ContextAnalysis
{
    public interface IContextStrategyFactory
    {
        IContextStrategy GetContextStrategy(ContextInformation contextInformation);
    }
}