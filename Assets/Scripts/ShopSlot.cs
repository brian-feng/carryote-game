using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopSlot : MonoBehaviour
{
    [SerializeField] private Button PurchaseButton;
    [SerializeField] private Unit CurrentUnit;

    public bool HasUnit()
    {
        return  CurrentUnit != null;
    }

    public void SetUnit(UnitModel unit)
    {
        if (CurrentUnit != null)
        {
            Destroy(CurrentUnit.gameObject);
        }
        CurrentUnit = Instantiate(unit.UnitPrefab, transform, false);
        CurrentUnit.Initialize(unit.Class.ToString(), unit.Origin.ToString(), unit.Name, unit.Cost.ToString());
    }
}
