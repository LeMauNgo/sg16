using Unity.VisualScripting;
using UnityEngine;

public class TetrominoManager : SaiBehaviour
{
    [SerializeField] protected TetrominoSpanwer spanwer;
    [SerializeField] protected TetrominoCtrl currentTetromino;

    [SerializeField] protected float spawnTimer = 0f;
    [SerializeField] protected float spawnInterval = 2f;

    protected virtual void FixedUpdate()
    {
        this.SpawnTetromino();
    }

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

    protected virtual void SpawnTetromino()
    {
        if(this.currentTetromino != null && this.currentTetromino.Mover.IsLanded()) this.currentTetromino = null;

        this.spawnTimer += Time.fixedDeltaTime;
        if (this.spawnTimer < this.spawnInterval) return;
        if (this.currentTetromino != null) return;
        this.spawnTimer = 0f;
        

        TetrominoCtrl prefab = this.spanwer.PoolPrefabs.GetRandom();
        this.currentTetromino = this.spanwer.Spawn(prefab, transform.position);
        this.currentTetromino.SetActive(true);
    }
}
