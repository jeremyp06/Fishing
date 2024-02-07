using UnityEngine;
using System.Collections.Generic;

public class FishReel : MonoBehaviour
{
    private List<GameObject> objectsInside = new List<GameObject>();
    private IFishingStrategy strategy;

    public reelSpeed = 1f;

    public void SetChooseItemStrategy(IFishingStrategy s)
    {
        strategy = s;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Fish fish = other.gameObject.GetComponent<Fish>(); 
        if (fish != null && !objectsInside.Contains(fish.gameObject))
        {
            objectsInside.Add(fish.gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        Fish fish = other.gameObject.GetComponent<Fish>(); 
        if (fish != null && objectsInside.Contains(fish.gameObject))
        {
            objectsInside.Remove(fish.gameObject);
        }
    }

    private GameObject ChooseItem()
    {
        if (strategy != null)
        {
            return strategy.ChooseItem(objectsInside);
        }
        return null;
    }

    private void ReelFish()
    {
        GameObject target = ChooseItem();
        if (target != null){
            Collider2D hitboxCollider = GetComponent<Collider2D>();
            Vector3 centerOfHitbox = hitboxCollider.bounds.center;
            chosenFish.GetComponent<Fish>().MoveTowardsLocation(centerOfHitbox, reelSpeed);
        }
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
        if (objectsInside.Count == 0)
            return null;

        Collider2D collider = objectsInside[0].GetComponent<Collider2D>();
        if (collider == null)
            return null;

        Vector2 colliderCenter = collider.bounds.center;

        GameObject closestObject = null;
        float minDistance = float.MaxValue;

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
