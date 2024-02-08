using UnityEngine;

public class RodController : MonoBehaviour
{
    public GameObject fishingRodPrefab;
    public GameObject clickableAreaPrefab;
    private GameObject currentFishingRod;
    private Collider2D rodCollider;
    private CashSystem cashSystem;

    private int fishingRodCost = 200;
    public ClickOff clickOff;

    private GameObject clickableArea;

    public LayerMask boxes;

    private void Start()
    {
        cashSystem = CashSystem.instance;
        clickOff = ClickOff.Instance;
    }

    public void OnBuyFishingRodClick()
    {
        if (cashSystem.Cash >= fishingRodCost)
        {
            currentFishingRod = Instantiate(fishingRodPrefab);
            cashSystem.SubtractCash(fishingRodCost);
            rodCollider = currentFishingRod.GetComponent<Collider2D>();

            Vector3 clickableAreaPosition = currentFishingRod.transform.position + new Vector3(0.04f, -0.2f, 0f);
            clickableArea = Instantiate(clickableAreaPrefab, clickableAreaPosition, Quaternion.Euler(0, 0, -45));
            clickableArea.GetComponent<RodUpgrader>().setRod(currentFishingRod);

            SpriteRenderer spriteRenderer = clickableArea.GetComponent<SpriteRenderer>();

            Color color = spriteRenderer.color;
            color.a = 0.2f;
            spriteRenderer.color = color;

            Debug.Log("oijefeooijfe");

            clickOff.GetComponent<ClickOff>().AddBox(clickableArea);
        }
    }

    bool CheckIntersectionWithLayer(GameObject otherObj)
    {
        if (otherObj == null)
        {
            return false;
        }

        Collider2D[] colliders = Physics2D.OverlapBoxAll(otherObj.transform.position, clickableArea.GetComponent<BoxCollider2D>().size, 45f, boxes);

        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject != otherObj)
            {
                return true;
            }
        }

        return false;
    }

    void Update()
    {
        if (currentFishingRod != null)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0f;
            currentFishingRod.transform.position = mousePos;

            Vector3 clickableAreaPosition = currentFishingRod.transform.position + new Vector3(0.04f, -0.2f, 0f);
            clickableArea.transform.position = clickableAreaPosition;

            SpriteRenderer spriteRenderer = clickableArea.GetComponent<SpriteRenderer>();

            if (rodCollider != null)
            {
                rodCollider.enabled = false;
            }

            if (CheckIntersectionWithLayer(clickableArea)){
                Color color = spriteRenderer.color;
                color.r = 1f;
                spriteRenderer.color = color;
            } 
            
            else if (spriteRenderer.color.r == 1f)
            {
                Color color = spriteRenderer.color;
                color.r = 0f;
                spriteRenderer.color = color;
            }

            else if (Input.GetMouseButtonDown(0))
            {
                if (rodCollider != null)
                {
                    rodCollider.enabled = true;
                }

                currentFishingRod = null;
                clickableArea = null;
            } 
        }
    }
}
