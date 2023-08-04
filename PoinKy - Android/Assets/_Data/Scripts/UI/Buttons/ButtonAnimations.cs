using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ButtonAnimations : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler,
    IPointerUpHandler
{
   // private RectTransform rectTransform;
    [SerializeField] private Vector3 setStartRotation;

    
    [Header("Start thingies")]
    private Vector3 startPosition;
    private Vector3 startScale;
    private Vector3 startRotation;
    
    [Header("Duration")]
    public float duration;
    [SerializeField] private float rotationDuration;
    
    [SerializeField] private bool isCanvasWorld = true;
    [SerializeField] private Vector3 newRotation;
    [SerializeField] private Vector3 newPosition;
    [SerializeField] private Vector3 newScale; 
    private void Start()
    {
        transform.eulerAngles = setStartRotation;
        
        startPosition = transform.position;
        startScale = transform.localScale;
        startRotation = transform.eulerAngles;
        
        transform.DORotate(startRotation + newRotation, rotationDuration, RotateMode.Fast).SetLoops(-1, LoopType.Yoyo)
            .SetRelative(false).SetEase(Ease.Linear);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //if (!isCanvasWorld)
        //{
        //    transform.DOMove(startPosition + newPosition, 0.3f, false).SetUpdate(true);
        //}
        //else
        //{
        //    transform.DOLocalMove(startPosition + newPosition, 0.3f, false).SetUpdate(true).SetRelative(true);
        //}
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        print("exit button");
        transform.DOMove(startPosition, 0.3f, false).SetUpdate(true);
        //rectTransform.DOLocalMove(startPosition, 0.3f, false).SetUpdate(true).SetRelative(true);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (GetComponent<Button>().interactable == true)
        {
            transform.DOScale(startScale + newScale, 0.2f).SetUpdate(true);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        transform.DOScale(startScale, 0.2f).SetUpdate(true).SetEase(Ease.InQuad);
    }
}
