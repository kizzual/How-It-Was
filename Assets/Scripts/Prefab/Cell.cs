using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Cell : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
	private bool dragOnSurfaces = true;
	public bool canMove = false;
	public bool canSwap = false;
	[SerializeField] private GameObject m_Parent;
	 public int ID;
	private Vector3 _position;
	//public Transform _rotation;
	public GameObject rotate_image;
	 public Image main_image;
	private GameObject Point_parrent;
	private RectTransform m_DraggingPlane;
	private CreateObjects _createObjects;
	private Inventory _inventory;
	private SoundPlay _sound;
	public GameObject RotatePanel_1;
	public GameObject RotatePanel_2;

	public GameObject MovePanel;
	public bool inInventory = true;
	public bool notMove;
	public bool FinishGameMove;
	
	public GameObject swap_parrent;
	public enum Type
    {
		first,
		second,
		third,
		fourth
    }
	public Type level;

	private void Start()
	{
		FinishGameMove = false;
		_sound = FindObjectOfType<SoundPlay>();
		_createObjects = FindObjectOfType<CreateObjects>();
		_inventory = FindObjectOfType<Inventory>();
		Point_parrent = GameObject.FindGameObjectWithTag("Points_spawn");
		MovePanel.SetActive(false);
		RotatePanel_1.SetActive(false);
		RotatePanel_2.SetActive(false);
		
		//LevelCheck();

	}

    public void OnBeginDrag(PointerEventData eventData)
	{
		if(!FinishGameMove)
        {
			if (canMove)
			{
				_position = m_Parent.transform.position;
				GetComponent<CircleCollider2D>().enabled = false;
			}
			if (canSwap)
			{
				GetComponent<CircleCollider2D>().enabled = false;

			}

		}

	}

	public void OnDrag(PointerEventData data)
	{
		if (!FinishGameMove)
		{
			if (canMove || canSwap)
			{
				if (level == Type.first || level == Type.second)
				{
					SetDraggedPosition(data);
				}

			}
			if (level == Type.third || Type.fourth == level)
			{
				SetDraggedPosition(data);

			}

		}
	}
	private void SetDraggedPosition(PointerEventData data)
    {
		if (canMove || canSwap)
		{
			if (level == Type.first || level == Type.second)
			{
				notMove = false;
				if (dragOnSurfaces && data.pointerEnter != null && data.pointerEnter.transform as RectTransform != null)
					m_DraggingPlane = data.pointerEnter.transform as RectTransform;

				Vector3 globalMousePos;
				if (RectTransformUtility.ScreenPointToWorldPointInRectangle(m_DraggingPlane, data.position, data.pressEventCamera, out globalMousePos))
				{
					m_Parent.transform.position = globalMousePos;
				}
			}
        }
		if (level == Type.third || Type.fourth == level)
        {
			if (dragOnSurfaces && data.pointerEnter != null && data.pointerEnter.transform as RectTransform != null)
				m_DraggingPlane = data.pointerEnter.transform as RectTransform;

			Vector3 globalMousePos;
			if (RectTransformUtility.ScreenPointToWorldPointInRectangle(m_DraggingPlane, data.position, data.pressEventCamera, out globalMousePos))
			{
				m_Parent.transform.position = globalMousePos;
			}
			notMove = false;
		}
    }
    private void OnMouseDown()
    {
		if (!FinishGameMove)
		{
			_sound.obj_tap.Play();
			canMove = true;
			canSwap = true;
			notMove = true;
		}
		
	}
    private void OnMouseDrag()
    {
		if (level == Type.first || level == Type.second)
		{
			canSwap = true;
		}
	}


    private void OnMouseUp()
    {

		if (!FinishGameMove && !inInventory)
		{
			if (Type.second == level)
			{
				if (notMove)
				{
					if (RotatePanel_1.activeSelf)
					{
						RotatePanel_1.SetActive(false);
						RotatePanel_2.SetActive(false);
					}
					else
					{
						RotatePanel_1.SetActive(true);
						RotatePanel_2.SetActive(true);
						_createObjects.resetrotateImage(this);
					}
				}
			}

			else if (Type.third == level || Type.fourth == level)
			{
				if (notMove)
				{
					if (MovePanel.activeSelf)
					{
						Debug.Log("Turn off");
						MovePanel.SetActive(false);
						RotatePanel_1.SetActive(false);
						RotatePanel_2.SetActive(false);
					}
					else
					{
						Debug.Log(" ON");
						MovePanel.SetActive(true);
						RotatePanel_1.SetActive(true);
						RotatePanel_2.SetActive(true);
						_createObjects.resetrotateImage(this);

					}
				}

			}
		}
		
	}
	public void HideRotationSPrites()
    {
		MovePanel.SetActive(false);
		RotatePanel_1.SetActive(false);
		RotatePanel_2.SetActive(false);
	}
	public void OnEndDrag(PointerEventData eventData)
	{
		if (!FinishGameMove)
		{
			if (canMove)
			{
				if (Type.first == level || Type.second == level)
				{
					GetComponent<CircleCollider2D>().enabled = true;
					m_Parent.transform.position = new Vector3(transform.position.x + 0.001f, transform.position.y, transform.position.z);
					StartCoroutine(ret());
				}
			
			}
			if (Type.third == level || Type.fourth == level)
			{
				GetComponent<CircleCollider2D>().enabled = true;

				m_Parent.transform.position = new Vector3(transform.position.x + 0.001f, transform.position.y, transform.position.z);

				_createObjects.InventoryCheck();
                if (!inInventory)
                {
                    RotatePanel_1.SetActive(true);
                    RotatePanel_2.SetActive(true);
                    MovePanel.SetActive(true);
                    _createObjects.resetrotateImage(this);

                }
                if (inInventory)
                {
                    StartCoroutine(ret());
                }
            }

			if (canSwap)
			{
				if (Type.first == level || Type.second == level)
				{

					GetComponent<CircleCollider2D>().enabled = true;
					m_Parent.transform.position = new Vector3(transform.position.x + 0.001f, transform.position.y, transform.position.z);
					StartCoroutine(ret());
				}
			}
		}
    }
	IEnumerator ret()
    {
		yield return new WaitForSeconds(0.11f);
		if (canMove)
        {
			m_Parent.transform.position = _position;
        }
       /* if (canSwap)
        {

		Debug.Log("312");
            m_Parent.transform.position = _position;

        }*/
        canSwap = false;


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

		if (collision.CompareTag("Point") && collision.gameObject != this && Type.first == level)
		{
			canMove = false;
			if (!collision.GetComponent<PointCheck>().isEmpty && canSwap && !inInventory)
			{
				var currenParrent = swap_parrent;
				Cell tarteGo = collision.GetComponentInChildren<Cell>();
				var targetParrent = collision.gameObject;
				var targetPos = collision.gameObject.transform.position;
				var target_pointRotation = collision.gameObject.transform.rotation;

				swap_parrent = tarteGo.swap_parrent;
				tarteGo.swap_parrent = currenParrent;
				tarteGo.m_Parent.transform.SetParent(currenParrent.transform);
				tarteGo.m_Parent.transform.localPosition = currenParrent.transform.position;
				tarteGo.transform.rotation = m_Parent.transform.rotation;


				m_Parent.transform.SetParent(targetParrent.transform);

				m_Parent.transform.position = targetPos;
				transform.rotation = target_pointRotation;
				m_Parent.transform.localScale = new Vector3(1, 1, 1);

				Debug.Log("SWAP");

			}
			else if (!collision.GetComponent<PointCheck>().isEmpty && canSwap && inInventory)
			{
				var currenParrent = swap_parrent;
				Cell tarteGo = collision.GetComponentInChildren<Cell>();
				var targetParrent = collision.gameObject;
				var targetPos = collision.gameObject.transform.position;
				var target_pointRotation = collision.gameObject.transform.rotation;
				swap_parrent = tarteGo.swap_parrent;

				tarteGo.swap_parrent = _inventory.gameObject;
				tarteGo.m_Parent.transform.SetParent(_inventory.gameObject.transform);
				tarteGo.m_Parent.transform.localPosition = _inventory.transform.position;
				tarteGo.transform.Rotate(0, 0, 0);
				tarteGo.inInventory = true;

				_inventory.currentInventory.Add(tarteGo.m_Parent);

				m_Parent.transform.SetParent(targetParrent.transform);
				m_Parent.transform.position = targetPos;
				transform.rotation = target_pointRotation;
				m_Parent.transform.localScale = new Vector3(1, 1, 1);
				_inventory.currentInventory.Remove(m_Parent);
				inInventory = false;
				Debug.Log("Inv swap");


			}

			else if (collision.GetComponent<PointCheck>().isEmpty)
			{
				swap_parrent = collision.gameObject;                                      // родитель 
				inInventory = false;                                                               // убираем с инвенторя
																								   //смена родителя и позиции
				m_Parent.transform.SetParent(collision.gameObject.transform);
				m_Parent.transform.position = collision.gameObject.transform.position;
				_position = m_Parent.transform.position;
				transform.rotation = collision.gameObject.transform.rotation;
				m_Parent.transform.localScale = new Vector3(1, 1, 1);
				_inventory.currentInventory.Remove(m_Parent);                               // удаление из секущего списка инвентарь панели

				//		collision.gameObject.GetComponent<CircleCollider2D>().enabled = false;          // отключение колайдера у точки
				collision.GetComponent<PointCheck>().isEmpty = false;                           // отключение свободы у точки
				Debug.Log(collision.GetComponent<PointCheck>().isEmpty);
				_createObjects.InventoryCheck();                                            // чек на количество ГО в инвентаре
				_createObjects.resetrotateImage(this);                                      // отключение Имейджа поворотов (кроме себя)
			}

		}

		else if (collision.CompareTag("Point") && collision.gameObject != this && Type.second == level)
		{
			canMove = false;
			if (!collision.GetComponent<PointCheck>().isEmpty && canSwap && !inInventory)
			{
				var currenParrent = swap_parrent;
				Cell tarteGo = collision.GetComponentInChildren<Cell>();
				var targetParrent = collision.gameObject;
				var targetPos = collision.gameObject.transform.position;
				var target_pointRotation = collision.gameObject.transform.rotation;

				swap_parrent = tarteGo.swap_parrent;
				tarteGo.swap_parrent = currenParrent;
				tarteGo.m_Parent.transform.SetParent(currenParrent.transform);
				tarteGo.m_Parent.transform.localPosition = currenParrent.transform.position;
				//		tarteGo.transform.rotation = m_Parent.transform.rotation;


				m_Parent.transform.SetParent(targetParrent.transform);

				m_Parent.transform.position = targetPos;
				//	transform.rotation = target_pointRotation;
				m_Parent.transform.localScale = new Vector3(1, 1, 1);

				RotatePanel_1.SetActive(true);
				RotatePanel_2.SetActive(true);
				_createObjects.resetrotateImage(this);

				Debug.Log("SWAP");

			}
			else if (!collision.GetComponent<PointCheck>().isEmpty && canSwap && inInventory)
			{
				var currenParrent = swap_parrent;
				Cell tarteGo = collision.GetComponentInChildren<Cell>();
				var targetParrent = collision.gameObject;
				var targetPos = collision.gameObject.transform.position;
				var target_pointRotation = collision.gameObject.transform.rotation;
				swap_parrent = tarteGo.swap_parrent;

				tarteGo.swap_parrent = _inventory.gameObject;
				tarteGo.m_Parent.transform.SetParent(_inventory.gameObject.transform);
				tarteGo.m_Parent.transform.localPosition = _inventory.transform.position;
				tarteGo.transform.Rotate(0, 0, 0);
				tarteGo.inInventory = true;
				_inventory.currentInventory.Add(tarteGo.m_Parent);

				m_Parent.transform.SetParent(targetParrent.transform);
				m_Parent.transform.position = targetPos;
				transform.rotation = target_pointRotation;
				m_Parent.transform.localScale = new Vector3(1, 1, 1);
				_inventory.currentInventory.Remove(m_Parent);
				inInventory = false;

				Debug.Log("Inv swap");
				RotatePanel_1.SetActive(true);
				RotatePanel_2.SetActive(true);
				_createObjects.resetrotateImage(this);

			}

			else if (collision.GetComponent<PointCheck>().isEmpty)
			{
				swap_parrent = collision.gameObject;                                      // родитель 
				inInventory = false;                                                               // убираем с инвенторя
																								   //смена родителя и позиции
				m_Parent.transform.SetParent(collision.gameObject.transform);
				m_Parent.transform.position = collision.gameObject.transform.position;
				_position = m_Parent.transform.position;
			//	transform.rotation = collision.gameObject.transform.rotation;
				m_Parent.transform.localScale = new Vector3(1, 1, 1);
				_inventory.currentInventory.Remove(m_Parent);                               // удаление из секущего списка инвентарь панели

				//		collision.gameObject.GetComponent<CircleCollider2D>().enabled = false;          // отключение колайдера у точки
				collision.GetComponent<PointCheck>().isEmpty = false;                           // отключение свободы у точки
				Debug.Log(collision.GetComponent<PointCheck>().isEmpty);
				_createObjects.InventoryCheck();
				RotatePanel_1.SetActive(true);
				RotatePanel_2.SetActive(true);
				_createObjects.resetrotateImage(this);                                      // отключение Имейджа поворотов (кроме себя)
			}

		}
		else if (collision.CompareTag("Collider") && collision.gameObject != this && Type.third == level && inInventory
			  || collision.CompareTag("Collider") && collision.gameObject != this && Type.fourth == level && inInventory)
		{

			canMove = false;
			inInventory = false;
			m_Parent.transform.SetParent(_createObjects.transform);

            _createObjects.resetrotateImage(this);
            RotatePanel_1.SetActive(true);
            RotatePanel_2.SetActive(true);
            MovePanel.SetActive(true);
            _inventory.currentInventory.Remove(m_Parent);
			_createObjects.InventoryCheck();


			//	_position = m_Parent.transform.position;
		}

	}

}
