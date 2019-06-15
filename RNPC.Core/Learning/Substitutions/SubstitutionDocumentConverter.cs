using System.Collections.Generic;
using System.Xml;
using RNPC.Core.Learning.Interfaces;

namespace RNPC.Core.Learning.Substitutions
{
    public class SubstitutionDocumentConverter : ISubstitutionDocumentConverter
    {
        public List<Substition> ConvertDocumentToList(XmlDocument document)
        {
            var substitions = new List<Substition>();

            if (document?.DocumentElement?.ChildNodes == null)
                return substitions;

            foreach (XmlNode node in document.DocumentElement.ChildNodes)
            {
                if(node==null)
                    continue;

                // ReSharper disable once PossibleNullReferenceException
                string leaf = node.Attributes["leaf"].Value;
                string subtree = node.Attributes["subtree"].Value;
                string conditionName = node.Attributes["conditionname"].Value;

                SubstitionCondition condition;

                switch (node.Attributes["condition"].Value.ToLower())
                {
                    case "parentnot":
                        condition = SubstitionCondition.ParentNot;
                        break;
                    default:
                        condition = SubstitionCondition.Default;
                        break;
                }

                var newSubstitutionRule = new Substition
                {
                    LeafName = leaf,
                    SubTreeName = subtree,
                    Condition = condition,
                    ConditionName = conditionName
                };

                substitions.Add(newSubstitutionRule);
            }

            return substitions;
        }
    }
}
