using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class settingsMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(PlayerPrefs.GetFloat("menusTime"));
        GameObject.Find("Main Camera").GetComponent<AudioSource>().time = PlayerPrefs.GetFloat("menusTime");
        GameObject.Find("Main Camera").GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("volumePercent") / 100f;
        GameObject.Find("buildNumber").GetComponent<TextMeshProUGUI>().text = "build4";
        GameObject.Find("volumeLabel").GetComponent<TextMeshProUGUI>().text = "Volume: " + Mathf.Round(PlayerPrefs.GetFloat("volumePercent")) + "%";
        GameObject.Find("volumeSlider").GetComponent<Slider>().value = PlayerPrefs.GetFloat("volumePercent");
        GameObject.Find("offsetSlider").GetComponent<Slider>().value = PlayerPrefs.GetFloat("beatOffset");
        GameObject.Find("offsetLabel").GetComponent<TextMeshProUGUI>().text = "Offset: " + PlayerPrefs.GetFloat("beatOffset") + "u";

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void resetSaveData()
    {
        PlayerPrefs.DeleteAll();
    }

    public void sliderSlided(string slider)
    {
        switch (slider)
        {
            case "volume":
                PlayerPrefs.SetFloat("volumePercent", GameObject.Find("volumeSlider").GetComponent<Slider>().value);
                GameObject.Find("volumeLabel").GetComponent<TextMeshProUGUI>().text = "Volume: " + Mathf.Round(GameObject.Find("volumeSlider").GetComponent<Slider>().value) + "%";
                break;
            case "offset":
                PlayerPrefs.SetFloat("beatOffset", GameObject.Find("offsetSlider").GetComponent<Slider>().value);
                GameObject.Find("offsetLabel").GetComponent<TextMeshProUGUI>().text = "Offset: " + GameObject.Find("offsetSlider").GetComponent<Slider>().value + "u";
                break;
        }
    }

    public void returnToMenu()
    {
        PlayerPrefs.SetFloat("menusTime", GameObject.Find("Main Camera").GetComponent<AudioSource>().time);
        PlayerPrefs.Save();
        SceneManager.LoadScene("mainMenu");
    }
}
