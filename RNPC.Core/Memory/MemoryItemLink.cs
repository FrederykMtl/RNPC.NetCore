using System;

namespace RNPC.Core.Memory
{
    [Serializable]
    public abstract class MemoryItemLink : MemoryItem
    {
        //Dates the link started and ended
        public GameTime.GameTime Started;
        public GameTime.GameTime Ended;

        protected MemoryItemLink(Enum type, Guid referenceId) : base(referenceId)
        {
            // ReSharper disable once VirtualMemberCallInConstructor
            ChangeType(type);
        }

        public string ReverseDescription { get; protected set; }

        public virtual void ChangeType(System.Enum newType)
        {
            ReverseDescription = "";
        }
    }
}