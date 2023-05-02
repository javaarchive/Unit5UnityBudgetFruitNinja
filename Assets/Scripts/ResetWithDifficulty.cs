using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResetWithDifficulty : MonoBehaviour
{

    private Button button;

    private GameManager gmScript;

    public float spawnRateForDifficulty = 10f;

    void Awake(){
        gmScript = GameObject.Find("SpawnManager").GetComponent<GameManager>();
        button = GetComponent<Button>();
    }

    // Start is called before the first frame update
    void Start()
    {
        // https://docs.unity3d.com/2021.3/Documentation/ScriptReference/UIElements.Button-onClick.html
        // https://docs.unity3d.com/2021.3/Documentation/ScriptReference/UIElements.Clickable-clicked.html 
        // onclick was deprecated/obselete
        button.onClick.AddListener(SetDifficultyAndReset);
        // button.clicked += SetDifficultyAndReset;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetDifficultyAndReset(){
        Debug.Log("Reset called");
        gmScript.ResetGame(this.spawnRateForDifficulty);
    }
}
