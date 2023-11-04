using UnityEngine;

public class FlashlightControl : MonoBehaviour
{
    private Light flashlightLight;
    public float lowIntensity = 1.0f;   
    public float mediumIntensity = 3.0f; 
    public float highIntensity = 5.0f;  

    private void Start()
    {
        flashlightLight = GetComponent<Light>();
        if (flashlightLight == null)
        {
            Debug.LogError("No Light component found on this object. Ensure you have a Light component to represent the flashlight's beam.");
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SetFlashlightIntensity(lowIntensity);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SetFlashlightIntensity(mediumIntensity);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SetFlashlightIntensity(highIntensity);
        }

    }

    void SetFlashlightIntensity(float intensity)
    {
        if (flashlightLight != null)
        {
            flashlightLight.intensity = intensity;
        }
    }
}
