using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    public float Speed;
    public float Stamina = 3.0f;
    public bool Runable = true;
    public GameManager manager;

    Rigidbody2D rigid;
    Animator anim;
    float h;
    float v;
    bool isHorizonMove;
    Vector3 dirVec;
    GameObject scanObject;


    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }    

    void Update()
    {
        //플레이어가 상하좌우를 w,a,s,d 또는 방향키로 움직이게 한다.(이 때, 대각선움직임과 대화창 이벤트시에는 움직이지 못함)
        h = manager.isAction ? 0 : Input.GetAxisRaw("Horizontal");
        v = manager.isAction ? 0 : Input.GetAxisRaw("Vertical");

        bool hDown = manager.isAction ? false : Input.GetButtonDown("Horizontal");
        bool vDown = manager.isAction ? false : Input.GetButtonDown("Vertical");
        bool hUp = manager.isAction ? false : Input.GetButtonUp("Horizontal");
        bool vUp = manager.isAction ? false : Input.GetButtonUp("Vertical");

        if (hDown)
            isHorizonMove = true;
        else if(vDown)
            isHorizonMove = false;
        else if(hUp || vUp)
            isHorizonMove = h !=0;

        if(anim.GetInteger("hAxisRaw") != h){
            anim.SetBool("isChange", true);
            anim.SetInteger("hAxisRaw", (int)h);
        }
        else if(anim.GetInteger("vAxisRaw") != v){
            anim.SetBool("isChange", true);
            anim.SetInteger("vAxisRaw", (int)v);
        }
        else
            anim.SetBool("isChange", false);

        if(vDown && v == 1)
            dirVec = Vector3.up;
        else if(vDown && v == -1)
            dirVec = Vector3.down;
        else if(hDown && h == -1)
            dirVec = Vector3.left;
        else if(hDown && h == 1)
            dirVec = Vector3.right;

        if(Input.GetButtonDown("Jump") && scanObject != null)
            manager.Action(scanObject);
    }

    void FixedUpdate()
    {
        //움직임의 속도 
        Vector2 moveVec = isHorizonMove ? new Vector2(h,0) : new Vector2(0,v);
        
        float RunSpeed = Speed;

        if (Input.GetButton("Debug Multiplier") && Runable)
        {
            RunSpeed = Speed * 1.8f;
            Stamina -= Time.deltaTime;
            if (Stamina < 0)
            {
                Runable = false;
                RunSpeed = Speed;
            }
        }
        else
        {
            Stamina += Time.deltaTime;
            if (Stamina >= 3.0f)
            {
                Stamina = 3.0f;
                Runable = true;
            }
            RunSpeed = Speed;
        }
        rigid.velocity = moveVec * RunSpeed;

        Debug.DrawRay(rigid.position, dirVec * 0.7f, new Color(0,1,0));
        RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, dirVec, 0.7f, LayerMask.GetMask("Object"));

        if(rayHit.collider != null) {
            scanObject = rayHit.collider.gameObject;
        }
        else   
            scanObject = null;
    }
}

