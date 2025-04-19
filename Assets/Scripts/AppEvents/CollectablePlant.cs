using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectablePlant : MonoBehaviour
{
    void OnTriggerEnter(Collider c)
    {
        if (c.attachedRigidbody != null)
        {
            PlantCollector pc = c.attachedRigidbody.gameObject.GetComponent<PlantCollector>();

            if (pc != null)
            {
                Debug.Log("Circe close to a plant!");
                //pc.ReceiveBall();
                //EventManager.TriggerEvent<BombBounceEvent, Vector3>(c.transform.position);
                //Destroy(this.gameObject);
                //pc.PlantDetected(this);
            }
        }
    }
}