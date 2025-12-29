using System;
using UnityEngine;

[Serializable]
public class UnitModel
{
    public int Cost;
    public string Name;
    public Trait.Origin Origin;
    public Trait.Class Class;
    
    public int AttackDamage;
    public double AttackSpeed;
    public int Health;
    public int Armor;
    public int AbilityDamage;
    
    public ShopUnit ShopUnitPrefab;
    public FieldUnit FieldUnitPrefab;
}
