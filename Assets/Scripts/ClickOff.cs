using System.Collections.Generic;
using UnityEngine;

public class ClickOff : MonoBehaviour
{
    // Singleton instance
    private static ClickOff _instance;
    public static ClickOff Instance { get { return _instance; } }

    public List<GameObject> boxList;

    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }

        boxList = new List<GameObject>();
    }

    public void AddBox(GameObject gameObject)
    {
        if (gameObject != null)
        {
            boxList.Add(gameObject);
        }

        Debug.Log("Contents of the list:");
        foreach (GameObject x in boxList)
        {
            Debug.Log(x.transform.position);
        }
    }

    public void ClickOffAllBoxes()
    {
        //Debug.Log("Click all of them off");
        foreach (GameObject obj in boxList)
        {
            SpriteRenderer spriteRenderer = obj.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                Color color = spriteRenderer.color;
                color.a = 0.01f;
                spriteRenderer.color = color;
            }
        }
    }

    bool CheckIntersection(Collider2D collider)
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return collider.OverlapPoint(mousePosition);
    }

    private void OnMouseDown(){
        foreach (GameObject obj in boxList)
        {
            if (CheckIntersection(obj.GetComponent<Collider2D>())){
                obj.GetComponent<RodUpgrader>().Highlight();
                Debug.Log("found");
                return;
            }
        } 

        Debug.Log("not found");
        ClickOffAllBoxes();
    }
}
