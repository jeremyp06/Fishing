using UnityEngine;
using System.Collections.Generic;

public class Fish : MonoBehaviour
{
    private List<GameObject> objectsInside = new List<GameObject>();
    private IFishingStrategy strategy;

    public void SetChooseItemStrategy(IFishingStrategy strategy)
    {
        strategy = strategy;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!objectsInside.Contains(other.gameObject))
        {
            objectsInside.Add(other.gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (objectsInside.Contains(other.gameObject))
        {
            objectsInside.Remove(other.gameObject);
        }
    }

    public GameObject ChooseItem()
    {
        if (strategy != null)
        {
            return strategy.ChooseItem(objectsInside);
        }
        return null;
    }
}


public interface IFishingStrategy
{
    GameObject ChooseItem(List<GameObject> objectsInside);
}

public class First : IFishingStrategy
{
    public GameObject ChooseItem(List<GameObject> objectsInside)
    {
        if (objectsInside.Count > 0)
        {
            return objectsInside[0];
        }
        return null;
    }
}

public class Last : IFishingStrategy
{
    public GameObject ChooseItem(List<GameObject> objectsInside)
    {
        if (objectsInside.Count > 0)
        {
            return objectsInside[objectsInside.Count - 1];
        }
        return null;
    }
}

public class Close : IFishingStrategy
{
    public GameObject ChooseItem(List<GameObject> objectsInside)
    {
        return null;
    }
}

public class Strong : IFishingStrategy
{
    public GameObject ChooseItem(List<GameObject> objectsInside)
    {
        return null;
    }
}