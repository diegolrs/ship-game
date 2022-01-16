using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    List<GameObject> _objects = new List<GameObject>();

    public void InstantiateObjects(GameObject prefab, int quantity)
    {
        for(int i = 0; i < quantity; i++)
        {
            _objects.Add(InstantiateObject(prefab));
        }
    }

    private GameObject InstantiateObject(GameObject prefab, bool enabled=false)
    {
        GameObject _ = Instantiate(prefab, Vector3.zero, Quaternion.identity) as GameObject;
        _.SetActive(enabled);
        return _;
    }

    public GameObject PoolObject(GameObject prefab)
    {
        for(int i = 0; i < _objects.Count; i++)
        {
            if(!_objects[i].activeInHierarchy)
                return _objects[i];
        }

        var obj = InstantiateObject(prefab);
        _objects.Add(obj);
        return obj;
    }
}