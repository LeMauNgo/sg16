using UnityEngine;

public class TetrominoMover : SaiBehaviour
{
    [SerializeField] protected TetrominoCtrl ctrl;
    [SerializeField] protected float moveSpeed = 1f;
    [SerializeField] protected float dropSpeed = 1f;
    [SerializeField] protected float autoDropInterval = 1f;
    [SerializeField] protected float minX = -7f;
    [SerializeField] protected float maxX = 7f;
    [SerializeField] protected float minY = -14f;

    [SerializeField] protected float moveDelay = 0.1f;
    [SerializeField] protected float dropDelay = 0.05f;
    [SerializeField] protected float autoDropTimer = 0f;
    [SerializeField] protected float moveTimer = 0f;
    [SerializeField] protected float dropTimer = 0f;
    [SerializeField] protected bool hasLanded = false;
    [SerializeField] protected bool isBlockLeft = false;
    [SerializeField] protected bool isBlockRight = false;

    [SerializeField] protected LayerMask blockLayer = -1;

    void Update()
    {
        if (transform.parent == null || hasLanded) return;

        this.CheckBlockLeftRight();
        HandleMovement();
        HandleRotation();
        HandleAutoDrop();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadCtrl();
    }

    protected virtual void LoadCtrl()
    {
        if (this.ctrl != null) return;
        this.ctrl = GetComponentInParent<TetrominoCtrl>();
        Debug.LogWarning(transform.name + ": LoadCtrl", gameObject);

    }
    void HandleMovement()
    {
        moveTimer += Time.deltaTime;
        dropTimer += Time.deltaTime;

        this.MoveLeft();
        this.MoveRight();
        this.MoveDown();
    }
    protected virtual void MoveLeft()
    {
        Transform parent = transform.parent;

        if (Input.GetKey(KeyCode.RightArrow) && moveTimer >= moveDelay)
        {
            if (parent.position.x + moveSpeed <= maxX && !this.IsBlockedRight())
            {
                parent.position += Vector3.right * moveSpeed;
                moveTimer = 0f;
            }
        }
    }

    protected virtual void MoveRight()
    {
        Transform parent = transform.parent;

        if (Input.GetKey(KeyCode.LeftArrow) && moveTimer >= moveDelay)
        {
            if (parent.position.x - moveSpeed >= minX && !this.IsBlockedLeft())
            {
                parent.position += Vector3.left * moveSpeed;
                moveTimer = 0f;
            }
        }
    }

    protected virtual void MoveDown()
    {
        if (Input.GetKey(KeyCode.DownArrow) && dropTimer >= dropDelay)
        {
            TryMoveDown();
            dropTimer = 0f;
        }
    }

    void HandleRotation()
    {
        if (!this.ctrl.IsRotatable()) return;
        if (this.IsBlockedLeft() || this.IsBlockedRight()) return;

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            transform.parent.Rotate(0f, 0f, -90f);
            ClampPosition();
        }
    }

    void HandleAutoDrop()
    {
        autoDropTimer += Time.deltaTime;
        if (autoDropTimer >= autoDropInterval)
        {
            TryMoveDown();
            autoDropTimer = 0f;
        }
    }

    void TryMoveDown()
    {
        if (this.IsLanded()) return;
        transform.parent.position += Vector3.down * dropSpeed;
    }

    public bool IsLanded()
    {
        return hasLanded;
    }

    public void SetLandedState(bool state)
    {
        this.hasLanded = state;
        //if(this.hasLanded) this.OnBlockLanded();
        Debug.Log("SetCollisionState", gameObject);
    }

    void OnBlockLanded()
    {
        Debug.Log("Block has landed: "+transform.parent.name, gameObject);
    }

    void ClampPosition()
    {
        Transform parent = transform.parent;
        Vector3 clampedPosition = parent.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, minX, maxX);
        clampedPosition.y = Mathf.Max(clampedPosition.y, minY);
        parent.position = clampedPosition;
    }

    protected bool IsBlockedLeft()
    {
        return this.isBlockLeft;
    }

    protected bool IsBlockedRight()
    {
        return this.isBlockRight;
    }


    void OnDrawGizmos()
    {
        foreach (Transform child in transform)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawRay(child.position, Vector3.left * moveSpeed);
            Gizmos.DrawRay(child.position, Vector3.right * moveSpeed);
        }
    }

    protected virtual void CheckBlockLeftRight()
    {
        bool isBlockLeft = false;
        bool isBlockRight = false;
        foreach (CubeCollision cube in this.ctrl.Cubes)
        {
            if (cube.IsBlockLeft) isBlockLeft = true;
            if (cube.IsBlockRight) isBlockRight = true;
        }

        this.isBlockLeft = isBlockLeft;
        this.isBlockRight = isBlockRight;
    }
}
