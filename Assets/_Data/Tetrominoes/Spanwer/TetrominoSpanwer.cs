using UnityEngine;
using UnityEngine.Analytics;

public class TetrominoSpanwer : Spawner<TetrominoCtrl>
{
    public virtual TetrominoCtrl SpawnTetromino(int PlayerId, Vector3Int pos)
    {
        if (!GameManager.Instance.IsPlaying) return null;
        int TetrominoIndex = Random.Range(0, this.PoolPrefabs.Prefabs.Count);
        TetrominoCtrl tetrominoPrefabs = this.PoolPrefabs.Prefabs[TetrominoIndex];
        TetrominoCtrl tetromino = this.Spawn(tetrominoPrefabs);
        tetromino.SetPlayerID(PlayerId);
        tetromino.SetPosition(pos);
        tetromino.gameObject.SetActive(true);
        this.CheckLose(tetromino, tetromino.Position);
        return tetromino;
    }
    protected virtual void CheckLose(TetrominoCtrl tetromino, Vector3Int spawnPosition)
    {
        Debug.Log(GridManager.Instance.IsValidPosition(tetromino, spawnPosition));
        if(!GridManager.Instance.IsValidPosition(tetromino, spawnPosition))
        {
            GameManager.Instance.GameOver();
        }
    }
}
