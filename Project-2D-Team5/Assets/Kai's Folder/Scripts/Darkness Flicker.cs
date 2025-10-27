using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LanternFlickerPanel : MonoBehaviour
{
    public Image panelImage;

    private float baseAlpha = 230f / 255f;
    private float minAlpha = 220f / 255f;
    private float maxAlpha = 240f / 255f;

    private Vector2 flickerSpeedRange = new Vector2(1f, 2f);
    private Vector2 flickerIntervalRange = new Vector2(0.5f, 1f);

    private void Start()
    {
        if (panelImage == null)
            panelImage = GetComponent<Image>();

        Color c = panelImage.color;
        c.a = baseAlpha;
        panelImage.color = c;

        StartCoroutine(FlickerEffect());
    }

    IEnumerator FlickerEffect()
    {
        while (true)
        {
            float targetAlpha = Random.Range(minAlpha, maxAlpha);
            float currentAlpha = panelImage.color.a;
            float flickerSpeed = Random.Range(flickerSpeedRange.x, flickerSpeedRange.y);
            float flickerInterval = Random.Range(flickerIntervalRange.x, flickerIntervalRange.y);

            float t = 0f;
            while (t < 1f)
            {
                t += Time.unscaledDeltaTime / flickerSpeed;
                float newAlpha = Mathf.Lerp(currentAlpha, targetAlpha, t);

                Color c = panelImage.color;
                c.a = newAlpha;
                panelImage.color = c;

                yield return null;
            }

            yield return new WaitForSecondsRealtime(flickerInterval);
        }
    }
}