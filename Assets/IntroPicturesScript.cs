using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroPicturesScript : MonoBehaviour {

    public List<Sprite> listOfIntroSprites = new List<Sprite>();
    private int currentIntroFrame = 0;
    private SpriteRenderer myRenderer;

	void Start () {
        myRenderer = this.GetComponent<SpriteRenderer>();
        myRenderer.sprite = listOfIntroSprites[0];
	}
	
	void Update () {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            currentIntroFrame += 1;
            if (currentIntroFrame == listOfIntroSprites.Count)
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            else
                myRenderer.sprite = listOfIntroSprites[currentIntroFrame];
        }
	}
}
