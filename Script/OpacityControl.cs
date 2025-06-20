using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpacityControl : MonoBehaviour
{
    public Slider opacitySlider; // Inspector에서 슬라이더를 할당하기 위한 public 변수
    public Image targetImage; // Inspector에서 대상 이미지를 할당하기 위한 public 변수
    public Image targetImage2;
    public Image mainImage;
    
    private void Start()
    {
        // 슬라이더의 값을 초기 이미지 투명도로 설정
        opacitySlider.value = 1f;
        // 슬라이더의 "On Value Changed" 이벤트에 이 스크립트의 SetOpacity 메서드 연결
        opacitySlider.onValueChanged.AddListener(SetOpacity);
    }
    
    // 슬라이더를 조절할 때 호출되는 메서드
    private void SetOpacity(float opacity)
    {
        // 현재 이미지의 색상 정보를 가져와서 투명도 값만 변경
        Color currentColor = targetImage.color;
        currentColor.a = opacity;
        targetImage.color = currentColor;

        Color currentColor2 = targetImage2.color;
        currentColor2.a = opacity;
        targetImage2.color = currentColor2;

        mainImage.color = currentColor;
    }
}
