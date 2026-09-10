using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FieldSlot : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private GameObject Outline;
    public FieldUnit CurrentUnit;
    public UnitModel CurrentUnitModel { get; private set; }
    [SerializeField] private Image BackgroundImage;
    
    private readonly float minAlpha = 1f/5f;
    private readonly float maxAlpha = 1f/2f;

    public void OnDrop(PointerEventData eventData)
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        BackgroundImage.color = new Color(BackgroundImage.color.r, BackgroundImage.color.g, BackgroundImage.color.b,
            minAlpha);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        BackgroundImage.color = new Color(BackgroundImage.color.r, BackgroundImage.color.g, BackgroundImage.color.b,
            maxAlpha);
    }

    public bool HasaUnit()
    {
        return CurrentUnit != null && CurrentUnitModel != null;
    }

    public void SetUnit(FieldUnit unit)
    {
        CurrentUnit = unit;
        CurrentUnitModel = unit.UnitModel;
        unit.CurrentSlot = this;
    }

    public void ClearUnit()
    {
        if (CurrentUnit != null)
        {
            CurrentUnit.CurrentSlot = null;
        }

        CurrentUnit = null;
        CurrentUnitModel = null;
    }
}
