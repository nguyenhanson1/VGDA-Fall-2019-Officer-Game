using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingReticle : MonoBehaviour
{
    [SerializeField] private Camera persCam = null;
    [SerializeField] private Camera rotationCam = null;
    [SerializeField] private RectTransform mainReticle = null;
    [SerializeField] private Transform player = null;
    [SerializeField] private RectTransform rectTrans = null;

    private void OnEnable()
    {
        GameManager.UpdateOccurred += SetPosition;
    }
    private void OnDisable()
    {
        GameManager.UpdateOccurred -= SetPosition;
    }

    private void SetPosition()
    {
        Vector3 newPosition = rotationCam.WorldToScreenPoint((persCam.ScreenToWorldPoint(mainReticle.position)));
        rectTrans.position = newPosition;
    }
}
