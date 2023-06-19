using UnityEngine;

namespace FlushGameCase.Core
{
    public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static volatile T instance = null;

        public static T Instance => instance;
        
        [SerializeField] private bool dontDestroyOnLoad = false;

        private void Awake()
        {
            Singleton();
        }

        private void Singleton()
        {
            if (dontDestroyOnLoad)
            {
                if (instance == null)
                {
                    instance = this as T;
                    DontDestroyOnLoad(gameObject);
                }
                else
                {
                    Destroy(gameObject);
                }
            }
            else
            {
                if (instance == null)
                {
                    instance = this as T;
                }
            }
        }
    }
}
