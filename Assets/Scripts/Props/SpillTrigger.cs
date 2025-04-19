using UnityEngine;

public class SpillTrigger : MonoBehaviour
{
    private bool circeInPosition = false;
    private PotionsActionsController circeController;

    public Transform vaseTransform;

    public AbstractDoor doorController;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Circe entered button standing spot.");
            circeInPosition = true;

            circeController = other.GetComponent<PotionsActionsController>();

            if (circeController != null)
            {
                circeController.SetVaseTransform(vaseTransform);
                circeController.SetDoorController(doorController);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Circe left the button standing spot.");
            circeController = other.GetComponent<PotionsActionsController>();
            circeInPosition = false;
            if (circeController != null)
            {
                circeController.SetVaseTransform(null);
                circeController.SetDoorController(null);
            }
        }
    }

    public bool IsCirceInPosition()
    {
        return circeInPosition;
    }

    public PotionsActionsController GetCirceController()
    {
        return circeController;
    }
}
