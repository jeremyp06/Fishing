using UnityEngine;
using System.Collections;

public class Fish : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public float weight = 0.0f;

    private SpriteRenderer spriteRenderer;
    private float adjustedMoveSpeed;
    private bool isCaught = false;

    void Start()
    {
        transform.Rotate(0f, 0f, 90f);

        spriteRenderer = GetComponent<SpriteRenderer>();
        transform.localScale = transform.localScale * (1 + weight * 0.3f);
        Color color = new Color(1f, 1f - 0.2f * weight, 1f - 0.2f * weight);
        spriteRenderer.color = color;
        adjustedMoveSpeed = moveSpeed + weight * 0.2f;
    }

    public void setWeight(float newWeight)
    {
        weight = newWeight;
    }

    public void setIsCaught(bool c)
    {
        isCaught = c;
    }

    public bool getIsCaught()
    {
        return isCaught;
    }

    void Update()
    {
        if (!isCaught)
        {
            transform.Translate(new Vector3(0f, 1f, 0f) * adjustedMoveSpeed * Time.deltaTime);
        } 
    }
}