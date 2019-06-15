using RNPC.Core.Enums;
using RNPC.Core.Resources;

namespace RNPC.Core.TraitGeneration
{
    public static class RandomAttributeGenerator
    {
        /// <summary>
        /// Returns a random gender
        /// </summary>
        /// <returns>random gender</returns>
        public static Gender GetRandomGender()
        {
            int malePercentage = int.Parse(Demographics.Gender_male);
            int femalePercentage = int.Parse(Demographics.Gender_female) + malePercentage;
            int intersexPercentage = int.Parse(Demographics.Gender_intersex) + femalePercentage;
            int genderfluidPercentage = int.Parse(Demographics.Gender_genderfluid) + intersexPercentage;

            int value = RandomValueGenerator.GeneratePercentileIntegerValue();

            if (value > 0 && value <= malePercentage)
                return Gender.Male;

            if (malePercentage < value && value <= femalePercentage)
                return Gender.Female;

            if (femalePercentage < value && value <= intersexPercentage)
                return Gender.Intersex;

            if (intersexPercentage < value && value <= genderfluidPercentage)
                return Gender.Genderfluid;

            return Gender.Agender;
        }

        /// <summary>
        /// Returns a random orientation
        /// </summary>
        /// <returns>random orientation</returns>
        public static Orientation GetRandomOrientation()
        {
            int straightPercentage = int.Parse(Demographics.Orientation_straight);
            int gayPercentage = int.Parse(Demographics.Orientation_gay) + straightPercentage;
            int biPercentage = int.Parse(Demographics.Orientation_bi) + gayPercentage;
            int asexualPercentage = int.Parse(Demographics.Orientation_asexual) + biPercentage;
            int panPercentage = int.Parse(Demographics.Orientation_pansexual) +  asexualPercentage;

            int value = RandomValueGenerator.GeneratePercentileIntegerValue();

            if (value > 0 && value <= straightPercentage)
                return Orientation.Straight;

            if (straightPercentage < value && value <= gayPercentage)
                return Orientation.Gay;

            if (gayPercentage < value && value <= biPercentage)
                return Orientation.Bisexual;

            if (biPercentage < value && value <= asexualPercentage)
                return Orientation.Asexual;

            if (asexualPercentage < value && value <= panPercentage)
                return Orientation.Pansexual;

            return Orientation.Undefined;
        }

        /// <summary>
        /// Returns a random sex
        /// </summary>
        /// <returns>random sex</returns>
        public static Sex GetRandomSex()
        {
            int malePercentage = int.Parse(Demographics.Sex_male);
            int femalePercentage = int.Parse(Demographics.Sex_female) + malePercentage;
            int intersexPercentage = int.Parse(Demographics.Sex_intersex) + femalePercentage;

            int value = RandomValueGenerator.GeneratePercentileIntegerValue();

            if (value > 0 && value <= malePercentage)
                return Sex.Male;

            if (malePercentage < value && value <= femalePercentage)
                return Sex.Female;

            if (femalePercentage < value && value <= intersexPercentage)
                return Sex.Intersex;

            return Sex.Undefined;
        }
    }
}