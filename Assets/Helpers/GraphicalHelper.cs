using UnityEngine;
using System.Collections;

public static class GraphicalHelper
{
    public static IEnumerator FadeOut(this SpriteRenderer trailPartRenderer, bool thenDelete = true)
    {
        Color startColor = trailPartRenderer.color;
        Color toColor = startColor;
        toColor.a = 0;
        float timestamp = 0;
        while (trailPartRenderer.color != toColor)
        {
            timestamp += 0.01f;
            trailPartRenderer.color = Color.Lerp(startColor, toColor, timestamp);
            yield return new WaitForEndOfFrame();
        }
        if (thenDelete)
            MonoBehaviour.Destroy(trailPartRenderer.gameObject);
    }

}
