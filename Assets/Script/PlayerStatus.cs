using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStatus : MonoBehaviour
{
    [Header ("组件")]
    private Rigidbody2D _rb2d;
    private Animator _animator;
    public AnimatorStateInfo _aniInfo;

    [Header("图层")]
    int trapsLayer;
    public LayerMask Trap; //敌人图层 
    
    [Header ("基本属性")]
    static public int HP;
    static public int MAXHP = 100;
    static public int MP;
    static public int MAXMP = 10;

    [Header ("受击")]
    public float hitBackForce = 10f; //弹开的力量
    public float hitBackTime = 0.5f; //弹开的持续时间
    // Start is called before the first frame update
    void Start()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();

        //刷新时为最大
        HP = MAXHP;
        MP = MAXMP;
        trapsLayer = LayerMask.NameToLayer("Trap");
    }

    // Update is called once per frame
    void Update()
    {
        //GetHit(Vector2.left, true);


        _aniInfo = _animator.GetCurrentAnimatorStateInfo(0);

        if (_aniInfo.normalizedTime >= 1 && _aniInfo.IsName("Player_Die"))
        {
            gameObject.SetActive(false);

            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            GameManager.PlayerDied();
        }


    }

    
    /*
    private void GetHit(Vector2 direction, bool isFacingRight)
    {
        _aniInfo = _animator.GetCurrentAnimatorStateInfo(0);
        if(_rb2d.IsTouchingLayers(Trap))
        {
            _animator.SetTrigger("GetHit");
            HP -= 1;

            _rb2d.velocity = Vector2.zero; //当前速度重置为0

            float x = isFacingRight ? 1f : -1f;
            Vector2 hitBackDirection = new Vector2(x,1f).normalized;

            hitBackDirection = (hitBackDirection + direction.normalized).normalized;

            //根据传入方向,给角色冲击力
            _rb2d.AddForce(direction.normalized * hitBackForce, ForceMode2D.Impulse);

            //开始协程
            StartCoroutine(StopHitBack());
        }
    }
    */

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == trapsLayer)
        {
            _animator.SetTrigger("Die");
        }
    }

    private IEnumerator StopHitBack()
    {
        yield return new WaitForSeconds(hitBackTime);
        _rb2d.velocity = Vector2.zero;
    }
}
