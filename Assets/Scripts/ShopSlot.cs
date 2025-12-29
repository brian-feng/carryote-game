using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopSlot : MonoBehaviour
{
    [SerializeField] private Button PurchaseButton;
    [SerializeField] private ShopUnit CurrentUnit;
    [SerializeField] private UnitModel CurrentUnitModel;

    void Start()
    {
        PurchaseButton.onClick.AddListener(AttemptToBuyUnit);
    }

    private void AttemptToBuyUnit()
    {
        if (CurrentUnit != null && CurrentUnitModel != null)
        {
            if (GameLogicManager.Instance.BuyUnit(CurrentUnitModel))
            {
                Destroy(CurrentUnit.gameObject);
                CurrentUnit = null;
                CurrentUnitModel = null;
                AudioManager.Instance.PlayBuyUnitSoundEffect();
            }
        }
    }

    public void SetUnit(UnitModel unitModel)
    {
        if (CurrentUnit != null && CurrentUnitModel != null)
        {
            Destroy(CurrentUnit.gameObject);
            CurrentUnit = null;
            CurrentUnitModel = null;
        }
        CurrentUnitModel = unitModel;
        CurrentUnit = Instantiate(unitModel.ShopUnitPrefab, transform, false);
        CurrentUnit.Initialize(unitModel);
    }
}
