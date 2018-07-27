using System.Collections;
using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(PlayerMotor))]
[RequireComponent(typeof(PlayerGun))]
public class PlayerController : MonoBehaviour
{
   #region Delegates and Events
   public delegate void RatioUpdateDelegate(float newRatio);
   public static event RatioUpdateDelegate RatioUpdateEvent;
   #endregion

   #region Non-Editor Variables
   // True when the player is moving, false otherwise
   private bool m_IsMoving = false;
   #endregion

   #region Cached Components
   private PlayerMotor m_Motor;
   private PlayerGun m_Gun;
   #endregion

   #region Cached Instance References
   private Camera m_MainCam;
   #endregion

   #region First Time Initialization and Set Up
   private void Awake()
   {
      m_Motor = GetComponent<PlayerMotor>();
      m_Gun = GetComponent<PlayerGun>();
   }

   private void Start()
   {
      m_MainCam = FindObjectOfType<Camera>();
   }
   #endregion

   #region OnEnable, Set Ups, and Resetters
   private void OnEnable()
   {
      AmmoPickup.AddAmmoEvent += AddAmmo;

      m_Gun.ResetBulletCount(0);
      StartCoroutine(WaitAndUpdateRatio());
   }

   private IEnumerator WaitAndUpdateRatio()
   {
      yield return new WaitForSeconds(0.2f);
      if (RatioUpdateEvent != null)
         RatioUpdateEvent(m_Gun.GetRatio());
   }
   #endregion

   #region OnDisable and Other Enders
   private void OnDisable()
   {
      AmmoPickup.AddAmmoEvent -= AddAmmo;
   }
   #endregion

   #region Main Updates
   private void Update()
   {
      if (Input.GetButton("Fire1"))
      {
         m_Motor.UpdateSpeed(m_Gun.GetRatio(), true);
         m_IsMoving = true;
      }
      else
      {
         m_Motor.UpdateSpeed(0);
         m_IsMoving = false;
      }
      m_Motor.UpdateTarget(m_MainCam.ScreenToWorldPoint(Input.mousePosition));

      if (Input.GetButtonDown("Jump") && CanFire())
         Fire();
   }
   #endregion
   
   #region Firing Methods
   private bool CanFire()
   {
      return !m_IsMoving && m_Gun.HasAmmo();
   }

   private void Fire()
   {
      m_Gun.Fire();

      if (RatioUpdateEvent != null)
         RatioUpdateEvent(m_Gun.GetRatio());
   }
   #endregion

   #region Ammo Amount Modifiers
   private void AddAmmo(int amt)
   {
      m_Gun.AddAmmo(amt);

      if (RatioUpdateEvent != null)
         RatioUpdateEvent(m_Gun.GetRatio());
   }
   #endregion
}