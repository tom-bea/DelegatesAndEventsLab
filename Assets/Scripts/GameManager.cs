using System.Collections;
using UnityEngine;

[DisallowMultipleComponent]
public class GameManager : MonoBehaviour
{
   #region Delegates and Events
   public delegate void GameStateDelegate(int num);
   public static event GameStateDelegate StartGameEvent;
   public static event GameStateDelegate FinalSprintEvent;
   public static event GameStateDelegate EndGameEvent;
   #endregion

   #region Editor Variables
   [SerializeField]
   [Tooltip("The player's prefab. This will be spawned at the start of every game.")]
   private PlayerController m_PlayerPrefab;

   [SerializeField]
   [Tooltip("How long each game lasts (in seconds).")]
   private int m_SecondsPerGame = 15;

   [SerializeField]
   [Tooltip("How many seconds in the final sprint. This should be smaller than the" +
      " total number of seconds a round takes.")]
   private int m_SecondsPerFinalSprint;
   #endregion

   #region Non-Editor Variables
   // The current score
   private int m_CurrentScore = 0;
   #endregion

   #region Cached Instance References
   private PlayerController m_Player;
   #endregion

   #region First Time Initialization and Set Up
   private void Awake()
   {
      m_Player = Instantiate(m_PlayerPrefab);
      m_Player.gameObject.SetActive(false);
   }
   #endregion

   #region OnEnable, Set Ups, and Resetters
   private void OnEnable()
   {
      // TODO: CODE HERE
      // TODO: CODE HERE
      EnemyController.PlayerDestroyedEvent += PlayerDestroyed;
   }

   private void StartGame()
   {
      m_CurrentScore = 0;

      m_Player.transform.position = Vector2.zero;
      m_Player.transform.rotation = Quaternion.identity;
      m_Player.gameObject.SetActive(true);

      StartCoroutine(GameLoop());
   }
   #endregion

   #region Game Loop Updates
   private IEnumerator GameLoop()
   {
      if (StartGameEvent != null)
         StartGameEvent(0);

      yield return new WaitForSeconds(m_SecondsPerGame - m_SecondsPerFinalSprint);

      if (FinalSprintEvent != null)
         FinalSprintEvent(m_SecondsPerFinalSprint);

      yield return new WaitForSeconds(m_SecondsPerFinalSprint);

      EndGame();
   }
   #endregion

   #region OnDisable and Other Enders
   private void OnDisable()
   {
      // TODO: CODE HERE
      // TODO: CODE HERE
      EnemyController.PlayerDestroyedEvent -= PlayerDestroyed;
   }

   private void EndGame()
   {
      StopAllCoroutines();

      m_Player.gameObject.SetActive(false);

      if (EndGameEvent != null)
         EndGameEvent(m_CurrentScore);
   }
   #endregion

   #region Score Tracking Methods
   private void AddPoint()
   {
      m_CurrentScore++;
   }
   #endregion

   #region Player Destroyed Methods
   private void PlayerDestroyed()
   {
      EndGame();
   }
   #endregion
}
