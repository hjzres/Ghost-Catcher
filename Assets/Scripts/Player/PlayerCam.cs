using UnityEngine;

namespace player
{
	public class PlayerCam : MonoBehaviour
	{
		[SerializeField] private float sensitivity;

		[SerializeField] private Transform orentation;

		private float _xRotation;
		private float _yRotation;

		private void Awake(){
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
		}

		private void Update(){
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