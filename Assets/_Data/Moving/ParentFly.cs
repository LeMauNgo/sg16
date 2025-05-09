using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ParentFly : SaiBehaviour
{
    [SerializeField] protected float moveSpeed = 1f;
    [SerializeField] protected Vector3 direction = Vector3.right;

    void Update()
    {
        transform.parent.Translate(this.direction * this.moveSpeed * Time.deltaTime);
    }
    public virtual void SetSpeed(float speed)
    {
        this.moveSpeed = speed;
    }
}
