using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class CurseEvent : MonoBehaviour
{
    [SerializeField] private GameObject element = null;
    [SerializeField] private GameObject winElement = null;
    [SerializeField] private CursedOrb[] orbs = null; //Ensure orbs are in the elemental order provided by MapData.indexes
    [SerializeField] private Image orbImage = null;
    [SerializeField] private TextMeshProUGUI orbText = null;
    [SerializeField] private Button[] debuffChoices = null;

    private Player player = null;
    private bool ableToChoose = false;

    private void Awake()
    {
        Player.OnPlayerEnter += OnPlayerEnterEventHandler;
        Boss.OnAnyBossDefeated += OnAnyBossDefeatedEventHandler;
    }

    private void OnPlayerEnterEventHandler(Player player)
    {
        this.player = player;
    }

    private void OnAnyBossDefeatedEventHandler(int level)
    {
        if (level == orbs.Length - 1)
        {
            winElement.SetActive(true);
            return;
        }

        orbImage.sprite = orbs[level].image;
        orbText.text = "Cursed " + orbs[level].element + " Orb";
        ableToChoose = true;

        for (int i = 0; i < debuffChoices.Length; i++)
        {
            debuffChoices[i].GetComponentInChildren<TextMeshProUGUI>().text = orbs[level].curses[i].GetDescription();
            orbs[level].curses[i].SetPlayer(player);
            debuffChoices[i].onClick.AddListener(orbs[level].curses[i].ChangePlayerStats);
            debuffChoices[i].onClick.AddListener(OnCurseChosen);
        }

        element.SetActive(true);
    }

    private void OnCurseChosen()
    {
        Debug.Log(gameObject.name + ": " + SceneManager.GetActiveScene().name);
        // Prevents double activation from occurring on the second level completion
        if (ableToChoose)
        {
            SceneController.Instance.SwitchLevel("MapPhase");
            MapData.currentLevel++;
            PersistentPlayerStats.Instance.SavePlayerStats();
            ableToChoose = false;
        }        
    }

    private void OnDestroy()
    {
        Player.OnPlayerEnter -= OnPlayerEnterEventHandler;
        Boss.OnAnyBossDefeated -= OnAnyBossDefeatedEventHandler;
    }
}
