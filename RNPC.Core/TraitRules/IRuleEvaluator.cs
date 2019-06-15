namespace RNPC.Core.TraitRules
{
    interface IRuleEvaluator
    {
        void EvaluateAndApplyAllRules(CharacterTraits traits);
    }
}
