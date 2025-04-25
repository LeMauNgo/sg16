using UnityEngine;

public class CubeSpawnerManager : SaiSingleton<CubeSpawnerManager>
{
    [SerializeField] protected CubeSpawner spanwer;
    public CubeSpawner Spanwer => spanwer;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadSpawner();
    }

    protected virtual void LoadSpawner()
    {
        if (this.spanwer != null) return;
        this.spanwer = GetComponent<CubeSpawner>();
        Debug.LogWarning(transform.name + ": LoadSpawner", gameObject);
    }
}
