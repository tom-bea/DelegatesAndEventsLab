using System;
using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(BulletPoolManager))]
public class PlayerGun : MonoBehaviour
{
   #region Editor Variables
   [SerializeField]
   [Tooltip("A struct to contain important information about the gun.")]
   private GunInfo m_Info;
   #endregion

   #region Non-Editor Variables
   // The number of bullets left in the gun
   private int m_BulletsLeft = 0;
   #endregion

   #region Cached Components
   private BulletPoolManager m_BulletPool;
   #endregion

   #region First Time Initialization and Set Up
   private void Awake()
   {
      m_BulletPool = GetComponent<BulletPoolManager>();
   }
   #endregion

   #region OnEnable, Set Ups, and Resetters
   public void ResetBulletCount(int num)
   {
      m_BulletsLeft = m_Info.NumStartingBullets;
   }
   #endregion

   #region Getters
   public float GetRatio()
   {
      return (float)m_BulletsLeft / m_Info.MaxBullets;
   }
   #endregion

   #region Checkers and Verifiers
   public bool HasAmmo()
   {
      return m_BulletsLeft > 0;
   }
   #endregion

   #region Firing Methods
   public void Fire()
   {
      if (!HasAmmo())
         return;
      
      Bullet bullet = m_BulletPool.GetNextGO();
      bullet.transform.position = transform.position + transform.up * 0.9f;
      bullet.transform.rotation = transform.rotation;
      bullet.gameObject.SetActive(true);

      m_BulletsLeft--;
   }
   #endregion

   #region Ammo Amount Modifiers
   public void AddAmmo(int amt)
   {
      if (m_BulletsLeft + amt < m_Info.MaxBullets)
         m_BulletsLeft += amt;
      else if (m_BulletsLeft < m_Info.MaxBullets)
         m_BulletsLeft = m_Info.MaxBullets;
   }
   #endregion
}

[Serializable]
public struct GunInfo
{
   #region Editor Variables
   [Header("PROPERTIES: BULLETS COUNT")]

   [SerializeField]
   [Tooltip("The number of bullets the player starts each game with.")]
   private int m_NumStartingBullets;

   [SerializeField]
   [Tooltip("The maximum number of bullets the player can hold at any one " +
      "point in time.")]
   private int m_MaxBullets;
   #endregion

   #region Constructors
   public GunInfo(int numStartingBullets = 30, int maxBullets = 100)
   {
      m_NumStartingBullets = numStartingBullets;
      m_MaxBullets = maxBullets;
   }
   #endregion

   #region Accessors and Mutators
   public int NumStartingBullets { get { return m_NumStartingBullets; } }
   public int MaxBullets { get { return m_MaxBullets; } }
   #endregion
}