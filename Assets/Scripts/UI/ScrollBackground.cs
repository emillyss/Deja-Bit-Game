using System;
using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{

    Material _mat;
    [SerializeField] float speed;
    Vector2 _offset;
    float _offsetX;
    void Awake()
    {
        _mat = GetComponentInChildren<SpriteRenderer>().material;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ScrollBackground();
    }

    void ScrollBackground()
    {
        _offsetX += Time.deltaTime * speed;
        _offset.x = _offsetX;
        _mat.mainTextureOffset = _offset;
    }
}