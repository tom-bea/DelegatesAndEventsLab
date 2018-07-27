using UnityEngine;

[DisallowMultipleComponent]
public class AmmoPickup : MonoBehaviour
{
   #region Delegates and Events
   public delegate void AddAmmoDelegate(int amt);
   public static event AddAmmoDelegate AddAmmoEvent;
   #endregion

   #region Editor Variables
   [SerializeField]
   [Tooltip("The amount of ammo that the player will receive upon collecting" +
      " this pickup.")]
   private int m_AmmoAmount = 0;
   #endregion

   #region Collision Methods
   private void OnCollisionEnter2D(Collision2D collision)
   {
      GameObject other = collision.gameObject;
      if (other.CompareTag("Player") && AddAmmoEvent != null)
      {
         AddAmmoEvent(m_AmmoAmount);
         gameObject.SetActive(false);
      }
   }
   #endregion
}