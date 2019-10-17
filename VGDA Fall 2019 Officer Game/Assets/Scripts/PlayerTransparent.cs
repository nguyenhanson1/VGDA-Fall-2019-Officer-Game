using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTransparent : MonoBehaviour
{
    [SerializeField] private MoveReticle cursor;
    [SerializeField] private Material material;

    private Material oldMat;
    private Material newMat;

    private void OnEnable()
    {
        GameManager.StartOccurred += GetColors;
        GameManager.UpdateOccurred += Transparent;
    }
    private void OnDisable()
    {
        GameManager.StartOccurred -= GetColors;
        GameManager.UpdateOccurred -= Transparent;
    }

    private void GetColors()
    {
        oldMat = newMat = material;
        Color transparent = material.color;
        transparent.a = 0.5f;
        newMat.color = transparent;
    }
    private void Transparent()
    {
        Vector2 cursorPos = cursor.CursorPosition;
        Debug.Log(cursorPos);

        if(Mathf.Abs(cursorPos.x) <= 0.1)
        {
            material = newMat;
        }
        else
        {
            material = oldMat;
        }
    }
}
