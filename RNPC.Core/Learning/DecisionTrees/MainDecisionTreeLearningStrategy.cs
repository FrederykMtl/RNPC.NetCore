using System.Linq;
using RNPC.Core.Action;
using RNPC.Core.Interfaces;
using RNPC.Core.Learning.Interfaces;
using RNPC.Core.Learning.Resources;
using RNPC.Core.TraitGeneration;

namespace RNPC.Core.Learning.DecisionTrees
{
    //TODO: Eventually change to a reward function
    internal class MainDecisionTreeLearningStrategy : ILearningStrategy
    {
        private readonly ISubstitutionController _controller;

        //used as a control to dtermine if any actual changes were done without checking the file.
        public bool DecisionTreeEvolved;
        private readonly ITreeBuilder _builder;

        public MainDecisionTreeLearningStrategy(ISubstitutionController controller, ITreeBuilder builder)
        {
            _controller = controller;
            _builder = builder;
        }

        /// <inheritdoc />
        public bool AnalyzeAndLearn(Character learningCharacter)
        {
            var actionsToAnalyze = learningCharacter.MyMemory.GetMyPastActions();

            //If there are not enough nodes for change we stop here.
            if (actionsToAnalyze.Count < LearningParameters.DecisionLearningThreshold)
                return true;

            var groupedActions = actionsToAnalyze.GroupBy(q => q.EventName).ToList();

            foreach (var action in groupedActions)
            {
                if(action.Count() < LearningParameters.DecisionLearningThreshold)
                    continue;

                //Reasoning is that overusage of a response will lead to pruning down your options
                if (action.Count() > LearningParameters.DecisionDevolutionThreshold)
                {
                    //if you lose the lottery you go down!
                    if (RandomValueGenerator.GeneratePercentileIntegerValue() <= LearningParameters.DecisionDevolveRate)
                        if (!DevolveDecisionTree(learningCharacter, action.ToList()[0]))
                            return false;       //if there has been any issue we exit
                }
                //While meeeting the minimum thrreshold will lead to  a normal evolution
                else if (action.Count() >= LearningParameters.DecisionLearningThreshold)
                {
                    //if you win the lottery you go up!
                    if (RandomValueGenerator.GeneratePercentileIntegerValue() >= (100 - LearningParameters.DecisionLearningRate))
                        if (!EvolveDecisionTree(learningCharacter, action.ToList()[0]))
                            return false; //if there has been any issue we exit
                }
            }

            return true;
        }

        /// <summary>
        /// When coniditons are met we evolve the character's decision tree
        /// </summary>
        /// <param name="learningCharacter">Character evolving</param>
        /// <param name="actionToEvolve">Action that led to an evolution</param>
        /// <returns>false = a problem occured</returns>
        private bool EvolveDecisionTree(Character learningCharacter, Action.Action actionToEvolve)
        {
            //If an exception occurs it's because an Action was set in the list
            //This should never happen. The conception of the framework is such
            //that it should ALWAYS be a Reaction. 
            string treeName = ((Reaction) actionToEvolve).InitialEvent.EventName;

            DecisionTreeEvolved =_controller.SubstituteNode(_builder, learningCharacter.MyName, actionToEvolve, treeName);

            return DecisionTreeEvolved;
        }

        /// <summary>
        /// When coniditons are met we prune the character's decision tree
        /// </summary>
        /// <param name="learningCharacter">Character not caring anymore</param>
        /// <param name="overusedAction">Action that led to a devolution</param>
        /// <returns>false = a problem occured</returns>
        private bool DevolveDecisionTree(Character learningCharacter, Action.Action overusedAction)
        {
            //If an exception occurs it's because an Action was set in the list
            //This should never happen. The conception of the framework is such
            //that it should ALWAYS be a Reaction. 
            string treeName = ((Reaction)overusedAction).InitialEvent.EventName;

            DecisionTreeEvolved = _controller.PruneDecisionTree(_builder, learningCharacter.MyName, overusedAction, treeName);

            return DecisionTreeEvolved;
        }
    }
}
