using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingGallery : MonoBehaviour
{
    [SerializeField] private Transform endPoint = null;
    [SerializeField] private int speed = 20;
    [SerializeField] private GameObject targetPrefab = null;

    [SerializeField] private int amtTargets = 10;
    [SerializeField] private List<GameObject> targets = new List<GameObject>();

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
        targets = new List<GameObject>();
        for (int i = 0; i < amtTargets; i++)
        {
            MakeStartTarget(i);
        }
    }

    private void MakeStartTarget(float iteration)
    {
        GameObject obj = Instantiate(targetPrefab);

        //Put block on the row
        Debug.Log((iteration / (amtTargets)));
        float percentage = (iteration / amtTargets);
        Vector3 translation = (endPoint.position - transform.position) * percentage;
        Vector3 xyz = transform.position + translation;
        obj.transform.position = xyz;

        ChangeVariables(obj);
        obj.transform.parent = transform;
        targets.Add(obj);

    }

    private void ChangeVariables(GameObject obj)
    {
        PracticeTarget target = obj.GetComponent<PracticeTarget>();
        target.speed = speed;
        target.start = transform.position;
        target.end = endPoint.position;

    }
}
