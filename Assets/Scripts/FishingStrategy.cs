using UnityEngine;
using System.Collections.Generic;

public interface IFishingStrategy
{
    //abstract interface for strategy pattern
    GameObject ChooseItem(List<GameObject> objectsInside);
}

public class First : IFishingStrategy
{
    public GameObject ChooseItem(List<GameObject> objectsInside)
    {
        //picks the first item that entered its sight radius
        if (objectsInside.Count > 0)
        {
            return objectsInside[0];
        }
        return null;
    }
}

public class Last : IFishingStrategy
{

    //picks the last item that entered its sight radius
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
        if (objectsInside.Count == 0)
            return null;

        Collider2D collider = objectsInside[0].GetComponent<Collider2D>();
        if (collider == null)
            return null;

        Vector2 colliderCenter = collider.bounds.center;

        GameObject closestObject = null;
        float minDistance = float.MaxValue;

        //finds the closest object to the center of the hitbox and picks that
        foreach (GameObject obj in objectsInside)
        {
            Vector2 objPosition = obj.transform.position;
            float distance = Vector2.Distance(colliderCenter, objPosition);
            if (distance < minDistance)
            {
                minDistance = distance;
                closestObject = obj;
            }
        }

        return closestObject;
    }
}

public class Strong : IFishingStrategy
{
    public GameObject ChooseItem(List<GameObject> objectsInside)
    {
        GameObject strongestFish = null;
        float maxWeight = float.MinValue;

        //picks the fish with the highest weight stat
        foreach (GameObject fishObject in objectsInside)
        {
            Fish fish = fishObject.GetComponent<Fish>();
            if (fish != null && fish.weight > maxWeight)
            {
                maxWeight = fish.weight;
                strongestFish = fishObject;
            }
        }

        return strongestFish;
    }
}
