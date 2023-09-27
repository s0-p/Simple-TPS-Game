using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class VirtualJoyStick : InputCtrlBase, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField]
    RectTransform _leverRectTrsf;
    RectTransform _rectTrsf;

    [SerializeField, Range(30, 100)]
    float _leverRange = 50f;
    Vector2 _firstTouch;
    void Awake()
    {
        _rectTrsf = GetComponent<RectTransform>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        _firstTouch = eventData.position;

        var leverDir = _firstTouch - _rectTrsf.anchoredPosition;
        var clampeDir = leverDir.magnitude < _leverRange ? leverDir : leverDir.normalized * _leverRange;

        _leverRectTrsf.anchoredPosition = clampeDir;
        InputDir = _leverRectTrsf.anchoredPosition / _leverRange;
    }

    public void OnDrag(PointerEventData eventData)
    {
        var leverDir = eventData.position - _firstTouch;
        var clampeDir = leverDir.magnitude < _leverRange ? leverDir : leverDir.normalized * _leverRange;

        _leverRectTrsf.anchoredPosition = clampeDir;
        InputDir = _leverRectTrsf.anchoredPosition / _leverRange;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _leverRectTrsf.anchoredPosition = Vector2.zero;
        InputDir = _leverRectTrsf.anchoredPosition / _leverRange;
    }
}
