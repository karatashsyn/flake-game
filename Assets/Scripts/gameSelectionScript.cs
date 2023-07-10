using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Security.Cryptography.X509Certificates;

public class gameSelectionScript : MonoBehaviour
{
    public int level;
    public Text levelText;
    public Animator transition;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(transition);
        levelText.text = level.ToString();   
    }

    public void OpenScene() {
        StartCoroutine(load("Level_"+level.ToString()));

    }

    IEnumerator load(String levelName) {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(levelName);
    }
}
