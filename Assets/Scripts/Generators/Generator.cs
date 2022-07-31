using UnityEngine;
using UnityEngine.Pool;
using Random = UnityEngine.Random;

namespace GDRTest.Generators
{
    public class Generator<T> : MonoBehaviour where T : MonoBehaviour
    {
        [SerializeField]
        protected Transform _parent;

        [SerializeField]
        [Header("Префабы")]
        protected T[] _prefabs;



        [Header("Настройки пула")]
        [SerializeField]
        protected float _cooldown = 1;
        [SerializeField]
        protected int _poolSize;
        [SerializeField]
        protected int _maxPoolSize;
        [SerializeField]
        protected bool _checkCollection = false;

        protected string _coroutineName;
        protected int _id;

        protected string _tag = "";

        protected bool _isPoolCreated = false;

        protected ObjectPool<GameObject> _pool;

        protected ScreenController _screen;

        protected virtual void Awake()
        {
            _screen = ScreenController.Instance;
        }

        public virtual void Init(in int maxNumOfObjects)
        {
            for(int i = 0; i < maxNumOfObjects; i+=1)
            {
                Spawn();
            }
        }

        protected virtual void Start()
        {
            _isPoolCreated = false;

            _pool = new ObjectPool<GameObject>(
                    createFunc: () => SpawnRandomGameObject(),
                    actionOnGet: (obj) => ResetObject(obj),
                    actionOnRelease: (obj) => ReleaseObject(obj),
                    actionOnDestroy: (obj) => Destroy(obj),
                    collectionCheck: _checkCollection,
                    defaultCapacity: _poolSize,
                    maxSize: _maxPoolSize
                );

            _isPoolCreated = true;
        }

        protected virtual T CreateObjectById(in int id, in Vector2 position,
            in Quaternion rotation, in Transform parent)
        {
            return Instantiate(
               _prefabs[id], position,
               rotation, parent
           );
        }

        /*public void StartSpawning()
        {
            _coroutineName = CoroutineManager.Instance.Wait(_cooldown,
                new System.Action(() => { Spawn(); }));
        }

        public void StopSpawning()
        {
            CoroutineManager.Instance.StopWait(_coroutineName);
        }*/

        protected virtual GameObject Spawn()
        {
            var obj = _pool.Get();       
            return obj;
        }

        protected virtual GameObject SpawnRandomGameObject()
        {
            _id = Random.Range(0, _prefabs.Length - 1);

            var obj = CreateObjectById(_id, new Vector2(0, 0), Quaternion.identity, _parent);

            return obj.gameObject;
        }

        //virtual вместо abstract, так как метод не обязателен
        protected virtual void SetUpObject(T obj, in Vector2 position, int id)
        {
        }
        //способ очистки грязного объекта
        protected virtual void ResetObject(GameObject obj)
        {
            obj.SetActive(true);
        }

        protected virtual void ReleaseObject(GameObject obj)
        {
            obj.SetActive(false);
        }

        public virtual void Clear()
        {
            foreach (Transform obj in gameObject.GetComponentInChildren<Transform>())
            {
                Destroy(obj.gameObject);
            }

            _pool.Clear();
            _pool.Dispose();
        }
    }
}
