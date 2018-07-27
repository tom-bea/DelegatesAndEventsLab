using UnityEngine;

public abstract class ObjectPoolManager<Type> : MonoBehaviour where Type : MonoBehaviour
{
   #region Editor Variables
   [SerializeField]
   [Tooltip("The prefab of the object to pool.")]
   private Type m_Object;

   [SerializeField]
   [Tooltip("The number of objects to instantiate in the beginning.")]
   private int m_NumToMake;
   #endregion

   #region Non-Editor Variables
   // All of the instantiated game objects in an easy to access array
   private Type[] m_PooledObjects;

   // The next game object to cycle through
   private int m_Next;
   #endregion

   #region First Time Initialization and Set Up
   private void Awake()
   {
      m_PooledObjects = new Type[m_NumToMake];
      for (int i = 0; i < m_NumToMake; i++)
      {
         m_PooledObjects[i] = Instantiate(m_Object);
         m_PooledObjects[i].gameObject.SetActive(false);
      }
      
      m_Next = 0;
   }
   #endregion
   
   #region Getters
   public Type GetNextGO()
   {
      m_Next = (m_Next + 1) % m_PooledObjects.Length;
      return m_PooledObjects[m_Next];
   }
   #endregion
}
