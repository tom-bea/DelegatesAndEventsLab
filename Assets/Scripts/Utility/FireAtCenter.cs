using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(Rigidbody2D))]
public class FireAtCenter : MonoBehaviour
{
   #region Cached Components
   private Rigidbody2D m_Rb;
   #endregion

   #region First Time Initialization and Set Up
   private void Awake()
   {
      m_Rb = GetComponent<Rigidbody2D>();
   }
   #endregion

   #region OnEnable, Set Ups, and Resetters
   private void OnEnable()
   {
      Vector2 dir = (-transform.position).normalized;
      m_Rb.velocity = dir * 3;
   }
   #endregion
}
