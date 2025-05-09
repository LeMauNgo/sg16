using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Accessibility;

public class UiMoveToUi : SaiBehaviour
{
    [SerializeField] protected RectTransform target;
    [SerializeField] protected RectTransform objMove;
    [SerializeField] protected float time = 3f;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadRectTransform();
    }
    protected virtual void LoadRectTransform()
    {
        if (objMove != null) return;
        this.objMove = GetComponent<RectTransform>();
        Debug.LogWarning(gameObject.name + " LoadRectTransform", gameObject);
    }
    //void OnEnable()
    //{
    //    this.Moving();
    //}
    //protected virtual void Moving()
    //{
    //    this.objMove.DOMove(target.position, time).OnComplete(() =>
    //    {
    //        objMove.gameObject.SetActive(false);
    //    });
    //}
    public virtual void SetPosition(Vector3 position)
    {
        this.objMove.anchoredPosition = position;
    }
}
