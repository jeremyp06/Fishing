using UnityEngine;
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
        transform.localScale = Vector3.one * (1 + weight * 0.1f);
        Color color = Color.red + new Color(weight * 0.1f, 0, 0);
        spriteRenderer.color = color;
        adjustedMoveSpeed = moveSpeed + weight * 0.2f;
    }

    void setWeight(float newWeight)
    {
        weight = newWeight;
    }

    void Update()
    {
        if (!isCaught)
        {
            transform.Translate(new Vector3(0f, 1f, 0f) * adjustedMoveSpeed * Time.deltaTime);
        }
    }

    public void MoveTowardsLocation(Vector3 targetPosition, float moveSpeed)
    {
        isMoving = true;
        StartCoroutine(MoveTowardsCoroutine(targetPosition, moveSpeed));
    }

    private IEnumerator MoveTowardsCoroutine(Vector3 targetPosition, float moveSpeed)
    {
        while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
        {
            Vector3 direction = (targetPosition - transform.position).normalized;
            transform.position += direction * moveSpeed * Time.deltaTime;
            yield return null;
        }
        transform.position = targetPosition;
        Destroy(gameObject);
    }
}