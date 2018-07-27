using UnityEngine;

public class EnemyController : MonoBehaviour
{
   #region Delegates and Events
   public delegate void EnemyHitDelegate();
   public static event EnemyHitDelegate PlayerDestroyedEvent;
   #endregion

   #region Collision Methods
   private void OnCollisionEnter2D(Collision2D collision)
   {
      GameObject other = collision.gameObject;
      if (other.CompareTag("Player") && PlayerDestroyedEvent != null)
         PlayerDestroyedEvent();
   }
   #endregion
}
