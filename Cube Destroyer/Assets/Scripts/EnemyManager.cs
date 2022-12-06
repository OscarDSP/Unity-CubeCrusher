using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private int startObject;
	
    public List<GameObject> enemies;
    private LevelManager levelManager;
    void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
    }

    void Update()
    {
    }

    private void SetUpTmps(int p_tmp)
    {
		if (!enemies[p_tmp].activeSelf)
		{
			enemies[p_tmp].SetActive(true);
		}
	}

    public void ActivateCubes()
    {
		if (levelManager.GetScore() >= 11)
		{
			for (int i = 0; i < enemies.Count; i++)
			{
				SetUpTmps(i);
			}
		}
		else if (levelManager.GetScore() >= 4 && levelManager.GetScore() <= 10)
		{
			int tmp = Random.Range(0, enemies.Count-1);
			int tmp2 = Random.Range(0, enemies.Count-1);
			if (tmp == tmp2 && tmp2 == enemies.Count)
			{
				tmp--;
			}
			else if (tmp == tmp2)
			{
				tmp++;
			}

			SetUpTmps(tmp);
			SetUpTmps(tmp2);
		}
		else if (levelManager.GetScore() <= 3)
		{
			int tmp = Random.Range(0, enemies.Count - 1);
			SetUpTmps(tmp);
		}
	}

	public void StartGame()
    {
		int tmp = Random.Range(0, enemies.Count - 1);
		enemies[tmp].gameObject.SetActive(true);
	}

	public void Resetvalues()
    {
		for(int i = 0; i< enemies.Count; i++)
        {
			enemies[i].GetComponent<CubesBehav>().Resetvalues();
        }
		StartGame();
    }

	public void ResetForHome()
    {
		for (int i = 0; i < enemies.Count; i++)
		{
			enemies[i].GetComponent<CubesBehav>().Resetvalues();
		}
	}
}
