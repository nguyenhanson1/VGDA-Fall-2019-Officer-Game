using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObjectPooler : MonoBehaviour
{
    //Slap this boi onto the empty gameObject to hold the bullets

    [Tooltip("Prefab of object with Bullet script being used.")]
    [SerializeField] private GameObject genericBulletPrefab = null;
    [Tooltip("Number of Bullets wanted to be instantiated at start of scene.")]
    [SerializeField] private int totalGenericBullets = 50;

    //List that's going to hold all the bullets
    private List<GameObject> genericBulletPool = new List<GameObject>();
    //Number of bullets made after the start of the game
    private int instantiatedGenericBullets = 0;

    private void OnEnable()
    {
        GameManager.StartOccurred += fillObjectPool;
    }
    private void OnDisable()
    {
        GameManager.StartOccurred -= fillObjectPool;
    }

    //Fill the ObjectPool with bullets
    private void fillObjectPool()
    {
        //Reset and fill list with bullets
        genericBulletPool = new List<GameObject>();
        for(int i = 0; i < totalGenericBullets; i++)
        {
            makeBullet();
        }
    }

    //Make a bullet and add it to the ObjectPool
    private void makeBullet()
    {
        GameObject obj = Instantiate(genericBulletPrefab);
        //Set the object with the ObjectPooler script the parent of the bullets
        obj.transform.parent = transform;
        obj.SetActive(false);
        genericBulletPool.Add(obj);
    }

    //Return a bullet ready to be shot
    public GameObject GetGenericBullet()
    {
        //Find an inactive bullet
        for(int i = 0; i < genericBulletPool.Count; i++)
        {
            if (!genericBulletPool[i].activeInHierarchy)
                return genericBulletPool[i];
        }

        //Make a new Bullet if none are inactive
        makeBullet();
        //Add to the new bullet counter
        instantiatedGenericBullets++;
        //Return the newly made bullet
        return genericBulletPool[totalGenericBullets - 1];
    }

    private void OnApplicationQuit()
    {
        //Print out the number of bullets made after the start of the scene
        Debug.Log("There are " + instantiatedGenericBullets + " new bullets in the " + this.gameObject.name + " gameObject.");
    }
}
