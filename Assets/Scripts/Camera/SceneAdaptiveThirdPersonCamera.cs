using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class SceneAdaptiveThirdPersonCamera : MonoBehaviour
{
	[SerializeField] private Transform target;

	[Header("Camera Offsets")]
	[SerializeField] private Vector3 outdoorPositionOffset = new Vector3(0, 7, -8);
	[SerializeField] private Vector3 outdoorRotationOffset = new Vector3(25, 0, 0);
	[SerializeField] private Vector3 indoorPositionOffset = new Vector3(0, 3.5f, -4f);
	[SerializeField] private Vector3 indoorRotationOffset = new Vector3(15, 0, 0);

	private Vector3 positionOffset;
	private Vector3 rotationOffset;
	private Vector3 desiredPosition;
	private Quaternion desiredRotation;

	private bool isIndoor = false;  // Default to outdoor

	void Start() 
	{
		if (target == null)
		{
			// If not set, set to "Player"
			target = GameObject.FindWithTag("Player")?.transform;

			if (target == null)
			{
				Debug.LogError("No camera target found. Assign the Player object to the 'target' field.");
				return;
			}
		}

		SceneManager.sceneLoaded += OnSceneLoaded;
		DetectSceneType();
		SetSceneCameraOffset();
		
		// Place camera at correct position at the start
		CalculateCameraAdjustments();
		transform.position = desiredPosition;
		transform.rotation = desiredRotation;
	}
  private void DetectSceneType()
	{
		string sceneName = SceneManager.GetActiveScene().name.ToLower();

		if (sceneName.Contains("palace"))
		{
			isIndoor = true;
		}
		else
		{
			isIndoor = false;
		}
	}
	private void SetSceneCameraOffset() 
	{
		if (isIndoor) 
		{
			positionOffset = indoorPositionOffset;
			rotationOffset = indoorRotationOffset;
		} 
		else 
		{
			positionOffset = outdoorPositionOffset;
			rotationOffset = outdoorRotationOffset;
		}
	}
	private void CalculateCameraAdjustments()
	{
		if (target == null) return;

		// Adjust camera position based on target and scene type
		desiredPosition = target.position + positionOffset;
		
		// Adjust camera rotation based on scene type
		desiredRotation = Quaternion.Euler(rotationOffset);
	}

	void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		DetectSceneType();
	}
	
	void OnDestroy()
	{
		SceneManager.sceneLoaded -= OnSceneLoaded;
	}

}
