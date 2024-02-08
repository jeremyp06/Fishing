using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class CashSystem : MonoBehaviour
{
    public static CashSystem instance; //singleton

    private int cash = 250; 

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
        cash = 250;
        UpdateUI();
    }

    public void AddCash(int amount)
    {
        cash += amount;
        checkWin();
        UpdateUI();
    }

    public void SubtractCash(int amount)
    {
        cash -= amount;
        checkDeath();
        UpdateUI();
    }

    private void UpdateUI()
    {
        cashText.text = "$" + cash.ToString();
    }

    private void checkDeath()
    {
        if (cash < 0){
            SceneManager.LoadScene("lose");
        }
    }

    private void checkWin()
    {
        if (cash > 10000){
            SceneManager.LoadScene("win");
        }
    }

}
