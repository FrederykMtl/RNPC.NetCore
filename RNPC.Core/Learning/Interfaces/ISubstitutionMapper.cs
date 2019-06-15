using System.Collections.Generic;
using RNPC.Core.Learning.Substitutions;

namespace RNPC.Core.Learning.Interfaces
{
    public interface ISubstitutionMapper
    {
        Substition GetSubstitutableSubTreeForLeaf(string leafName);
        Substition GetSubstitutableLeafForSubTree(string subTreeName);
        void AddSubstitution(Substition substitionToAdd);
        void AddSubstitution(List<Substition> substitionsToAdd);
        void RemoveSubstitution(Substition substitionToRemove);
        void ClearSubstitutionList();
    }
}
