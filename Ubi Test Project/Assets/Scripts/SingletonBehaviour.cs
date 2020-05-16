using UnityEngine;
using System.Collections;

namespace UbiTest
{
    public class SingletonBehaviour<T> : MonoBehaviour // petite class que j'aime bien faire pour simplifier l'ajout de singletons
        where T : SingletonBehaviour<T>
    {
        private static T s_instance = null;
        public static T Instance
        {
            get
            {
#if UNITY_EDITOR
                if (s_instance == null && !Application.isPlaying)
                {
                    s_instance = FindObjectOfType(typeof(T)) as T;
                }
#endif
                return s_instance;
            }
        }

        protected virtual void Awake()
        {
            s_instance = this as T;
        }

        protected virtual void OnDestroy()
        {
            s_instance = null;
        }
    }
}
