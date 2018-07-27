using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[DisallowMultipleComponent]
[RequireComponent(typeof(Text))]
public class Countdown : MonoBehaviour
{
   #region Delegates and Events
   // TODO: CODE HERE
   #endregion

   #region Editor Variables
   [SerializeField]
   [Tooltip("How long before the game officially starts after pressing play.")]
   private int m_Start = 3;
   #endregion

   #region Cached Components
   private Text m_Text;
   #endregion

   #region First Time Initialization and Set Up
   private void Awake()
   {
      m_Text = GetComponent<Text>();
   }
   #endregion

   #region OnEnable, Set Ups, and Resetters
   private void OnEnable()
   {
      StartCoroutine(BeginCountdown());
   }
   #endregion

   #region OnDisable and Other Enders
   private void OnDisable()
   {
      // TODO: CODE HERE
   }
   #endregion

   #region Countdown Methods
   private IEnumerator BeginCountdown()
   {
      for (int i = m_Start; i > 0; i--)
      {
         m_Text.text = i.ToString();
         yield return new WaitForSeconds(1);
      }

      m_Text.text = "GO!";
      yield return new WaitForSeconds(0.5f);
      
      gameObject.SetActive(false);
   }
   #endregion
}
