using Unity.VisualScripting;
using UnityEngine;

namespace player
{
	public class PlayerCam : MonoBehaviour
	{
		[SerializeField] private float sensitivity;

		[SerializeField] private Transform orentation;
		
		private PlayerMovement _playerMovement;

		private float _xRotation;
		private float _yRotation;
		
		public float WalkFOV;
		[SerializeField][ReadOnly] private float runFOV;
		[SerializeField] private float FOVMultiplier;
		
		private Camera _camera;

		private void Awake()
		{
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
			
			_camera = GetComponent<Camera>();
			_camera.fieldOfView = WalkFOV;
			runFOV = WalkFOV * FOVMultiplier;
			
			_playerMovement = GetComponentInParent<PlayerMovement>();
		}

		private void Update()
		{
			_camera.fieldOfView = _playerMovement.isRunning ? runFOV : WalkFOV;
			
			float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensitivity; 
			float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensitivity; 

			_yRotation += mouseX;
			_xRotation -= mouseY;
			_xRotation = Mathf.Clamp(_xRotation, -90, 90);

			transform.rotation = Quaternion.Euler(_xRotation, _yRotation, 0);
			orentation.rotation = Quaternion.Euler(0, _yRotation, 0);
		}
	}
}