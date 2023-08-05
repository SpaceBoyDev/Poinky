using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine.Serialization;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;
using Image = UnityEngine.UI.Image;

public class ButtonAnimations : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler,
    IPointerUpHandler
{
    [SerializeField] private GameObject parentMenu;
    [SerializeField] private bool isCanvasWorld;
    [SerializeField] private bool hasShadow;

    [SerializeField] private Vector3 shadowPosition;
    [SerializeField] private Vector3 shadowScale;

    [Header("Set alternative start values")]
    [SerializeField] private Vector3 setStartRotation;

    [Header("Start values")]
    private Vector3 startPosition;
    private Vector3 startScale;
    private Vector3 startRotation;
    
    [Header("Duration")]
    public float duration;
    [SerializeField] private float positionDuration;
    [SerializeField] private float scaleDuration;
    [SerializeField] private float rotationDuration;

    [Header("End values (added to start values)")]
    [SerializeField] private Vector3 newPosition;
    [SerializeField] private Vector3 newScale; 
    [SerializeField] private Vector3 newRotation;
    private void Start()
    {
        transform.eulerAngles = setStartRotation;
        
        startPosition = transform.position;
        startScale = transform.localScale;
        startRotation = transform.eulerAngles;
        
        transform.DORotate(startRotation + newRotation, rotationDuration, RotateMode.Fast).SetLoops(-1, LoopType.Yoyo)
            .SetRelative(false).SetEase(Ease.Linear);

        if (hasShadow) //not used for now
        {
            GameObject buttonShadow = new GameObject(gameObject.name + " Shadow");
            
            buttonShadow.AddComponent<Image>();
            buttonShadow.GetComponent<Image>().sprite = GetComponent<Image>().sprite;
            buttonShadow.GetComponent<Image>().color = Color.black;
            buttonShadow.GetComponent<RectTransform>().sizeDelta = gameObject.GetComponent<RectTransform>().sizeDelta;
            buttonShadow.transform.position = transform.position - shadowPosition;
            buttonShadow.transform.SetParent(parentMenu.transform);
            buttonShadow.transform.localScale = transform.localScale;
            
            transform.SetParent(buttonShadow.transform);
            
            
            buttonShadow.transform.eulerAngles = setStartRotation;
            buttonShadow.transform.DORotate(startRotation + newRotation, rotationDuration, RotateMode.Fast).SetLoops(-1, LoopType.Yoyo)
                .SetRelative(false).SetEase(Ease.Linear);
        }
    }
    
    
    private void Update()
    {
        if (hasShadow)
        {
            UpdateShadow();
        }
    }

    private void UpdateShadow()
    {
        
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
            transform.DOScale(startScale + newScale, scaleDuration).SetUpdate(true);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        transform.DOScale(startScale, scaleDuration).SetUpdate(true).SetEase(Ease.InQuad);
    }
}
