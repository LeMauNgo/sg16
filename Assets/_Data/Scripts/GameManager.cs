using UnityEngine;
using UnityEngine.UI;

public class GameManager : SaiSingleton<GameManager>
{
    [SerializeField] GameMode gameMode;
    [SerializeField] bool isPlaying;
    public bool IsPlaying => isPlaying;
    public virtual void StartGame()
    {
        isPlaying = true;
        UIManager.Instance.ShowUi("GameUi");
        TetrominoManager.Instance.Spanwer.SpawnTetromino(1, new Vector3Int(3,20,0));
        if (gameMode != GameMode.TwoPlayer) return;
        TetrominoManager.Instance.Spanwer.SpawnTetromino(2,new Vector3Int(13,20,0));
        Debug.Log("Game Start");
    }
    public virtual void GameOver()
    {
        isPlaying = false;
        GameUi gameUi = UIManager.Instance.GetUiCtrl("GameUi") as GameUi;
        gameUi.GameOver.SetActive(true);
        Debug.Log("Game Over");
    }
    public virtual void SetGameMode(GameMode mode)
    {
        gameMode = mode;
        Debug.Log("Game Mode: " + gameMode);
    }
}
