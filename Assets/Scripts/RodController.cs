using UnityEngine;

public class RodController : MonoBehaviour
{
    public GameObject fishingRodPrefab;
    private GameObject currentFishingRod;

    public void OnBuyFishingRodClick()
    {
        currentFishingRod = Instantiate(fishingRodPrefab);
    }

    void Update()
    {
        if (currentFishingRod != null)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0f;
            currentFishingRod.transform.position = mousePos;
            if (Input.GetMouseButtonDown(0))
            {
                currentFishingRod = null;
            }
        }
    }
}
