using UnityEngine;

public class BtnTwoPlayer : ButttonAbstract
{
    public override void OnClick()
    {
        GameManager.Instance.SetGameMode(GameMode.TwoPlayer);
        GameManager.Instance.StartGame();
    }
}
