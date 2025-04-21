using UnityEngine;

public class UICtrl : SaiBehaviour
{
    [SerializeField] protected UIManager uiManager;
    public UIManager UIManager => uiManager;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadUiManager();
    }
    protected virtual void LoadUiManager()
    {
        if (this.uiManager != null) return;
        this.uiManager = GetComponentInParent<UIManager>();
        Debug.LogWarning(gameObject.name + " LoadUiManager", gameObject);
    }
}
