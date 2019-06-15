using System;
using RNPC.Core.Enums;

namespace RNPC.Core.Memory
{
    [Serializable]
    public abstract class MemoryItem
    {
        //Used for common knowledge items - if the
        public Guid ReferenceId { get; }
        public string Name;

        public string Description { set; get; }
        public MemoryItemType ItemType { protected set; get; }

        protected MemoryItem(Guid referenceId)
        {
            ReferenceId = (referenceId == Guid.Empty)? Guid.NewGuid() : referenceId;
        }

        #region Data copy methods

        /// <summary>
        /// Returns a copy of the object with all the information copied accurately
        /// </summary>
        /// <returns>Item with the information</returns>
        public virtual MemoryItem GetAccurateCopy()
        {
            throw new Exception();
            //return this;
        }

        /// <summary>
        /// Returns a copy of the object with all the information copied inaccurately
        /// this represents the character having wrong information
        /// </summary>
        /// <returns>Item with the information</returns>
        public virtual MemoryItem GetInaccurateCopy()
        {
            throw new Exception();
            //return this;
        }

        public override bool Equals(object obj)
        {
            MemoryItem item = obj as MemoryItem;

            if (item == null)
                return false;

            return item.ReferenceId == ReferenceId;
        }

        public override int GetHashCode()
        {
            return ReferenceId.GetHashCode();
        }

        #endregion
    }
}