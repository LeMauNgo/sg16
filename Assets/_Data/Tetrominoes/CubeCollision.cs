using UnityEngine;

public class CubeCollision : SaiBehaviour
{
    [SerializeField] protected TetrominoCtrl ctrl;
    [SerializeField] protected float raycastDistance = 1f;
    [SerializeField] protected LayerMask blockLayer = -1;
    [SerializeField] protected Transform hitBlock;

    [SerializeField] protected bool isBlockLeft = false;
    public bool IsBlockLeft { get { return isBlockLeft; } }

    [SerializeField] protected bool isBlockRight = false;
    public bool IsBlockRight { get { return isBlockRight; } }

    [SerializeField] protected Transform leftCube = null;
    public Transform LeftCube { get { return leftCube; } }

    [SerializeField] protected Transform rightCube = null;
    public Transform RightCube { get { return rightCube; } }

    protected virtual void FixedUpdate()
    {
        this.UpdateLeftBlock();
        this.UpdateRightBlock();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadTetrominoCtrl();
    }

    protected virtual void LoadTetrominoCtrl()
    {
        if (ctrl != null) return;
        ctrl = transform.parent.parent.GetComponent<TetrominoCtrl>();
        Debug.LogWarning(transform.name + ": LoadTetrominoCtrl", gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.parent.parent == ctrl.transform) return;
        if (ctrl.Mover.IsLanded()) return;
        if (CheckRaycastDown()) ctrl.Mover.SetLandedState(true);
    }

    protected bool CheckRaycastDown()
    {
        return PerformRaycast(Vector3.down, out _);
    }

    protected bool CheckRaycastLeft()
    {
        return PerformRaycast(Vector3.left, out leftCube);
    }

    protected bool CheckRaycastRight()
    {
        return PerformRaycast(Vector3.right, out rightCube);
    }

    protected bool PerformRaycast(Vector3 direction, out Transform hitTransform)
    {
        RaycastHit hit;
        Vector3 rayOrigin = transform.position;
        bool hasHit = Physics.Raycast(rayOrigin, direction, out hit, raycastDistance, blockLayer);

        if (hasHit)
        {
            Transform hitParent = hit.transform.parent?.parent;
            if (hitParent == ctrl.transform)
            {
                hitTransform = null;
                return false;
            }
            hitTransform = hit.transform;
            return true;
        }

        hitTransform = null;
        return false;
    }

    void OnDrawGizmos()
    {
        Vector3 rayOrigin = transform.position;
        Color rayColor = (hitBlock != null) ? Color.red : Color.green;

        Debug.DrawRay(rayOrigin, Vector3.down * raycastDistance, rayColor);
        Debug.DrawRay(rayOrigin, Vector3.left * raycastDistance, rayColor);
        Debug.DrawRay(rayOrigin, Vector3.right * raycastDistance, rayColor);
    }

    protected void UpdateLeftBlock()
    {
        this.isBlockLeft = this.CheckRaycastLeft();
    }

    protected void UpdateRightBlock()
    {
        this.isBlockRight = this.CheckRaycastRight();
    }
}
