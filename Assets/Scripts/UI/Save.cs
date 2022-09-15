 
public static class Save
{
    public static float Player1Health = 1f;
    public static float Player2Health = 1f;
    public static float Player1Timer = 1.5f;
    public static float Player2Timer = 1.5f;
    public static bool TimeOut = false;
    public static int Player1Wins = 0;
    public static int Player2Wins = 0;
    public static int TotalLevelWinnerID=0; //Ids are stored in scriptable object
    public static int chosenPlayer1ID;
    public static int chosenPlayer2ID;

    public static void ReducePlayer1Health(float amount)
    {
        //Player1Health = Mathf.Max(0, Player1Health -= amount);
        Player1Health -= amount;
    }
    public static void ReducePlayer2Health(float amount)
    {
      //  Player2Health = Mathf.Max(0, Player2Health -= amount);
        Player2Health -= amount;
    }
}
