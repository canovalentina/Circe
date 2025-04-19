using UnityEngine;

public class StatueController : MonoBehaviour
{
    public float activationDistance = 5f;
    public float lightIntensity = 10f;
    public float lightRange = 8f; 
    private GameObject player;
    private Light statueLight;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        
        statueLight = gameObject.AddComponent<Light>();
        statueLight.type = LightType.Point;
        statueLight.range = lightRange;
        statueLight.intensity = 0f;
        statueLight.color = new Color(1f, 0.9f, 0.6f); //Color.white;
    }

    void Update()
    {
        setLightIntensity();
    }
    
    private void setLightIntensity() 
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.transform.position);

        if (distance <= activationDistance)
        {
            statueLight.intensity = lightIntensity;
        }
        else
        {
            statueLight.intensity = 0f;
        }
    }
    
}
