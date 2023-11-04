using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeControll : MonoBehaviour
{
    private PlayerStatusHandle playerStatusHandle;
    public GameObject explosion;
    private GameObject prefab;
    private AudioSource audioSource;
    public AudioClip explosionSound;
    // Start is called before the first frame update
    void Start()
    {
        playerStatusHandle = GameObject.Find("GameStatus").GetComponent<PlayerStatusHandle>();
        audioSource = GameObject.FindObjectOfType<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
