using UnityEngine;
using UnityEngine.UI;

[DisallowMultipleComponent]
public class MainMenuManager : MonoBehaviour
{
   #region Editor Variables
   [SerializeField]
   [Tooltip("The button to click when starting the game.")]
   private Button m_PlayButton;
   #endregion

   #region OnEnable, Set Ups, and Resetters
   private void OnEnable()
   {
      GameManager.EndGameEvent += ActivatePlayButton;
   }
   #endregion

   #region OnDisable and Other Enders
   private void OnDisable()
   {
      GameManager.EndGameEvent -= ActivatePlayButton;
   }
   #endregion

   #region Activater Methods
   private void ActivatePlayButton(int num)
   {
      m_PlayButton.gameObject.SetActive(true);
   }
   #endregion
}
