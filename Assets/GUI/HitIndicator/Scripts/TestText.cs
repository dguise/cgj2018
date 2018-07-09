using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestText : MonoBehaviour
{

    float m_time;
    int iteration = 0;
    bool m_test_dmg;

    void Start()
    {
        m_time = Time.deltaTime;
        m_test_dmg = true;
    }


    void FixedUpdate()
    {
        if (m_time % 10 == 0) {
            m_test_dmg = !m_test_dmg;
        }

        m_time += Time.deltaTime;
        if (m_time > 0.1)
        {
            m_time = 0;
            if (m_test_dmg == true)
            {
                TextManager.CreateDamageText(Random.Range(-10, 10).ToString(), this.transform, 0.3f);
            }
            else
            {
                TextManager.CreateHealText(Random.Range(-10, 10).ToString(), this.transform, 0.3f);
            }

        }
    }
}
