using Microsoft.VisualStudio.TestTools.UnitTesting;
using RNPC.Core.Enums;
using RNPC.Core.InitializationStrategies;

// ReSharper disable InconsistentNaming

namespace RNPC.Tests.Unit.DTO.Enum
{
    /// <summary>
    /// Summary description for StrongPointsValidation
    /// </summary>
    [TestClass]
    public class StrongPointsValidation
    {
        [TestMethod]
        public void StrongPoints_ByArchetype_AllStrongPointsAccountedForInCompiledStrongPointsEnum()
        {
            //Arrange
            //Act
            TheCaregiverInitializationMethod caregiverInitializationMethod = new TheCaregiverInitializationMethod();
            TheCreatorInitializationMethod creatorInitializationMethod = new TheCreatorInitializationMethod();
            TheDestroyerInitializationMethod destroyerInitializationMethod = new TheDestroyerInitializationMethod();
            TheInnocentInitializationMethod innocentInitializationMethod = new TheInnocentInitializationMethod();
            TheJesterInitializationMethod jesterInitializationMethod = new TheJesterInitializationMethod();
            TheLoverInitializationMethod loverInitializationMethod = new TheLoverInitializationMethod();
            TheMagicianInitializationMethod magicianInitializationMethod = new TheMagicianInitializationMethod();
            TheOrphanInitializationMethod orphanInitializationMethod = new TheOrphanInitializationMethod();
            TheRulerInitializationMethod rulerInitializationMethod = new TheRulerInitializationMethod();
            TheSageInitializationMethod sageInitializationMethod = new TheSageInitializationMethod();
            TheSeekerInitializationMethod seekerInitializationMethod = new TheSeekerInitializationMethod();
            TheWarriorInitializationMethod warriorInitializationMethod = new TheWarriorInitializationMethod();

            //ASSERT
            caregiverInitializationMethod.StrongPoints.ForEach(s => Assert.IsTrue(QualityIsDefinedInEnum(s)));
            creatorInitializationMethod.StrongPoints.ForEach(s => Assert.IsTrue(QualityIsDefinedInEnum(s)));
            destroyerInitializationMethod.StrongPoints.ForEach(s => Assert.IsTrue(QualityIsDefinedInEnum(s)));
            innocentInitializationMethod.StrongPoints.ForEach(s => Assert.IsTrue(QualityIsDefinedInEnum(s)));
            jesterInitializationMethod.StrongPoints.ForEach(s => Assert.IsTrue(QualityIsDefinedInEnum(s)));
            loverInitializationMethod.StrongPoints.ForEach(s => Assert.IsTrue(QualityIsDefinedInEnum(s)));
            magicianInitializationMethod.StrongPoints.ForEach(s => Assert.IsTrue(QualityIsDefinedInEnum(s)));
            orphanInitializationMethod.StrongPoints.ForEach(s => Assert.IsTrue(QualityIsDefinedInEnum(s)));
            rulerInitializationMethod.StrongPoints.ForEach(s => Assert.IsTrue(QualityIsDefinedInEnum(s)));
            sageInitializationMethod.StrongPoints.ForEach(s => Assert.IsTrue(QualityIsDefinedInEnum(s)));
            seekerInitializationMethod.StrongPoints.ForEach(s => Assert.IsTrue(QualityIsDefinedInEnum(s)));
            warriorInitializationMethod.StrongPoints.ForEach(s => Assert.IsTrue(QualityIsDefinedInEnum(s)));
        }


        private static bool QualityIsDefinedInEnum(string weakPoint)
        {
            if (System.Enum.IsDefined(typeof(CompiledStrongPoints), weakPoint))
                return true;

            Assert.Fail("The following strong point is missing: " + weakPoint);
            return false;
        }
    }
}