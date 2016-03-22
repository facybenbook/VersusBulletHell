﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Global : MonoBehaviour {
    const string ALLY_LAYER_SUFFIX = "Ally";
    const string ALLY_BULLET_LAYER_SUFFIX = "AllyBullet";

    public static Global S;

    public GameObject wave;
    public GameObject whirlpool;
    public GameObject tidalWaveWarning;
    public GameObject whirlpoolWarning;
    public bool enableEvents;
    GameObject spawner1;
    GameObject spawner2;
    public bool player1tut;
    public bool player2tut;

    public float gameTimer;

    public void destroyLevelEnemies(Vector2 position, string enemy_layer_prefix) {
        GameObject[] enemy_bullets = GameObject.FindGameObjectsWithTag(enemy_layer_prefix + ALLY_BULLET_LAYER_SUFFIX);
        foreach (GameObject enemy_bullet in enemy_bullets) {
            enemy_bullet.SendMessage("Dissipate");
        }

        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemy_layer_prefix + ALLY_LAYER_SUFFIX);
        foreach (GameObject enemy in enemies) {
            Destroy(enemy);
        }
    }

    IEnumerator pickRandomEvent()
    {
        while (true)
        {
            float interval = Random.Range(15f, 30f);
            yield return new WaitForSeconds(interval);

            if (!enableEvents) yield break;
            int rand = Random.Range(1, 3);
            switch (rand)
            {
                case 1:
                    StartCoroutine(makewaves());
                    break;
                case 2:
                    StartCoroutine(makeWhirlpools());
                    break;
            }
        }
    }

    IEnumerator makewaves()
    {
        HUB.S.PlaySound("Siren", 1f);
        tidalWaveWarning.SetActive(true);
            EventIndicator.Panels.SetPanel(0);
            yield return new WaitForSeconds(2f);
            int rand = Random.Range(10, 20);
        HUB.S.PlaySound("TidalWaves", 1f);
            for (int c = 0; c < rand; ++c)
            {
                float xpos = Random.Range(-9f, 9f);
                float ypos = Random.Range(5f, 7f);
                Instantiate(wave, new Vector3(xpos, ypos, 0f), transform.rotation);
            }
            tidalWaveWarning.SetActive(false);
    }

    IEnumerator makeWhirlpools()
    {
        HUB.S.PlaySound("Siren", 1f);
        whirlpoolWarning.SetActive(true);
            EventIndicator.Panels.SetPanel(0);
            yield return new WaitForSeconds(2f);
            int rand = Random.Range(1, 3);

        HUB.S.PlaySound("TidalWaves", 1f);
        for (int c = 0; c < rand; ++c)
            {
                float xpos = Random.Range(-3.5f, 3.5f);
                float ypos = Random.Range(-4f, 4f);
                Instantiate(whirlpool, new Vector3(xpos, ypos, 0f), transform.rotation);
            }
            whirlpoolWarning.SetActive(false);
    }

    void Awake()
    {
        S = this;
    }

	// Use this for initialization
	void Start () {
        gameTimer = 0;
        enableEvents = false;
        spawner1 = GameObject.Find("Spawner");
        spawner2 = GameObject.Find("Spawner (1)");
        spawner1.SetActive(false);
        spawner2.SetActive(false);
        StartCoroutine(pickRandomEvent());
        Instantiate(Resources.Load("FullFish"));
        Instantiate(Resources.Load("FullBear"));
	}
	
	// Update is called once per frame
	void Update () {
        gameTimer += Time.deltaTime;
        if (gameTimer > 3)
        {
            spawner1.SetActive(true);
            spawner2.SetActive(true);
        }
        player1tut = spawner1.GetComponent<Spawner>().tutorial;
        player2tut = spawner2.GetComponent<Spawner>().tutorial;
        if (!player1tut && !player2tut) enableEvents = true;
    }
}
