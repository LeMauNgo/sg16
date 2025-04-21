using UnityEngine;

public class BtnBackHome : ButttonAbstract
{
    public override void OnClick()
    {
        UIManager.Instance.ShowUi("HomeUi");
    }
}
