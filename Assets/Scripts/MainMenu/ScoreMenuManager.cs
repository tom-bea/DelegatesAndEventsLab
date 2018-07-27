using UnityEngine;
using UnityEngine.UI;

[DisallowMultipleComponent]
public class ScoreMenuManager : MonoBehaviour
{
   #region Editor Variables
   [SerializeField]
   [Tooltip("The text component that displays the player's score from the" +
      " last game and the best score throughout the whole session.")]
   private Text m_ScoreText;
   #endregion

   #region Non-Editor Variables
   // The score received in the last game
   private int m_LastScore = 0;

   // The best score received in this play session
   private int m_BestScore = 0;
   #endregion

   #region OnEnable, Set Ups, and Resetters
   private void OnEnable()
   {
      GameManager.EndGameEvent += UpdateScore;
   }
   #endregion

   #region OnDisable and Other Enders
   private void OnDisable()
   {
      GameManager.EndGameEvent -= UpdateScore;
   }
   #endregion

   #region Set Score
   private void UpdateScore(int newScore)
   {
      if (newScore > m_BestScore)
         m_BestScore = newScore;

      m_LastScore = newScore;

      m_ScoreText.gameObject.SetActive(true);
      m_ScoreText.text = "Best Score: " + m_BestScore + "\nLast Score: " + m_LastScore;
   }
   #endregion
}
