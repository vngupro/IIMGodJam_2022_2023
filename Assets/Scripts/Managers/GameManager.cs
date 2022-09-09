using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Camera _camera;
    public GameObject player;
    public Vector2 offset = Vector2.zero;
    private void Awake()
    {
        _camera = Camera.main;
    }
    // Update is called once per frame
    void Update()
    {
        _camera.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + offset.y, _camera.transform.position.z);   
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }
}
