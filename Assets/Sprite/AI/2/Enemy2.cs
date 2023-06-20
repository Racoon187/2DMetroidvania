using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Enemy2 : MonoBehaviour
{
    [Header("组件")]
    public Animator _animator;
    public Rigidbody2D _rb2d;
    public AnimatorStateInfo _aniInfo;

    [Header("Layer")]
    public LayerMask Attack;

    [Header("血量")]
    public float HP;
    public float MAXHP;
    public float timer = 0f;

    [Header("判断")]
    public bool isHit;
    public bool isDead;
    // Start is called before the first frame update
    void Start()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        HP = MAXHP;
    }

    // Update is called once per frame
    void Update()
    {
        _aniInfo = _animator.GetCurrentAnimatorStateInfo(0);
        if (_rb2d.IsTouchingLayers(Attack))
        {
            _animator.SetBool("getHit", true);
            HP -= 1;
        }
        else if (_aniInfo.normalizedTime >= .9f) //动画播放90%
        {
            _animator.SetBool("getHit", false);
        }

        if (HP <= 0)
        {
            _animator.SetTrigger("Death");
            timer += Time.deltaTime;

            if (timer >= 0.5f)
            {
                Destroy(gameObject);
            }
        }
    }
}
