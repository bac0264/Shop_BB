using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool
{
    public GameObject source;
    public int Amount;
    [HideInInspector]
    public List<GameObject> listActive;
    [HideInInspector]
    public List<GameObject> listDeactive;
    [HideInInspector]
    public List<GameObject> waitRemove;
}
public class PoolManager : SerializedMonoBehaviour
{
    public static PoolManager Instance;
    public Dictionary<string, Pool> PoolDic;
    // Start is called before the first frame update
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        foreach (KeyValuePair<string, Pool> kp in PoolDic)
        {
            //Debug.Log(kp.Value.Amount);
            kp.Value.listActive = new List<GameObject>();
            kp.Value.listDeactive = new List<GameObject>();
            kp.Value.waitRemove = new List<GameObject>();
            for (int i = 0; i < kp.Value.Amount; i++)
            {
                GameObject obj = Instantiate(kp.Value.source);
                obj.SetActive(false);
                kp.Value.listDeactive.Add(obj);
            }
        }
    }
    public GameObject Instantiate(string key)
    {
        Pool pool = PoolDic[key];
        GameObject res;
        if (pool.listDeactive.Count > 0)
        {
            res = pool.listDeactive[0];
            pool.listDeactive.RemoveAt(0);
        }
        else
        {
            res = pool.listActive[0];
            pool.listActive.RemoveAt(0);
        }
        res.SetActive(true);
        pool.listActive.Add(res);
        return res;
    }
    public void Remove(string key,GameObject obj,float time=0)
    {
        StartCoroutine(RemoveIEnumerator(key, obj, time));
    }
    IEnumerator RemoveIEnumerator(string key, GameObject obj, float time)
    {
        Pool pool = PoolDic[key];
        yield return new WaitForSeconds(time);
        obj.SetActive(false);
        pool.listDeactive.Add(obj);
        pool.listActive.Remove(obj);
    }
}
