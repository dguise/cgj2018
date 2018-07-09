using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestText : MonoBehaviour
{

    float m_time;
    float m_time_2;
    APopupText conversation;
    int iteration = 0;
    bool m_test_dmg;

    void Start()
    {
        m_time = Time.deltaTime;
        m_time_2 = Time.deltaTime;
        m_test_dmg = true;
    }


    void FixedUpdate()
    {

        m_time += Time.deltaTime;
        m_time_2 += Time.deltaTime;
        if (m_time > 0.1)
        {
            m_time = 0;
            if (m_test_dmg == true)
            {
                TextManager.CreateDamageText(Random.Range(-10, 10).ToString(), this.transform, 0.5f);
            }
            else
            {
                TextManager.CreateHealText(Random.Range(-10, 10).ToString(), this.transform, 0.5f);
            }

        }

        if (m_time_2 >= 1)
        {
            m_time_2 = 0;
            iteration++;
            if (iteration == 3)
            {
                conversation = TextManager.CreateConversationText("TESTING", this.transform);
            }
            if (iteration == 6)
            {
                conversation.SetText("This");
				m_test_dmg = false;

            }
            if (iteration == 9)
            {
				conversation.SetColor(TextManager.DAMAGE_COLOR);
                conversation.SetText("asdasdasdasd asd asdasdasdasdas asddasdasdasdasdasdasd sdas asd");
            }

            if (iteration == 14)
            {
				m_test_dmg = true;
                conversation.DestroyText();
                TextManager.CreateYellText("ARGH", this.transform);
            }

            if (iteration == 18)
            {
                iteration = 0;
            }
        }
    }
}
