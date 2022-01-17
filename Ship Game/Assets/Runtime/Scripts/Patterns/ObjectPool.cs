using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    List<GameObject> _objects;

    List<GameObject> ObjectList 
    {
        get 
        {
            if(_objects == null)
                _objects = new List<GameObject>();

            return _objects;
        }
    }

    private GameObject InstantiateObject(GameObject prefab, bool enabled=false)
    {
        GameObject obj = Instantiate(prefab, Vector3.zero, Quaternion.identity) as GameObject;
        obj.SetActive(enabled);

        obj.transform.parent = transform;
        ObjectList.Add(obj);

        return obj;
    }

    public GameObject PoolObject(GameObject prefab, bool enabled=false)
    {
        for(int i = 0; i < ObjectList.Count; i++)
        {
            if(!ObjectList[i].activeInHierarchy)
                return ObjectList[i];
        }

        return InstantiateObject(prefab, enabled);
    }
}