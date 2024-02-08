using UnityEngine;
using UnityEngine.UI;

public class Targeting : MonoBehaviour
{
    public Button firstButton;
    public Button lastButton;
    public Button closeButton;
    public Button strongButton;

    public GameObject rod;

    void Start()
    {
        HighlightButton(firstButton);
        HideButtons();
    }

    public void HideButtons()
    {
        HideButton(firstButton);
        HideButton(lastButton);
        HideButton(closeButton);
        HideButton(strongButton);
    }

    public void ShowButtons()
    {
        ShowButton(firstButton);
        ShowButton(lastButton);
        ShowButton(closeButton);
        ShowButton(strongButton);

        if (rod != null)
        {
            FishReel fishReel = rod.GetComponent<FishReel>();
            if (fishReel != null)
            {
                IFishingStrategy strategyType = fishReel.GetStrategy();
                switch (strategyType)
                {
                    case First:
                        HighlightButton(firstButton);
                        break;
                    case Last:
                        HighlightButton(lastButton);
                        break;
                    case Close:
                        HighlightButton(closeButton);
                        break;
                    case Strong:
                        HighlightButton(strongButton);
                        break;
                    default:
                        Debug.LogError("Unknown fishing strategy type: " + strategyType);
                        break;
                }
            }
        }

        else 
        {
            HighlightButton(firstButton);
        }
    }

    public void ShowButton (Button button)
    {
        button.gameObject.SetActive(true);
    }

    public void HideButton (Button button)
    {
        button.gameObject.SetActive(false);
    }

    public void HighlightButton(Button button)
    {
        SetButtonColor(firstButton, Color.white);
        SetButtonColor(lastButton, Color.white);
        SetButtonColor(closeButton, Color.white);
        SetButtonColor(strongButton, Color.white);

        SetButtonColor(button, new Color(0.91f, 0.31f, 0.31f));
    }

    void SetButtonColor(Button button, Color color)
    {
        button.GetComponent<Image>().color = color;
    }

    public void SetRod(GameObject r){
        rod = r;
    }

    public void OnFirstButtonClick()
    {
        FishReel fishReel = rod.GetComponent<FishReel>();
        fishReel.SetChooseItemStrategy(new First());
        HighlightButton(firstButton);
    }

    public void OnLastButtonClick()
    {
        FishReel fishReel = rod.GetComponent<FishReel>();
        fishReel.SetChooseItemStrategy(new Last());
        HighlightButton(lastButton);
    }

    public void OnCloseButtonClick()
    {
        FishReel fishReel = rod.GetComponent<FishReel>();
        fishReel.SetChooseItemStrategy(new Close());
        HighlightButton(closeButton);

    }

    public void OnStrongButtonClick()
    {
        FishReel fishReel = rod.GetComponent<FishReel>();
        fishReel.SetChooseItemStrategy(new Strong());
        HighlightButton(strongButton);
    }
}