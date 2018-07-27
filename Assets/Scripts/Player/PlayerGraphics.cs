using System;
using UnityEngine;

[DisallowMultipleComponent]
public class PlayerGraphics : MonoBehaviour
{
   #region Editor Variables
   [SerializeField]
   [Tooltip("The front left side of the gun.")]
   private ParticleSystem m_FrontLeft;

   [SerializeField]
   [Tooltip("The front right side of the gun.")]
   private ParticleSystem m_FrontRight;

   [SerializeField]
   [Tooltip("A struct to contain important information about the graphics.")]
   private GraphicsInfo m_Info;
   #endregion

   #region OnEnable, Set Ups, and Resetters
   private void OnEnable()
   {
      PlayerController.RatioUpdateEvent += UpdateGunGraphics;
   }
   #endregion

   #region OnDisable and Other Enders
   private void OnDisable()
   {
      PlayerController.RatioUpdateEvent -= UpdateGunGraphics;
   }
   #endregion

   #region Graphic Updaters
   private void UpdateGunGraphics(float ratio)
   {
      if (ratio == 0)
      {
         m_FrontLeft.gameObject.SetActive(false);
         m_FrontRight.gameObject.SetActive(false);
         return;
      }
      else
      {
         m_FrontLeft.gameObject.SetActive(true);
         m_FrontRight.gameObject.SetActive(true);
      }
      
      var mainL = m_FrontLeft.main;
      var mainR = m_FrontRight.main;

      mainR.startLifetime = mainL.startLifetime = 
         ratio * (m_Info.MaxLifetime - m_Info.MinLifetime) + m_Info.MinLifetime;

      mainR.startSize = mainL.startSize = 
         ratio * (m_Info.MaxSize - m_Info.MinSize) + m_Info.MinSize;

      m_FrontLeft.transform.localRotation = 
         Quaternion.Euler(0, 0, (ratio - 1) * (m_Info.MaxAngle - m_Info.MinAngle) - m_Info.MinAngle);
      m_FrontRight.transform.localRotation = 
         Quaternion.Euler(0, 0, (1 - ratio) * (m_Info.MaxAngle - m_Info.MinAngle) + m_Info.MinAngle);
   }
   #endregion
}

[Serializable]
public struct GraphicsInfo
{
   #region Editor Variables
   [Header("PROPERTIES: LIFETIME")]

   [SerializeField]
   [Tooltip("How short the gun is when the player is nearly out of ammo.")]
   private float m_MinLifetime;

   [SerializeField]
   [Tooltip("How long the gun is when the player is at maximum ammo.")]
   private float m_MaxLifetime;

   [Space(10)]

   [Header("PROPERTIES: SIZE")]

   [SerializeField]
   [Tooltip("How small the gun is when the player is nearly out of ammo.")]
   private float m_MinSize;

   [SerializeField]
   [Tooltip("How large the gun is when the player is at maximum ammo.")]
   private float m_MaxSize;

   [Space(10)]

   [Header("PROPERTIES: ANGLE")]

   [SerializeField]
   [Tooltip("The angle of the gun when the player at maximum ammo (different" +
      " from the previous properties!)")]
   private float m_MinAngle;

   [SerializeField]
   [Tooltip("The angle of the gun when the player is nearly out of ammo" +
      " (different from the previous properties!)")]
   private float m_MaxAngle;
   #endregion

   #region Constructors
   public GraphicsInfo(float minLifetime = 0.67f, float maxLifetime = 1.5f,
      float minSize = 0.3f, float maxSize = 0.5f,
      float minAngle = 9, float maxAngle = 14)
   {
      m_MinLifetime = minLifetime;
      m_MaxLifetime = maxLifetime;

      m_MinSize = minSize;
      m_MaxSize = maxSize;

      m_MinAngle = minAngle;
      m_MaxAngle = maxAngle;
   }
   #endregion

   #region Accessors and Mutators
   public float MinLifetime { get { return m_MinLifetime; } }
   public float MaxLifetime { get { return m_MaxLifetime; } }

   public float MinSize { get { return m_MinSize; } }
   public float MaxSize { get { return m_MaxSize; } }

   public float MinAngle { get { return m_MinAngle; } }
   public float MaxAngle { get { return m_MaxAngle; } }
   #endregion
}