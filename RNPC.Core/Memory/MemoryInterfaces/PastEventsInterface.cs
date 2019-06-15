using System;
using System.Collections.Generic;
using System.Linq;
using RNPC.Core.Enums;

// ReSharper disable once CheckNamespace
namespace RNPC.Core.Memory
{
    public partial class Memory
    {
        [NonSerialized]
        internal PastEventsInterface Events;

        internal class PastEventsInterface
        {
            //Reference to the parent class
            private readonly Memory _parent;

            internal PastEventsInterface(Memory parent)
            {
                _parent = parent ?? throw new ArgumentNullException($"I can't find my memories!");
            }

            public PastEvent FindEventByName(string eventName)
            {
               return (PastEvent)_parent._longTermMemory.FirstOrDefault(e => e.ItemType == MemoryItemType.PastEvent &&  e.Name == eventName)??
                      (PastEvent)_parent._longTermMemory.FirstOrDefault(e => e.ItemType == MemoryItemType.PastEvent && e.Name.Contains(eventName));
            }

            public List<PastEvent> FindEventsByType(PastEventType type)
            {
                return _parent._longTermMemory.Where(e => e.ItemType == MemoryItemType.PastEvent).Cast<PastEvent>().Where(p => p.Type== type).ToList();
            }

            public List<PastEvent> FindEventsByTypeAndRelatedPerson(PastEventType type, string relatedPerson)
            {
                return _parent._longTermMemory.Where(e => e.ItemType == MemoryItemType.PastEvent).Cast<PastEvent>().Where(p => p.IsPersonAssociatedWithThisEvent(relatedPerson)).ToList();
            }

            public List<PastEvent> FindEventsByTypeAndRelatedPerson(PastEventType type, Person relatedPerson)
            {
                return _parent._longTermMemory.Where(e => e.ItemType == MemoryItemType.PastEvent).Cast<PastEvent>().Where(p => p.IsPersonAssociatedWithThisEvent(relatedPerson.Name)).ToList();
            }
        }
    }
}