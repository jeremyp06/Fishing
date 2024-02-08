using UnityEngine;
using System.Collections.Generic;

public class FishReel : MonoBehaviour
{
    private List<GameObject> objectsInside = new List<GameObject>();
    private IFishingStrategy strategy;

    public float reelSpeed = 1f;
    public float reelCooldown = 3.0f;
    private float reelCooldownTimer = 0.0f;
    private bool isReelingCooldown = false; 
    private bool isFishing = false; 

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

        if (!isReelingCooldown && !isFishing)
        {
            ReelFish();
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
            GameObject chosenFish = strategy.ChooseItem(objectsInside);
            if (chosenFish != null)
            {
                objectsInside.Remove(chosenFish);
            }
            return chosenFish;
        }
        return null;
    }

    private void StartReelCooldown()
    {
        reelCooldownTimer = reelCooldown;
        isReelingCooldown = true;
    }

    private void ReelFish()
    {
        if (isFishing || isReelingCooldown){
            return;
        }

        GameObject target = ChooseItem();

        if (target == null)
        {
            return;
        }

        Fish targetFish = target.GetComponent<Fish>();


        if (targetFish.getIsCaught())
        {
            return;
        }
        
        isFishing = true;
        Collider2D hitboxCollider = GetComponent<Collider2D>();
        Vector3 centerOfHitbox = hitboxCollider.bounds.center;

        MoveFishTowardsLocation(target, centerOfHitbox, reelSpeed);
    }

    private void MoveFishTowardsLocation(GameObject target, Vector3 targetPosition, float moveSpeed)
    {
        Fish targetFish = target.GetComponent<Fish>();
        targetFish.setIsCaught(true);
        StartCoroutine(MoveTowardsCoroutine(target, targetPosition, moveSpeed - 0.1f * targetFish.weight));
    }

    private IEnumerator<YieldInstruction> MoveTowardsCoroutine(GameObject target, Vector3 targetPosition, float moveSpeed)
    {
        Vector3 lastPosition = target.transform.position + new Vector3(1f, 1f, 1f);
        
        while (Vector3.Distance(target.transform.position, targetPosition) > 0.2f)
        {
            Vector3 direction = (targetPosition - target.transform.position).normalized;
            target.transform.position += direction * moveSpeed * Time.deltaTime;
            if (Vector3.Distance(target.transform.position, targetPosition) > Vector3.Distance(lastPosition, targetPosition))
            {
                break;
            }

            lastPosition = target.transform.position;
            yield return null;   
        }

        target.transform.position = targetPosition;
        StartReelCooldown();
        Destroy(target);
    }

    void Start()
    {
        SetChooseItemStrategy(new First());
    }

    void Update()
    {
        if (isReelingCooldown)
        {
            reelCooldownTimer -= Time.deltaTime;
            if (reelCooldownTimer <= 0.0f)
            {
                isReelingCooldown = false;
                isFishing = false;

                if (objectsInside.Count > 0)
                {
                    ReelFish();
                } 
            }
        }
    }
}

