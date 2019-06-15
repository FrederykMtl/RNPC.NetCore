using System.Collections.Generic;
using RNPC.Core.Memory;

namespace RNPC.Core.KnowledgeBuildingStrategies
{
    public interface IKnowledgeRandomizationStrategy
    {
        List<MemoryItem> BuildRandomizedKnowledgeBase(List<MemoryItem> knowledgeBase);
        MemoryItem RandomizeKnowledgeItem(MemoryItem knowledgeItem);
    }
}
