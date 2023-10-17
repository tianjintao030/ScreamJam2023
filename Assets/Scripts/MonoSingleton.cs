using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    /// <summary>
    /// 
    /// </summary>
    public class MonoSingleton<T> : MonoBehaviour where T:MonoSingleton<T>
    {
        private static T _instance;
        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    //在场景中根据类型查找引用
                    _instance = FindObjectOfType<T>();
                    if (_instance == null)
                    {
                        //创建脚本对象(立即执行Awake)
                        _instance = new GameObject("Singleton of " + typeof(T)).AddComponent<T>();
                        //DontDestroyOnLoad(_instance);
                    }
                    if (_instance == null)
                    {
                        Debug.LogError("Failed to create instance of" + typeof(T));
                    }
                }
                return _instance;
            }
        }
        protected void Awake()
        {
            if (_instance == null)
            {
                _instance = this as T;
            }
            Init();
        }
        public virtual void Init()
        { }
    }

