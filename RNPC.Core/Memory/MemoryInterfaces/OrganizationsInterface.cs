using System;

// ReSharper disable once CheckNamespace
namespace RNPC.Core.Memory
{
    public partial class Memory
    {
        [NonSerialized]
        internal OrganizationsInterface Organizations;

        internal class OrganizationsInterface
        {
            //Reference to the parent class
            private readonly Core.Memory.Memory _parent;

            internal OrganizationsInterface(Core.Memory.Memory parent)
            {
                _parent = parent ?? throw new ArgumentNullException($"I can't find my memories!");
            }
        }
    }
}