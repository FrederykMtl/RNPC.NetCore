using System;
using System.Linq;
using RNPC.Core.Enums;

// ReSharper disable once CheckNamespace
namespace RNPC.Core.Memory
{
    public partial class Memory
    {
        [NonSerialized]
        internal PersonsInterface Persons;

        internal class PersonsInterface
        {
            //Reference to the parent class
            private readonly Memory _parent;

            internal PersonsInterface(Memory parent)
            {
                _parent = parent ?? throw new ArgumentNullException($"I can't find my memories!");
            }

            /// <summary>
            /// Check if I know this person.
            /// </summary>
            /// <param name="person">Person I'm checking for.</param>
            /// <returns>Whether I know the person or not.</returns>
            public bool DoIKnowThisPerson(Person person)
            {
                if (_parent._longTermMemory.Contains(person))
                    return true;

                if (_parent._shortTermMemory.Contains(person))
                    return true;

                var knownPerson = _parent._longTermMemory.FirstOrDefault(p => p.ItemType == MemoryItemType.Person && p.Name == person.Name) ??
                                  _parent._shortTermMemory.FirstOrDefault(p => p.ItemType == MemoryItemType.Person && p.Name == person.Name);

                return knownPerson != null;
            }

            /// <summary>
            /// Find a Person based only on their name
            /// </summary>
            /// <param name="personName"></param>
            /// <returns></returns>
            public Person FindPersonByName(string personName)
            {
                var person = (Person)_parent._longTermMemory.FirstOrDefault(o => o.ItemType == MemoryItemType.Person && o.Name == personName);

                if (person != null)
                    return person;

                return (Person)_parent._longTermMemory.FirstOrDefault(o => o.ItemType == MemoryItemType.Person && o.Name.Contains(personName));
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="personToRemove"></param>
            /// <returns></returns>
            public bool RemoveFromMemory(Person personToRemove)     //TODO cover every object
            {
                var relationships = _parent.Me.GetAllMyRelationshipsWithThisPerson(personToRemove);

                relationships.ForEach(relationship => _parent.Me.RemoveRelationship(relationship));

                _parent.RemoveOpinionsAbout(personToRemove);

                _parent._longTermMemory.Remove(personToRemove);

                return true;
            }
        }
    }
}