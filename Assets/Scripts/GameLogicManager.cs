using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameLogicManager : MonoBehaviour
{
    public static GameLogicManager Instance { get; private set; }
    [SerializeField] private Shop Shop;
    [SerializeField] public Field Field;
    [SerializeField] private TextMeshProUGUI GoldText;
    [SerializeField] private Image XPBar;
    [SerializeField] private TextMeshProUGUI LevelText;
    [SerializeField] private TextMeshProUGUI XPText;
    [SerializeField] private TextMeshProUGUI DifficultyText;
    [SerializeField] private TextMeshProUGUI PowerText;
    [SerializeField] private TextMeshProUGUI DefensiveText;
    [SerializeField] private TextMeshProUGUI PhysicalText;
    [SerializeField] private TextMeshProUGUI MagicalText;
    private FightCalculator calc;
    public Dictionary<int, List<float>> LevelOddsDictionary {get; private set;} = new Dictionary<int, List<float>>()
    {
        { 1,  new List<float>() { 1f, 0, 0, 0, 0 } },
        { 2,  new List<float>() { 1f, 0, 0, 0, 0 } },
        { 3,  new List<float>() { 0.75f, 0.25f, 0, 0, 0 } },
        { 4,  new List<float>() { 0.55f, 0.3f, 0.15f } },
        { 5,  new List<float>() { 0.45f, 0.33f, 0.2f, 0.02f } },
        { 6,  new List<float>() { 0.3f, 0.4f, 0.25f, 0.05f } },
        { 7,  new List<float>() { 0.19f, 0.3f, 0.4f, 0.1f, 0.01f } },
        { 8,  new List<float>() { 0.15f, 0.2f, 0.32f, 0.3f, 0.03f } },
        { 9,  new List<float>() { 0.1f, 0.17f, 0.25f, 0.33f, 0.15f } },
        { 10, new List<float>() { 0.05f, 0.1f, 0.2f, 0.4f, 0.25f } }
    };
    public Dictionary<int, int> levelUpXpRequirementsDictionary { get; private set; } =  new Dictionary<int, int>()
    {
        { 1, 2 },
        { 2, 2 },
        { 3, 6 },
        { 4, 10 },
        { 5, 20 },
        { 6, 36 },
        { 7, 60 },
        { 8, 68 },
        { 9, 68 },
        {10, 99999999}
    };
    public int PlayerLevel { get; private set; } = 1;
    public int Gold { get; private set; } = 999;
    public int XP { get; private set; } = 0;
    public int Difficulty { get; private set; } = 100;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        else
        {
            Instance = this;
        }
    }

    void Update()
    {
        GoldText.text = Gold.ToString();
        LevelText.text = PlayerLevel.ToString();
        XPText.text = XP.ToString() + "/" +  levelUpXpRequirementsDictionary[PlayerLevel];
        XPBar.fillAmount = (float)XP/levelUpXpRequirementsDictionary[PlayerLevel];
        calc = new FightCalculator(Field.BoardedUnits());
        DifficultyText.text = "Goal: " + Difficulty.ToString();
        PowerText.text = "Power: " + calc.CalculateCombatPower().ToString();
        DefensiveText.text = "Defensive: " + calc.Defensive.ToString();
        PhysicalText.text = "Physical: " + calc.Physical.ToString();
        MagicalText.text = "Magical: " + calc.Magical.ToString();
    }

    public void NextRound()
    {
        XP += 2;
        while (XP >= levelUpXpRequirementsDictionary[PlayerLevel])
        {
            XP -= levelUpXpRequirementsDictionary[PlayerLevel];
            PlayerLevel++;
        }
        Gold += GetInterest();
    }

    private int GetInterest()
    {
        return 5 + Mathf.Min(Gold / 10, 5);
    }
    
    public bool BuyReroll()
    {
        if (Gold <= 1)
        {
            return false;
        }
        Gold -= 2;
        return true;
    }

    public bool BuyXP()
    {
        if (Gold <= 3)
        {
            return false;
        }
        Gold -= 4;
        XP += 4;
        while (XP >= levelUpXpRequirementsDictionary[PlayerLevel])
        {
            XP -= levelUpXpRequirementsDictionary[PlayerLevel];
            PlayerLevel++;
        }
        return true;
    }

    public bool BuyUnit(UnitModel unit)
    {
        if (Gold < unit.Cost)
        {
            return false;
        }

        if (Field.AddUnit(unit))
        {
            Gold -= unit.Cost;
            return true;   
        }

        return false;
    }

    public bool AttemptFight()
    {
        if (Difficulty < calc.CalculateCombatPower())
        {
            Gold += calc.CalculateEconomicReturn();
            Difficulty = (int)(Difficulty * 1.2);
            return true;
        }

        return false;
    }

    public void AddGold(int amount)
    {
        Gold += amount;
    }
}
