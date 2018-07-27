using System.Collections;
using UnityEngine;

public class Spawner : ObjectPoolManager<MonoBehaviour>
{
   #region Editor Variables
   [SerializeField]
   [Tooltip("How long the spawn edges at the left and right should be. Also" +
      " the distance from which objects will spawn on the top and bottom.")]
   private float m_SpawnBoxLength;

   [SerializeField]
   [Tooltip("How wide the spawn edges at the top and bottom should be. Also" +
      " the distance from which objects will spawn on the left and right.")]
   private float m_SpawnBoxWidth;

   [SerializeField]
   [Tooltip("A little bit of a buffer so that nothing spawns on the wall.")]
   private float m_Buffer;

   [SerializeField]
   [Tooltip("Minimum number of seconds before the next object is spawned.")]
   private float m_MinTimeToSpawn;

   [SerializeField]
   [Tooltip("Maximum number of seconds before the next object is spawned.")]
   private float m_MaxTimeToSpawn;
   #endregion

   #region OnEnable, Set Ups, and Resetters
   private void OnEnable()
   {
      GameManager.StartGameEvent += StartSpawning;
      GameManager.EndGameEvent += StopSpawning;
   }
   #endregion

   #region OnDisable and Other Enders
   private void OnDisable()
   {
      GameManager.StartGameEvent -= StartSpawning;
      GameManager.EndGameEvent -= StopSpawning;
   }
   #endregion

   #region Spawning Methods
   private void StartSpawning(int num)
   {
      StartCoroutine(SpawnLoop());
   }

   private IEnumerator SpawnLoop()
   {
      while (true)
      {
         yield return new WaitForSeconds(Random.Range(m_MinTimeToSpawn, m_MaxTimeToSpawn));

         switch (Random.Range(0, 4))
         {
            case 0:
               SpawnObject(Random.Range(-m_SpawnBoxWidth, m_SpawnBoxWidth), m_SpawnBoxLength + m_Buffer);
               break;
            case 1:
               SpawnObject(Random.Range(-m_SpawnBoxWidth, m_SpawnBoxWidth), -m_SpawnBoxLength - m_Buffer);
               break;
            case 2:
               SpawnObject(m_SpawnBoxWidth + m_Buffer, Random.Range(-m_SpawnBoxLength, m_SpawnBoxLength));
               break;
            case 3:
               SpawnObject(-m_SpawnBoxWidth - m_Buffer, Random.Range(-m_SpawnBoxLength, m_SpawnBoxLength));
               break;
         }
      }
   }

   private void SpawnObject(float x, float y)
   {
      GameObject go = GetNextGO().gameObject;
      go.transform.position = new Vector2(x, y);
      go.SetActive(true);
   }

   private void StopSpawning(int num)
   {
      StopAllCoroutines();
   }
   #endregion
}
