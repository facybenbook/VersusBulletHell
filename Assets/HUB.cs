﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HUB : MonoBehaviour {

    public static HUB S;

    public Text livesLeft, livesRight, weaponLeft, weaponRight, powerupLeft, powerupRight, countdownLeft, countdownRight;
    public GameObject BearShink, FishShink;
    public Image flash;

    public void UpdateLives()
    {
        int lives1 = Player.players[0].lives;
        int lives2 = Player.players[1].lives;
        livesLeft.text = "x" + lives1;
        livesRight.text = "x" + lives2;

    }

    public void UpdateWeapon()
    {
        int num = Player.players[0].upgrade_level + 1;
        weaponLeft.text = "Weapon Lv: " + num;
        num = Player.players[1].upgrade_level + 1;
        weaponRight.text = "Weapon Lv: " + num;
    }

    public void UpdatePowerup()
    {
        powerupLeft.text =/* (Player.players[0].powerup_points /(float) Player.players[0].POWERUPTHRESHOLD * 100f).ToString("F2") +*/ "\nAttack: " + Player.players[0].PowerupName;
        powerupRight.text = /*(Player.players[1].powerup_points / (float)Player.players[1].POWERUPTHRESHOLD * 100f).ToString("F2") + */"\nAttack: " + Player.players[1].PowerupName;

    }

    void Awake()
    {
        S = this;
    }

	// Use this for initialization
	void Start () {
        UpdateLives();
        UpdateWeapon();
	}

    IEnumerator animateFish()
    {
        PlaySound("Sharpen", 1);
        FishShink.SetActive(true);
        Color c = flash.color;
        c.a = 1;
        flash.color = c;
        for(int d = 0; d < 5; ++d)
        {
            yield return new WaitForSeconds(0.01f);
            c.a -= 0.2f;
            flash.color = c;
        }
        yield return new WaitForSeconds(0.5f);
        FishShink.SetActive(false);
    }

    IEnumerator animateBear()
    {
        PlaySound("Sharpen", 1);
        BearShink.SetActive(true);
        Color c = flash.color;
        c.a = 1;
        flash.color = c;
        for (int d = 0; d < 5; ++d)
        {
            yield return new WaitForSeconds(0.05f);
            c.a -= 0.2f;
            flash.color = c;
        }
        yield return new WaitForSeconds(0.5f);
        BearShink.SetActive(false);
    }

    public void FishUsedPowerupEffect()
    {
        StartCoroutine(animateFish());
    }

    public void BearUsedPowerupEffect()
    {

        StartCoroutine(animateBear());
    }



    public void PlaySound(string name, float volume = 1f)
    {
        GameObject g = new GameObject();
        AudioSource adsrc = g.AddComponent<AudioSource>();
        g.transform.position = Camera.main.transform.position;
        adsrc.spatialBlend = 0;
        AudioClip ac = Resources.Load("Sound/" + name) as AudioClip;
        adsrc.clip = ac;
        adsrc.volume = volume;
        adsrc.Play();
        Destroy(g, ac.length);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
