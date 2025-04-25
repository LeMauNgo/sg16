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
        PlayerManager.Instance.CreateTetromino(new Vector3Int(GridManager.Instance.With / 2 - 2, GridManager.Instance.Height - 2, 0));
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
