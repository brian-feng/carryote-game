using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Field : MonoBehaviour
{
    [SerializeField] private List<FieldSlot> BoardSlots;
    [SerializeField] private List<FieldSlot> BenchSlots;
    private List<FieldUnit> CurrentUnits = new List<FieldUnit>();
    
    public bool AddUnit(UnitModel unit)
    {
        if (AttemptToMakeUpgrade(unit))
        {
            return true;
        }
        if (IsBenchFull())
        {
            return false;
        }
        FieldSlot firstAvailableSlot = BenchSlots[0];
        for (int i = 0; i < BenchSlots.Count; i++)
        {
            if (BenchSlots[i].HasaUnit() == false)
            {
                firstAvailableSlot = BenchSlots[i];
                break;
            }
        } 
        FieldUnit newUnit = Instantiate(unit.FieldUnitPrefab, firstAvailableSlot.transform, false);
        newUnit.Initialize(unit, firstAvailableSlot);
        firstAvailableSlot.SetUnit(newUnit);
        CurrentUnits.Add(newUnit);
        return true;
    }

    private bool IsBenchFull()
    {
        foreach (FieldSlot slot in BenchSlots)
        {
            if (slot.HasaUnit() == false)
            {
                return false;
            }
        }

        return true;
    }

    public List<FieldUnit> BoardedUnits()
    {
        List<FieldUnit> ret = new List<FieldUnit>();
        foreach (FieldUnit fUnit in CurrentUnits)
        {
            if (BoardSlots.Contains(fUnit.CurrentSlot))
            {
                ret.Add(fUnit);
            }
        }

        return ret;
    }

    private bool AttemptToMakeUpgrade(UnitModel unit)
    {
        List<FieldSlot> copies = new List<FieldSlot>();
        foreach (FieldUnit fUnit in CurrentUnits)
        {
            if (fUnit.UnitModel.Name == unit.Name && fUnit.StarLevel == 1)
            {
                copies.Add(fUnit.CurrentSlot);
            }
        }
        
        if (copies.Count == 2)
        {
            FieldUnit targetUnit;
            FieldUnit otherUnit;
            if (BoardSlots.Contains(copies[0]) && BoardSlots.Contains(copies[1]) == false)
            {
                targetUnit = copies[0].CurrentUnit;
                otherUnit = copies[1].CurrentUnit;
            }
            else if (BoardSlots.Contains(copies[0]) == false && BoardSlots.Contains(copies[1]) == true)
            {
                targetUnit = copies[1].CurrentUnit;
                otherUnit = copies[0].CurrentUnit;
            }
            else
            {
                targetUnit = copies[0].CurrentUnit;
                otherUnit = copies[1].CurrentUnit;
            }
            
            otherUnit.CurrentSlot.ClearUnit();
            CurrentUnits.Remove(otherUnit);
            Destroy(otherUnit.gameObject);
            targetUnit.UpdateStarLevel(2);
            
            AttemptToMakeThreeStar(unit);
            
            return true;
        }

        return false;
    }

    private void AttemptToMakeThreeStar(UnitModel unit)
    {
        List<FieldSlot> copies = new List<FieldSlot>();
        foreach (FieldUnit fUnit in CurrentUnits)
        {
            if (fUnit.UnitModel.Name == unit.Name && fUnit.StarLevel == 2)
            {
                copies.Add(fUnit.CurrentSlot);
            }
        }

        if (copies.Count == 3)
        {
            FieldUnit targetUnit = null;
            FieldUnit otherUnit1 = null;
            FieldUnit otherUnit2 = null;

            foreach (var copy in copies)
            {
                if (BoardSlots.Contains(copy))
                {
                    targetUnit = copy.CurrentUnit;
                    break;
                }
            }

            var others = copies
                .Where(c => c.CurrentUnit != targetUnit)
                .Select(c => c.CurrentUnit)
                .ToArray();

            otherUnit1 = others[0];
            otherUnit2 = others[1];

            otherUnit1.CurrentSlot.ClearUnit();
            CurrentUnits.Remove(otherUnit1);
            Destroy(otherUnit1.gameObject);
            otherUnit2.CurrentSlot.ClearUnit();
            CurrentUnits.Remove(otherUnit2);
            Destroy(otherUnit2.gameObject);
            
            targetUnit.UpdateStarLevel(3);
        }
    }
}
