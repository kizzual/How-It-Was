using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RotateArrow : MonoBehaviour,  IDragHandler, IBeginDragHandler
{

    public float Angle;
    public GameObject RotateObj;
    private bool rotate;
    [SerializeField] private Vector3 axis;// = Vector3.down;
 //   private Vector3 currentAxis;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }


    public void OnBeginDrag(PointerEventData eventData)
    {
       // currentAxis = transform.rotation.eulerAngles.z;
    }
    public void OnDrag(PointerEventData eventData)
    {

        axis = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 difference = axis;
        difference.Normalize();
        float rotation_z = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        RotateObj.transform.rotation = Quaternion.Euler(0f, 0f, rotation_z);

    }

   
}
