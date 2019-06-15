using System;
using System.Collections.Generic;
using System.Linq;
using RNPC.Core.Enums;
using RNPC.Core.TraitGeneration;

namespace RNPC.Core.Memory
{
    [Serializable]
    public class Organization : MemoryItem
    {
        public OrganizationType Type;

        //Dates of inception and end of the organization
        public GameTime.GameTime Started;
        public GameTime.GameTime Ended;

        #region Constructor

        public Organization(string name, OrganizationType type, Guid referenceId, string description = "") : base(referenceId)
        {
            Name = name;
            Type = type;
            Description = description;
            ItemType = MemoryItemType.Organization;
        }

        #endregion

        #region Associations
        //People associated to the organization
        private List<Association> _associations;
        public void AddAssociation(Association newAssociation)
        {
            if(_associations == null)
                _associations = new List<Association>();

            if (!_associations.Contains(newAssociation))
            {
                _associations.Add(newAssociation);
            }
        }

        public Association FindAssociation(Person withPerson, AssociationType type)
        {
            return _associations.FirstOrDefault(p => p.AssociatedPerson == withPerson && p.Type == type);
        }

        #endregion

        #region Data copy methods
        /// <summary>
        /// Returns a copy of the object with all the information copied accurately
        /// </summary>
        /// <returns>Item with the information</returns>
        public override MemoryItem GetAccurateCopy()
        {
            Organization copy = new Organization(Name, Type, ReferenceId, Description)
            {
                ItemType = ItemType,
                Started = Started,
                Ended = Ended,
                _associations = _associations
            };

            return copy;
        }

        /// <summary>
        /// Returns a copy of the object with all the information copied inaccurately
        /// this represents the character having wrong information
        /// </summary>
        /// <returns>Item with the information</returns>
        public override MemoryItem GetInaccurateCopy()
        {
            GameTime.GameTime started = Started;
            GameTime.GameTime ended = Ended;
            OrganizationType type = Type;

            //TODO : Randomize name

            int falsificationCase = RandomValueGenerator.GenerateIntWithMaxValue(4);

            switch (falsificationCase)
            {
                case 1:
                    int variance = RandomValueGenerator.GenerateRealWithinValues(-30, 30);
                    started?.SetYear(started.GetYear() + variance);
                    break;
                case 2:
                    int deathVariance = RandomValueGenerator.GenerateRealWithinValues(-30, 30);
                    ended?.SetYear(ended.GetYear() + deathVariance);
                    break;
                case 3:
                    type = (OrganizationType)RandomValueGenerator.GenerateIntWithMaxValue(Enum.GetNames(typeof(OrganizationType)).Length);
                    break;
                case 4:
                    type = (OrganizationType)RandomValueGenerator.GenerateIntWithMaxValue(Enum.GetNames(typeof(OrganizationType)).Length);
                    variance = RandomValueGenerator.GenerateRealWithinValues(-30, 30);
                    started?.SetYear(started.GetYear() + variance);
                    break;
            }

            Organization copy = new Organization(Name, type, ReferenceId, Description)
            {
                ItemType = ItemType,
                Started = started,
                Ended = ended,
                _associations = _associations
            };

            return copy;
        }
        #endregion
    }
}