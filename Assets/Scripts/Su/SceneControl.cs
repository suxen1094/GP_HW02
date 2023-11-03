using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControl : MonoBehaviour
{
    public GameObject player;
    public GameObject startPoint;
    // Start is called before the first frame update
    void Start()
    {
        player = gameObject;
        startPoint = GameObject.FindGameObjectWithTag("StartPoint");
        player.transform.position = startPoint.transform.position;
    }

    void OnCollisionEnter(Collision collisionObject)
    {
        if (collisionObject.gameObject.tag == "EndPoint")
        {
            int current_scene_idx = SceneManager.GetActiveScene().buildIndex;

            // Not the final level
            if (current_scene_idx <= 2)
            {
                // Load the next level
                SceneManager.LoadScene(current_scene_idx + 1);
            }
        }
    }
}
