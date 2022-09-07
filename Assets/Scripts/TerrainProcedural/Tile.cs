using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

//[ExecuteInEditMode]
public class Tile : MonoBehaviour
{
    [SerializeField] private Color baseColor = Color.white;
    [SerializeField] private Color offsetColor = Color.black;
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private Collider2D _collider;
    [SerializeField] public GameObject highlight;
    public bool isSpawnPoint = false;
    public bool isDecor = false;

    public void Init(bool isOffset)
    {
        _renderer.color = isOffset ? offsetColor : baseColor;
    }

    public void OnMouseEnter()
    {
        //Debug.Log("On Mouse Enter");
        highlight.SetActive(true);
    }

    public void OnMouseExit()
    {
        //Debug.Log("On Mouse Exit");
        highlight.SetActive(false);
    }

    public int GetWidth()
    {
        return (int)_renderer.bounds.size.x;
    }

    public int GetHeight()
    {
        return (int)_renderer.bounds.size.y;
    }
}