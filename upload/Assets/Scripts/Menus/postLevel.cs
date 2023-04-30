using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class postLevel : MonoBehaviour
{
    string[] inputs;
    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("Main Camera").GetComponent<AudioSource>().time = PlayerPrefs.GetFloat("menusTime");
        GameObject.Find("Main Camera").GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("volumePercent") / 100f;
        Debug.Log(PlayerPrefs.GetString("postLevel"));
        inputs = PlayerPrefs.GetString("postLevel").Split(',');
        GameObject.Find("scoreText").GetComponent<TextMeshProUGUI>().text = $"You scored {Mathf.Round(float.Parse(inputs[1]))}pts out of {float.Parse(inputs[2])} on a '{inputs[0]}' beatmap!";

        if (inputs[^1] == "c") GameObject.Find("backButtonLabel").GetComponent<TextMeshProUGUI>().text = "Back to Beatmap Editor";
        else GameObject.Find("backButtonLabel").GetComponent<TextMeshProUGUI>().text = "Back to Level Select";
    }

    public void backButtonPressed()
    {
        if (inputs[^1] == "c")
        {
            SceneManager.LoadScene("levelRecorder");
            PlayerPrefs.SetInt("recordMode", 1);
            PlayerPrefs.SetString("selectedLevel", "level0");
            PlayerPrefs.Save();
            SceneManager.LoadScene("levelRecorder");
        }
        else
        {
            PlayerPrefs.SetFloat("menusTime", GameObject.Find("Main Camera").GetComponent<AudioSource>().time);
            PlayerPrefs.Save();
            SceneManager.LoadScene("levelSelect");
        }
    }
}
