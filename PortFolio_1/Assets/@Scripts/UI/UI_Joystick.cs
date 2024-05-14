using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Joystick : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    [SerializeField]
    Image _background;

    [SerializeField]
    Image _handler;

    Vector2 _touchPoistion;
    float _joystickRadius;
    Vector2 _moveDir;

    bool isActive = false;

    void Start()
    {
        _joystickRadius = _background.gameObject.GetComponent<RectTransform>().sizeDelta.y / 2;

        _background.gameObject.SetActive(false);
        _handler.gameObject.SetActive(false);
    }

    private void Update()
    {
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (isActive == false)
        {
            isActive = true;
            _background.gameObject.SetActive(true);
            _handler.gameObject.SetActive(true);
        }
            
        _background.transform.position = eventData.position;
        _handler.transform.position = eventData.position;
        _touchPoistion = eventData.position;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if(isActive == true)
        {
            isActive = false;
            _background.gameObject.SetActive(false);
            _handler.gameObject.SetActive(false);
        }
            
        _handler.transform.position = _touchPoistion;
        _moveDir = Vector2.zero;

        Managers.Game.MoveDir = _moveDir;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 touchDir = eventData.position - _touchPoistion;

        float moveDist = Mathf.Min(touchDir.magnitude, _joystickRadius);
        _moveDir = touchDir.normalized;
        Vector2 newPosition = _touchPoistion + _moveDir * moveDist;
        _handler.transform.position = newPosition;

        Managers.Game.MoveDir = _moveDir;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        
    }
}
