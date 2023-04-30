using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class levelSelect : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        string levelUnlocks = PlayerPrefs.GetString("levelUnlocks");

        GameObject.Find("Main Camera").GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("volumePercent") / 100f;
        GameObject.Find("Main Camera").GetComponent<AudioSource>().time = PlayerPrefs.GetFloat("menusTime");

        Debug.Log(levelUnlocks);
        foreach (string level in levelUnlocks.Split(','))
        {
            Debug.Log(level);
            Debug.Log(level.Split('=')[1] + " for " + level.Split('=')[0]);
            if (level.Split('=')[1] == "t") GameObject.Find(level.Split("=")[0]).GetComponent<Button>().interactable = true;
            else GameObject.Find(level.Split("=")[0]).GetComponent<Button>().interactable = false;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void moveButtonPress()
    {

    }

    public void unlockAll()
    {
        string levelUnlocks = PlayerPrefs.GetString("levelUnlocks").Replace("1.2=f", "1.2=t").Replace("1.3=f", "1.3=t").Replace("2.1=f", "2.1=t").Replace("2.2=f", "2.2=t").Replace("3.1=f", "3.1=t").Replace("4.1=f", "4.1=t").Replace("4.2=f", "4.2=t");
        PlayerPrefs.SetString("levelUnlocks", levelUnlocks);
        PlayerPrefs.SetInt("Jammy", 1);
        PlayerPrefs.Save();
        Start();
    }

    public void mainMenu()
    {
        PlayerPrefs.SetFloat("menusTime", GameObject.Find("Main Camera").GetComponent<AudioSource>().time);
        PlayerPrefs.Save();
        SceneManager.LoadScene("mainMenu");
    }

    public void playLevel(string selectedLevel)
    {
        PlayerPrefs.SetInt("recordMode", 0);
        switch (selectedLevel)
        {
            case "1.1":
                PlayerPrefs.SetString("selectedLevel", "level0");
                PlayerPrefs.Save();
                SceneManager.LoadScene("level1-day");
                PlayerPrefs.SetInt("recordMode", 0);
                break;
            case "1.2":
                PlayerPrefs.SetString("selectedLevel", "level1");
                PlayerPrefs.Save();
                SceneManager.LoadScene("level1-night");
                PlayerPrefs.SetInt("recordMode", 0);
                break;
            case "1.3":
                PlayerPrefs.SetString("selectedLevel", "level2");
                PlayerPrefs.Save();
                SceneManager.LoadScene("level1-day");
                PlayerPrefs.SetInt("recordMode", 0);
                break;
            case "2.1":
                PlayerPrefs.SetString("selectedLevel", "2.1");
                PlayerPrefs.Save();
                SceneManager.LoadScene("desert-day");
                PlayerPrefs.SetInt("recordMode", 0);
                break;
            case "2.2":
                PlayerPrefs.SetString("selectedLevel", "2.2");
                PlayerPrefs.Save();
                SceneManager.LoadScene("desert-night");
                PlayerPrefs.SetInt("recordMode", 0);
                break;
            case "3.1":
                PlayerPrefs.SetString("selectedLevel", "3.1");
                PlayerPrefs.Save();
                SceneManager.LoadScene("city-day");
                PlayerPrefs.SetInt("recordMode", 0);
                break;
            case "4.1":
                PlayerPrefs.SetString("selectedLevel", "4.1");
                PlayerPrefs.Save();
                SceneManager.LoadScene("spaceScene");
                PlayerPrefs.SetInt("recordMode", 0);
                break;
            case "4.2":
                PlayerPrefs.SetString("selectedLevel", "4.2");
                PlayerPrefs.Save();
                SceneManager.LoadScene("spaceScene");
                PlayerPrefs.SetInt("recordMode", 0);
                break;
            case "5.1":
                PlayerPrefs.SetString("selectedLevel", "5.1");
                PlayerPrefs.Save();
                SceneManager.LoadScene("sea_night");
                PlayerPrefs.SetInt("recordMode", 0);
                break;

        }
    }
}
