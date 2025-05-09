using System.Collections.Generic;
using UnityEngine;

public class TetrominoPlayer : SaiBehaviour
{
    [SerializeField] protected int playerID;
    public int PlayerID => playerID;
    [SerializeField] protected TetrominoMover mover;
    public TetrominoMover Mover => mover;
    [SerializeField] protected TertrominoesVisual visual;
    public TertrominoesVisual TertrominoesVisual => visual;
    [SerializeField] protected TertrominoesRotation rotation;
    public TertrominoesRotation Rotation => rotation;
    [SerializeField] protected List<TetrominoDataSO> tetrominoDataSO;
    public List<TetrominoDataSO> TetrominoDataSO => tetrominoDataSO;
    [SerializeField] public List<TetrominoRotationData> rotationOffsets;
    public List<TetrominoRotationData> RotationOffsets => rotationOffsets;
    [SerializeField] protected int rotationState;
    public int RotationState => rotationState;
    [SerializeField] protected Vector3Int[] cells = new Vector3Int[4];
    public Vector3Int[] Cells => cells;
    [SerializeField] protected Vector3Int position;
    public Vector3Int Position => position;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadMover();
        //this.SetOffset();
        this.LoadTertrominoesVisual();
        this.LoadTertrominoesRotation();
        this.LoadTetrominoDataSO();
    }
    private void OnEnable()
    {
        this.rotationState = 0;
        //this.SetCells();
    }
    protected virtual void LoadTetrominoDataSO()
    {
        if (this.tetrominoDataSO.Count > 0) return;
        this.tetrominoDataSO = new List<TetrominoDataSO>();
        this.tetrominoDataSO.AddRange(Resources.LoadAll<TetrominoDataSO>("TetrominoOffset"));
        Debug.LogWarning(gameObject.name + " LoadTetrominoDataSO", gameObject);
    }
    protected virtual void LoadMover()
    {
        if (this.mover != null) return;
        this.mover = GetComponentInChildren<TetrominoMover>();
        Debug.LogWarning(transform.name + ": LoadMover", gameObject);
    }
    protected virtual void LoadTertrominoesVisual()
    {
        if (this.visual != null) return;
        this.visual = GetComponentInChildren<TertrominoesVisual>();
        Debug.LogWarning(gameObject.name + " LoadTertrominoesVisual", gameObject);
    }
    protected virtual void LoadTertrominoesRotation()
    {
        if (this.rotation != null) return;
        this.rotation = GetComponentInChildren<TertrominoesRotation>();
        Debug.LogWarning(gameObject.name + " LoadTertrominoesRotation", gameObject);
    }
    protected virtual void SetOffset(TetrominoDataSO tetrominoData)
    {
        this.rotationOffsets = tetrominoData.rotationOffsets;
    }
    public virtual void SetPosition(Vector3Int pos)
    {
        this.position = pos;
    }
    public virtual void SetPlayerID(int id)
    {
        this.playerID = id;
    }
    public virtual void SetCells()
    {
        for (int i = 0; i < 4; i++)
        {
            cells[i] = this.RotationOffsets[rotationState].cellPositions[i] + position;
        }
        this.TertrominoesVisual.UpdateVisuals();
    }
    public virtual void SetRotationState(int state)
    {
        this.rotationState = state;
    }
    public virtual void SetCells(Vector3Int[] vector3s)
    {
        this.cells = vector3s;
    }
    public virtual bool IsRotatable()
    {
        return true;
    }
    public virtual void CreateTetromino(Vector3Int pos)
    {
        this.rotationState = 0;
        this.SetPosition(pos);
        this.SetOffset(tetrominoDataSO[Random.Range(0, tetrominoDataSO.Count)]);
        this.TertrominoesVisual.CreateVisuals();
        //this.SetCells();
        this.SetRotationState(0);
        this.mover.SetMoving(true);
        this.rotation.SetRotation(true);
    }
}
