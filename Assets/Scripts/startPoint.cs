using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startPoint : MonoBehaviour
{
    private GameObject player;
    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.Find("unitychan");
        player.GetComponent<Transform>().position = transform.position;
        player.GetComponent<Transform>().rotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
