using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickOff : MonoBehaviour
{
    // Singleton instance
    private static ClickOff _instance;
    public static ClickOff Instance { get { return _instance; } }

    public GameObject T;
    private Targeting targeting;

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

    void Start()
    {
        targeting = T.GetComponent<Targeting>();
    }

    public void AddBox(GameObject gameObject)
    {
        if (gameObject != null)
        {
            boxList.Add(gameObject);
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

        targeting.HideButtons();
    }

    bool CheckIntersection(Collider2D collider)
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return collider.OverlapPoint(mousePosition);
    }

    private void OnMouseDown(){

        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        foreach (GameObject obj in boxList)
        {
            if (CheckIntersection(obj.GetComponent<Collider2D>())){
                obj.GetComponent<RodUpgrader>().Highlight();

                targeting.rod = obj.GetComponent<RodUpgrader>().getRod();
                targeting.ShowButtons();

                return;
            }
        } 

        ClickOffAllBoxes();
    }
}
