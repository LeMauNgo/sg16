using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjMovement : SaiBehaviour
{
    [Header("Obj Movement")]
    [SerializeField] protected Vector3 targetPosition;
    [SerializeField] protected float speed = 0.04f;
    [SerializeField] protected float distance;
    [SerializeField] protected float minDistance = 0f;
    public bool isMove;


    protected virtual void FixedUpdate()
    {
        //this.Moving();
        //this.DistanceTarget();
    }
    public virtual Vector3 GetTarget()
    {
        return this.targetPosition;
    }
    public virtual void SetSpeed(float speed)
    {
        this.speed = speed;
    }
    protected virtual bool CanMove()
    {
        return this.distance > this.minDistance;
    }
    protected virtual void Moving()
    {
        if (!this.CanMove() || !this.isMove) return;
        Vector3 newPos = Vector3.MoveTowards(transform.parent.position, targetPosition, this.speed);
        transform.parent.position = newPos;
    }
    protected virtual void DistanceTarget()
    {
        this.distance = Vector3.Distance(transform.position, this.targetPosition);
    }
    protected virtual bool IsTarget()
    {
        return this.distance == 0;
    }
}
