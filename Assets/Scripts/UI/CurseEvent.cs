using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class CursedOrb
{
    public string element;
    public Sprite sprite;
    public Curse[] curses;
}

public class CurseEvent : MonoBehaviour
{
    [SerializeField] private GameObject element = null;
    [SerializeField] private GameObject winElement = null;
    [SerializeField] private CursedOrb[] orbs = null;
    [SerializeField] private Image orbImage = null;
    [SerializeField] private TextMeshProUGUI orbText = null;
    [SerializeField] private Button[] debuffChoices = null;

    private Dictionary<string, CursedOrb> orbDictionary = null;
    private Player player = null;
    private CursedOrb currentOrb = null;
    private PlayerPerformanceManager.Performance performance = PlayerPerformanceManager.Performance.NEUTRAL;
    private bool ableToChoose = false;

    private void Awake()
    {
        FillOrbDictionary();

        Player.OnPlayerEnter += OnPlayerEnterEventHandler;
        PlayerPerformanceManager.OnFinalPerformancChange += OnFinalPerformanceChangeEventHandler;
        Boss.OnAnyBossDefeated += OnAnyBossDefeatedEventHandler;
    }

    private void FillOrbDictionary()
    {
        // Keys should be the element of the orbs (check inspector)
        orbDictionary = new Dictionary<string, CursedOrb>()
        {
            { "fire", orbs[0] },
            { "water", orbs[1] },
            { "wood", orbs[2] }
        };
    }

    private void OnPlayerEnterEventHandler(Player player)
    {
        this.player = player;
    }

    private void OnFinalPerformanceChangeEventHandler(PlayerPerformanceManager.Performance performance)
    {
        this.performance = performance;
    }

    
    private void OnAnyBossDefeatedEventHandler(int level)
    {
        currentOrb = orbDictionary[MapData.currentElement];

        if (level == orbs.Length - 1)
        {
            winElement.SetActive(true);
            return;
        }

        orbImage.sprite = currentOrb.sprite;
        orbText.text = "<u>Cursed " + currentOrb.element + " Orb</u>";
        GetBuffDescription();
        ableToChoose = true;

        for (int i = 0; i < debuffChoices.Length; i++)
        {
            debuffChoices[i].GetComponentInChildren<TextMeshProUGUI>().text = currentOrb.curses[i].GetDescription();
            currentOrb.curses[i].SetDrawback(performance);
            debuffChoices[i].onClick.AddListener(currentOrb.curses[i].ChangePlayerStats);
            debuffChoices[i].onClick.AddListener(OnCurseChosen);
        }

        element.SetActive(true);
    }


    private void GetBuffDescription()
    {
        orbText.text += "\n";

        switch (MapData.currentElement)
        {
            case "fire":
                orbText.text += "Increased Damage";
                break;

            case "water":
                orbText.text += "Increased Speed";
                break;

            case "wood":
                orbText.text += "Healing Bullets";
                break;
        }
    }

    private void OnCurseChosen()
    {
        string debugMessage = gameObject.name + ": " + SceneManager.GetActiveScene().name + " Is";
        if (ableToChoose) // Prevents double activation from occurring on the second level completion
        {
            PersistentPlayerStats.Instance.SavePlayerStats();
            SceneController.Instance.SwitchLevel("MapPhase");
            MapData.currentLevel++;
            ableToChoose = false;
            debugMessage += " No Longer";
        }
        Debug.Log(debugMessage + " Active");

        for (int i = 0; i < debuffChoices.Length; i++)
        {
            debuffChoices[i].onClick.RemoveListener(currentOrb.curses[i].ChangePlayerStats);
            debuffChoices[i].onClick.RemoveListener(OnCurseChosen);
        }
    }

    private void OnDestroy()
    {
        Player.OnPlayerEnter -= OnPlayerEnterEventHandler;
        PlayerPerformanceManager.OnFinalPerformancChange -= OnFinalPerformanceChangeEventHandler;
        Boss.OnAnyBossDefeated -= OnAnyBossDefeatedEventHandler;
    }
}
