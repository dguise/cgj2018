using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public static class GamePadHelper
{
    public static void Rumble(MonoBehaviour any, PlayerIndex index, float seconds, float leftIntensity = 0, float rightIntensity = 0)
    {
        any.StartCoroutine(Rumble(index, seconds, leftIntensity, rightIntensity));
    }
    public static IEnumerator Rumble(PlayerIndex index, float seconds, float leftIntesity, float rightIntensity)
    {
        GamePad.SetVibration(index, leftIntesity, rightIntensity);
        yield return new WaitForSeconds(seconds);
        GamePad.SetVibration(index, 0, 0);
    }

}
