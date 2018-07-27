using System;
using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMotor : MonoBehaviour
{
   #region Editor Variables
   [SerializeField]
   [Tooltip("A struct to contain important information about the motor.")]
   private MotorInfo m_Info;
   #endregion

   #region Non-Editor Variables
   // Where the player is looking at
   private Vector2 m_LookAtTarget = Vector2.zero;
   #endregion

   #region Cached Components
   private Rigidbody2D m_Rb;
   #endregion

   #region First Time Initialization and Set Up
   private void Awake()
   {
      m_Rb = GetComponent<Rigidbody2D>();
   }
   #endregion

   #region Main Updates
   private void FixedUpdate()
   {
      Rotate();
   }
   #endregion

   #region Movement Methods
   public void UpdateSpeed(float normalizedSpeed, bool isMoving = false)
   {
      if (isMoving)
         m_Rb.velocity = transform.up * RealSpeed(normalizedSpeed);
      else
         m_Rb.velocity = Vector2.zero;
   }

   private float RealSpeed(float normalizedSpeed)
   {
      return -normalizedSpeed * (m_Info.MaxSpeed - m_Info.MinSpeed) + m_Info.MaxSpeed;
   }
   #endregion

   #region Rotation Methods
   public void UpdateTarget(Vector2 target)
   {
      m_LookAtTarget = target;
   }
   
   private void Rotate()
   {
      Vector2 toTarget = m_LookAtTarget - m_Rb.position;
      float theta = Vector2.SignedAngle(transform.up, toTarget) * Time.fixedDeltaTime *  m_Info.RotationSpeed;

      m_Rb.MoveRotation(m_Rb.rotation + theta);
   }
   #endregion
}

[Serializable]
public struct MotorInfo
{
   #region Editor Variables
   [Header("PROPERTIES: MOVEMENT")]

   [SerializeField]
   [Tooltip("When the player has maximum bullets, they will be traveling at" +
      "this speed.")]
   private float m_MinSpeed;

   [SerializeField]
   [Tooltip("When the player has run out of bullets, they will be traveling" +
      "at this speed.")]
   private float m_MaxSpeed;

   [Space(10)]

   [Header("PROPERTIES: ROTATION")]

   [SerializeField]
   [Tooltip("How quickly the player rotates.")]
   public float m_RotationSpeed;
   #endregion
   
   #region Constructors
   public MotorInfo(float minSpeed = 3, float maxSpeed = 7, float rotationSpeed = 15)
   {
      m_MinSpeed = minSpeed;
      m_MaxSpeed = maxSpeed;

      m_RotationSpeed = rotationSpeed;
   }
   #endregion

   #region Accessors and Mutators
   public float MaxSpeed { get { return m_MaxSpeed; } }
   public float MinSpeed { get { return m_MinSpeed; } }

   public float RotationSpeed { get { return m_RotationSpeed; } }
   #endregion
}