using UnityEngine;

[DisallowMultipleComponent]
public class SetActiveOnGameOver : MonoBehaviour
{
   #region Editor Variables
   [SerializeField]
   [Tooltip("Every game object that should be activated once the game ends.")]
   private GameObject[] m_ObjectsToActivate;
   #endregion

   #region OnEnable, Set Ups, and Resetters
   private void OnEnable()
   {
      GameManager.EndGameEvent += ActivateAll;
   }
   #endregion

   #region OnDisable and Other Enders
   private void OnDisable()
   {
      GameManager.EndGameEvent -= ActivateAll;
   }
   #endregion

   #region Activator Methods
   private void ActivateAll(int num)
   {
      foreach (GameObject go in m_ObjectsToActivate)
         go.SetActive(true);
   }
   #endregion
}
