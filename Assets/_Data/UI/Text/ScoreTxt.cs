using UnityEngine;

public class ScoreTxt : TextAbstact
{
    private void Update()
    {
        this.textPro.text = ScoreManager.Instance.Score.ToString();
    }
}
