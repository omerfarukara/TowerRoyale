
namespace GameFolders.Scripts.General.Enum
{
    public enum GameState
    {
        Idle,
        Play,
        Finish,
        Lose
    }
    
    public enum ValueType
    {
        Constant,
        Range
    }

    public enum BorderType
    {
        Rectangle,
        Circle,
        HallowCircle
    }

    public enum OrderType
    {
        Ordered,
        Random
    }

    public enum CharacterType
    {
        Knight,
        Archer,
        Dragon,
        Support
    }

    public enum Target
    {
        Ground,
        Air,
        AirAndGround
    }

    public enum OwnerType
    {
        Player,
        Enemy
    }
}
