
using UnityEngine;

public static class Save
{
    public static float Player1Health = 1f;
    public static float Player2Health = 1f;

    public static void ReducePlayer1Health(float amount)
    {
        Player1Health = Mathf.Max(0, Player1Health -= amount);
    }
    public static void ReducePlayer2Health(float amount)
    {
        Player2Health = Mathf.Max(0, Player2Health -= amount);
    }
}
