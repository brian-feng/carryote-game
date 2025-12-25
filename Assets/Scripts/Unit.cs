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
