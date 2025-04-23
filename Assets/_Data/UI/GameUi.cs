using UnityEngine;

public class GameUi : UICtrl
{
    [SerializeField] protected GameObject gameOver;
    public GameObject GameOver => gameOver;
    private void OnDisable()
    {
        GridManager.Instance.ClearGrid();
        TetrominoManager.Instance.Spanwer.DespawnAll();
        ScoreManager.Instance.ResetScore();
        gameOver.SetActive(false);
    }
}
