using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotteryController : MonoBehaviour
{
    public GameObject intactPottery; 
    public GameObject crackedPottery;
    public GameObject hiddenItem;
    public Transform spawnPoint;

    private bool isBroken = false;

    private AudioSource audioSource;

    void Start()
    {
        intactPottery.SetActive(true);
        crackedPottery.SetActive(false);
        if (hiddenItem != null)
        {
            hiddenItem.SetActive(false);
        }

        audioSource = GetComponent<AudioSource>();
    }


    public void BreakPottery()
    {
        if (isBroken) return;

        isBroken = true;
        intactPottery.SetActive(false);
        crackedPottery.SetActive(true);

        if (hiddenItem != null && spawnPoint != null)
        {
            hiddenItem.transform.SetParent(null);

            hiddenItem.transform.position = spawnPoint.position;
            hiddenItem.transform.rotation = Quaternion.Euler(-65f, -26f, 11f);
            hiddenItem.SetActive(true);
        }

        EventManager.TriggerEvent<PlayerBreaksPotteryEvent, GameObject>(this.gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && audioSource != null)
        {
            audioSource.Play();
        }
    }
}
