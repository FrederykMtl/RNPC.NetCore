using System;

namespace RNPC.Core.Memory
{
    [Serializable]
    public abstract class MemoryItemRelationship : MemoryItem
    {
        //Dates the relationship started and ended
        public GameTime.GameTime Started;
        public GameTime.GameTime Ended;

        protected MemoryItemRelationship(Enum type, Guid referenceId) : base(referenceId)
        {
            // ReSharper disable once VirtualMemberCallInConstructor
            ChangeType(type);
        }

        public virtual void ChangeType(Enum newType)
        {
            Description = "";
        }
    }
}