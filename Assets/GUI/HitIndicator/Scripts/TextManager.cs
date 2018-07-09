using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextManager : MonoBehaviour
{

    public static Color32 DAMAGE_COLOR = new Color32(240, 0, 0, 255);
    public static Color32 HEAL_COLOR = new Color32(0, 240, 0, 255);
    public static Color32 CONV_COLOR = new Color32(0, 0, 0, 255);
    public static Color32 YELL_COLOR = new Color32(255, 255, 255, 255);


    private static GameObject m_screen_canvas;
    private static GameObject m_world_canvas;

    private static APopupText m_damage_text;
    private static APopupText m_conversation_text;
    private static APopupText m_yell_text;


    /// <summary> 
    /// To create a text, call TextManager.Create<typeoftext>Text(string Text, Transform location, float? range).
    /// location is the position of the created text, range is a random range x interal which the text will be created around.
    /// To change text, call TextManager.SetText(string text);
    ///</summary>
    static TextManager()
    {

        GameObject screen_canvas = Resources.Load<GameObject>("Prefabs/Text/ScreenCanvas");
        if (screen_canvas == null)
        {
            Debug.LogError("Could not load canvas resources");
        }
        m_screen_canvas = Instantiate(screen_canvas);

        m_world_canvas = Resources.Load<GameObject>("Prefabs/Text/WorldCanvas");
        if (m_world_canvas == null)
        {
            Debug.LogError("Could not load canvas resources");
        }

        m_damage_text = Resources.Load<APopupText>("Prefabs/Text/DamageTextParent");
        if (m_damage_text == null)
        {
            Debug.LogError("Could not load damage text prefab");
        }

        m_conversation_text = Resources.Load<APopupText>("Prefabs/Text/ConversationTextParent");
        if (m_conversation_text == null)
        {
            Debug.LogError("Could not load conversation text prefab");
        }

        m_yell_text = Resources.Load<APopupText>("Prefabs/Text/YellTextParent"); 
        if (m_yell_text == null)
        {
            Debug.LogError("Could not load yell text prefab");
        }
    }

    public static void CreateDamageText(string text, Transform location, float? range = null, Color32? color = null)
    {
        float random_range = range != null ? Random.Range(-(float)range, (float)range) : 0;
        Color32 text_color = color ?? DAMAGE_COLOR;
        CreatePopupText(text, location, m_damage_text, text_color, pad_x: random_range);
    }

    public static void CreateHealText(string text, Transform location, float? range = null, Color32? color = null)
    {
        CreateDamageText(text, location, range, color = HEAL_COLOR);
    }


    public static APopupText CreateConversationText(string text, Transform location, Color32? color = null)
    {
        Color32 text_color = color ?? CONV_COLOR;
        return CreatePopupText(text, location, m_conversation_text, text_color, attach_to_object: true);
    }


    public static APopupText CreateYellText(string text, Transform location, Color32? color = null) // TODO: should be fairly easy to implement. Same way as createDamageText but with a longer animation, and with attach_to_object=true
    {
        Color32 text_color = color ?? YELL_COLOR;
        return CreatePopupText(text, location, m_yell_text, text_color, attach_to_object: true);
    }

    private static APopupText CreatePopupText(string text, Transform transform, APopupText popuptext, Color32 color, bool attach_to_object = false, float pad_x = 0, float pad_y = 0)
    {
        APopupText instance = Instantiate(popuptext);

        if (attach_to_object)
        {
            GameObject canvas = Instantiate(m_world_canvas);
            canvas.transform.SetParent(transform, false);
            instance.transform.SetParent(canvas.transform, false);
        }
        else
        {
            Vector2 screen;
            screen = Camera.main.WorldToScreenPoint(new Vector2(transform.position.x + pad_x, transform.position.y + pad_y));
            instance.transform.SetParent(m_screen_canvas.transform, false);
            instance.transform.position = screen;
        }

        instance.SetText(text);
        instance.SetColor(color);
        return instance;
    }
}
