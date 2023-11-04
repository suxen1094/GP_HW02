using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombHandler : MonoBehaviour
{
    private PlayerStatusHandle playerStatusHandle;
    public GameObject explosion;
    private GameObject prefab;
    private AudioSource audioSource;
    public AudioClip explosionSound;
    bool first = true;
    // Start is called before the first frame update
    void Awake()
    {
        playerStatusHandle = GameObject.Find("GameStatus").GetComponent<PlayerStatusHandle>();
        audioSource = GameObject.FindObjectOfType<AudioSource>();
    }
    void Start()
    {
        Destroy(gameObject, 5);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collisionObject)
    {
        if (collisionObject.gameObject.tag == "Monster"){
            prefab = Instantiate(explosion, gameObject.transform.position, Quaternion.identity);
            audioSource.PlayOneShot(explosionSound, 0.6f);
            Destroy(collisionObject.gameObject);
            Destroy(gameObject); // Destroy bombs
            Destroy(prefab, 1);  // Destroy explosion
            if(first) playerStatusHandle.score += 5;
            first = false;
        }
    }
}
