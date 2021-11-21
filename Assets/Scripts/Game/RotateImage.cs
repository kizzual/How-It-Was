using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;

public class RotateImage : MonoBehaviour
{
    [SerializeField] private Vector3 axis;// = Vector3.down;
    public float speeds;
    public GameObject RotateImg;
    void Start()
    {

    }

    void Update()
    {

    }
    private void OnMouseDown()
    {
    //    axis = new Vector3(transform.rotation.x, transform.rotation.y, transform.rotation.z);
    }
    void OnMouseDrag()
    {

        /*      Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
              // Угол между объектами
              float angle = Vector2.Angle(Vector2.up, position - transform.position);

              // Мгновенное вращение
              // transform.eulerAngles = new Vector3(0f, 0f, transform.position.x < position.x ? -angle : angle);
              // Вращение с задержкой
              transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, transform.position.x < position.x ? -angle : angle), speeds * Time.deltaTime);
      */





        axis = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 difference = axis;
        difference.Normalize();
        float rotation_z = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        RotateImg.transform.rotation = Quaternion.Euler(0f, 0f, rotation_z);


    }
}

