using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static WeaponAttr;

public class Smoke : MonoBehaviour
{

    private SpriteRenderer sr;

    [SerializeField] private float waitTime = 1f;
    [SerializeField] private float fadeDuration = 2f; // 淡出持续时间（秒）
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        StartCoroutine(FadeOutAndDestroy());
    }

    // Update is called once per frame
    void Update()
    {
    }

    IEnumerator DestroyAfterTime(float time, GameObject obj)
    {
        yield return new WaitForSeconds(time);
        if (obj != null)
        {
            Destroy(obj);
        }
    }
    private IEnumerator FadeOutAndDestroy()
    {
        yield return new WaitForSeconds(waitTime);
        float elapsedTime = 0f;
        Color originalColor = sr.color;
        Color targetColor = new Color(originalColor.r, originalColor.g, originalColor.b, 0f);

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            sr.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null;
        }

        sr.color = targetColor;
        Destroy(gameObject);
    }
}
