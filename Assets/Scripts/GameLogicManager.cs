using System.Collections.Generic;
using UnityEngine;

public class GameLogicManager : MonoBehaviour
{
    public static GameLogicManager Instance { get; private set; }
    
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
        { 9, 68 }
    };
    public int PlayerLevel { get; private set; } = 8;
    public int Difficulty { get; private set; } = 0;

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
}
