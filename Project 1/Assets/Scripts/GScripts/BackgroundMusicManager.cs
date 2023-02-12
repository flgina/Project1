using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackgroundMusicManager : MonoBehaviour
{
    private AudioSource BGM;

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        BGM = gameObject.GetComponent<AudioSource>();

        SceneManager.activeSceneChanged += UpdateBGM;
    }

    public void UpdateBGM(Scene current, Scene next)
    {
        //int currentScene = SceneManager.GetActiveScene().buildIndex;

        if (next.buildIndex == 0 || next.buildIndex == 1)
        {
            BGM.Play();
        }
        else
        {
            BGM.Stop();
        }
    }
}
