using UnityEngine;

[DisallowMultipleComponent]
public class Bullet : MonoBehaviour
{
   #region Delegates and Events
   // TODO: CODE HERE
   #endregion

   #region Collision Methods
   private void OnParticleCollision(GameObject other)
   {
      // TODO: CODE HERE
   }
   #endregion

   #region Unity Misc
   private void OnParticleSystemStopped()
   {
      gameObject.SetActive(false);
   }
   #endregion
}
