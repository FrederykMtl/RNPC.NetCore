using System;
using RNPC.Core.Enums;

namespace RNPC.Core.Action
{
    /// <inheritdoc />
    /// <summary>
    /// An action is something done by a character
    /// It is a perceived event
    /// </summary>
    [Serializable]
    public class Action : PerceivedEvent
    {
        //The intent of the action: Friendly, neutral or hostile.
        public Intent Intent;
        //A message, if there are any. If the character is saying something this is where it should be stored.
        //To be used as additional information when there is no speech.
        public string Message;
        //The type of social interaction
        public ActionType ActionType;
        //The tone of the message, if there is one
        public Tone Tone;
        //The karma associated with this action.
        //Bad actions have a negative score, good action a positive one.
        //0 is neutral. **This only concerns NPC character.
        public int AssociatedKarma = 0;
    }
}