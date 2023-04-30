using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class scoreCounter : MonoBehaviour
{

    bool playing;
    double pixelsPerSecond = -1;
    public List<string> beatTimes;
    public List<string> beatKeys;
    public List<GameObject> beatObjects;
    float xOffset;
    levelController ctrl;
    double score = -1;
    public TextMeshProUGUI scoreText;
    public float scoreSensitivity = 0.15f;

    public float scoreFactor = 100;

    float recordLastX = 0;

    public List<string> record;

    public bool recordMode = false;
    double totalScoreAvailable = 0;

    public int damagedSpriteCount = 1;
    public int goodSpriteCount = 2;


    int recordOffset = 0;

    // Start is called before the first frame update
    void Start()
    {
        pixelsPerSecond = GetComponent<spawnObjects>().pixelsPerSecond;
        scoreSensitivity = scoreSensitivity * (float)pixelsPerSecond;
        playing = GetComponent<levelController>().playing;
        ctrl = GetComponent<levelController>();
        xOffset = GetComponent<spawnObjects>().xOffset;

        scoreText = GameObject.Find("scoreText").GetComponent<TextMeshProUGUI>();


        if (recordMode) record = new List<string>();
    }

    // Update is called once per frame
    void Update()
    {
        if (totalScoreAvailable == 0) totalScoreAvailable = beatObjects.Count * 100;
        string key = "";
        if (Input.GetKeyDown("space"))
        {
            key = "_";
        }
        if (Input.GetKeyDown("e"))
        {
            key = "e";
        }
        if (Input.GetKeyDown("f"))
        {
            key = "f";
        }
        if (Input.GetKeyDown("a"))
        {
            key = "a";
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            key = "+";
        }
        if (Input.GetKeyDown("9"))
        {
            key = "9";
        }
        if (Input.GetKeyDown("8"))
        {
            key = "8";
        }
        if (Input.GetKeyDown("7"))
        {
            key = "7";
        }
        if (Input.GetKeyDown("6"))
        {
            key = "6";
        }
        if (Input.GetKeyDown("5"))
        {
            key = "5";
        }



        if (ctrl.playing)
        {

            if (recordMode)
            {
                if (Input.GetKeyDown("`"))
                {
                    string output = "";
                    foreach (string s in record) output += $"{s}\n";
                    scoreText.text = $"{output}";

                    ctrl.playing = false;
                }
                else if (key != "")
                {
                    if (dif(GetComponent<levelController>().player.transform.position.x, recordLastX) < 1)
                    {
                        if (recordOffset == 0) recordOffset = 1;
                        else recordOffset = 0;
                    } recordOffset = 0;
                    recordLastX = GetComponent<levelController>().player.transform.position.x;
                    record.Add($"{GetComponent<spawnObjects>().xToMillis(GetComponent<levelController>().player.transform.position.x)} {key.ToUpper()} {recordOffset}");
                    GetComponent<spawnObjects>().otfInstantiate(GetComponent<levelController>().player.transform.position.x, key.ToUpper());
                }
            }
            else
            {

                if (beatObjects.Count == 0 && !ctrl.beatmapEnded)
                {
                    ctrl.beatmapEnded = true;


                    Debug.Log($"You scored {score} out of {totalScoreAvailable}");

                    PlayerPrefs.SetFloat("earnedScore", Mathf.Round(PlayerPrefs.GetFloat("earnedScore") + (float)score));

                    string customLevel = "";
                    if (GetComponent<spawnObjects>().customBeatmap) customLevel = ",c";

                    PlayerPrefs.SetString("postLevel", $"{GetComponent<spawnObjects>().trackname},{score},{totalScoreAvailable}{customLevel}");
                    PlayerPrefs.Save();
                    int percent = (int)((score / totalScoreAvailable) * 100f);

                    if (percent > 50)
                    {
                        switch (GetComponent<spawnObjects>().beatmapName)
                        {
                            case "level0":
                                PlayerPrefs.SetString("levelUnlocks", PlayerPrefs.GetString("levelUnlocks").Replace("1.2=f", "1.2=t").Replace("2.1=f", "2.1=t"));
                                PlayerPrefs.Save();
                                break;
                            case "level1":
                                PlayerPrefs.SetString("levelUnlocks", PlayerPrefs.GetString("levelUnlocks").Replace("1.3=f", "1.3=t"));
                                PlayerPrefs.Save();
                                break;
                            case "level2":
                                break;
                            case "2.1":
                                PlayerPrefs.SetString("levelUnlocks", PlayerPrefs.GetString("levelUnlocks").Replace("2.2=f", "2.2=t").Replace("3.1=f", "3.1=t"));
                                PlayerPrefs.Save();
                                break;
                            case "2.2":
                                break;
                            case "3.1":
                                PlayerPrefs.SetString("levelUnlocks", PlayerPrefs.GetString("levelUnlocks").Replace("4.1=f", "4.1=t"));
                                PlayerPrefs.Save();
                                break;
                            case "4.1":
                                PlayerPrefs.SetString("levelUnlocks", PlayerPrefs.GetString("levelUnlocks").Replace("4.2=f", "4.2=t"));
                                PlayerPrefs.Save();
                                break;
                            case "4.2":
                                PlayerPrefs.SetString("levelUnlocks", PlayerPrefs.GetString("levelUnlocks").Replace("5.1=f", "5.1=t"));
                                PlayerPrefs.Save();
                                break;
                        }
                    }
                }


                if (ctrl.beatmapEnded && !GetComponent<AudioSource>().isPlaying)
                {

                    SceneManager.LoadScene("postLevel");
                }

                int[] indexes = getClosestKeyIndexes();

                float d = dif(GetComponent<levelController>().player.transform.position.x, beatObjects[indexes[0]].transform.position.x, false);
                //d = d - xOffset;
                if (Mathf.Abs(d) < scoreSensitivity)
                {
                    if (key != "")
                    {
                        d = Mathf.Abs(d);
                        if (d == 0) d = 0.0001f;
                        foreach (int i in indexes)
                        {
                            if (beatKeys[i].ToLower() == key)
                            {
                                if (scoreFactor / d > 50) beatObjects[i].GetComponent<ParticleSystem>().Play();


                                System.Random r = new System.Random();
                                beatObjects[i].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>($"Sprites/parcel{r.Next(0, goodSpriteCount) + 1}");

                                if (scoreFactor / d > 100) score += 100; 
                                else score += scoreFactor / d;
                                beatKeys.RemoveAt(i);
                                beatTimes.RemoveAt(i);
                                beatObjects.RemoveAt(i);

                                scoreText.text = $"Score: {(int)score}";

                                break;
                            }
                        }
                    }
                }
                else if (d > 0)
                {
                    foreach (int i in indexes)
                    {
                        System.Random r = new System.Random();
                        beatObjects[i].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>($"Sprites/badparcel{r.Next(0, damagedSpriteCount) + 1}");
                        beatKeys.RemoveAt(i);
                        beatTimes.RemoveAt(i);
                        beatObjects.RemoveAt(i);
                    }
                }
            }
        }

    }

    int[] getClosestKeyIndexes() //Maybe don't return early? will allow multiple pass rather than having to one-shot and merge... or just make merge code? lol
    {
        float curX = GetComponent<levelController>().player.transform.position.x;

        float closestX = 100000000f;
        List<int> indexes = new List<int>();
        int lastIndex = -1;

        for (int i = 0; i < beatObjects.Count; i++)
        {
            GameObject t = beatObjects[i];
            float d = dif(t.transform.position.x, curX);

            if (d == closestX)
            {
                if (lastIndex > 0) { indexes.Add(lastIndex); lastIndex = -1; }
                indexes.Add(i);
            }
            else if (d < closestX)
            {
                lastIndex = i;
                closestX = d;
            }
            //else break;
        }

        if (lastIndex > -1) indexes.Add(lastIndex);
        return indexes.ToArray();

    }

    float dif(float a, float b, bool abs = true)
    {
        if (abs) return Mathf.Abs(a - b);
        else return a - b;
    }



    public void back()
    {
        SceneManager.LoadScene("levelSelect");
    }
}
