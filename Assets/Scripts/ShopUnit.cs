using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ShopUnit : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI ClassText;
    [SerializeField] private TextMeshProUGUI OriginText;
    [SerializeField] private TextMeshProUGUI NameText;
    [SerializeField] private TextMeshProUGUI CostText;
    [SerializeField] private GameObject ArtContainer;
    public UnitModel UnitModel { get; private set; }

    public void Initialize(UnitModel model)
    {
        ClassText.text = model.Class.ToString();
        OriginText.text = model.Origin.ToString();
        NameText.text = model.Name;
        CostText.text = model.Cost.ToString();
        UnitModel = model;
    }

    void Start()
    {
        foreach (Image image in ArtContainer.GetComponentsInChildren<Image>())
        {
            image.raycastTarget = false;
        }
    }
}
