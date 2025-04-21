using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class UIManager : SaiSingleton<UIManager>
{
    [SerializeField] protected List<UICtrl> uICtrls;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadUiCtrls();
    }
    protected virtual void LoadUiCtrls()
    {
        if (this.uICtrls.Count > 0) return;
        foreach(Transform child in transform)
        {
            UICtrl uICtrl = child.GetComponent<UICtrl>();
            if (uICtrl == null) continue;
            this.uICtrls.Add(uICtrl);
        }
        Debug.LogWarning(gameObject.name + " LoadUiCtrls", gameObject);
    }
    public virtual void ShowUi(string name)
    {
        foreach (UICtrl uICtrl in uICtrls)
        {
            if (uICtrl.gameObject.name == name)
            {
                uICtrl.gameObject.SetActive(true);
            }
            else
            {
                uICtrl.gameObject.SetActive(false);
            }
        }
    }
    public virtual UICtrl GetUiCtrl(string name)
    {
        foreach (UICtrl uICtrl in this.uICtrls)
        {
            if (name != uICtrl.gameObject.name) continue;
            return uICtrl;
        }
        return null;
    }
}
