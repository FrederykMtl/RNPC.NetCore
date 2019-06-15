using System;
using RNPC.Core;
using RNPC.Core.Enums;
using RNPC.Core.Exceptions;
using RNPC.Core.GameTime;
using RNPC.Core.Learning;
using RNPC.Core.Memory;
using RNPC.Core.TraitGeneration;

namespace RNPC.API.Training
{
    public class PersonalTrainer
    {
        private readonly IItemLinkFactory _linkFactory;
        public event EventHandler<Character> OneMonthTrained;

        public PersonalTrainer(IItemLinkFactory linkFactory)
        {
            if(linkFactory == null)
                throw new RnpcParameterException("An item link factory is required.");

            _linkFactory = linkFactory;
        }

        /// <summary>
        /// Put a character through a year of events and training
        /// </summary>
        /// <param name="characterToTrain">Caracter to be trained</param>
        /// <returns></returns>
        public bool TrainForAYear(Character characterToTrain)
        {
            for (int i = 0; i < TrainingStatistics.DaysPerYear; i++)
            {
                int numberOfEventsForTheDay = RandomValueGenerator.GenerateRealWithinValues(TrainingStatistics.MinimumEventsPerDay, TrainingStatistics.MaximumEventsPerDay);

                for (int j = 0; j < numberOfEventsForTheDay; j++)
                {
                    characterToTrain.InteractWithMe(RandomEventGenerator.GetRandomEvent());
                }

                characterToTrain.GoToSleep(new LearningController());

                if(i % 30 == 0)
                    OneMonthTrained?.Invoke(this, characterToTrain);
            } 
            
            return true;
        }

        /// <summary>
        /// Train a character X years
        /// </summary>
        /// <param name="characterToTrain">Caracter to be trained</param>
        /// <param name="numberOfYears">Number of years of training</param>
        /// <returns>Training was successful</returns>
        public bool TrainForXNumberOfYears(Character characterToTrain, int numberOfYears)
        {
            SetupRelationships(characterToTrain);
            for (int i = 0; i < numberOfYears; i++)
            {
                if(!TrainForAYear(characterToTrain))
                    return false;

                if(i % 5 == 0)
                    characterToTrain.MyMemory.ResetItemsForTraining();
            }

            CleanupRelationships(characterToTrain);
            return true;
        }

        /// <summary>
        /// Put a character trhough a lifetime of experience
        /// </summary>
        /// <param name="characterToTrain">Character to be trained</param>
        /// <param name="currentTime">current/starting game time</param>
        /// <returns></returns>
        public bool TrainForALifetime(Character characterToTrain, GameTime currentTime)
        {
            SetupRelationships(characterToTrain);

            int iterations = characterToTrain.MyMemory.Me.Age(currentTime) - TrainingStatistics.StartingAgeOfTraining;

            for (int i = 0; i < iterations; i++)
            {
                if (!TrainForAYear(characterToTrain))
                    return false;
            }

            characterToTrain.TrainingDone();
            CleanupRelationships(characterToTrain);

            return true;
        }

        /// <summary>
        /// Creates relationships in memory
        /// </summary>
        /// <param name="characterToTrain"></param>
        private void SetupRelationships(Character characterToTrain)
        {
            //friend
            Person friend = new Person("Tested_Friend", Guid.NewGuid());
            characterToTrain.AddContentToLongTermMemory(friend);

            _linkFactory.CreateRelationshipBetweenTwoPersons(characterToTrain.MyMemory.Me, friend, PersonalRelationshipTypeName.Friend, PersonalRelationshipTypeName.Friend);

            var newOpinion = new Opinion(friend, OpinionType.Like, null);
            characterToTrain.MyMemory.FormAnOpinionAbout(newOpinion);

            //enemy
            var enemy = new Person("An_Enemy", Gender.Male, Sex.Male, Orientation.Straight, Guid.NewGuid());
            _linkFactory.CreateRelationshipBetweenTwoPersons(characterToTrain.MyMemory.Me, enemy, PersonalRelationshipTypeName.Enemy, PersonalRelationshipTypeName.Enemy);

            characterToTrain.AddContentToLongTermMemory(enemy);

            var newOpinion2 = new Opinion(enemy, OpinionType.Hate, null);
            characterToTrain.MyMemory.FormAnOpinionAbout(newOpinion2);

            //the conflict
            var conflict = new PastEvent("Nasty fight", Guid.NewGuid()) { Type = PastEventType.Conflict };
            _linkFactory.CreateInvolvementBetweenPersonAndEvent(enemy, conflict, PersonalInvolvementType.Started);
            _linkFactory.CreateInvolvementBetweenPersonAndEvent(characterToTrain.MyMemory.Me, conflict, PersonalInvolvementType.WasCaughtIn);
            characterToTrain.AddContentToLongTermMemory(conflict);
        }

        /// <summary>
        /// Creates relationships in memory
        /// </summary>
        /// <param name="characterToTrain"></param>
        private static void CleanupRelationships(Character characterToTrain)
        {
            //friend
            Person friend = characterToTrain.MyMemory.Persons.FindPersonByName("Tested_Friend");
            characterToTrain.MyMemory.Persons.RemoveFromMemory(friend);

            //enemy
            var enemy = characterToTrain.MyMemory.Persons.FindPersonByName("An_Enemy");
            characterToTrain.MyMemory.Persons.RemoveFromMemory(enemy);
        }
    }
}
