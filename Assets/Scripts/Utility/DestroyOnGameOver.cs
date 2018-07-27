using UnityEngine;

[DisallowMultipleComponent]
public class DestroyOnGameOver : MonoBehaviour
{
   #region OnEnable, Set Ups, and Resetters
   private void OnEnable()
   {
      GameManager.EndGameEvent += Deactivate;
   }
   #endregion

   #region OnDisable and Other Enders
   private void OnDisable()
   {
      GameManager.EndGameEvent -= Deactivate;
   }
   #endregion

   #region Deactivation Methods
   private void Deactivate(int num)
   {
      gameObject.SetActive(false);
   }
   #endregion
}
