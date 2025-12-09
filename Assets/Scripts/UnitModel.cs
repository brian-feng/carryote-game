using System;
using UnityEngine;

[Serializable]
public class UnitModel
{
    public int Cost;
    public string Name;
    public Trait.Origin Origin;
    public Trait.Class Class;
    public int AttackDamage1Star;
    public int AttackDamage2Star;
    public int AttackDamage3Star;
    public int MaxHealth1Star;
    public int MaxHealth2Star;

    public int MaxHealth3Star;
    public double AttackSpeed;

    public Unit UnitPrefab;
}
