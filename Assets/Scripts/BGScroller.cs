using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGScroller : MonoBehaviour
{
    [SerializeField] protected float _bgScrollSpeed=0.5f;

    protected Material _myMat;

    protected Vector2 _offset;
    // Start is called before the first frame update
    void Start()
    {
        _myMat = GetComponent<Renderer>().material;
        _offset=new  Vector2(0f,_bgScrollSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        _myMat.mainTextureOffset += _offset*Time.deltaTime;
    }
}
