using System.Collections.Generic;
using System.Linq;
using RNPC.Core.Learning.Interfaces;

namespace RNPC.Core.Learning.Substitutions
{
    public class SubstitutionMapper : ISubstitutionMapper
    {
        private readonly List<Substition> _substitutionsList;

        public SubstitutionMapper()
        {
            _substitutionsList = new List<Substition>();
        }

        public Substition GetSubstitutableSubTreeForLeaf(string leafName)
        {
            return _substitutionsList.FirstOrDefault(s => s.LeafName == leafName);
        }

        public Substition GetSubstitutableLeafForSubTree(string subTreeName)
        {
            return _substitutionsList.FirstOrDefault(s => s.SubTreeName == subTreeName);
        }

        public void AddSubstitution(Substition substitionToAdd)
        {
            _substitutionsList.Add(substitionToAdd);
        }

        public void AddSubstitution(List<Substition> substitionsToAdd)
        {
            _substitutionsList.AddRange(substitionsToAdd);
        }

        public void RemoveSubstitution(Substition substitionToRemove)
        {
            _substitutionsList.Remove(substitionToRemove);
        }

        public void ClearSubstitutionList()
        {
            _substitutionsList.Clear();
        }
    }

    public enum SubstitionCondition
    {
        Default,
        ParentNot
    }

    public class Substition
    {
        public string LeafName;
        public string SubTreeName;
        public SubstitionCondition Condition;
        public string ConditionName;
    }
}