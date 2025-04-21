using UnityEngine;

public class InputManager : SaiSingleton<InputManager>
{
    [SerializeField] protected PlayerState Player1;
    public PlayerState Player1State => Player1;
    [SerializeField] protected PlayerState Player2;
    public PlayerState Player2State => Player2;
    private void Update()
    {
        this.HandleInput();
    }
    private void HandleInput()
    {
        Player1 = PlayerState.None;
        if (Input.GetKeyDown(KeyCode.LeftArrow))
            Player1 = PlayerState.Left;
        else if (Input.GetKeyDown(KeyCode.RightArrow))
            Player1 = PlayerState.Right;
        else if (Input.GetKeyDown(KeyCode.UpArrow))
            Player1 = PlayerState.Rotate;
        else if (Input.GetKeyDown(KeyCode.DownArrow))
            Player1 = PlayerState.Down;

        // Player 2
        Player2 = PlayerState.None;
        if (Input.GetKeyDown(KeyCode.A))
            Player2 = PlayerState.Left;
        else if (Input.GetKeyDown(KeyCode.D))
            Player2 = PlayerState.Right;
        else if (Input.GetKeyDown(KeyCode.W))
            Player2 = PlayerState.Rotate;
        else if (Input.GetKeyDown(KeyCode.S))
            Player2 = PlayerState.Down;
    }
}
