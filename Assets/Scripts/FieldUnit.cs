using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FieldUnit : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Image BackgroundImage;
    [SerializeField] private List<Image> StarImages;
    public UnitModel UnitModel { get; private set; }
    public FieldSlot OldSlot { get; private set; }
    public FieldSlot CurrentSlot;
    private bool IsHovering;

    public int StarLevel = 1;

    [HideInInspector] public Transform ParentAfterDrag;
    
    void Update()
    {
        if (IsHovering && Input.GetKeyDown(KeyCode.E))
        {
            CurrentSlot.ClearUnit();
            int result = UnitModel.Cost * (int)Mathf.Pow(3, StarLevel - 1);
            if (UnitModel.Cost >= 2)
            {
                result--;
            }
            GameLogicManager.Instance.AddGold(result);
            Destroy(gameObject);
        }
    }

    public void Initialize(UnitModel unit, FieldSlot slot)
    {
        UnitModel = unit;
        CurrentSlot = slot;
    }
    
    public void OnBeginDrag(PointerEventData eventData)
    {
        OldSlot = CurrentSlot;
        transform.localScale *= 5f/4f;
        ParentAfterDrag = transform.parent;
        CurrentSlot.ClearUnit();
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        BackgroundImage.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.localScale *= 4f/5f;
        transform.SetParent(ParentAfterDrag);
        BackgroundImage.raycastTarget = true;
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        IsHovering = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        IsHovering = false;
    }
    public void UpdateStarLevel(int newStarLevel)
    {
        StarLevel = newStarLevel;
        switch (StarLevel)
        {
            case 1:
                StarImages[0].gameObject.SetActive(false);
                StarImages[1].gameObject.SetActive(false);
                StarImages[2].gameObject.SetActive(false);
                break;
            case 2:
                StarImages[0].gameObject.SetActive(true);
                StarImages[1].gameObject.SetActive(true);
                StarImages[2].gameObject.SetActive(false);
                break;
            case 3:
                StarImages[0].gameObject.SetActive(true);
                StarImages[1].gameObject.SetActive(true);
                StarImages[2].gameObject.SetActive(true);
                break;
        }
    }
}
