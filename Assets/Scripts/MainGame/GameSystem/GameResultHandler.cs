using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class GameResultHandler : GameSystemBase
{
    private int highestGameTime = 0;
    public GameResultHandler(SurvivorLikeGame2DFacade survivorLikeGame) : base(survivorLikeGame)
    {
        Initialize();
    }

    public override void Initialize()
    {
        
    }

    /// <summary>
    /// �i��C������
    /// </summary>
    public void GameSettlement(GameResult result)
    {
        GameEnd(result);
    }

    /// <summary>
    /// �C������
    /// </summary>
    private async void GameEnd(GameResult gameResult)
    {
        await SaveResultData(gameResult);
    }

    /// <summary>
    /// �x�s�C�����
    /// </summary>
    /// <param name="gameResult"></param>
    private async Task SaveResultData(GameResult gameResult)
    {
        var jsonGameResult = JsonUtility.ToJson(gameResult);
        await FirebaseManager.Instance.SavePlayerGameResultData(jsonGameResult);

        LoadPlayerHighestResult(gameResult);
    }

    /// <summary>
    /// �Ǧ^���a�ͦs�ɶ��̪������G
    /// </summary>
    public async void LoadPlayerHighestResult(GameResult gameResult)
    {
        await FirebaseManager.Instance.LoadPlayerLog();
        int gameTime = GameResultData.GameResults[0].GameTime;
        for (int i = 0; i < GameResultData.GameResults.Count; i++)
        {
            if (GameResultData.GameResults[i].GameTime > gameTime)
            {
                gameTime = GameResultData.GameResults[i].GameTime;
            }
        }
        highestGameTime = gameTime;

        survivorLikeGame.SetGameResultUI(gameResult,highestGameTime);
        survivorLikeGame.ShowGameResultUI();
    }
}

public class GameResult
{
    public int Score;
    public int GameTime;
    public string RecordTime;

    public GameResult(int score, int gameTime, string recordTime)
    {
        this.Score = score;
        this.GameTime = gameTime;
        this.RecordTime = recordTime;
    }
    //Own Skills�٨S��
}
