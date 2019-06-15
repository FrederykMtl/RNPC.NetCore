using System;
using RNPC.Core.ContextAnalysis;
using RNPC.Core.Enums;
using PublicInteractionStrategy = RNPC.API.ContextStrategy.PublicInteractionStrategy;

namespace RNPC.API.Context
{
    public class ContextStrategyFactory : IContextStrategyFactory
    {
        public IContextStrategy GetContextStrategy(ContextInformation contextInformation)
        {

            switch (contextInformation.LocationalContext)
            {
                case LocationalContext.Church:
                    break;
                case LocationalContext.Government:
                    break;
                case LocationalContext.Home:
                    break;
                case LocationalContext.Public:
                    return new PublicInteractionStrategy();
                case LocationalContext.Palace:
                    break;
                case LocationalContext.Workplace:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return new PublicInteractionStrategy();
        }
    }
}