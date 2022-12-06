using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private int score, gameState, newColor;
    [SerializeField] private TextMeshProUGUI scoreT,highscoreT;
    [SerializeField] private EnemyManager enemyManager;
    [SerializeField] private GameObject DeathMenu, Cube3D;
    [SerializeField] private Material cubeMat;
    [SerializeField] private ParticleSystem particles;
    [SerializeField] private float rotX, rotY, rotZ;

    [Header("TextWithColors")]
    public List<TextMeshProUGUI> allText;
    
    private Color blueOne, blueTwo, green, red, yellow, pink;
    private bool deleted = false;

    private void Awake()
    {
        SetUpColor();
    }

    void Start()
    {
        gameState = 0;
        highscoreT.text = "High Score: " 
            + PlayerPrefs.GetInt("HighScore", 0).ToString();
    }

    void Update()
    {
        scoreT.text = score.ToString();
        if (gameState == 0)
        {
            Cube3D.transform.Rotate(rotX, rotY, rotZ);
        }

        if (gameState == 2)
            DeathMenu.SetActive(true);

        if (score > PlayerPrefs.GetInt("HighScore"))
            PlayerPrefs.SetInt("HighScore", score);
    }

    private void SetUpColor()
    {
        blueOne = new Color(0, 0.227451f, 1);
        blueTwo = new Color(0, 1, 0.9607843f);
        green = new Color(0.003921569f, 1, 0);
        yellow = new Color(1, 0.8941177f, 0);
        red = new Color(1, 0, 0.01960784f);
        pink = new Color(1, 0, 0.8588235f);

        newColor = Random.Range(0, 6);
        switch (newColor){
            case 0:
                ChangeColor(blueOne);
                break;
            case 1:
                ChangeColor(blueTwo);
                break;
            case 2:
                ChangeColor(green);
                break;
            case 3:
                ChangeColor(yellow);
                break;
            case 4:
                ChangeColor(red);
                break;
            case 5:
                ChangeColor(pink);
                break;
        }
    }

    private void ChangeColor(Color p_color)
    {
        var main = particles.main;
        main.startColor = p_color;
        for(int i = 0; i< allText.Count; i++)
        {
            allText[i].color = p_color;
        }

        for(int i = 0; i <enemyManager.enemies.Count; i++)
        {
            enemyManager.enemies[i].GetComponent<SpriteRenderer>().color = p_color;
        }

        var col = particles.colorOverLifetime;
        Gradient grad = new Gradient();
        grad.SetKeys(new GradientColorKey[] {
            new GradientColorKey(p_color, 0.0f), 
            new GradientColorKey(Color.black, 1.0f) }, 
            new GradientAlphaKey[] { 
                new GradientAlphaKey(1.0f, 0.0f), 
                new GradientAlphaKey(0.0f, 1.0f) });

        col.color = grad;

        cubeMat.color = p_color;
    }

    public int GetScore()
    {
        return score;
    }

    public void UpdateScore(int p_value)
    {
        score += p_value;
    }

    public int GetState()
    {
        return gameState;
    }

    public void SetState(int p_value)
    {
        gameState = p_value;
    }

    public void StartGame()
    {
        gameState = 1;
        enemyManager.StartGame();
    }

	public void DeleteRecords()
	{
		PlayerPrefs.DeleteKey("HighScore");
		deleted = true;
	}

	public void UpdateHighScore()
	{
		if (deleted)
			highscoreT.text = "High Score: " + 0.ToString();

		deleted = false;
	}

	public void GoHome()
    {
        gameState = 0;
        score = 0;
		highscoreT.text = "High Score: "
			+ PlayerPrefs.GetInt("HighScore", 0).ToString();
		SetUpColor();
        enemyManager.ResetForHome();
    }

    public void ResetValues()
    {
        gameState = 1;
        score = 0;
        SetUpColor();
        enemyManager.Resetvalues();
    }

	public void LinkReader(string link)
	{
		Application.OpenURL(link);
	}
}
