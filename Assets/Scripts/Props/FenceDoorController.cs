using System.Collections;
using UnityEngine;

public class FenceDoorController : AbstractDoor
{
    private HingeJoint hinge;
    private bool isOpen = false;
    public float openSpeed = 2f;
    public float openAngle = 90f;

    private void Start()
    {
        hinge = GetComponent<HingeJoint>();
        if (hinge == null)
        {
            Debug.LogError("Hinge Joint not found on the door.");
        }
    }

    public override void OpenDoor()
    {
        if (!isOpen)
        {
            StartCoroutine(SwingDoor(openAngle));
            PlaytestingLogger.LogAction("Fence opened");
            isOpen = true;
        }
    }

    private IEnumerator SwingDoor(float targetAngle)
    {
        float elapsed = 0f;
        float duration = 1.5f;
        float initialAngle = hinge.transform.localEulerAngles.y;
        Rigidbody rb = hinge.GetComponent<Rigidbody>();

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime * openSpeed;
            float newAngle = Mathf.Lerp(initialAngle, targetAngle, elapsed / duration);
            hinge.transform.localEulerAngles = new Vector3(0, newAngle, 0);
            yield return null;
        }
        hinge.transform.localEulerAngles = new Vector3(0, targetAngle, 0);

        if (rb != null)
        {
            rb.isKinematic = true; // Disable physics on the door
            rb.constraints = RigidbodyConstraints.FreezeAll; // Prevent movement
        }
    }
}
