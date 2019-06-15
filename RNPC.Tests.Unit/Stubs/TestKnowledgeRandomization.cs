using System.Collections.Generic;
using RNPC.Core.KnowledgeBuildingStrategies;
using RNPC.Core.Memory;

namespace RNPC.Tests.Unit.Stubs
{
    public class TestKnowledgeRandomization : IKnowledgeRandomizationStrategy
    {
        public List<MemoryItem> BuildRandomizedKnowledgeBase(List<MemoryItem> knowledgeBase)
        {
            return knowledgeBase;
        }

        public MemoryItem RandomizeKnowledgeItem(MemoryItem knowledgeItem)
        {
            return knowledgeItem;
        }
    }
}
