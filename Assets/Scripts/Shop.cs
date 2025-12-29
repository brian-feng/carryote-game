using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Shop : MonoBehaviour
{
    [SerializeField] private List<ShopSlot> ShopSlots;
    [SerializeField] private Button RerollButton;
    [SerializeField] private Button BuyXPButton;
    [SerializeField] private UnitsScriptableObject UnitsPool;
    [SerializeField] private GameObject FightImage;
    [SerializeField] private Button FightButton;

    void Start()
    {
        InitializeShop();
        RerollButton.onClick.AddListener(Reroll);
        BuyXPButton.onClick.AddListener(BuyXP);
        FightButton.onClick.AddListener(BeginFight);
    }

    private void InitializeShop()
    {
        RefreshShop();
    }

    private void Reroll()
    {
        if (GameLogicManager.Instance.BuyReroll())
        {
            RefreshShop();
            AudioManager.Instance.PlayRollSoundEffect();   
        }
    }
    private void RefreshShop()
    {
        foreach (ShopSlot slot in ShopSlots)
        {
            UnitModel newUnit = GenerateUnit();
            slot.SetUnit(newUnit);
        }
    }

    private UnitModel GenerateUnit()
    {
        // Decide what cost of unit to generate
        List<float> odds = GameLogicManager.Instance.LevelOddsDictionary[GameLogicManager.Instance.PlayerLevel];
        float roll = Random.Range(0f, 1f); 
        float cumulative = 0f;
        float cost = 1;
        for (int i = 0; i < odds.Count; i++)
        {
            cumulative += odds[i];
            if (roll < cumulative)
            {
                cost = i + 1;
                break;
            }
        }
        
        // Get the pool of units that are possible to generate
        List<UnitModel> PotentialUnits;
        switch (cost)
        {
            case 1:
                PotentialUnits = UnitsPool.OneCosts;
                break;
            case 2:
                PotentialUnits = UnitsPool.TwoCosts;
                break;
            case 3:
                PotentialUnits = UnitsPool.ThreeCosts;
                break;
            case 4:
                PotentialUnits = UnitsPool.FourCosts;
                break;
            case 5:
                PotentialUnits = UnitsPool.FiveCosts;
                break;
            default:
                Debug.Log("Cost is fucked ?XD");
                PotentialUnits = UnitsPool.OneCosts;
                break;
        }
        
        // Choose a random unit within that pool
        return PotentialUnits[Random.Range(0, PotentialUnits.Count)];
    }

    private void BuyXP()
    {
        if (GameLogicManager.Instance.BuyXP())
        {
            AudioManager.Instance.PlayBuyXPSoundEffect();
        }
    }

    public void BeginFight()
    {
        FightImage.SetActive(false);
        FightButton.interactable = false;
        StartCoroutine(Fight());
    }

    public IEnumerator Fight()
    {
        yield return new WaitForSeconds(1f);
        if (GameLogicManager.Instance.AttemptFight())
        {
            NextRound();
        }
    }
    
    public void NextRound()
    {
        FightImage.SetActive(true);
        FightButton.interactable = true;
        GameLogicManager.Instance.NextRound();
        RefreshShop();
    }
}
