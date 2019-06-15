using RNPC.Core.Enums;

// ReSharper disable once CheckNamespace
namespace RNPC.Core.Learning.Interfaces
{
    public interface IPersonalValueAssociations
    {
        PersonalValues? GetAssociatedValue(string value);
        PersonalValues? GetAssociatedValue(PersonalValues value);
    }
}