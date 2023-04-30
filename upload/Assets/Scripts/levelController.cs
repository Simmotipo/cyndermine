using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class levelController : MonoBehaviour
{
    // Start is called before the first frame update

    double pixelsPerSecond = -1;
    public float scroll;
    public GameObject player;
    AudioSource track;
    public bool playing = false;
    public bool beatmapEnded = false;

    public float playheadPosition;


    void Start()
    {
        player = GameObject.Find("Player");

        GameObject.Find("PlayerSprite").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/van_" + PlayerPrefs.GetString("selectedVan"));
        playing = false;
        track = GetComponent<AudioSource>();
        track.volume = PlayerPrefs.GetFloat("volumePercent") / 100f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!track.isPlaying && playing && !beatmapEnded)
        {
            track.clip = Resources.Load<AudioClip>($"tracks/{GetComponent<spawnObjects>().trackname}");
            track.loop = false;
            track.Play();
            pixelsPerSecond = GetComponent<spawnObjects>().pixelsPerSecond; //Check moving this here from Start() has fixed wrong scroll speed issues :)
            GameObject.Find("trackNameSign").GetComponent<TextMeshPro>().text = GetComponent<spawnObjects>().trackname;

        }
        playheadPosition = track.time * (float)pixelsPerSecond;
        player.transform.position = new Vector3(playheadPosition, player.transform.position.y, player.transform.position.z);
    }
}
