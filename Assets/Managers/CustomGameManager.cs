using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class CustomGameManager : MonoBehaviour{
    float timestamp = 0;
    float cooldown = 60f;


    public void FixedUpdate() {

        if (PlayerManager.gameStarted) {
            if (timestamp + cooldown < Time.time) {
                timestamp = Time.time;
                cooldown = Random.Range(80, 140);
                PlayerManager.Capricious();
            }
        }
    }

    public void GameOver() {
        Invoke("Restart", 1f);
        foreach (GameObject player in PlayerManager.PlayerObjects) {
            if (player != null) {
                Destroy(player);
            }
        }
        PlayerManager.Reset();
    }

    public void Restart() {
        SceneManager.LoadScene(0);
        Destroy(this.gameObject);
    }

    public void Victory() {
        Invoke("Win", 2f);
    }
    

    public void Win() {
        var time = Time.time - PlayerManager.time; 
        var record = PlayerPrefs.GetFloat("score");

        if (time <= record) {
            GuiScript.instance.Talk(new Message(aText: "YOU HAVE WON!\nTIME: " + time + ".\nRECORD: "));
        } else {
            GuiScript.instance.Talk(new Message(aText: "YOU HAVE WON AND BEATEN THE RECORD!\nTIME: " + time + ".\nLAST RECORD: " + record));
            PlayerPrefs.SetFloat("score", time);
        }

        Invoke("Restart", 30f);
    }
}