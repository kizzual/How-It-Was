using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointCheck : MonoBehaviour
{
    public int ID;
    [SerializeField] private Vector3 _rotation;
    [SerializeField] private Vector3 _position;
    public Image m_image;
    public Image children_image;
    public bool isEmpty;

    void Start()
    {
        isEmpty = true;
    }
    void Update()
    {
    }
    /*void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, 0.5f);
    }*/
    public void ShowImage()
    {
        m_image.color = new Color(0, 0, 0, 0);
        children_image.color = new Color(255, 255, 255, 255);
    }
    public void SHowFinishImage()
    {
        m_image.color = new Color(0, 0, 0, 0);
        children_image.color = new Color32(0, 0, 0, 70);
    //    children_image.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);

    }
    public void HideImage()
    {
        m_image.color = new Color32(0, 0, 0, 100);
        children_image.color = new Color(255, 255, 255, 0);

    }
    public void HideAll()
    {
        m_image.color = new Color(0, 0, 0, 0);
        children_image.color = new Color(255, 255, 255, 0);

    }
}
