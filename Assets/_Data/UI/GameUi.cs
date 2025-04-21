using UnityEngine;

public class GameUi : UICtrl
{
    [SerializeField] protected GameObject map;
    [SerializeField] protected GameObject gameOver;
    public GameObject GameOver => gameOver;
    private void OnEnable()
    {
        this.map.SetActive(true);
    }
    private void OnDisable()
    {
        this.map.SetActive(false);
        GridManager.Instance.ClearGrid();
        TetrominoManager.Instance.Spanwer.DespawnAll();
        ScoreManager.Instance.ResetScore();
        gameOver.SetActive(false);
    }
}
