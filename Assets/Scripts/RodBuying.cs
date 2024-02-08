using UnityEngine;

public class RodController : MonoBehaviour
{
    public GameObject fishingRodPrefab;
    public GameObject clickableAreaPrefab;
    private GameObject currentFishingRod;
    private Collider2D rodCollider;
    private CashSystem cashSystem;

    private int fishingRodCost = 200;

    private void Start()
    {
        cashSystem = CashSystem.instance;
    }

    public void OnBuyFishingRodClick()
    {
        if (cashSystem.Cash >= fishingRodCost)
        {
            currentFishingRod = Instantiate(fishingRodPrefab);
            cashSystem.SubtractCash(fishingRodCost);
            rodCollider = currentFishingRod.GetComponent<Collider2D>();
        }
    }

    void Update()
    {
        if (currentFishingRod != null)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0f;
            currentFishingRod.transform.position = mousePos;

            if (rodCollider != null)
            {
                rodCollider.enabled = false;
            }

            if (Input.GetMouseButtonDown(0))
            {
                if (rodCollider != null)
                {
                    rodCollider.enabled = true;
                }

                Vector3 clickableAreaPosition = currentFishingRod.transform.position + new Vector3(0.04f, -0.2f, 0f);

                GameObject clickableArea = Instantiate(clickableAreaPrefab, clickableAreaPosition, Quaternion.Euler(0, 0, -45));

                clickableArea.GetComponent<RodUpgrader>().setRod(currentFishingRod);

                currentFishingRod = null;
            }   
        }
    }
}
