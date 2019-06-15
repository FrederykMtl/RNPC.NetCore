using System;
using System.Linq;
using RNPC.Core.Enums;

// ReSharper disable once CheckNamespace
namespace RNPC.Core.Memory
{
    public partial class Memory
    {
        [NonSerialized]
        internal OccupationsInterface Occupations;

        internal class OccupationsInterface
        {
            //Reference to the parent class
            private readonly Memory _parent;

            internal OccupationsInterface(Memory parent)
            {
                _parent = parent ?? throw new ArgumentNullException($"I can't find my memories!");
            }

            public Occupation FindOccupationByName(string name)
            {
                var occupation =(Occupation)_parent._longTermMemory.FirstOrDefault(o => o.ItemType == MemoryItemType.Occupation && o.Name == name);

                if(occupation!= null)
                    return occupation;

                return (Occupation)_parent._longTermMemory.FirstOrDefault(o => o.ItemType == MemoryItemType.Occupation && 
                                                                               o.Name.Contains(name));
            }
        }
    }
}