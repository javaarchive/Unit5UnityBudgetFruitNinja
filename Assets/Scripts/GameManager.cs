using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    public GameObject[] targetPrefabs;

    [SerializeField]
    public GameObject[] targetHalves;

    private int points = 0;
    private int lives = 5;

    public bool CheckAlive(){
        return lives > 0;
    }

    public void SubtractLife(){
        if(CheckAlive()) lives --; // sometimes you can run out of lives while fruit is still on the screen and have negative lives because of those falling, we ensure lives srays positive here. 
        UpdateScore();
        if(!CheckAlive()) OnDeath();    
    }

    [SerializeField]
    private TextMeshProUGUI scoreText;

    [SerializeField]
    private TextMeshProUGUI gameOverText;

    [SerializeField]
    private GameObject gameOverGroup;

    [SerializeField]
    private UnityEngine.UI.Button gameOverMenuButton;

    [SerializeField]
    private GameObject menuGroup;

    private void UpdateScore(){
        scoreText.text = "Score: " + points + "\n" + "Lives: " + lives;
    }

    public void ChangeScore(int amount){
        this.points += amount;
        UpdateScore();
    }

    private float spawnRate = 0.5f;

    private IEnumerator spawningCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        points = 0;
        lives = 5;
        spawningCoroutine = SpawnTarget();
        // StartCoroutine(spawningCoroutine);
        UpdateScore();
        // scoreText.gameObject.SetActive(false); // hide score as we are in menu
        
        // make the return to menu button for game over work. 
        gameOverMenuButton.onClick.AddListener(EnterMenu);

        EnterMenu();
    }

    void EnterMenu(){
        if(spawningCoroutine != null){
            StopCoroutine(spawningCoroutine);
            spawningCoroutine = null;
        }
        scoreText.gameObject.SetActive(false);
        menuGroup.gameObject.SetActive(true);
        gameOverGroup.gameObject.SetActive(false);
    }

    private IEnumerator SpawnTarget(){
        while(CheckAlive()){
            int choice = Random.Range(0, targetPrefabs.Length);
            GameObject fruit = targetPrefabs[choice];
            Instantiate(fruit, StartingPosition(), fruit.transform.rotation);
            yield return new WaitForSeconds(spawnRate);
        }
    }

    private Vector3 StartingPosition(){
        float x = Random.Range(-4.5f, -5f);
        Vector3 location = new Vector3(x,0, -1f);
        return location;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetGame(float spawnRate = 1f){
        Debug.Log("Resetting with spawn rate " + spawnRate + " secs ");
        this.spawnRate = spawnRate;
        points = 0;
        lives = 5;
        UpdateScore();
        if(spawningCoroutine != null){
            StopCoroutine(spawningCoroutine);
        }
        spawningCoroutine = SpawnTarget();
        StartCoroutine(spawningCoroutine);
        scoreText.gameObject.SetActive(true);
        menuGroup.gameObject.SetActive(false);
        // gameOverText.gameObject.SetActive(false);
        gameOverGroup.gameObject.SetActive(false);
    }

    private void OnDeath(){
        // gameOverText.gameObject.SetActive(true);
        // instead of just controlling the text I find it easier to ref a group GameObject
        gameOverGroup.gameObject.SetActive(true);

        // alternatively we can StopCoroutine the spawning logic here but changing it in the while loop also works
        if(spawningCoroutine != null) StopCoroutine(spawningCoroutine);
        spawningCoroutine = null;
        // there is a rare case that can cause two coroutines to be running at once so I am stopping it anyways
    }
}
