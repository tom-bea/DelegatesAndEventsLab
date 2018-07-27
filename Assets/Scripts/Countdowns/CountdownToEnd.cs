using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[DisallowMultipleComponent]
public class CountdownToEnd : MonoBehaviour
{
   #region Editor Variables
   [SerializeField]
   [Tooltip("The text component to use for all even numbers.")]
   private Text m_EvensText;

   [SerializeField]
   [Tooltip("The text component to use for all odd numbers.")]
   private Text m_OddsText;
   #endregion

   #region Non-Editor Variables
   // The color of the text. Makes the code a bit more consistent looking
   private Color m_TextColor;

   // How much time is left before the game ends
   private int m_TimeLeft;
   #endregion

   #region First Time Initialization and Set Up
   private void Awake()
   {
      m_TextColor = Color.white;
   }
   #endregion

   #region OnEnable, Set Ups, and Resetters
   private void OnEnable()
   {
      GameManager.FinalSprintEvent += BeginCountingDown;
      GameManager.EndGameEvent += StopCountdown;
   }
   #endregion

   #region OnDisable and Other Enders
   private void OnDisable()
   {
      GameManager.FinalSprintEvent -= BeginCountingDown;
      GameManager.EndGameEvent -= StopCountdown;
   }
   #endregion

   #region Countdown Methods
   private void BeginCountingDown(int start)
   {
      m_TimeLeft = start;

      m_EvensText.gameObject.SetActive(true);
      m_OddsText.gameObject.SetActive(true);

      m_EvensText.text = "";
      m_EvensText.color = new Color(m_TextColor.r, m_TextColor.g, m_TextColor.b, 0);
      m_OddsText.text = "";
      m_OddsText.color = new Color(m_TextColor.r, m_TextColor.g, m_TextColor.b, 0);

      if (m_TimeLeft % 2 == 0)
         StartCoroutine(Next(m_EvensText, true));
      else
         StartCoroutine(Next(m_OddsText, true));
   }
   
   private IEnumerator Next(Text text, bool isFirst = false)
   {
      if (!isFirst)
         yield return new WaitForSeconds(1f);

      if (m_TimeLeft < 1)
      {
         if (m_EvensText.gameObject.activeSelf)
            m_EvensText.gameObject.SetActive(false);
         if (m_OddsText.gameObject.activeSelf)
            m_OddsText.gameObject.SetActive(false);
         yield break;
      }

      if (m_TimeLeft % 2 == 0)
         StartCoroutine(Next(m_OddsText));
      else
         StartCoroutine(Next(m_EvensText));

      text.text = m_TimeLeft.ToString();
      m_TimeLeft--;

      float alpha = 0;
      while (text.color.a < 1)
      {
         alpha += Time.deltaTime;
         text.color = new Color(m_TextColor.r, m_TextColor.g, m_TextColor.b, alpha);
         yield return null;
      }

      while (text.color.a > 0)
      {
         alpha -= Time.deltaTime * 3;
         text.color = new Color(m_TextColor.r, m_TextColor.g, m_TextColor.b, alpha);
         yield return null;
      }

      text.color = new Color(m_TextColor.r, m_TextColor.g, m_TextColor.b, 0);
   }

   private void StopCountdown(int num)
   {
      StopAllCoroutines();
      if (m_EvensText.gameObject.activeSelf)
         m_EvensText.gameObject.SetActive(false);
      if (m_OddsText.gameObject.activeSelf)
         m_OddsText.gameObject.SetActive(false);
   }
   #endregion
}
