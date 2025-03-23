using UnityEngine;

public class InputManager : SaiSingleton<InputManager>
{
    [SerializeField] protected bool isLeft;
    public bool IsLeft => isLeft;
    [SerializeField] protected bool isRight;
    public bool IsRight => isRight;
    [SerializeField] protected bool isRotat;
    public bool IsRotat => isRotat;
    [SerializeField] protected bool isDown;
    public bool IsDown => isDown;
    private void Update()
    {
        this.HandleInput();
    }
    private void HandleInput()
    {
        isLeft = Input.GetKeyDown(KeyCode.LeftArrow) ? true : false;
        isRight = Input.GetKeyDown(KeyCode.RightArrow) ? true : false;
        isRotat = Input.GetKeyDown(KeyCode.UpArrow) ? true : false;
        isDown = Input.GetKeyDown(KeyCode.DownArrow) ? true : false;
    }
}
