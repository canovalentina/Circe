using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingColumnCollider : MonoBehaviour
{
    public GameObject fallingColumn;
    public GameObject bricks;
    public GameObject circe;

    void Start()
    {
        bricks.SetActive(false);
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            bricks.SetActive(true);
        }
        //Rigidbody rb = fallingColumn.GetComponent<Rigidbody>();
        //rb.AddForce(new Vector3(-300f, 0, 0), ForceMode.Impulse);
    }
}
