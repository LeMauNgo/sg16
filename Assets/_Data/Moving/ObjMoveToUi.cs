using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjMoveToUi : ObjMovement
{
    //[SerializeField] EffectCtrl ctrl;

    //[SerializeField] protected RectTransform uiTarget;
    //protected override void ResetValue()
    //{
    //    base.ResetValue();
    //    this.isMove = true;
    //    this.speed = 0.1f;
    //    targetPosition = GetWorldPositionFromUI(uiTarget);
    //}

    //protected override void LoadComponents()
    //{
    //    base.LoadComponents();
    //    this.LoadCtrl();
    //}
    //protected virtual void LoadCtrl()
    //{
    //    if (this.ctrl != null) return;
    //    this.ctrl = GetComponentInParent<EffectCtrl>();
    //    Debug.LogWarning(gameObject.name + " LoadCtrl", gameObject);
    //}
    //protected override void FixedUpdate()
    //{
    //    base.FixedUpdate();
    //    this.ArrivedTarget();
    //}
    //protected virtual void ArrivedTarget()
    //{
    //    if (!this.IsTarget()) return;
    //    ctrl.Despawn.DoDespawn();
    //}
    //protected virtual Vector3 GetWorldPositionFromUI(RectTransform uiElement)
    //{
    //    Vector3 screenPos = RectTransformUtility.WorldToScreenPoint(null, uiElement.position);
    //    Vector3 worldPos = Camera.main.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, 0f));
    //    worldPos.z = 0f; // Giữ nguyên Z = 0 để đảm bảo đúng 2D
    //    return worldPos;
    //}
}
