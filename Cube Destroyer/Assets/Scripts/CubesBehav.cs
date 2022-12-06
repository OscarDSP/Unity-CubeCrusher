using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;
using static UnityEngine.ParticleSystem;

public class CubesBehav : MonoBehaviour
{
    [Header("Velocity")]
    [SerializeField] private float minSpeed;
    [SerializeField] private float maxSpeed;
	[SerializeField] private float speed;

    [Header("Position")]
    [SerializeField] private float posX;
	[SerializeField] private float posY;

    [Header("Components")]
    [SerializeField] private GameObject expParticles;

    [Header("Audio")]
    [SerializeField] private AudioSource expAudio;

    private Rigidbody2D rb;
    private LevelManager levelManager;
    private EnemyManager enemyManager;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
		levelManager = FindObjectOfType<LevelManager>();
        enemyManager = FindObjectOfType<EnemyManager>();
    }
    void Start()
    {
	}

    void FixedUpdate()
    {
        MovementSystem();
    }

    private void MovementSystem()
    {
        if (levelManager.GetState() == 1)
            rb.velocity = new Vector2(0, Vector2.down.y * speed);
        else
        {
            rb.velocity = Vector2.zero;
            StartParticles();
            transform.position = new Vector3(posX, posY, 0);
            gameObject.SetActive(false);
        }
    }

    private void OnMouseDown()
    {
        StartParticles();
        transform.position = new Vector3(posX, posY, 0);
        speed = Random.Range(minSpeed, maxSpeed);
        maxSpeed += 1;
        levelManager.UpdateScore(1);
        if(levelManager.GetScore() <= 11)
        {
            gameObject.SetActive(false);
            enemyManager.ActivateCubes();
        }
    }

    private void StartParticles()
    {
		Instantiate(expParticles,
						new Vector3(this.transform.position.x,
						this.transform.position.y,
						expParticles.transform.position.z),
						expParticles.transform.rotation);
        expAudio.Play();
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
		levelManager.SetState(2);
	}

    public void Resetvalues()
    {
        speed = 1;
        maxSpeed = 5;
    }
}
