using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RabbitWarning : MonoBehaviour
{
    //번쩍번쩍스크립트
    public Slider slider; // 슬라이더를 드래그 앤 드롭으로 연결
    private Outline outlineComponent;
    private float duration = 1.0f; // 1초 간격
    private Coroutine outlineCoroutine;

    void Start()
    {
        // Fill Area 하위의 Fill을 찾습니다.
        Transform fillArea = slider.transform.Find("Fill Area");
        Transform fill = fillArea.Find("Fill");

        if (fill != null)
        {
            // Outline 컴포넌트를 가져옵니다. 없으면 추가합니다.
            outlineComponent = fill.GetComponent<Outline>();
            if (outlineComponent == null)
            {
                outlineComponent = fill.gameObject.AddComponent<Outline>();
            }

            // 초기 색상 설정 (알파값은 0)
            outlineComponent.effectColor = new Color(1, 0, 0, 0);
        }
    }

    public void StartOutlineEffect()
    {
        if (outlineCoroutine == null)
        {
            outlineCoroutine = StartCoroutine(AnimateOutlineTransparency());
        }
    }

    public void StopOutlineEffect()
    {
        if (outlineCoroutine != null)
        {
            StopCoroutine(outlineCoroutine);
            outlineCoroutine = null;
            // 효과를 중지할 때 알파값을 0으로 설정
            outlineComponent.effectColor = new Color(1, 0, 0, 0);
        }
    }

    private IEnumerator AnimateOutlineTransparency()
    {
        while (true)
        {
            // 0에서 1까지의 값을 반복
            for (float t = 0; t <= 1; t += Time.deltaTime / duration)
            {
                float alpha = Mathf.Lerp(0, 1, t);
                outlineComponent.effectColor = new Color(1, 0, 0, alpha);
                yield return null;
            }

            // 1에서 0까지의 값을 반복
            for (float t = 0; t <= 1; t += Time.deltaTime / duration)
            {
                float alpha = Mathf.Lerp(1, 0, t);
                outlineComponent.effectColor = new Color(1, 0, 0, alpha);
                yield return null;
            }
        }
    }
}