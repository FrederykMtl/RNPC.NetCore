using System;
using RNPC.Core.Learning.Interfaces;
using RNPC.Core.TraitGeneration;

namespace RNPC.Core.Learning.Emotions
{
    internal class MainEmotionLearningStrategy : ILearningStrategy
    {
        public bool AnalyzeAndLearn(Character learningCharacter)
        {
            foreach (var emotionalState in learningCharacter.MyTraits.GetEmotionalStateValues())
            {
                switch (emotionalState.Key)
                {
                    case "Anger":
                        learningCharacter.MyTraits.LongTermEmotions.Anger = CalculateEmotionalVariation(emotionalState.Value, learningCharacter.MyTraits.LongTermEmotions.Anger,
                                                                            //Memory makes you remember bad things and hold on to anger, Quietude helps you forget
                                                                            //Ignorance is bliss!
                                                                            learningCharacter.MyTraits.Memory, learningCharacter.MyTraits.Quietude);
                        break;
                    case "Fear":
                        learningCharacter.MyTraits.LongTermEmotions.Fear = CalculateEmotionalVariation(emotionalState.Value, learningCharacter.MyTraits.LongTermEmotions.Fear,
                                                                            //Awareness lets you know the world is full of danger, Confidence makes you believe you can face it.
                                                                            learningCharacter.MyTraits.Awareness, learningCharacter.MyTraits.Confidence);
                        break;
                    case "Happiness":
                        learningCharacter.MyTraits.LongTermEmotions.Happiness = CalculateEmotionalVariation(emotionalState.Value, learningCharacter.MyTraits.LongTermEmotions.Happiness,
                                                                        //A positive outlook helps you being happy, while overthinking things hinders it.
                                                                        learningCharacter.MyTraits.Outlook, learningCharacter.MyTraits.CriticalSense);
                        break;
                    case "Sadness":
                        learningCharacter.MyTraits.LongTermEmotions.Sadness = CalculateEmotionalVariation(emotionalState.Value, learningCharacter.MyTraits.LongTermEmotions.Sadness,
                                                                            //A low outlook will prevent you from believing things will get better, high imagination wil help you see it.
                                                                            learningCharacter.MyTraits.Outlook, learningCharacter.MyTraits.Imagination);
                        break;
                    case "Disgust":
                        learningCharacter.MyTraits.LongTermEmotions.Disgust = CalculateEmotionalVariation(emotionalState.Value, learningCharacter.MyTraits.LongTermEmotions.Disgust,
                                                                            //Disliking yourself will foster disgust for everything, Compassion will help you forgive other's flaws
                                                                            learningCharacter.MyTraits.SelfEsteem, learningCharacter.MyTraits.Compassion);
                        break;
                    case "Curiosity":
                        learningCharacter.MyTraits.LongTermEmotions.Curiosity = CalculateEmotionalVariation(emotionalState.Value, learningCharacter.MyTraits.LongTermEmotions.Curiosity,
                                                                                //Being quick-witted makes you want to explore the world and learn more, being self absorbed prevents it
                                                                                learningCharacter.MyTraits.Acuity, learningCharacter.MyTraits.SelfEsteem);
                        break;
                    case "Surprise":
                        //Can you be surprised long-term?
                        break;
                    case "Jealousy":
                        learningCharacter.MyTraits.LongTermEmotions.Jealousy = CalculateEmotionalVariation(emotionalState.Value, learningCharacter.MyTraits.LongTermEmotions.Jealousy,
                                                                                //Being aware of what others have can foster jealousy, being humble prevents it.
                                                                                    learningCharacter.MyTraits.Awareness, learningCharacter.MyTraits.Modesty);
                        break;
                    case "Shame":
                        learningCharacter.MyTraits.LongTermEmotions.Shame = CalculateEmotionalVariation(emotionalState.Value, learningCharacter.MyTraits.LongTermEmotions.Shame,
                                                                            //Memory makes you remember bad things, Modesty helps with forgiveness
                                                                            learningCharacter.MyTraits.Memory, learningCharacter.MyTraits.Modesty);
                        break;
                    case "Disappointment":
                        learningCharacter.MyTraits.LongTermEmotions.Disappointment = CalculateEmotionalVariation(emotionalState.Value, learningCharacter.MyTraits.LongTermEmotions.Disappointment,
                                                                            //Having high expectation can ead to more disappointment, having compassion will help to let it go
                                                                            learningCharacter.MyTraits.Ambition, learningCharacter.MyTraits.Compassion);
                        break;
                    case "Pride":
                        learningCharacter.MyTraits.LongTermEmotions.Pride = CalculateEmotionalVariation(emotionalState.Value, learningCharacter.MyTraits.LongTermEmotions.Pride,
                                                                                    //Loving yourself too much leads to being proud, Modesty helps to fight it
                                                                                    learningCharacter.MyTraits.SelfEsteem, learningCharacter.MyTraits.Modesty);
                        break;
                }
            }
            return true;
        }

        private static int CalculateEmotionalVariation(int emotionalStateValue, int longTermEmotion, int firstQualityTrait, int secondQualityTraint)
        {
            //calculate the differential
            int emotionalDelta = emotionalStateValue - longTermEmotion;

            if (emotionalDelta == 0)
                return longTermEmotion;

            int newEmotionalValue = longTermEmotion;

            int retentionPercentage = (firstQualityTrait + (100 - secondQualityTraint)) / 2;

            for (int i = 0; i < Math.Abs(emotionalDelta); i++)
            {
                if (emotionalDelta > 0)
                {
                    if (RandomValueGenerator.GeneratePercentileIntegerValue() < retentionPercentage)
                        newEmotionalValue++;
                }
                else
                {
                    if (RandomValueGenerator.GeneratePercentileIntegerValue() >= retentionPercentage)
                        newEmotionalValue--;
                }
            }

            return newEmotionalValue;
        }
    }
}
