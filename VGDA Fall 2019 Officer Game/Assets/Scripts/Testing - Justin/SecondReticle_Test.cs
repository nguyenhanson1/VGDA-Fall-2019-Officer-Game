using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondReticle_Test : MonoBehaviour
{
    [SerializeField] private RectTransform reticle = null;
    [SerializeField] private Camera persCam = null;
    [SerializeField] private Bullet bullet = null;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = persCam.WorldToViewportPoint(persCam.ScreenToWorldPoint(new Vector3(reticle.position.x, reticle.position.y, (bullet.Speed * bullet.DespawnTime) / 2)));
        pos.x = pos.x * Screen.width;
        pos.y = pos.y * Screen.height;
        transform.position = pos;
    }
}
