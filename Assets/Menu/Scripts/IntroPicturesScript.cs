using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroPicturesScript : MonoBehaviour {

    public List<Sprite> listOfIntroSprites = new List<Sprite>();
    private int currentIntroFrame = 0;
    private SpriteRenderer myRenderer;
    private float timestamp;
    private float cooldown = 0.5f;

	void Start () {
        myRenderer = this.GetComponent<SpriteRenderer>();
        myRenderer.sprite = listOfIntroSprites[0];
        timestamp = -cooldown;
        SoundManager.instance.PlayMusic(0);
	}
	
	void Update () {
        

        if (Input.GetButton(Inputs.AButton()) || Input.GetKeyUp(KeyCode.Space))
        {
            if (timestamp + cooldown < Time.time)
            {
                timestamp = Time.time;
                currentIntroFrame += 1;
                if (currentIntroFrame == listOfIntroSprites.Count)
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                else
                    myRenderer.sprite = listOfIntroSprites[currentIntroFrame];
            }
        }
	}
}
