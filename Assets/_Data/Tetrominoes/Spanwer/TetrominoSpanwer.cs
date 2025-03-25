using UnityEngine;

public class TetrominoSpanwer : Spawner<TetrominoCtrl>
{
    private void OnEnable()
    {
        this.SpawnTetromino();
    }
    public virtual void SpawnTetromino()
    {
        int TetrominoIndex = Random.Range(0, this.PoolPrefabs.Prefabs.Count);
        TetrominoCtrl tetrominoPrefabs = this.PoolPrefabs.Prefabs[TetrominoIndex];
        TetrominoCtrl tetromino = this.Spawn(tetrominoPrefabs);
        tetromino.gameObject.SetActive(true);
    }
}
