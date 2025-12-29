using System;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class FightCalculator
{
    private List<FieldUnit> units;
    private Dictionary<Trait.Class, int> Classes = new Dictionary<Trait.Class, int>();
    private Dictionary<Trait.Origin, int> Origins = new Dictionary<Trait.Origin, int>();
    public int Defensive = 0;
    public int Physical = 0;
    public int Magical = 0;
    
    public FightCalculator(List<FieldUnit> u)
    {
        units = u;
        
        foreach (Trait.Origin origin in Enum.GetValues(typeof(Trait.Origin)))
        {
            Origins.Add(origin, 0);
        }
        foreach (Trait.Class classes in Enum.GetValues(typeof(Trait.Class)))
        {
            Classes.Add(classes, 0);
        }
        
        HashSet<UnitModel> uniqueUnits = new HashSet<UnitModel>();
        foreach (FieldUnit f in u)
        {
            uniqueUnits.Add(f.UnitModel);
        }

        foreach (UnitModel m in uniqueUnits)
        {
            Classes[m.Class]++;
            Origins[m.Origin]++;
        }

        if (Origins[Trait.Origin.Allfather] >= 1)
        {
            foreach (Trait.Origin origin in Enum.GetValues(typeof(Trait.Origin)))
            {
                Origins[origin]++;
            }
            foreach (Trait.Class classes in Enum.GetValues(typeof(Trait.Class)))
            {
                Classes[classes]++;
            }
        }
    }

    public int CalculateCombatPower()
    {
        int defensives = 0;
        foreach (FieldUnit unit in units)
        {
            UnitModel model = unit.UnitModel;
            int health = GetStarLevelMultiplier(model.Health, model.Cost, unit.StarLevel);
            int armor = unit.UnitModel.Armor;
            
            if (Classes[Trait.Class.Bruiser] >= 2 && model.Class == Trait.Class.Bruiser)
            {
                health *= (int)(Classes[Trait.Class.Bruiser] * 0.1);
            }
            if (Classes[Trait.Class.Defender] >= 2 && model.Class == Trait.Class.Defender)
            {
                armor += Classes[Trait.Class.Defender] * 15;
            }
            int total = health * armor;
            if (Classes[Trait.Class.Cavalier] >= 2 && model.Class == Trait.Class.Cavalier)
            {
                total = (int)(total * (1 + Classes[Trait.Class.Cavalier] * 0.1));
            }
            defensives += total;
        }

        defensives /= 100;

        int physical = 0;
        foreach (FieldUnit unit in units)
        {
            UnitModel model = unit.UnitModel;
            int damage = GetStarLevelMultiplier(model.AttackDamage, model.Cost, unit.StarLevel);
            double speed = unit.UnitModel.AttackSpeed;

            if (Classes[Trait.Class.Blademaster] >= 2 && model.Class == Trait.Class.Blademaster)
            {
                speed *= (speed * (1 + Classes[Trait.Class.Blademaster] * 0.15));
            }
            if (Classes[Trait.Class.Gunslinger] >= 2 && model.Class == Trait.Class.Gunslinger)
            {
                damage += Classes[Trait.Class.Gunslinger] * 10;
            }
            int total = (int)(damage * speed);
            if (Classes[Trait.Class.Vanquisher] >= 2 && model.Class == Trait.Class.Vanquisher)
            {
                total = (int)(total * (1 + Classes[Trait.Class.Vanquisher] * 0.2));
            }
            physical += total;
        }
        physical *= 4;

        int magical = 0;
        foreach (FieldUnit unit in units)
        {
            UnitModel model = unit.UnitModel;
            int damage = GetStarLevelMultiplier(model.AbilityDamage, model.Cost, unit.StarLevel);
            double speed = unit.UnitModel.AttackSpeed;
            
            if (Classes[Trait.Class.Blademaster] >= 2 && model.Class == Trait.Class.Blademaster)
            {
                speed *= (speed * (1 + Classes[Trait.Class.Blademaster] * 0.15));
            }
            if (Classes[Trait.Class.Scholar] >= 2 && model.Class == Trait.Class.Scholar)
            {
                speed *= (speed * (1 + Classes[Trait.Class.Scholar] * 0.2));
            }
            if (Classes[Trait.Class.Woman] >= 2 && model.Class == Trait.Class.Woman)
            {
                damage += Classes[Trait.Class.Woman] * 200;
            }
            int total = (int)(damage * speed);
            magical += total;
        }

        magical *= 1;
        
        if (Classes[Trait.Class.Drunk] >= 2)
        {
            defensives = (int)(defensives * (1 + Classes[Trait.Class.Drunk] * 0.1));
        }
        if (Classes[Trait.Class.Demolitionist] >= 2)
        {
            physical = (int)(physical * (1 + Classes[Trait.Class.Demolitionist] * 0.1));
            magical = (int)(magical * (1 + Classes[Trait.Class.Demolitionist] * 0.1));
        }
        if (Origins[Trait.Origin.Noxus] >= 3)
        {
            defensives += 5000;
            physical += 5000;
        }
        if (Origins[Trait.Origin.Matthew] >= 3)
        {
            defensives += 5000;
            magical += 5000;
        }
        if (Origins[Trait.Origin.Yote] >= 3)
        {
            physical += 5000;
            magical += 5000;
        }
        if (Origins[Trait.Origin.Ventyx] >= 3)
        {
            physical += 5000;
            magical += 5000;
            defensives += 5000;
        }
        Defensive = defensives;
        Physical = physical;
        Magical = magical;
        
        int finalTotal = defensives + physical + magical;
        if (Origins[Trait.Origin.Friend] >= 2)
        {
            finalTotal = (int)(finalTotal * (1 + Origins[Trait.Origin.Friend] * 0.15));
        }
        if (Origins[Trait.Origin.Family] >= 2)
        {
            finalTotal = (int)(finalTotal * (1 + Origins[Trait.Origin.Family] * 0.1));
        }
        return finalTotal;
    }

    public int CalculateEconomicReturn()
    {
        if (Origins[Trait.Origin.Carreon] >= 2)
        {
            return Origins[Trait.Origin.Carreon];
        }

        return 0;
    }

    private int GetStarLevelMultiplier(int stat, int cost, int starLevel)
    {
        if(cost == 1)
        {
            if (starLevel == 1)
            {
                return stat;
            }
            if (starLevel == 2)
            {
                return (int)(stat * 1.5);
            }

            if (starLevel == 3)
            {
                return (int)(stat * 2);
            }
        }
        if(cost == 2)
        {
            if (starLevel == 1)
            {
                return stat;
            }
            if (starLevel == 2)
            {
                return (int)(stat * 1.5);
            }

            if (starLevel == 3)
            {
                return (int)(stat * 2);
            }
        }
        if(cost == 3)
        {
            if (starLevel == 1)
            {
                return stat;
            }
            if (starLevel == 2)
            {
                return (int)(stat * 1.5);
            }

            if (starLevel == 3)
            {
                return (int)(stat * 2.5);
            }
        }
        if(cost == 4)
        {
            if (starLevel == 1)
            {
                return stat;
            }
            if (starLevel == 2)
            {
                return (int)(stat * 1.5);
            }

            if (starLevel == 3)
            {
                return (int)(stat * 10);
            }
        }
        if(cost == 5)
        {
            if (starLevel == 1)
            {
                return stat;
            }
            if (starLevel == 2)
            {
                return (int)(stat * 1.5);
            }

            if (starLevel == 3)
            {
                return (int)(stat * 30);
            }
        }
        if(cost == 69)
        {
            if (starLevel == 1)
            {
                return stat;
            }
            if (starLevel == 2)
            {
                return (int)(stat * 1.5);
            }

            if (starLevel == 3)
            {
                return (int)(stat * 30);
            }
        }

        Debug.Log("Invalid star level");
        return 0;
    }
}
