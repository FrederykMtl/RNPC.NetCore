using Microsoft.VisualStudio.TestTools.UnitTesting;
using RNPC.Core.Enums;
using RNPC.Core.InitializationStrategies;

// ReSharper disable InconsistentNaming

namespace RNPC.Tests.Unit.DTO.Enum
{
    /// <summary>
    /// Summary description for WeakPointsValidation
    /// </summary>
    [TestClass]
    public class WeakPointsValidation
    {
        [TestMethod]
        public void WeakPoints_ByArchetype_AllWeakPointsAccountedForInCompiledWeakPointsEnum()
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
            caregiverInitializationMethod.WeakPoints.ForEach(s => Assert.IsTrue(QualityIsDefinedInEnum(s)));
            creatorInitializationMethod.WeakPoints.ForEach(s => Assert.IsTrue(QualityIsDefinedInEnum(s)));
            destroyerInitializationMethod.WeakPoints.ForEach(s => Assert.IsTrue(QualityIsDefinedInEnum(s)));
            innocentInitializationMethod.WeakPoints.ForEach(s => Assert.IsTrue(QualityIsDefinedInEnum(s)));
            jesterInitializationMethod.WeakPoints.ForEach(s => Assert.IsTrue(QualityIsDefinedInEnum(s)));
            loverInitializationMethod.WeakPoints.ForEach(s => Assert.IsTrue(QualityIsDefinedInEnum(s)));
            magicianInitializationMethod.WeakPoints.ForEach(s => Assert.IsTrue(QualityIsDefinedInEnum(s)));
            orphanInitializationMethod.WeakPoints.ForEach(s => Assert.IsTrue(QualityIsDefinedInEnum(s)));
            rulerInitializationMethod.WeakPoints.ForEach(s => Assert.IsTrue(QualityIsDefinedInEnum(s)));
            sageInitializationMethod.WeakPoints.ForEach(s => Assert.IsTrue(QualityIsDefinedInEnum(s)));
            seekerInitializationMethod.WeakPoints.ForEach(s => Assert.IsTrue(QualityIsDefinedInEnum(s)));
            warriorInitializationMethod.WeakPoints.ForEach(s => Assert.IsTrue(QualityIsDefinedInEnum(s)));
        }

        private static bool QualityIsDefinedInEnum(string weakPoint)
        {
            if(System.Enum.IsDefined(typeof(CompiledWeakPoints), weakPoint))
                return true;

            Assert.Fail("The following weak point is missing: " + weakPoint);
            return false;
        }
    }
}