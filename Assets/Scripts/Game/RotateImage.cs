using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;

public class RotateImage : MonoBehaviour
{
    [SerializeField]
    private Transform _parentTransform;

    private Transform _myTransfrom;
    private Vector3 _myForward;

    private void Awake()
    {
        _myTransfrom = transform;
        if (_parentTransform == null) _parentTransform = _myTransfrom.parent;
        var forward = _parentTransform.forward;
        _myForward = _myTransfrom.position.x > _parentTransform.position.x ? forward : -forward;
    }

    private void OnMouseDrag()
    {
        Debug.Log("1");
        var position = _parentTransform.position;
        var mousePos = Input.mousePosition;
        mousePos.z = position.z;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);

        _parentTransform.rotation = Quaternion.LookRotation(_parentTransform.forward, Vector3.Cross(_myForward, mousePos - position));
    }
}

