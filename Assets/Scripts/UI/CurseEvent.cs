using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CurseEvent : MonoBehaviour
{
    [SerializeField] private GameObject element = null;
    [SerializeField] private CursedOrb[] orbs = null; //Ensure orbs are in the elemental order provided by MapData.indexes
    [SerializeField] private Image orbImage = null;
    [SerializeField] private TextMeshProUGUI orbText = null;
    [SerializeField] private Button[] debuffChoices = null;

    private Player player = null;

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
        orbImage.sprite = orbs[level].image;
        orbText.text = "Cursed" + orbs[level].element + "Orb";

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
        SceneController.Instance.SwitchLevel("MapPhase");
        MapData.currentLevel++;
    }

    private void OnDestroy()
    {
        Player.OnPlayerEnter -= OnPlayerEnterEventHandler;
        Boss.OnAnyBossDefeated -= OnAnyBossDefeatedEventHandler;
    }
}
