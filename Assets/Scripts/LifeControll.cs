using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LifeControll : MonoBehaviour
{
    private GameObject player;
    private PlayerStatusHandle playerStatusHandle;
    public GameObject explosion;
    private GameObject prefab;
    private AudioSource audioSource;
    public AudioClip explosionSound;
    public AudioClip healSound;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerStatusHandle = GameObject.Find("GameStatus").GetComponent<PlayerStatusHandle>();
        audioSource = GameObject.FindObjectOfType<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // Fall out of the map, only validate in game scene
        if (player.transform.position.y < -20 && SceneManager.GetActiveScene().buildIndex != 4 
            && SceneManager.GetActiveScene().buildIndex != 5)
        {
            playerStatusHandle.currentHP = -10;
        }
    }
    void OnCollisionEnter(Collision collisionObject)
    {
        if (collisionObject.gameObject.tag == "Monster" || collisionObject.gameObject.tag == "FireBall"){
            prefab = Instantiate(explosion, gameObject.transform.position, Quaternion.identity);
            audioSource.PlayOneShot(explosionSound, 0.6f);
            Destroy(prefab, 1);  // Destroy explosion
            playerStatusHandle.currentHP -= 5;
        }
    }

    void OnTriggerEnter(Collider colliderObject)
    {
        // Get healing item
        if (colliderObject.gameObject.tag == "HealingItem")
        {
            audioSource.PlayOneShot(healSound, 0.6f);
            Destroy(colliderObject.gameObject);
            if (playerStatusHandle.maxHP - playerStatusHandle.currentHP < 20)
            {
                playerStatusHandle.currentHP = 100;
            }
            else
            {
                playerStatusHandle.currentHP += 10;
            }
        }
    }
}
