using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextManager : MonoBehaviour
{

    public static Color32 DAMAGE_COLOR = new Color32(240, 0, 0, 255);
    public static Color32 HEAL_COLOR = new Color32(0, 240, 0, 255);


    private static GameObject m_screen_canvas;
    private static APopupText m_damage_text;


    /// <summary> 
    /// To create a text, call TextManager.Create<typeoftext>Text(string Text, Transform location, float? range).
    /// location is the position of the created text, range is a random range x interal which the text will be created around.
    /// To change text, call TextManager.SetText(string text);
    ///</summary>
    static TextManager()
    {
        GameObject screen_canvas = PrefabRepository.instance.TextManagerScreenCanvas;
        if (screen_canvas == null)
        {
            Debug.LogError("Could not load canvas resources");
        }
        m_screen_canvas = Instantiate(screen_canvas);

        m_damage_text = PrefabRepository.instance.DamageTextParent;
        if (m_damage_text == null)
        {
            Debug.LogError("Could not load damage text prefab");
        }
    }


    public static void CreateDamageText(string text, Transform location, float? range = null, Color32? color = null)
    {
        float random_range = range != null ? Random.Range(-(float)range, (float)range) : 0;
        Color32 text_color = color ?? DAMAGE_COLOR;
        CreatePopupText(text, location, m_damage_text, text_color, pad_x: random_range, pad_y: 0.5f);
    }

    public static void CreateHealText(string text, Transform location, float? range = null, Color32? color = null)
    {
        CreateDamageText(text, location, range, color = HEAL_COLOR);
    }

    private static APopupText CreatePopupText(string text, Transform transform, APopupText popuptext, Color32 color, bool attach_to_object = false, float pad_x = 0, float pad_y = 0)
    {
        APopupText instance = Instantiate(popuptext);

        Vector2 screen;
        screen = Camera.main.WorldToScreenPoint(new Vector2(transform.position.x + pad_x, transform.position.y + pad_y));

        if (!m_screen_canvas) {
            GameObject screen_canvas = PrefabRepository.instance.TextManagerScreenCanvas;
            m_screen_canvas = Instantiate(screen_canvas);
        }

        instance.transform.SetParent(m_screen_canvas.transform, false);
        instance.transform.position = screen;

        instance.SetText(text);
        instance.SetColor(color);
        return instance;
    }
}
