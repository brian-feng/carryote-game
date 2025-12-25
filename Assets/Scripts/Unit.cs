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

    private Color OneCostColor = new Color(72f / 255, 72f / 255, 72f / 255);
    private Color TwoCostColor = new Color(0, 0, 0);
    private Color ThreeCostColor = new Color(0, 0, 0);
    private Color FourCostColor = new Color(0, 0, 0);
    private Color FiveCostColor = new Color(171f / 255, 136f / 255, 30f / 255);

    private const float OutlineBrightnessRatio = 1f;

    public void Initialize(string ArgClass, string ArgOrigin, string ArgName, string ArgCost)
    {
        ClassText.text = ArgClass;
        OriginText.text = ArgOrigin;
        NameText.text = ArgName;
        CostText.text = ArgCost;
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
