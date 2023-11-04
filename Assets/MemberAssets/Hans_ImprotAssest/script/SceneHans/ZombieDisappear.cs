using UnityEngine;

public class ZombieDisappear : MonoBehaviour
{
    public Transform flashlight;   
    public Light flashlightLight;    
    public float detectDistance = 100f;  
    public float requiredIntensity = 3f; 
    public string playerTag = "Player";  

    private void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(flashlight.position, flashlight.forward, out hit, detectDistance))
        {
            if (hit.transform == this.transform)
            {
                if (flashlightLight.intensity >= requiredIntensity)
                {
                    Destroy(gameObject);
                }
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(playerTag))
        {
            Destroy(gameObject);
        }
    }
}
