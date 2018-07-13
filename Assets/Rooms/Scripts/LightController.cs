using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour {
	private List<Light> lights = new List<Light>();

	void Start () {
		transform.GetComponentsInChildren<Light>(lights);
	}

	public void SetActive(bool active) {
		foreach (Light l in lights) {
			l.gameObject.SetActive(active);
		}
	}

	public void SetRange(float range) {
		foreach (Light l in lights) {
			l.range = range;
		}
	}

	public void SetColor(Color color) {
		foreach (Light l in lights) {
			l.color = color;
		}
	}
}
