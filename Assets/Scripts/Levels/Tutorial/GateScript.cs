using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateScript : MonoBehaviour
{
    public string hintText;

    public float cooldownSeconds = 5f;

    private float lastShownTime = 0f;

    public void Open()
    {
        Animation anim = GetComponent<Animation>();
        anim.Play("GateOpen");
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && !string.IsNullOrWhiteSpace(hintText) && (lastShownTime == 0f || Time.time - lastShownTime >= cooldownSeconds))
        {
            TutorialController tc = collision.gameObject.GetComponent<TutorialController>();
            tc.ShowTutorialText(hintText);
            lastShownTime = Time.time;
        }
    }
}
