using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Unit : MonoBehaviour
{
    [SerializeField] private GameObject Root;
    [SerializeField] private TextMeshProUGUI ClassText;
    [SerializeField] private TextMeshProUGUI OriginText;
    [SerializeField] private TextMeshProUGUI NameText;
    [SerializeField] private TextMeshProUGUI CostText;
    [SerializeField] private Image BackgroundImage;
    [SerializeField] private Image OutlineImage;
    [SerializeField] private Image LineImage;

    private Color OneCostColor = new Color(72, 72, 72);
    private Color TwoCostColor = new Color(0, 0, 0);
    private Color ThreeCostColor = new Color(0, 0, 0);
    private Color FourCostColor = new Color(0, 0, 0);
    private Color FiveCostColor = new Color(171, 136, 30);

    private const float OutlineBrightnessRatio = 1.2f;

    public void Instantiate(string ArgClass, string ArgOrigin, string ArgName, string ArgCost)
    {
        ClassText.text = ArgClass;
        OriginText.text = ArgOrigin;
        NameText.text = ArgName;
        CostText.text = ArgCost;
        
        int cost = int.Parse(ArgCost);
        switch (cost)
        {
            case 1:
                BackgroundImage.color = OneCostColor;
                OutlineImage.color = OneCostColor * OutlineBrightnessRatio;
                break;
            case 2:
                BackgroundImage.color = TwoCostColor;
                OutlineImage.color = TwoCostColor * OutlineBrightnessRatio;
                break;
            case 3:
                BackgroundImage.color = ThreeCostColor;
                OutlineImage.color = ThreeCostColor * OutlineBrightnessRatio;
                break;
            case 4:
                BackgroundImage.color = FourCostColor;
                OutlineImage.color = FourCostColor * OutlineBrightnessRatio;
                break;
            case 5:
                BackgroundImage.color = FiveCostColor;
                OutlineImage.color = FiveCostColor * OutlineBrightnessRatio;
                break;
            default:
                BackgroundImage.color = OneCostColor;
                OutlineImage.color = OneCostColor * OutlineBrightnessRatio;
                break;
        }
    }

    public void ShowUnit()
    {
        Root.SetActive(true);
    }

    public void HideUnit()
    {
        Root.SetActive(false);
    }
}
