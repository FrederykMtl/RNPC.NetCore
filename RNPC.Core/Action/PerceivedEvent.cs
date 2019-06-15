using System;
using RNPC.Core.Enums;

namespace RNPC.Core.Action
{
    /// <summary>
    /// An event perceived by the character, coming from the environment, people around the character 
    /// or from the character's body (wounds, sensations, etc.)
    /// UML diagram for this event and its inheritors:
    /// http://www.plantuml.com/plantuml/uml/RP7FQiCm3CRlUWhVGr_0xD31SXXiK9g-WEUAJE3AnLO2eTkxJywVXAuzMVhqwIVBtcTnIRfB01zSpkQ0oIFrUAvxPBc1gPhOgMHDbaJSgd0QhcYi6GrZ3BymTxzweSWtg4ukjIXhK_NEWgpvT4BrYJcxTgEkX2EwXT7ucDH1wjT-4d9Xd6d_NN9hFdmFcVniKy97zAx0x5fP52XRABmHkdI1Ig83iST4CgWyHnkwvfvIv8k25_x2U2nDkFfANXDCrcnZZuxBsc-n-mfOyFUxlFpiTlChmdGKPNsL9-wxdP_K7sy7U-Ip_lWV
    /// </summary>
    [Serializable]
    public class PerceivedEvent
    {
        public EventType EventType;
        //The target of the action. It can be null (no specific target).
        public string Target;
        public string Source;
        public string EventName;
    }
}