using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class CustomGameManager : MonoBehaviour{

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


}