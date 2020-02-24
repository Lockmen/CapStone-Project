using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.UI;

public class ScaleController : MonoBehaviour
{

    ARSessionOrigin m_ARSessionOrigin;

    public Slider scaleSlider;

    private void Awake()
    {
        m_ARSessionOrigin = GetComponent<ARSessionOrigin>();
    }
    // Start is called before the first frame update
    void Start()
    {
        scaleSlider.onValueChanged.AddListener(OnSliderValueChanged); // addlistener는 함수를 불러오는 메서드 (보통 버튼에서 쓰는듯)
    }

    public void OnSliderValueChanged(float value)
    {
        if (scaleSlider !=null)
        {
            m_ARSessionOrigin.transform.localScale = Vector3.one / value; // 부모 트랜스폼과 상대적인 트랜스폼의 스케일을 나타낸다. 
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
