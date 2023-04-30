using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class beater : MonoBehaviour
{
    // Start is called before the first frame update

    public float bpm;
    public float sync;
    public GameObject playerSprite;

    float lastBeatNumber = 0;
    float lastBeatMillis = 0;


    void Start()
    {
        GetComponent<beater>().playerSprite = GameObject.Find("PlayerSprite");
        Debug.Log("Sync Offset: " + sync);
    }

    // Update is called once per frame
    void Update()
    {
        //float millis = GetComponent<AudioSource>().time * 1000f - sync;
        //float nextBeatDue = (lastBeatNumber + 1) * (60f / bpm);
        //nextBeatDue *= 1000f;


        //if (millis > nextBeatDue)
        //{
        //    Debug.Log("beat");
        //    lastBeatNumber++;
        //    playerSprite.transform.position = new Vector3(playerSprite.transform.position.x, 2, playerSprite.transform.position.z);
        //}
    }
}
