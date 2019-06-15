using System;
using RNPC.Core.Enums;

namespace RNPC.Core.Memory
{
    /// <summary>
    /// This class stores the information on a node test for learning analysis
    /// </summary>
    [Serializable]
    public class NodeTestInfo
    {
        public int AttributeValue;
        public int PassingValue;
        public bool Result;
        public double ProfileScore;
        public CharacteristicType TestedCharacteristic;
        public string CharacteristicName;
        public int Modifier;
        public string Description;
    }
}