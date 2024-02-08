using UnityEngine;
using TMPro;

public class CashSystem : MonoBehaviour
{
    public static CashSystem instance; //singleton

    private int cash = 400; 

    public int Cash => cash;

    public TextMeshProUGUI cashText;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        UpdateUI();
    }

    public void AddCash(int amount)
    {
        cash += amount;
        UpdateUI();
    }

    public void SubtractCash(int amount)
    {
        cash -= amount;
        UpdateUI();
    }

    private void UpdateUI()
    {
        cashText.text = "$" + cash.ToString();

    }
}
