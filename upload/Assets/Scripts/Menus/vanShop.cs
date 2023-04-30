using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class vanShop : MonoBehaviour
{

    GameObject usa;
    GameObject eng;
    GameObject aus;
    GameObject gay;

    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("Main Camera").GetComponent<AudioSource>().time = PlayerPrefs.GetFloat("menusTime");
        GameObject.Find("Main Camera").GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("volumePercent") / 100f;
        PlayerPrefs.Save();
        usa = GameObject.Find("van_usa_button");
        eng = GameObject.Find("van_eng_button");
        aus = GameObject.Find("van_aus_button");
        gay = GameObject.Find("van_gay_button");
        updateGUI();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("`"))
        {
            PlayerPrefs.SetInt("Jammy", 1);
            PlayerPrefs.SetFloat("earnedScore", PlayerPrefs.GetFloat("earnedScore") + 5000f);
            updateGUI();
        }
    }

    void updateGUI()
    {
        GameObject.Find("pointsLabel").GetComponent<TextMeshProUGUI>().text = "Points to Spend: " + Convert.ToString(PlayerPrefs.GetFloat("earnedScore") - PlayerPrefs.GetFloat("spentScore"));


        switch (PlayerPrefs.GetString("selectedVan"))
        {
            case "usa":
                usa.GetComponent<Button>().interactable = false;
                usa.GetComponentInChildren<TextMeshProUGUI>().text = "Selected";
                break;

            case "eng":
                eng.GetComponent<Button>().interactable = false;
                eng.GetComponentInChildren<TextMeshProUGUI>().text = "Selected";
                break;

            case "aus":
                aus.GetComponent<Button>().interactable = false;
                aus.GetComponentInChildren<TextMeshProUGUI>().text = "Selected";
                break;

            case "gay":
                gay.GetComponent<Button>().interactable = false;
                gay.GetComponentInChildren<TextMeshProUGUI>().text = "Selected";
                break;
        }

        foreach (string van in PlayerPrefs.GetString("vanUnlocks").Split(","))
        {

            Debug.Log(van);

            if (van.Split('=')[1] == "t")
            {
                GameObject.Find($"van_{van.Split('=')[0]}").GetComponent<Image>().sprite = Resources.Load<Sprite>($"Sprites/van_{van.Split('=')[0]}");
                if (PlayerPrefs.GetString("selectedVan") != van.Split('=')[0])
                {
                    switch (van.Split('=')[0])
                    {
                        case "usa":
                            usa.GetComponent<Button>().interactable = true;
                            usa.GetComponentInChildren<TextMeshProUGUI>().text = "Select";
                            break;

                        case "eng":
                            eng.GetComponent<Button>().interactable = true;
                            eng.GetComponentInChildren<TextMeshProUGUI>().text = "Select";
                            break;

                        case "aus":
                            aus.GetComponent<Button>().interactable = true;
                            aus.GetComponentInChildren<TextMeshProUGUI>().text = "Select";
                            break;

                        case "gay":
                            gay.GetComponent<Button>().interactable = true;
                            gay.GetComponentInChildren<TextMeshProUGUI>().text = "Select";
                            break;
                    }
                }
            }
            else
            {
                GameObject.Find($"van_{van.Split('=')[0]}").GetComponent<Image>().sprite = Resources.Load<Sprite>($"Sprites/van_{van.Split('=')[0]}_locked");
                switch (van.Split('=')[0])
                {
                    case "usa":
                        usa.GetComponent<Button>().interactable = true;
                        usa.GetComponentInChildren<TextMeshProUGUI>().text = "Unlock for 0";
                        break;

                    case "eng":
                        eng.GetComponent<Button>().interactable = true;
                        eng.GetComponentInChildren<TextMeshProUGUI>().text = "Unlock for 10,000pts";
                        break;

                    case "aus":
                        aus.GetComponent<Button>().interactable = true;
                        aus.GetComponentInChildren<TextMeshProUGUI>().text = "Unlock for 25,000pts";
                        break;

                    case "gay":
                        gay.GetComponent<Button>().interactable = true;
                        gay.GetComponentInChildren<TextMeshProUGUI>().text = "Unlock for 69,420pts";
                        break;
                }
            }
        }

    }

    public void vanButtonClicked(string van)
    {
        switch (van)
        {
            case "usa":
                if (usa.GetComponentInChildren<TextMeshProUGUI>().text.Contains("Select")) selectVan(van);
                break;
            case "eng":
                if (eng.GetComponentInChildren<TextMeshProUGUI>().text.Contains("Select")) selectVan(van);
                else unlockVan(van);
                break;
            case "aus":
                if (aus.GetComponentInChildren<TextMeshProUGUI>().text.Contains("Select")) selectVan(van);
                else unlockVan(van);
                break;
            case "gay":
                if (gay.GetComponentInChildren<TextMeshProUGUI>().text.Contains("Select")) selectVan(van);
                else unlockVan(van);
                break;
        }
        updateGUI();
    }

    public void unlockVan(string van)
    {
        switch (van)
        {
            case "usa":
                break;
            case "eng":
                if (PlayerPrefs.GetFloat("earnedScore") - PlayerPrefs.GetFloat("spentScore") > 9999)
                {
                    eng.GetComponent<Button>().interactable = true;
                    eng.GetComponentInChildren<TextMeshProUGUI>().text = "Select";
                    PlayerPrefs.SetFloat("spentScore", PlayerPrefs.GetFloat("spentScore") + 10000);
                    PlayerPrefs.SetString("vanUnlocks", PlayerPrefs.GetString("vanUnlocks").Replace("eng=f", "eng=t"));
                }
                break;
            case "aus":
                if (PlayerPrefs.GetFloat("earnedScore") - PlayerPrefs.GetFloat("spentScore") > 24999)
                {
                    aus.GetComponent<Button>().interactable = true;
                    aus.GetComponentInChildren<TextMeshProUGUI>().text = "Select";
                    PlayerPrefs.SetFloat("spentScore", PlayerPrefs.GetFloat("spentScore") + 25000);
                    PlayerPrefs.SetString("vanUnlocks", PlayerPrefs.GetString("vanUnlocks").Replace("aus=f", "aus=t"));
                }
                break;
            case "gay":
                if (PlayerPrefs.GetFloat("earnedScore") - PlayerPrefs.GetFloat("spentScore") > 69419)
                {
                    gay.GetComponent<Button>().interactable = true;
                    gay.GetComponentInChildren<TextMeshProUGUI>().text = "Select";
                    PlayerPrefs.SetFloat("spentScore", PlayerPrefs.GetFloat("spentScore") + 69420);
                    PlayerPrefs.SetString("vanUnlocks", PlayerPrefs.GetString("vanUnlocks").Replace("gay=f", "gay=t"));
                }
                break;
        }
        updateGUI();
    }

    public void selectVan(string van)
    {
        PlayerPrefs.SetString("selectedVan", van);

        if (van == "usa")
        {
            usa.GetComponent<Button>().interactable = false;
            usa.GetComponentInChildren<TextMeshProUGUI>().text = "Selected";
        }
        else if (!usa.GetComponent<Button>().interactable)
        {

            usa.GetComponent<Button>().interactable = true;
            usa.GetComponentInChildren<TextMeshProUGUI>().text = "Select";
        }

        if (van == "eng")
        {
            eng.GetComponent<Button>().interactable = false;
            eng.GetComponentInChildren<TextMeshProUGUI>().text = "Selected";
        }
        else if (!eng.GetComponent<Button>().interactable)
        {

            eng.GetComponent<Button>().interactable = true;
            eng.GetComponentInChildren<TextMeshProUGUI>().text = "Select";
        }

        if (van == "aus")
        {
            aus.GetComponent<Button>().interactable = false;
            aus.GetComponentInChildren<TextMeshProUGUI>().text = "Selected";
        }
        else if (!aus.GetComponent<Button>().interactable)
        {

            aus.GetComponent<Button>().interactable = true;
            aus.GetComponentInChildren<TextMeshProUGUI>().text = "Select";
        }

        if (van == "gay")
        {
            gay.GetComponent<Button>().interactable = false;
            gay.GetComponentInChildren<TextMeshProUGUI>().text = "Selected";
        }
        else if (!aus.GetComponent<Button>().interactable)
        {

            gay.GetComponent<Button>().interactable = true;
            gay.GetComponentInChildren<TextMeshProUGUI>().text = "Select";
        }

    }

    public void back()
    {
        PlayerPrefs.SetFloat("menusTime", GameObject.Find("Main Camera").GetComponent<AudioSource>().time);
        PlayerPrefs.Save();
        SceneManager.LoadScene("MainMenu");
    }
}
