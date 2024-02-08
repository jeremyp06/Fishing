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
        Debug.Log("Fish Starting Reel");
        isFishing = true;
        GameObject target = ChooseItem();
        if (target != null){
            Collider2D hitboxCollider = GetComponent<Collider2D>();
            Vector3 centerOfHitbox = hitboxCollider.bounds.center;
            Fish targetFish = target.GetComponent<Fish>();

            MoveFishTowardsLocation(target, centerOfHitbox, reelSpeed);
        }
    }

    public void MoveFishTowardsLocation(GameObject target, Vector3 targetPosition, float moveSpeed)
    {
        Fish targetFish = target.GetComponent<Fish>();
        targetFish.setIsCaught(true);
        StartCoroutine(MoveTowardsCoroutine(target, targetPosition, moveSpeed - 0.1f * targetFish.weight));
    }

    private IEnumerator<YieldInstruction> MoveTowardsCoroutine(GameObject target, Vector3 targetPosition, float moveSpeed)
    {
        while (Vector3.Distance(target.transform.position, targetPosition) > 0.2f)
        {
            Vector3 direction = (targetPosition - target.transform.position).normalized;
            target.transform.position += direction * moveSpeed * Time.deltaTime;
            yield return null;
        }
        target.transform.position = targetPosition;
        StartReelCooldown();
        Debug.Log("Finish reeling fish, cooldown started"); 
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
                Debug.Log("Finish cooling down, ready to reel another fish");
                if (objectsInside.Count > 0)
                {
                    ReelFish();
                } 

                else 
                {
                    isReelingCooldown = false;
                    isFishing = false;
                }
            }
        }
    }
}

