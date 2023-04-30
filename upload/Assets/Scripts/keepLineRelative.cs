using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keepLineRelative : MonoBehaviour
{

    public GameObject player;
    public float y;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = new Vector3(player.transform.position.x, y, 0);
    }
}
