using System;
using RNPC.Core.Enums;
using RNPC.Core.TraitGeneration;
using Action = RNPC.Core.Action.Action;

namespace RNPC.API.Training
{
    public static class RandomEventGenerator
    {
        public static Action GetRandomEvent()
        {
            switch (GetRandomEventType())
            {
                case EventType.Biological:
                    return RandomBiologicalEvent();
                case EventType.Environmental:
                    return GetRandomEnvironmentalEvent();
                case EventType.Interaction:
                    return GetRandomSocialInteraction();
                case EventType.Temperature:
                    return GetRandomTemperatureEvent();
                case EventType.Time:
                    return GetRandomTimeEvent();
                case EventType.Weather:
                    return GetRandomWeatherEvent();
                case EventType.Unknown:
                    return new Action
                    {
                        EventType = EventType.Unknown,
                        ActionType = ActionType.NonVerbal,
                        Message = "Unknown event",
                        Tone = Tone.Confused
                    };
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private static EventType GetRandomEventType()
        {
            int randomValue = RandomValueGenerator.GeneratePercentileIntegerValue();

            if (randomValue <= TrainingStatistics.Biological)
                return EventType.Biological;

            if (randomValue <= TrainingStatistics.Environmental)
                return EventType.Environmental;

            if (randomValue <= TrainingStatistics.Interaction)
                return EventType.Interaction;

            if (randomValue <= TrainingStatistics.Time)
                return EventType.Time;

            if (randomValue <= TrainingStatistics.Temperature)
                return EventType.Temperature;

            return EventType.Weather;
        }

        private static Action RandomBiologicalEvent()
        {
            return new Action
            {
                EventType = EventType.Biological,
                EventName = "unknown"
            };
        }

        private static Action GetRandomEnvironmentalEvent()
        {
            return new Action
            {
                EventType = EventType.Environmental,
                EventName = "unknown"
            };
        }

        private static Action GetRandomTemperatureEvent()
        {
            return new Action
            {
                EventType = EventType.Temperature,
                EventName = "unknown"
            };
        }

        private static Action GetRandomWeatherEvent()
        {
            return new Action
            {
                EventType = EventType.Weather,
                EventName = "unknown"
            };
        }

        private static Action GetRandomTimeEvent()
        {
            return new Action
            {
                EventType = EventType.Time,
                EventName = "unknown"
            };
        }

        private static Action GetRandomSocialInteraction()
        {
            var actionType = GetRandomActionType();
            var intentType = GetRandomIntent();

            var eventEnumType = Type.GetType("RNPC.API.Training." + actionType + intentType);

            if(eventEnumType==null)
                return new Action
                {
                    Intent = intentType,
                    ActionType = actionType,
                    EventType = EventType.Interaction,
                    EventName = "",
                    Tone = Tone.Curious,
                    Source = GetRandomSource()
                };

            var eventName = GetRandomEventName(eventEnumType);

            var tone = Tone.Neutral;

            if (eventName == "Threat")
                tone = Tone.Threatening;

            return new Action
            {
                Tone = tone,
                Intent = intentType,
                ActionType = actionType,
                EventType = EventType.Interaction,
                EventName = eventName,
                Message = GetRandomMessage(eventName, actionType, intentType),
                Source = GetRandomSource()
            };
        }

        /// <summary>
        /// Provides a random event message
        /// </summary>
        /// <param name="eventName"></param>
        /// <param name="actionType"></param>
        /// <param name="intentType"></param>
        /// <returns></returns>
        private static string GetRandomMessage(string eventName, ActionType actionType, Intent intentType)
        {
            if (actionType == ActionType.NonVerbal || actionType == ActionType.Physical)
                return string.Empty;

            switch (intentType)
            {
                case Intent.Friendly:
                    return "My friend...";
                case Intent.Neutral:
                    return "Well...";
                case Intent.Hostile:
                    int index = RandomValueGenerator.GenerateIntWithMaxValue(5);

                    if (eventName == "Threat")
                    {
                        switch (index)
                        {
                            case 1:
                                return "I'm going to punch your face!";
                            case 2:
                                return "When I'm done with you, you'll be the same of your family!";
                            case 3:
                                return "I'm going to leave you!";
                            case 4:
                                return "I will ruin you!";
                            case 5:
                                return "God wil damn you!";
                        }
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(intentType), intentType, null);
            }

            return string.Empty;
        }

        private static string GetRandomEventName(Type eventEnumType)
        {
            int possibilities = Enum.GetNames(eventEnumType).Length;

            int index = RandomValueGenerator.GenerateIntWithMaxValue(possibilities);

            return eventEnumType.GetEnumValues().GetValue(index - 1).ToString();
        }

        private static string GetRandomSource()
        {
            switch (RandomValueGenerator.GenerateIntWithMaxValue(4))
            {
                case 1:
                    return "An_Enemy";
                case 2:
                    return "A_Stranger";
                case 3:
                    return "Tested_Friend";
                case 4:
                    return "Another_Stranger";
            }

            return "The_Unknown";
        }

        private static Intent GetRandomIntent()
        {
            int randomValue = RandomValueGenerator.GeneratePercentileIntegerValue();

            if (randomValue < TrainingStatistics.Friendly)
                return Intent.Friendly;

            if (randomValue < TrainingStatistics.Neutral)
                return Intent.Neutral;

            return Intent.Hostile;
        }

        private static ActionType GetRandomActionType()
        {
            int randomValue = RandomValueGenerator.GeneratePercentileIntegerValue();

            if (randomValue < TrainingStatistics.NonVerbal)
                return ActionType.NonVerbal;

            if (randomValue < TrainingStatistics.Physical)
                return ActionType.Physical;

            return ActionType.Verbal;

        }
    }
}