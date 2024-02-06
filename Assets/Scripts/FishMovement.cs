using UnityEngine;

public class FishMovement : MonoBehaviour
{
    public float moveSpeed = 5.0f;

    void Start()
    {
        transform.Rotate(0f, 0f, 90f);
    }

    void Update()
    {
        transform.Translate(new Vector3(0f, 1f, 0f) * moveSpeed * Time.deltaTime);
    }
}