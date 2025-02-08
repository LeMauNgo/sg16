using UnityEngine;

public class MoveDown : MonoBehaviour
{
    public float fallInterval = 1f;
    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= fallInterval)
        {
            transform.parent.position += Vector3.down;
            timer = 0f; 
        }
    }
}
