using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]
public abstract class ItemDropCtrl : PoolObj
{
    [SerializeField] protected BoxCollider boxCollider;
    public BoxCollider BoxCollider => boxCollider;
    [SerializeField] protected Rigidbody _rigidbody;
    public Rigidbody Rigidbody => _rigidbody;
    [SerializeField] protected int dropCount;
    public int DropCount => dropCount;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadBoxCollider();
        this.LoadRigidbody();
    }
    public abstract ItemCode GetItemCode();
    public override string GetName()
    {
        return this.GetItemCode().ToString();
    }
    protected virtual void LoadBoxCollider()
    {
        if (this.boxCollider != null) return;
        this.boxCollider = GetComponent<BoxCollider>();
        this.boxCollider.size = new Vector3(0.5f, 0.5f, 0.5f);
        Debug.LogWarning(gameObject.name + "LoadBoxCollider", gameObject);
    }
    protected virtual void LoadRigidbody()
    {
        if (this._rigidbody != null) return;
        this._rigidbody = GetComponent<Rigidbody>();
        Debug.LogWarning(gameObject.name + "LoadRigidbody", gameObject);
    }
    public virtual void SetDropCount(int dropCount)
    {
        this.dropCount = dropCount;
    }
}
