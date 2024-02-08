using UnityEngine;

public class RodUpgrader : MonoBehaviour
{

    private GameObject rod;

    public void setRod(GameObject r)
    {
        rod = r;
    }

    private void OnMouseDown()
    {
        Debug.Log("Rod clicked!");
    }
}