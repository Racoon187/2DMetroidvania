using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header ("组件")]
    private Rigidbody2D _rb2d;
    private Animator _animator;
    public PolygonCollider2D _polygon2d;
    public AnimatorStateInfo _aniInfo;
    private SpriteRenderer _sr;


    [Header ("Layer")]
    public LayerMask Ground; //地面图层


    [Header ("Move")]
    public float speed = 10; //行走速度


    [Header ("Jump")]
    public bool isGrounded; //地面检测
    private bool isJumping; // 跳跃检测
    public float jumpHeight = 20;  //跳跃高度
    public Transform foot; //脚底实体(地面检测时检测foot而不是Collider)
    

    [Header ("Dash")]
    public float dashSpeed = 15;  //dash速度
    public float dashTime;  //dash时间
    private float dashTimer;  //dash计时器
    private bool isDashing;  //dash检测
    

    [Header ("Attack")]
    private bool isAttacking = false;  //攻击状态判定
    private int attackPhase = 0;  //攻击阶段
    private float Timer = 0.0f;  //计时器
    public float Timer_end = 0.5f;  //攻击延迟
    public float attackTime;
    
    private float x,y;
    
    //获取组件
    void Start()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _polygon2d = GetComponent<PolygonCollider2D>();
        _sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Jump();
        Dash();
        Attack();

        /*if (Input.GetKey(KeyCode.D))
        {
            _sr.flipX = false;
            for (int i = 0; i < backGrounds.Length; i++)
            {
                backGrounds[i].MoveBackGround(1);
            }
            transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
        }
        if (Input.GetKey(KeyCode.A))
        {
            _sr.flipX = true;
            for (int i = 0; i < backGrounds.Length; i++)
            {
                backGrounds[i].MoveBackGround(-1);
            }
            transform.position -= new Vector3(speed * Time.deltaTime, 0, 0);
        }
        */
        
    }


    // Update is called once per 0.02s
    private void FixedUpdate()
    {
        Walk();
        isGrounded = Physics2D.OverlapCircle(foot.position,0.1f,Ground); //地面检测
    }


    //移动系统
    private void Walk()
    {
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(x,y,0);
        _rb2d.transform.position += movement * speed * Time.deltaTime;

        if( x > 0 )  //左移
        {
            _rb2d.transform.eulerAngles = new Vector3(0f,0f,0f);
            _animator.SetBool("Walk",true);
        }

        if ( x < 0 )  //右移
        {
            _rb2d.transform.eulerAngles = new Vector3(0f,180f,0f);  //模型反转
            _animator.SetBool("Walk",true);
        }

        if ( x < 0.001f && x > -0.001f )  //静止
        {
            _animator.SetBool("Walk",false);
        }
    }

    //跳跃系统
    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.K) && !isJumping)
        {
            isJumping = true;
            _rb2d.AddForce(Vector3.up * jumpHeight, ForceMode2D.Impulse);
            _animator.SetTrigger("Jumping");
        }
        else if (_rb2d.velocity.y < 0)  //下落检测
        {
            _animator.SetTrigger("Falling");
        }

        else if (isGrounded)
        {
            _animator.SetTrigger("idle");
            isJumping = false;
        }
        else if (isAttacking)
        {
            isJumping = false;
        }
    }

    //Dash系统
    private void Dash()
    {
        if (!isDashing)
        {
            if(Input.GetKeyDown(KeyCode.L))
            {
                //开始dashing
                isDashing = true;
                dashTimer = dashTime;
            }
        }
        else
        {
            dashTimer -= Time.deltaTime;
            if(dashTimer <= 0)
            {
                isDashing = false;
            }
            else
            {
                _rb2d.velocity = transform.right * dashSpeed;
                _animator.SetTrigger("Dash");
            }
        }
    }

    //攻击系统
    private void Attack()
    {
        if (Input.GetKeyDown(KeyCode.J) && !isAttacking)
        {
            isAttacking = true;
            attackPhase = 1;
            Timer = 0.0f;
            _animator.SetInteger("attackPhase",attackPhase);
            _rb2d.velocity = transform.right * 3;
        }

        else if(Input.GetKeyDown(KeyCode.J) && isAttacking)
        {
            Timer = 0.0f;  //重置计时器
            attackPhase++;
            if(attackPhase > 6)
            {
                attackPhase = 0;
            }
            else if(attackPhase <= 3)
            {
                _animator.SetInteger("attackPhase",attackPhase);
                _rb2d.velocity = transform.right * 6;
            }
        }

        if(isAttacking)  //Combo触发条件&返回待机状态
        {
            speed = 0;
            Timer += Time.deltaTime;
            if (Timer >= Timer_end)
            {
                isAttacking = false;
                attackPhase = 0;
                _animator.SetInteger("attackPhase",attackPhase);
            }
        }
        else
        {
            speed = 10;
        }
        
        //限制
        if(isJumping)
        {
            isAttacking = false;
        }
    }
}
