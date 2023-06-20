using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magic : MonoBehaviour
{
    [SerializeField]private Rigidbody2D _rb2d;
    [SerializeField]public float magicSpeed;
    [SerializeField]public float lifetime;
    public LayerMask Trap;
    public LayerMask Ground;
    void Start()
    {
        _rb2d.velocity = transform.right * magicSpeed;
        //_rb2d.velocity = new Vector2(0,0);
        Invoke("DestoryMagic", lifetime);
    }

    // Update is called once per frame
    void Update()
    {
        if(_rb2d.IsTouchingLayers(Ground))
        {
            Destroy(gameObject);
        }
        else if(_rb2d.IsTouchingLayers(Trap))
        {
            Destroy(gameObject);
        }
    }

    void DestoryMagic()
    {
        Destroy(gameObject);
    }
}
