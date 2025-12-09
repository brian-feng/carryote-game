using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewUnitsScriptableObject", menuName = "ScriptableObjects/Units")]
public class UnitsScriptableObject : ScriptableObject
{
    public List<UnitModel> OneCosts;
    public List<UnitModel> TwoCosts;
    public List<UnitModel> ThreeCosts;
    public List<UnitModel> FourCosts;
    public List<UnitModel> FiveCosts;
}
