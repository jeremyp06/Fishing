using UnityEngine;

public class RodUpgrader : MonoBehaviour
{

    private GameObject rod;

    public ClickOff clickOff;

    public void setRod(GameObject r)
    {
        rod = r;
    }

    public GameObject getRod()
    {
        return rod;
    }

    public void Start()
    {
        clickOff = ClickOff.Instance;
    }

    public void Highlight()
    {
        Debug.Log("Clicked");
        clickOff.GetComponent<ClickOff>().ClickOffAllBoxes();
        SpriteRenderer spriteRenderer = this.GetComponent<SpriteRenderer>();

        Color color = spriteRenderer.color;
        color.a = .2f;
        spriteRenderer.color = color;
    }
}