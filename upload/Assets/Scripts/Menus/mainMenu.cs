using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class mainMenu : MonoBehaviour
{
    public bool resetSaveData = false;

    //Start is called before the first frame update
    void Start()
    {
        Debug.Log(PlayerPrefs.GetFloat("menusTime"));
        createSave();
        GameObject.Find("Main Camera").GetComponent<AudioSource>().time = PlayerPrefs.GetFloat("menusTime");
        GameObject.Find("Main Camera").GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("volumePercent") / 100f;
    }

    //Update is called once per frame
    void Update()
    {
        if (resetSaveData) { PlayerPrefs.DeleteAll(); createSave();  resetSaveData = false; }
    }

    public void loadScene(string sceneName)
    {
        PlayerPrefs.SetFloat("menusTime", GameObject.Find("Main Camera").GetComponent<AudioSource>().time);
        PlayerPrefs.Save();
        SceneManager.LoadScene(sceneName);
    }

    public void levelEditorClicked()
    {
        SceneManager.LoadScene("levelRecorder");
        PlayerPrefs.SetInt("recordMode", 1);
        PlayerPrefs.SetString("selectedLevel", "level0");
        PlayerPrefs.Save();
    }
    public void quit()
    {
        PlayerPrefs.Save();
        Application.Quit();
        Debug.Log("I would have quit.");
    }
    public void createSave()
    {

        if (PlayerPrefs.GetInt("saveVersion") == 5) return;
        Debug.Log("Creating new Save Data");

        PlayerPrefs.SetFloat("menusTime", 0f);
        PlayerPrefs.SetString("vanUnlocks", "eng=f,aus=f,usa=t,gay=f");
        PlayerPrefs.SetString("selectedVan", "usa");
        PlayerPrefs.SetFloat("earnedScore", 0f);
        PlayerPrefs.SetFloat("spentScore", 0f);
        PlayerPrefs.SetInt("saveVersion", 5);
        PlayerPrefs.SetInt("Jammy", 0);
        PlayerPrefs.SetString("levelUnlocks", "1.1=t,1.2=f,1.3=f,2.1=f,2.2=f,3.1=f,4.1=f,4.2=f,5.1=f");
        PlayerPrefs.SetString("levelRecords", "1.1=0,1.2=0,1.3=0,2.1=0,2.2=0,3.1=0,4.1=0,4.2=0,5.1=0");
        PlayerPrefs.SetFloat("volumePercent", 30f);
        PlayerPrefs.SetFloat("beatOffset", 0.19f);
        PlayerPrefs.SetInt("recordMode", 0);
        PlayerPrefs.SetString("selectedLevel", "");
        PlayerPrefs.Save();

    }
}