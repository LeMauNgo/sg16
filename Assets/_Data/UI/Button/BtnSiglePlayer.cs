using UnityEngine;

public class BtnSiglePlayer : ButttonAbstract
{
    public override void OnClick()
    {
        GameManager.Instance.SetGameMode(GameMode.SinglePlayer);
        GameManager.Instance.StartGame();
        LevelManager.Instance.LoadLevel(0);
    }
}
