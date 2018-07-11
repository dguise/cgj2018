using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer))]
public class Fader : MonoBehaviour
{
    void Start()
    {
        var sprite = GetComponent<SpriteRenderer>();
        StartCoroutine(sprite.FadeOut());
    }
}
