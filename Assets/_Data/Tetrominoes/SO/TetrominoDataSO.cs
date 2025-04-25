using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "TetrominoData", menuName = "Tetris/Tetromino Data")]
public class TetrominoDataSO : ScriptableObject
{
    public TetrominoCode tetrominoCode;

    public List<TetrominoRotationData> rotationOffsets;
}
