using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameResultData
{
    private static List<GameResult> gameResults = new List<GameResult>();

    public static List<GameResult> GameResults { get => gameResults; set => gameResults = value; }
}
