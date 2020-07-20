using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FirstPersonShooter
{
    /// <summary>
    /// Хранилище закешированных объектов
    /// </summary>
    public class CacheObjectRepo : MonoBehaviour
    {
        [SerializeField] private List<CacheObject> _cacheObjects;
        private Dictionary<string, Queue<GameObject>> _cacheDictionary;

        #region Singleton

        public static CacheObjectRepo Instance;
        private object _lock;

        private void Awake()
        {
            //TODO: реализовать безопасный синглтон
            Instance = this;
        }

        #endregion

        private void Start()
        {
            _cacheDictionary = new Dictionary<string, Queue<GameObject>>();

            foreach (CacheObject pool in _cacheObjects)
                RegisterInternalPool(pool);
        }


        public bool RegisterPool<T>(int poolSize, GameObject prefab, Type type, string tag)
        {
            if (!_cacheDictionary.ContainsKey(tag))
            {
                CacheObject pool = new CacheObject(prefab, poolSize, tag);
                RegisterInternalPool(pool);
                return true;
            }

            return false;
        }

        private void RegisterInternalPool(CacheObject pool)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.PoolSize; i++)
            {
                GameObject obj = Instantiate(pool.Prefab,transform);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            _cacheDictionary.Add(pool.Tag, objectPool);
        }

        public GameObject SpawnCachedObject<T>(string tag)
        {
            if (!_cacheDictionary.ContainsKey(tag))
            {
                Debug.LogWarning($"Pool with tag {tag} doesn't exist");
                //HACK
                //Пока не придумал как сделать по нормальному
                return null;
            }

            GameObject obj = _cacheDictionary[tag].Dequeue();
            obj.SetActive(true);
            return obj;
        }

        /// <summary>
        /// Получить объект из пула
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="position"></param>
        /// <param name="rotation"></param>
        /// <param name="strategy"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public GameObject SpawnCachedObject(string tag, Vector3 position, Quaternion rotation,
            PoolingStratagy strategy)
        {
            if (!_cacheDictionary.ContainsKey(tag))
            {
                Debug.LogWarning($"Pool with tag {tag} doesn't exist");
                //HACK
                //Пока не придумал как сделать по нормальному
                return null;
            }

            GameObject obj = _cacheDictionary[tag].Dequeue();
            obj.SetActive(true);
            obj.transform.position = position;
            obj.transform.rotation = rotation;
            switch (strategy)
            {
                case PoolingStratagy.InstantlyEnqueue:
                    _cacheDictionary[tag].Enqueue(obj);
                    break;
                case PoolingStratagy.EnqueueManuallyLater:
                    ExpandPoolIfNeeded(tag);
                    break;
                default:
                    throw new ArgumentOutOfRangeException("Указана некорректная стратегия кеширования");
            }

            return obj;
        }

        /// <summary>
        /// Вернуть объект в пул
        /// </summary>
        public void ReturnObjToPool(GameObject obj, string tag)
        {
            if (!_cacheDictionary.ContainsKey(tag))
            {
                Debug.LogWarning($"Pool with tag {tag} doesn't exist");
                return;
            }

            //TODO: оптимизировать
            if (_cacheDictionary[tag].Contains(obj))
                return;
            
            obj.SetActive(false);
            obj.transform.position = transform.position;
            _cacheDictionary[tag].Enqueue(obj);
        }
        
        private void ExpandPoolIfNeeded(string tag)
        {
            //TODO: прописать логику расширения пула
        }
    }
}