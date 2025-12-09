using TMPro;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] private GameObject Root;
    private TextMeshProUGUI ClassText;
    private TextMeshProUGUI OriginText;
    private TextMeshProUGUI NameText;
    private TextMeshProUGUI CostText;

    public void Instantiate(string ArgClass, string ArgOrigin, string ArgName, string ArgCost)
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
