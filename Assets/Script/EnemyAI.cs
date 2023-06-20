using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [Header ("组件")]
    public Animator _animator;
    public Rigidbody2D _rb2d;
    public AnimatorStateInfo _aniInfo; //读取animator动画进程(帧)


    [Header ("Layer")]
    public LayerMask Attack;


    [Header ("Hp")]
    public float HP;  //当前血量
    public float MAXHP;  //最大血量
    
    
    [Header ("AI基本属性")]
    public Transform target;  //怪物要跟随的目标(玩家)
    public float checkOnRadius; //玩家检测半径
    public float moveSpeed;  //怪物基本移速
                             // Start is called before the first frame update


    /// <summary>

    private float originSpeed = 0;

    /// </summary>
    void Start()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        HP = MAXHP; //刷新时保持最大血量
        target = GameObject.FindGameObjectsWithTag("Player")[0].transform;
        originSpeed = moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerCheck();
    }

    void FixedUpdate()
    {
        EnemyHit();
    }
    

    //怪物巡逻系统
    void PlayerCheck()  //巡逻
    {
        if (Mathf.Abs(transform.position.x - target.position.x) < checkOnRadius)  //绝对值
        {
            transform.position = Vector3.MoveTowards(transform.position,target.position,moveSpeed * Time.deltaTime);
            if (transform.position.x - target.position.x < 0) transform.eulerAngles = new Vector3(0,180,0);
            if (transform.position.x - target.position.x > 0) transform.eulerAngles = new Vector3(0,0,0);
        }
        _rb2d.velocity = new Vector2(0, 0);
    }

    void testA()
    {
       moveSpeed = originSpeed;
       // _rb2d.velocity = new Vector2(0,0);
    }

    void EnemyHit()  //受击
    {
       
        _aniInfo = _animator.GetCurrentAnimatorStateInfo(0);
        if(_rb2d.IsTouchingLayers(Attack))
        {
            _animator.SetBool("getHit",true);
            HP -= 1;
            moveSpeed = -originSpeed;
            //_rb2d.velocity = transform.right * 3;
            Invoke("testA", 0.5f);
            _rb2d.velocity = new Vector2(0,0);
            //  moveSpeed = 1;
        }
        else if(_aniInfo.normalizedTime >= .9f) //动画播放90%
        {
            _animator.SetBool("getHit",false);
           // moveSpeed = 3;
        }
        else if(HP <= 0)
        {
            Destroy(gameObject);
        }
    }
}
