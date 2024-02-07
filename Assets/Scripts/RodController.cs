using UnityEngine;

public class RodController : MonoBehaviour
{
    public GameObject fishingRodPrefab;
    private GameObject currentFishingRod;
    private Collider2D rodCollider;

    public void OnBuyFishingRodClick()
    {
        currentFishingRod = Instantiate(fishingRodPrefab);
        rodCollider = currentFishingRod.GetComponent<Collider2D>();
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
                currentFishingRod = null;
            }
        }
    }
}
