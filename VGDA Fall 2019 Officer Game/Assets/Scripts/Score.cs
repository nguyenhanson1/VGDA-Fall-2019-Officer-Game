using UnityEngine;
using UnityEngine.UI;
public class Score : MonoBehaviour {

    public Text scoreText;
    public static int score = 0;
    private void Awake()
    {
        scoreText = GetComponent<Text>();
    }
    // Update is called once per frame
    void Update() {
        scoreText.text = "Score: " + score;
        
    }
}
