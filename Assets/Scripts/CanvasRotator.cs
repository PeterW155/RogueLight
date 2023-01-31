using System;
using UnityEngine;

public class CanvasRotator : MonoBehaviour
{
    [SerializeField] private RectTransform _canvasRectTransform;

    private void Update()
    {
        _canvasRectTransform.rotation = Quaternion.identity;
    }
}
