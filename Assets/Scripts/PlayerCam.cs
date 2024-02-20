using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    [SerializeField] private float sensitivity;

    [SerializeField] private Transform orentation;

    private float xRotation;
    private float yRotation;

    private void Awake(){
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update(){
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensitivity; 
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensitivity; 

        yRotation += mouseX;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90, 90);

        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orentation.rotation = Quaternion.Euler(0, yRotation, 0);

    }
}
