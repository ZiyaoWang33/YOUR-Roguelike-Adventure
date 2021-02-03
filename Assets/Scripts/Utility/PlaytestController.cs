using UnityEngine;
using System.Collections;

public class PlaytestController : SceneTransition
{
    [SerializeField] private MapData mapData = null;
    [SerializeField] private string levelToLoad = string.Empty;
    [SerializeField] private int levelNumber = 0;
    [SerializeField] private GameObject enemyToSpawn = null;
    [SerializeField] private float spawnWait = 0;
    [SerializeField] private string chosenElement = string.Empty;

    public void StartTest() // For use in an OnClick event
    {
        mapData.SetElement(chosenElement, this);
        SceneController.Instance.LoadLevel(levelToLoad);
        mapData.SetLevel(levelNumber, this);
    }

    private IEnumerator WaitToSpawn()
    {
        yield return new WaitForSeconds(spawnWait);
        GameObject enemy = Instantiate(enemyToSpawn);
        enemy.GetComponent<Enemy>().player = FindObjectOfType<Player>().GetComponent<Health>();
        enemy.SetActive(true);
    }

    public void SpawnEnemy() // For use in an OnClick event
    {
        StartCoroutine(WaitToSpawn());
    }
}
