using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RNPC.API.Training;
using RNPC.Core.GameTime;
using RNPC.Core.Memory;
using RNPC.Core.Resources;

namespace RNPC.Tests.Functional.Training
{
    [TestClass]
    public class PersonalTrainerTest : AbstractFunctionalTest
    {
        [TestMethod]
        public void TrainACharacterFor2Years_RandomCharacter_CharacterEvolved()
        {
            //Arrange
            var character = GetMeSterlingArcher();
            VerifyTestFilesAndDirectory(character.MyName, true);
            TrainCharacterFor2Years(character);
        }

        [Ignore]
        [TestMethod]
        public void TrainACharacterForALifetime_CaregiverCharacter_CharacterEvolved()
        {
            //Arrange
            var character = GetMeJess();
            VerifyTestFilesAndDirectory(character.MyName, true);
            TrainACharacterForALifetime(character);
        }

        [Ignore]
        [TestMethod]
        public void TrainACharacterFor10Years_CaregiverCharacter_CharacterEvolved()
        {
            //Arrange
            var character = GetMeJess();
            TrainACharacterFor10Years(character);
        }

        [Ignore]
        [TestMethod]
        public void TrainACharacterForALifetime_InnocentCharacter_CharacterEvolved()
        {
            //Arrange
            var character = GetMeMorty();
            TrainACharacterForALifetime(character);
        }
        [Ignore]
        [TestMethod]
        public void TrainACharacterFor10Years_InnocentCharacter_CharacterEvolved()
        {
            //Arrange
            var character = GetMeMorty();
            TrainACharacterFor10Years(character);
        }

        public static void TrainCharacterFor2Years(global::RNPC.Core.Character charactertoTrain)
        {
            //ARRANGE
            PersonalTrainer trainer = new PersonalTrainer(new ItemLinkFactory());

            //ACT
            trainer.TrainForXNumberOfYears(charactertoTrain, 2);

            //Assert
            Assert.IsNotNull(charactertoTrain);
        }

        public static void TrainACharacterFor10Years(global::RNPC.Core.Character charactertoTrain)
        {
            //ARRANGE
            PersonalTrainer trainer = new PersonalTrainer(new ItemLinkFactory());

            //ACT
            trainer.TrainForXNumberOfYears(charactertoTrain, 10);

            //Assert
            Assert.IsNotNull(charactertoTrain);
        }

        public static void TrainACharacterForALifetime(global::RNPC.Core.Character charactertoTrain)
        {
            //ARRANGE
            PersonalTrainer trainer = new PersonalTrainer(new ItemLinkFactory());
            //result file
            var filePath = ConfigurationDirectory.Instance.LogFilesDirectory +  "LifetimeLog" + charactertoTrain.MyName + ".txt";
  
            if (File.Exists(filePath))
                File.Delete(filePath);

            StringBuilder testResultLog = new StringBuilder();

            testResultLog.AppendLine(@"Pre Values :");
            foreach (var value in charactertoTrain.MyTraits.PersonalValues)
            {
                testResultLog.AppendLine(value.ToString());
            }

            var preTrainingQs = charactertoTrain.MyTraits.GetPersonalQualitiesValues();

            Stopwatch timer = new Stopwatch();
            timer.Start();
            //ACT
            trainer.TrainForALifetime(charactertoTrain, new StandardDateTime(DateTime.Now));

            testResultLog.AppendLine(@"Post training result");
            bool changes = false;

            foreach (var quality in charactertoTrain.MyTraits.GetPersonalQualitiesValues())
            {
                if (quality.Value == preTrainingQs[quality.Key]) continue;

                testResultLog.AppendLine(quality.Key + @" changed: was " + preTrainingQs[quality.Key] + @" is now " + quality.Value);
                changes = true;
            }

            testResultLog.AppendLine(@"\nPost training values :");
            foreach (var value in charactertoTrain.MyTraits.PersonalValues)
            {
                testResultLog.AppendLine(value.ToString());
                changes = true;
            }

            if (!changes)
                testResultLog.AppendLine(@"no changes");

            timer.Stop();
            testResultLog.AppendLine(@"\n\nFinal time : " + (int)(timer.ElapsedMilliseconds / 1000));

            //Assert
            Assert.IsTrue(changes);
            Assert.IsNotNull(charactertoTrain);
            Assert.IsTrue(charactertoTrain.HasBeenTrained);
        }
    }
}
