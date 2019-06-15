using System.Collections.Generic;
using System.Xml;
using RNPC.Core.Learning.Substitutions;

namespace RNPC.Core.Learning.Interfaces
{
    public interface ISubstitutionDocumentConverter
    {
        List<Substition> ConvertDocumentToList(XmlDocument document);
    }
}