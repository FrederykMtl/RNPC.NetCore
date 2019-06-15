namespace RNPC.Core.Enums
{
    public enum PlaceType
    {
        Atoll,
        Borough,
        Cape,
        City,
        Cemetary,
        Continent,
        County,
        Country,
        Cosmopolis,
        Delta,
        Duchy,
        Earldom,
        Hamlet,
        Island,
        Kingdom,
        Megalopolis,
        Metropolis,
        Municipality,
        Park,
        Peninsula,
        Place,
        Polis,
        Province,
        Republic,
        State,
        Village
    }

    public enum AssociationType
    {
        Contractor,
        Employee,
        Founder,
        Leader,
        Member
    }

    public enum OccupationType
    {
        Bureaucrat,
        Contractor,
        Entrepreneur,
        Independant,
        Professional,
        Political,
        Trade
    }

    public enum OpinionType
    {
        Hate,
        Dislike,
        Contempt,
        Neutral,
        Respect,
        Like,
        Love,
        NoOpinion
    }

    public enum OrganizationType
    {
        Criminal,
        Government,
        Trade,
        Union
    }

    public enum PersonalRelationshipType
    {
        Business,
        Enmity,
        Family,
        Friendship,
        Hierarchical,
        Romantic
    }

    public enum PersonalRelationshipTypeName
    {
        Acquaintance,
        Associate,
        BestFriend,
        Boyfriend,
        Aunt,
        Boss,
        Brother,
        BrotherInLaw,
        BusinessPartner,
        Child,
        Daughter,
        DaughterInLaw,
        Employee,
        Enemy,
        Father,
        FatherInLaw,
        Fiancé,
        Fiancée,
        Friend,
        Frenemy,
        Girlfriend,
        Grandaunt,
        Grandchild,
        Granddaughter,
        Grandfather,
        Grandmother,
        Grandnephew,
        Grandniece,
        Grandparent,
        Grandson,
        Granduncle,
        Husband,
        LifePartner,
        Lover,
        Master,
        Mother,
        MotherInLaw,
        Nemesis,
        Nephew,
        Niece,
        Parent,
        Sister,
        SisterInLaw,
        Slave,
        Son,
        SonInLaw,
        Spouse,
        Stepbrother,
        Stepdaughter,
        Stepfather,
        Stepmother,
        Stepsister,
        Stepson,
        Uncle,
        Wife,
        WorstEnemy
    }

    public enum PastEventType
    {
        Battle,
        Conflict,
        Geographical,
        Life,
        Magical,
        Natural,
        Political,
        Religious,
        Social
    }

    public enum EventRelationshipType
    {
        Caused,
        CausedBy,
        Followed,
        Included,
        PartOf,
        Preceded
    }

    public enum GeographicRelationshipType
    {
        //TODO : Capital, Metropolis...
        EastOf,
        Includes,
        NorthOf,
        NorthEastOf,
        NorthWestOf,
        PartOf,
        SouthOf,
        SouthEastOf,
        SouthWestOf,
        WestOf
    }

    public enum PersonalInvolvementType
    {
        BornDuring,
        DiedDuring,
        Ended,
        FoughtIn,
        Led,
        ParticipatedIn,
        Started,
        WasCaughtIn
    }

    public enum OccupationalInvolvementType
    {
        BornDuring,
        DiedDuring,
        Ended,
        FoughtIn,
        Led,
        ParticipatedIn,
        Started,
        WasCaughtIn
    }

    public enum PersonalTieType
    {
        BornIn,
        Conquered,
        Destroyed,
        DiedIn,
        Founded,
        FoughtAgainst,
        FoughtFor,
        Inaugurated,
        LiveIn,
        Led,
        OriginatedFrom,
        Plundered
    }

    public enum OccupationalTieType
    {
        BornIn,
        Conquered,
        Destroyed,
        DiedIn,
        Founded,
        FoughtAgainst,
        FoughtFor,
        Inaugurated,
        LiveIn,
        Led,
        OriginatedFrom,
        Plundered
    }

    public enum OccurenceType
    {
        HappenedIn,
        Creation,
        Conquest,
        Destruction,
        EndedIn,
        Liberation,
        StartedIn,
        TriggeredBy
    }
}