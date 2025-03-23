using Unity.VisualScripting;
using UnityEngine;

public class TetrominoManager : SaiBehaviour
{
    [SerializeField] protected TetrominoSpanwer spanwer;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadSpawner();
    }

    protected virtual void LoadSpawner()
    {
        if (this.spanwer != null) return;
        this.spanwer = GetComponent<TetrominoSpanwer>();
        Debug.LogWarning(transform.name + ": LoadSpawner", gameObject);
    }
}
