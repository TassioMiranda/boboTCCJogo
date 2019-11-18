using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
  [SerializeField]string mouseXInputName, mouseYInputName;

  [SerializeField]float m_mouseSensitivity;

  [SerializeField] Transform m_playerBody;

  float xAxisClamp;
  void Awake()
  {
    LockCursor();
    xAxisClamp = .0f;
  }

  void Update()
  {
    CameraRotation();
  }

  void LockCursor()
  {
    Cursor.lockState = CursorLockMode.Locked;
  }

  void CameraRotation()
  {
    float mouseX = Input.GetAxis(mouseXInputName) * m_mouseSensitivity * Time.deltaTime;
    float mouseY = Input.GetAxis(mouseYInputName) * m_mouseSensitivity * Time.deltaTime;

    xAxisClamp += mouseY;

    if(xAxisClamp > 90.0f)
    {
      xAxisClamp = 90.0f;
      mouseY = .0f;
      ClampXAxisToValue(270.0f);
    } else if(xAxisClamp < -90.0f)
    {
      xAxisClamp = -90.0f;
      mouseY = .0f;
      ClampXAxisToValue(90.0f);
    }

    transform.Rotate(Vector3.left * mouseY);
    m_playerBody.Rotate(Vector3.up * mouseX);
  }
  void ClampXAxisToValue(float value)
  {
    Vector3 eulerRotation = transform.eulerAngles;
    eulerRotation.x = value;
    transform.eulerAngles = eulerRotation;
  }
}
