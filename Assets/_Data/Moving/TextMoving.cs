using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextMoving : UiMoveToUi
{
    [SerializeField] protected Text text;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadText();
    }
    protected virtual void LoadText()
    {
        if (this.text != null) return;
        this.text = GetComponent<Text>();
        Debug.LogWarning(gameObject.name + " LoadText", gameObject);
    }
    public virtual void SetText(string text)
    {
        this.text.text = "+ " + text;
    }
}
