using UnityEngine;

public class EffectSpawnerManager : SaiSingleton<EffectSpawnerManager>
{
    [SerializeField] protected EffectSpawner spanwer;
    public EffectSpawner Spanwer => spanwer;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadSpawner();
    }

    protected virtual void LoadSpawner()
    {
        if (this.spanwer != null) return;
        this.spanwer = GetComponent<EffectSpawner>();
        Debug.LogWarning(transform.name + ": LoadSpawner", gameObject);
    }
}
