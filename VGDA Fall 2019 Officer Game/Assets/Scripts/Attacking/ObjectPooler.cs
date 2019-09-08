using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObjectPooler : MonoBehaviour
{
    //Slap this boi onto the empty gameObject to hold the bullets

    public List<GameObject> genericBulletPool = new List<GameObject>();
    public GameObject genericBulletPrefab = null;
    public int totalGenericBullets = 50;

    private int instantiatedGenericBullets = 0;

    private void Start()
    {
        genericBulletPool = new List<GameObject>();
        for(int i = 0; i < totalGenericBullets; i++)
        {
            GameObject obj = Instantiate(genericBulletPrefab);
            obj.transform.parent = transform;
            obj.SetActive(false);
            genericBulletPool.Add(obj);

        }
    }

    public GameObject GetGenericBullet()
    {
        for(int i = 0; i < genericBulletPool.Count; i++)
        {
            if (!genericBulletPool[i].activeInHierarchy)
                return genericBulletPool[i];
        }

        GameObject obj = Instantiate(genericBulletPrefab);
        obj.transform.parent = transform;

        obj.SetActive(false);
        genericBulletPool.Add(obj);
        instantiatedGenericBullets++;

        return genericBulletPool[totalGenericBullets - 1];
    }

    private void OnApplicationQuit()
    {
        Debug.Log("There are " + instantiatedGenericBullets + " new bullets in the " + this.gameObject.name + " gameObject.");
    }
}
