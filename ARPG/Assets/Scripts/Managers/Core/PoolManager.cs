using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using static UnityEngine.UI.Image;

public class Pool
{
    private IObjectPool<GameObject> _pool;
    private GameObject _origin;
    private string _key;
    private int _refCount;
    private bool _checkRef;

    public Pool(string key, GameObject origin, bool checkRef)
    {
        _key = key;
        _origin = origin;
        _checkRef = checkRef;
        _pool = new ObjectPool<GameObject>(OnCreate, OnGet, OnRelease, OnDestroy);
    }

    public void Push(GameObject go)
    {
        if (go.activeSelf == true)
        {
            if (_checkRef == true)
                _refCount--;

            _pool.Release(go);
        }
    }

    public GameObject Pop()
    {
        if (_checkRef == true)
            _refCount++;

        return _pool.Get();
    }

    private GameObject OnCreate()
    {
        GameObject go = Object.Instantiate(_origin);
        go.name = $"{_origin.name} (Pool)";
        return go;
    }

    private void OnGet(GameObject go)
    {
        go.gameObject.SetActive(true);
    }

    private void OnDestroy(GameObject go)
    {
        if (_checkRef == true)
            _refCount--;

        Object.Destroy(go);
        if (_checkRef == true && _refCount <= 0)
            Managers.Resource.Release(_key, _refCount);
    }

    private void OnRelease(GameObject go)
    {
        go.gameObject.SetActive(false);
    }

    public void Clear()
    {
        _pool.Clear();
        if (_checkRef == true)
            Managers.Resource.Release(_key, _refCount);
    }
}

public class PoolManager
{
    private Dictionary<string, Pool> _pools = new Dictionary<string, Pool>();

    public bool CheckPoolObject(GameObject go)
    {
        if (_pools.ContainsKey(go.name) == false)
            return false;

        return true;
    }

    public void Push(string key, GameObject go)
    {
        if (_pools.TryGetValue(key, out var pool) == false)
        {
            Object.Destroy(go);
            return;
        }

        pool.Push(go);
    }

    public GameObject Pop(GameObject origin, string key = "", bool checkRef = true)
    {
        if (_pools.TryGetValue(key, out var pool) == false)
        {
            pool = new Pool(key, origin, checkRef);
            _pools.Add(origin.name, pool);
        }

        return pool.Pop();
    }

    public void Clear()
    {
        foreach (var pool in _pools.Values)
            pool.Clear();

        _pools.Clear();
    }
}
