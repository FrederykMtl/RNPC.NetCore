using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using RNPC.Core.Enums;
using RNPC.Core.TraitGeneration;

namespace RNPC.Core
{
    [Serializable]
    public class CharacterTraits
    {
        const int PointOfTiredness = 10;
        #region Internals
        internal List<string> StrongPoints;
        internal List<string> WeakPoints;
        internal List<PersonalValues> PersonalValues;
        public int Energy { private set; get; }
        public event EventHandler<int> ImTired;
        #endregion

        #region Public Properties
        /// <summary>
        /// These are a characters qualities, values that range from 1 to 100.
        /// </summary>
        #region Qualities
        [Quality]
            public int Acuity { get; internal set; }
            [Quality]
            public int Adaptiveness { get; internal set; }
            [Quality]
            public int Ambition { get; internal set; }
            [Quality]
            public int Assertiveness { get; internal set; }
            [Quality]
            public int Awareness { get; internal set; }
            [Quality]
            public int Changing { get; internal set; }
            [Quality]
            public int Charisma { get; internal set; }
            [Quality]
            public int Charitable { get; internal set; }
            [Quality]
            public int Compassion { get; internal set; }
            [Quality]
            public int Confidence { get; internal set; }

            [Quality]
            public int Conscience { get; internal set; }
            [Quality]
            public int CriticalSense { get; internal set; }
            [Quality]
            public int Determination { get; internal set; }
            [Quality]
            public int Energetic { get; internal set; }
            [Quality]
            public int Expressiveness { get; internal set; }
            [Quality]
            public int Gregariousness { get; internal set; }
            [Quality]
            public int Imagination { get; internal set; }
            [Quality]
            public int Introspection { get; internal set; }
            [Quality]
            public int Inventiveness { get; internal set; }
            [Quality]
            public int Memory { get; internal set; }

            [Quality]
            public int Modesty { get; internal set; }
            [Quality]
            public int Quietude { get; internal set; }
            [Quality]
            public int Outlook { get; internal set; }
            [Quality]
            public int SelfEsteem { get; internal set; }
            [Quality]
            public int Selflessness { get; internal set; }
            [Quality]
            public int Sociability { get; internal set; }
            [Quality]
            public int Tact { get; internal set; }
            [Quality]
            public int Tolerance { get; internal set; }
            [Quality]
            public int Willpower { get; internal set; }
        #endregion

        #region Neurophysical Identity
            [Identity]
            public Gender Gender { get; }
            [Identity]
            public Sex Sex { get; }
            [Identity]
            public Orientation Orientation { get; }
        #endregion

        #region Emotional States
            public EmotionalStates ShortTermEmotions;
            public EmotionalStates LongTermEmotions;
        #endregion

        public string InternalId { get; }
        #endregion

        #region Public methods

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="characterName">name of the character</param>
        /// <param name="characterSex">sex of the character</param>
        /// <param name="orientation">sexual orientation of the character</param>
        /// <param name="gender">gender of the character</param>
        public CharacterTraits(string characterName, Sex? characterSex, Orientation? orientation = null, Gender? gender = null)
        {
            const string pattern = "[^a-zA-Z\\d]";
            Regex rgx = new Regex(pattern);
            
            InternalId = rgx.Replace(characterName + DateTime.Now.ToString("O"), string.Empty);

            // ReSharper disable MergeConditionalExpression
            //disabled for code clarity
            Sex = characterSex.HasValue ? characterSex.Value : RandomAttributeGenerator.GetRandomSex();
            Orientation = orientation.HasValue ? orientation.Value : RandomAttributeGenerator.GetRandomOrientation();
            Gender = gender.HasValue ? gender.Value : RandomAttributeGenerator.GetRandomGender();

            PersonalValues = new List<PersonalValues>();
            ShortTermEmotions = new EmotionalStates();
            LongTermEmotions = new EmotionalStates();
        }

        /// <summary>
        /// Returns a list of all the qualities and their value
        /// </summary>
        /// <returns>a dictionary with their name and value</returns>
        public Dictionary<string, int> GetPersonalQualitiesValues()
        {
            var qualities = new Dictionary<string, int>
            {
                { "Acuity", Acuity },
                { "Adaptiveness", Adaptiveness },
                { "Ambition", Ambition },
                { "Assertiveness", Assertiveness },
                { "Awareness", Awareness },
                { "Changing", Changing },
                { "Charisma", Charisma },
                { "Charitable", Charitable },
                { "Compassion", Compassion },
                { "Confidence", Confidence },
                { "Conscience", Conscience },
                { "CriticalSense", CriticalSense },
                { "Determination", Determination },
                { "Energetic", Energetic },
                { "Expressiveness", Expressiveness },
                { "Gregariousness", Gregariousness },
                { "Imagination", Imagination },
                { "Introspection", Introspection },
                { "Inventiveness", Inventiveness },
                { "Memory", Memory },
                { "Modesty", Modesty },
                { "Quietude", Quietude },
                { "Outlook", Outlook },
                { "SelfEsteem", SelfEsteem },
                { "Selflessness", Selflessness },
                { "Sociability", Sociability },
                { "Tolerance", Tolerance },
                { "Tact", Tact },
                { "Willpower", Willpower }
            };

            return qualities;
        }

        /// <summary>
        /// Returns a list of all attributes of a specified type and their value
        /// </summary>
        /// <returns>a dictionary with their name and value</returns>
        public Dictionary<string, int> GetEmotionalStateValues()
        {
            var emotions = new Dictionary<string, int>
            {
                { "Anger", ShortTermEmotions.Anger },
                { "Fear", ShortTermEmotions.Fear },
                { "Happiness", ShortTermEmotions.Happiness },
                { "Sadness", ShortTermEmotions.Sadness },
                { "Disgust", ShortTermEmotions.Disgust },
                { "Curiosity", ShortTermEmotions.Curiosity },
                { "Surprise", ShortTermEmotions.Surprise },
                { "Jealousy", ShortTermEmotions.Jealousy },
                { "Shame", ShortTermEmotions.Shame },
                { "Disappointment", ShortTermEmotions.Disappointment },
                { "Pride", ShortTermEmotions.Pride }
            };

            return emotions;
        }

        /// <summary>
        /// Returns a list of all attributes of a specified type and their value
        /// </summary>
        /// <returns>a dictionary with their name and value</returns>
        public Dictionary<string, int> GetLongTermEmotionalStateValues()
        {
            var emotions = new Dictionary<string, int>
            {
                { "Anger", LongTermEmotions.Anger },
                { "Fear", LongTermEmotions.Fear },
                { "Happiness", LongTermEmotions.Happiness },
                { "Sadness", LongTermEmotions.Sadness },
                { "Disgust", LongTermEmotions.Disgust },
                { "Curiosity", LongTermEmotions.Curiosity },
                { "Surprise", LongTermEmotions.Surprise },
                { "Jealousy", LongTermEmotions.Jealousy },
                { "Shame", LongTermEmotions.Shame },
                { "Disappointment", LongTermEmotions.Disappointment },
                { "Pride", LongTermEmotions.Pride }
            };

            return emotions;
        }

        public void SetMyEnergy(int energy)
        {
            Energy = energy;

            if(energy <= PointOfTiredness)
                ImTired?.Invoke(this, energy);
        }

        #endregion

        #region Internal methods

        /// <summary>
        /// Resets short term emotions to long term values.
        /// Should be called each morning (after learning step)
        /// </summary>
        internal void ResetEmotions()
        {
            ShortTermEmotions.Anger = LongTermEmotions.Anger;
            ShortTermEmotions.Curiosity = LongTermEmotions.Curiosity;
            ShortTermEmotions.Disappointment = LongTermEmotions.Disappointment;
            ShortTermEmotions.Disgust = LongTermEmotions.Disgust;
            ShortTermEmotions.Fear = LongTermEmotions.Fear;
            ShortTermEmotions.Happiness = LongTermEmotions.Happiness;
            ShortTermEmotions.Jealousy = LongTermEmotions.Jealousy;
            ShortTermEmotions.Pride = LongTermEmotions.Pride;
            ShortTermEmotions.Sadness = LongTermEmotions.Sadness;
            ShortTermEmotions.Shame = LongTermEmotions.Shame;
            ShortTermEmotions.Surprise = LongTermEmotions.Surprise;
        }

        #region Qualities

        /// <summary>
        /// Allows to set the value of a property based on his index in the list of qualities
        /// </summary>
        /// <param name="index">Index in the list of qualities</param>
        /// <param name="value">new value</param>
        internal void SetQualityAttributeByIndex(int index, int value)
        {
            var qualities = typeof(CharacterTraits).GetProperties()
                .Where(prop => Attribute.IsDefined(prop, typeof(QualityAttribute)));

            qualities.ElementAt(index).SetValue(this, value);
        }

        /// <summary>
        /// Allows to set the value of a property based on his index in the list of qualities
        /// </summary>
        /// <param name="name">name of the property</param>
        /// <param name="value">new value</param>
        internal void SetQualityAttributeByName(string name, int value)
        {
            var quality = typeof(CharacterTraits).GetProperties()
                .FirstOrDefault(prop => Attribute
                                            .IsDefined(prop, typeof(QualityAttribute)) && prop.Name == name);

            if(quality != null)
                quality.SetValue(this, value);
        }

        /// <summary>
        /// Allows to set the value of a property based on his index in the list of qualities
        /// </summary>
        /// <param name="index">Index in the list of qualities</param>
        internal PropertyInfo GetQualityPropertyByIndex(int index)
        {
            var qualities = typeof(CharacterTraits).GetProperties()
                .Where(prop => Attribute.IsDefined(prop, typeof(QualityAttribute)));

            return qualities.ElementAt(index);
        }

        /// <summary>
        /// returns the total count of qualities.
        /// mostly used for randomizing purposes
        /// </summary>
        /// <returns></returns>
        internal static int GetPersonalQualitiesCount()
        {
            //This could have been gotten through reflection but the performance cost
            //outweighs the need for flexibility. This will be controlled via a unit test
            return 29;
        }

        #endregion

        /// <summary>
        /// returns the total count of emational states.
        /// mostly used for randomizing purposes
        /// </summary>
        /// <returns></returns>
        internal static int GetEmotionalStatesCount()
        {
            //This could have been gotten through reflection but the performance cost
            //outweighs the need for flexibility. This will be controlled via a unit test
            return 11;
        }

        internal void RaiseMyPride(uint raisePrideBy)
        {
            if (ShortTermEmotions.Pride + raisePrideBy >= 100)
                ShortTermEmotions.Pride = 100;

            ShortTermEmotions.Pride += (int)raisePrideBy;
        }

        internal void RaiseMyShame(uint raiseShameBy)
        {
            if (ShortTermEmotions.Pride - raiseShameBy <= -100)
                ShortTermEmotions.Pride = -100;

            ShortTermEmotions.Pride -= (int)raiseShameBy;
        }

        internal bool DoIPossessThisPersonalValue(PersonalValues value)
        {
            return PersonalValues.Contains(value);
        }

        internal List<PersonalValues> GetMyPersonalValues()
        {
            return PersonalValues;
        }

        #endregion
    }
}