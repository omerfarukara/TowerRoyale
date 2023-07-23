
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
        Archer
    }

    public enum Target
    {
        Ground,
        Air,
        AirAndGround
    }

    public enum Gamer
    {
        Player,
        AI
    }
}
