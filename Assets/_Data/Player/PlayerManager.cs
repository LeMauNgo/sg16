using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : SaiSingleton<PlayerManager>
{
    [SerializeField] protected List<TetrominoPlayer> players;
    public List<TetrominoPlayer> Players => players;
    public virtual void CreateTetromino(Vector3Int pos)
    {
        foreach (TetrominoPlayer player in this.players)
        {
            player.CreateTetromino(pos);
        }
    }
}
