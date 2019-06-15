using System;
using System.Collections.Generic;
using System.Linq;
using RNPC.Core.Enums;
using RNPC.Core.Learning.Interfaces;
using RNPC.Core.TraitGeneration;

// ReSharper disable InlineOutVariableDeclaration
namespace RNPC.Core.Learning
{
    public class PersonalValueAssociations : IPersonalValueAssociations
    {
        private readonly Dictionary<PersonalValues, List<PersonalValues>> _associationIndex = new Dictionary<PersonalValues, List<PersonalValues>>
        {
            { PersonalValues.Achievement, new List<PersonalValues> { PersonalValues.Challenge, PersonalValues.Success, PersonalValues.Determination } },
            { PersonalValues.Adventure, new List<PersonalValues> { PersonalValues.Challenge, PersonalValues.Freedom, PersonalValues.Boldness } },
            { PersonalValues.Authenticity, new List<PersonalValues> { PersonalValues.Truth } },
            { PersonalValues.Authority, new List<PersonalValues> { PersonalValues.Community, PersonalValues.Loyalty, PersonalValues.Leadership } },
            { PersonalValues.Autonomy, new List<PersonalValues> { PersonalValues.Freedom, PersonalValues.Independance } },
            { PersonalValues.Balance, new List<PersonalValues> { PersonalValues.Stability, PersonalValues.Justice } },
            { PersonalValues.Beauty, new List<PersonalValues> { PersonalValues.Pleasure, PersonalValues.Creativity } },
            { PersonalValues.Boldness, new List<PersonalValues> { PersonalValues.Adventure, PersonalValues.Strength, PersonalValues.Optimism } },
            { PersonalValues.Challenge, new List<PersonalValues> { PersonalValues.Achievement, PersonalValues.Boldness, PersonalValues.Growth } },
            { PersonalValues.Community, new List<PersonalValues> { PersonalValues.Friendship, PersonalValues.Service, PersonalValues.Loyalty } },
            { PersonalValues.Compassion, new List<PersonalValues> { PersonalValues.Love, PersonalValues.Kindness } },
            { PersonalValues.Competency, new List<PersonalValues> { PersonalValues.Achievement, PersonalValues.Reputation } },
            { PersonalValues.Contribution, new List<PersonalValues> { PersonalValues.Achievement, PersonalValues.Community, PersonalValues.Responsibility } },
            { PersonalValues.Creativity, new List<PersonalValues> { PersonalValues.Beauty, PersonalValues.Curiosity } },
            { PersonalValues.Curiosity, new List<PersonalValues> { PersonalValues.Knowledge, PersonalValues.Learning } },
            { PersonalValues.Determination, new List<PersonalValues> { PersonalValues.Success, PersonalValues.Achievement, PersonalValues.Strength } },
            { PersonalValues.Faith, new List<PersonalValues> { PersonalValues.Loyalty, PersonalValues.Tradition, PersonalValues.Community } },
            { PersonalValues.Fame, new List<PersonalValues> { PersonalValues.Success, PersonalValues.Recognition } },
            { PersonalValues.Freedom, new List<PersonalValues> { PersonalValues.Autonomy, PersonalValues.Independance } },
            { PersonalValues.Friendship, new List<PersonalValues> { PersonalValues.Loyalty, PersonalValues.Openness, PersonalValues.Community } },
            { PersonalValues.Growth, new List<PersonalValues> { PersonalValues.Learning, PersonalValues.Curiosity, PersonalValues.Challenge } },
            { PersonalValues.Happiness, new List<PersonalValues> { PersonalValues.InnerHarmony, PersonalValues.Pleasure, PersonalValues.Love } },
            { PersonalValues.Honesty, new List<PersonalValues> { PersonalValues.Openness, PersonalValues.Truth } },
            { PersonalValues.Honour, new List<PersonalValues> { PersonalValues.Truth, PersonalValues.Boldness, PersonalValues.Reputation } },
            { PersonalValues.Humour, new List<PersonalValues> { PersonalValues.Pleasure, PersonalValues.Creativity, PersonalValues.Happiness } },
            { PersonalValues.Independance, new List<PersonalValues> { PersonalValues.Autonomy, PersonalValues.Freedom, PersonalValues.Security } },
            { PersonalValues.Influence, new List<PersonalValues> { PersonalValues.Wealth, PersonalValues.Reputation, PersonalValues.Status } },
            { PersonalValues.InnerHarmony, new List<PersonalValues> { PersonalValues.Peace, PersonalValues.Poise} },
            { PersonalValues.Justice, new List<PersonalValues> { PersonalValues.Truth, PersonalValues.Balance } },
            { PersonalValues.Kindness, new List<PersonalValues> { PersonalValues.Compassion, PersonalValues.Authenticity, PersonalValues.Love } },
            { PersonalValues.Knowledge, new List<PersonalValues> { PersonalValues.Learning, PersonalValues.Truth, PersonalValues.Wisdom } },
            { PersonalValues.Leadership, new List<PersonalValues> { PersonalValues.Authority, PersonalValues.Loyalty } },
            { PersonalValues.Learning, new List<PersonalValues> { PersonalValues.Knowledge, PersonalValues.Truth } },
            { PersonalValues.Love, new List<PersonalValues> { PersonalValues.Honesty, PersonalValues.Openness, PersonalValues.Friendship } },
            { PersonalValues.Loyalty, new List<PersonalValues> { PersonalValues.Trustworthiness, PersonalValues.Service } },
            { PersonalValues.Openness, new List<PersonalValues> { PersonalValues.Honesty, PersonalValues.Truth } },
            { PersonalValues.Optimism, new List<PersonalValues> { PersonalValues.Faith, PersonalValues.Boldness } },
            { PersonalValues.Peace, new List<PersonalValues> { PersonalValues.Security, PersonalValues.Stability } },
            { PersonalValues.Pleasure, new List<PersonalValues> { PersonalValues.Beauty, PersonalValues.Boldness, PersonalValues.Humour } },
            { PersonalValues.Poise, new List<PersonalValues> { PersonalValues.Stability, PersonalValues.InnerHarmony } },
            { PersonalValues.Recognition, new List<PersonalValues> { PersonalValues.Fame, PersonalValues.Reputation, PersonalValues.Status } },
            { PersonalValues.Reputation, new List<PersonalValues> { PersonalValues.Recognition, PersonalValues.Honour, PersonalValues.Competency } },
            { PersonalValues.Respect, new List<PersonalValues> { PersonalValues.Status, PersonalValues.Authority, PersonalValues.SelfRespect } },
            { PersonalValues.Responsibility, new List<PersonalValues> { PersonalValues.Work, PersonalValues.Service, PersonalValues.Trustworthiness } },
            { PersonalValues.Security, new List<PersonalValues> { PersonalValues.Stability, PersonalValues.Strength } },
            { PersonalValues.SelfRespect, new List<PersonalValues> { PersonalValues.InnerHarmony, PersonalValues.Respect } },
            { PersonalValues.Service, new List<PersonalValues> { PersonalValues.Community, PersonalValues.Responsibility } },
            { PersonalValues.Stability, new List<PersonalValues> { PersonalValues.Balance, PersonalValues.Security, PersonalValues.Peace} },
            { PersonalValues.Status, new List<PersonalValues> { PersonalValues.Recognition, PersonalValues.Fame, PersonalValues.Influence } },
            { PersonalValues.Strength, new List<PersonalValues> { PersonalValues.Boldness, PersonalValues.Security, PersonalValues.Competency } },
            { PersonalValues.Success, new List<PersonalValues> { PersonalValues.Achievement, PersonalValues.Contribution, PersonalValues.Wealth } },
            { PersonalValues.Tradition, new List<PersonalValues> { PersonalValues.Community, PersonalValues.Loyalty, PersonalValues.Respect } },
            { PersonalValues.Trustworthiness, new List<PersonalValues> { PersonalValues.Loyalty, PersonalValues.Truth } },
            { PersonalValues.Truth, new List<PersonalValues> { PersonalValues.Curiosity, PersonalValues.Authenticity, PersonalValues.Wisdom } },
            { PersonalValues.Wealth, new List<PersonalValues> { PersonalValues.Fame, PersonalValues.Success, PersonalValues.Pleasure } },
            { PersonalValues.Wisdom, new List<PersonalValues> { PersonalValues.Curiosity, PersonalValues.Learning, PersonalValues.Truth } },
            { PersonalValues.Work, new List<PersonalValues> { PersonalValues.Achievement, PersonalValues.Responsibility, PersonalValues.Service } }
        };

        public PersonalValues? GetAssociatedValue(string value)
        {
            PersonalValues valueToFind;

            if (Enum.TryParse(value, out valueToFind))
                return GetAssociatedValue(valueToFind);

            return null;
        }

        public PersonalValues? GetAssociatedValue(PersonalValues value)
        {
            List<PersonalValues> associatedValues;
            _associationIndex.TryGetValue(value, out associatedValues);

            if (associatedValues != null && associatedValues.Any())
            {
                if (associatedValues.Count == 1)
                    return associatedValues[0];

                int position = RandomValueGenerator.GenerateIntWithMaxValue(associatedValues.Count) - 1;

                return associatedValues[position];
            }

            return null;
        }
    }
}
