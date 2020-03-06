using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.EventSystems;

[RequireComponent(typeof(PlayerController))]
public class Test : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    [SerializeField] private RectTransform rect_Background;
    [SerializeField] private RectTransform rect_Joystick;
   

    private float radius;
    private bool istouch = false;
    private Vector3 movePosition;
    
    [SerializeField] private GameObject go_Player;
    [SerializeField] private float moveSpeed;

    void Start()
    {
        radius = rect_Background.rect.width * 0.5f;
       
    }

    void Update()
    {
        if (istouch)
        { 
            go_Player.transform.position += movePosition;
        }
       
    }

    public void OnDrag(PointerEventData eventData)
    {

        Vector2 value = eventData.position - (Vector2)rect_Background.position;

        value = Vector2.ClampMagnitude(value, radius);
        rect_Joystick.localPosition = value;

        float distance = Vector2.Distance(rect_Background.position, rect_Joystick.position) / radius;
        value = value.normalized;
        movePosition = new Vector3(value.x * moveSpeed * distance * Time.deltaTime, 0f, value.y * moveSpeed * distance * Time.deltaTime);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        istouch = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        istouch = false;
        rect_Joystick.localPosition = Vector3.zero;
        movePosition = Vector3.zero;
    }
}