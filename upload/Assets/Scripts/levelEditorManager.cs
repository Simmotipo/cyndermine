using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Web;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class levelEditorManager : MonoBehaviour
{
    bool recording = false;

    // Start is called before the first frame update
    void Start()
    {
        recording = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void publish()
    {
        TMP_Dropdown dropdown = GameObject.Find("trackSelect").GetComponent<TMP_Dropdown>();

        string output = $"!trackname={dropdown.options[dropdown.value].text}N!scorefactor=10N";
        foreach (string s in GetComponent<scoreCounter>().record) output += $"{s}N";



        string levelData = "";
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://delivery.rakico.xyz:10000/set/" + output);
        request.Method = "GET";
        request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

        using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
        using (System.IO.Stream stream = response.GetResponseStream())
        using (StreamReader reader = new StreamReader(stream))
        {
            levelData = reader.ReadToEnd();
        }

        GameObject.Find("levelNameEntry").GetComponent<TMP_InputField>().text = levelData;

    }

    public void seek(int qty)
    {
        if (GetComponent<AudioSource>().time + qty < 0)GetComponent<AudioSource>().time = 0;
        else GetComponent<AudioSource>().time = GetComponent<AudioSource>().time + qty;
    }

    public void Reset()
    {
        SceneManager.LoadScene("levelRecorder");
    }

    public void backToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void recordButtonClicked()
    {
        if (recording)
        {
            recording = false;
            GetComponent<levelController>().playing = false;
            GetComponent<AudioSource>().Pause();
            Debug.Log("Paused");
            //stop recording
        }
        else
        {
            TMP_Dropdown dropdown = GameObject.Find("trackSelect").GetComponent<TMP_Dropdown>();
            GetComponent<spawnObjects>().trackname = dropdown.options[dropdown.value].text;
            recording = true;
            GetComponent<levelController>().playing = true;

        }
    }

    public void loadLevelClicked()
    {
        string levelName = GameObject.Find("levelNameEntry").GetComponent<TMP_InputField>().text.PadLeft(3, '0');
        try
        {
            string levelData = "";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://delivery.rakico.xyz:10000/get/" + levelName);
            request.Method = "GET";
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (System.IO.Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                levelData = reader.ReadToEnd();
            }


            levelData = HttpUtility.UrlDecode(levelData);

            if (levelData.Contains("?trackname"))
            {
                PlayerPrefs.SetString("selectedLevel", levelData);
                PlayerPrefs.SetInt("recordMode", 0);
                SceneManager.LoadScene("level1-day");
            }
            else
            {
                GameObject.Find("levelNameEntry").GetComponent<TMP_InputField>().text = $"Invalid Beatmap, or No Such Level";

            }
        }
        catch (Exception e)
        {
            GameObject.Find("levelNameEntry").GetComponent<TMP_InputField>().text = $"Error accessing: {e}";
            Console.ReadLine();
        }

    }
}
