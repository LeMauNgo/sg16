using UnityEngine;

public class TetrisSpawner : MonoBehaviour
{
    public GameObject[] tetrominoPrefabs; // Mảng chứa các khối Tetris

    private void Start()
    {
        SpawnNewBlock();
    }

    public void SpawnNewBlock()
    {
        int index = Random.Range(0, tetrominoPrefabs.Length);
        GameObject tetromino = Instantiate(tetrominoPrefabs[index], transform.position, Quaternion.identity);
        tetromino.gameObject.SetActive(true);
    }
}
