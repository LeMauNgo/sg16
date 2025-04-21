using UnityEngine;

public class ScoreManager : SaiSingleton<ScoreManager>
{
    [SerializeField] protected int score = 0;
    public int Score => score;
    public virtual void AddScore(int amountRow)
    {
        int scoreToAdd = 0;
        switch (amountRow)
        {
            case 1:
                scoreToAdd = 100;
                break;
            case 2:
                scoreToAdd = 300;
                break;
            case 3:
                scoreToAdd = 500;
                break;
            case 4:
                scoreToAdd = 800;
                break;
            default:
                scoreToAdd = 0;
                break;
        }
        this.score += scoreToAdd;
    }
    public virtual void ResetScore()
    {
        this.score = 0;
    }

}
