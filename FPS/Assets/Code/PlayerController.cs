using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
  [SerializeField]string horizontalInputName, verticalInputName;
  [Header("Moving Speed Settings")]
  [SerializeField]float m_walkSpeed;
  [SerializeField]float m_runSpeed;
  [SerializeField]float m_runBuildUpSpeed;
  float m_playerSpeed;

  [Header("Jump Settings")]
  [SerializeField]float m_fullJump;
  [SerializeField]KeyCode jumpKey;
  [SerializeField]float m_playerGravity = -9.81f;
  float m_jumpMultiplier;
  public float groundDistance = 0.4f;

  public Transform groundCheck;
  public LayerMask groundMask;
  CharacterController m_charController;
  Vector3 velocity;

  public bool isGrounded;
  void Awake()
  {
    m_charController = GetComponent<CharacterController>();
    m_jumpMultiplier = m_fullJump;

  }

  void Update()
  {
    PlayerMovement();
  }

  void PlayerMovement()
  {
    WalkInput();
    JumpInput();
    Gravity();
  }
  void WalkInput()
  {
    float hInput = Input.GetAxis(horizontalInputName);
    float vInput = Input.GetAxis(verticalInputName);
    Vector3 forwardMovement = transform.forward * vInput;
    Vector3 sideMovement = transform.right * hInput;

    if(vInput != 0f || hInput != 0f)
    {
      m_playerSpeed = Mathf.Lerp(m_playerSpeed, m_runSpeed, Time.deltaTime * m_runBuildUpSpeed);
    }else
    {
      m_playerSpeed = m_walkSpeed;
    }
    //Normalizando a Velocidade em diagonais
    m_charController.SimpleMove(Vector3.ClampMagnitude(forwardMovement + sideMovement, 1f) * m_playerSpeed);
  }

  void JumpInput()
  {
    isGrounded = (Physics.CheckSphere(groundCheck.position, groundDistance, groundMask));
    if (Input.GetKeyDown(jumpKey) && isGrounded)
    {
      m_charController.Move(Vector3.up * m_jumpMultiplier * (Time.deltaTime * Time.deltaTime));
      isGrounded = false;
    }
  }

  void Gravity()
  {
    if(!isGrounded)
    {
    velocity.y += m_playerGravity * Time.deltaTime;
    m_charController.Move(velocity * Time.deltaTime);
    }else
    {
      velocity.y = -2f;
    }
  }
}
