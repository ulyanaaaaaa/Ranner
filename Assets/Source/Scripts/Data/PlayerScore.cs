[System.Serializable]
public class PlayerScore
{
    public string playerName;
    public int coins;

    public PlayerScore(string playerName, int coins)
    {
        this.playerName = playerName;
        this.coins = coins;
    }
}
