using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Camera _camera;
    public GameObject player;
    public GameObject pauseMenu;
    public Vector2 offset = Vector2.zero;
    private void Awake()
    {
        _camera = Camera.main;
    }
    // Update is called once per frame
    void Update()
    {
        _camera.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + offset.y, _camera.transform.position.z);   
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }
    
    public void Retry()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }

    public void Quit()
    {
        Application.Quit();
    }
}
