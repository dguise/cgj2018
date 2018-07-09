using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageText : APopupText {

	protected override void ExtendedStart()
	{
		AnimatorClipInfo[] clip = m_popup_animator_text.GetCurrentAnimatorClipInfo(0);
		Destroy(this.gameObject, clip[0].clip.length);
	}

	public override void SetColor(Color32 color){
		// base.SetColor(new Color32((byte)(color.r/2), (byte)(color.g/2), (byte)(color.b/2), color.r)); // Uncomment if one wants to change base text too
		m_popup_animator_text.GetComponent<Outline>().effectColor = color;
	}

}
