using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove_Contoller : MonoBehaviour
{
    public PlayerSFXList PSP;
    private Animator animator;
    public float movementSpeed = 50.0f;
    public float JumpPower;
    private int hitpower;
    public Rigidbody2D rigid;
    private Vector2 GraundboxCastSize = new Vector2(0.4f,0.05f);
    private Vector2 WallboxCastSize = new Vector2(0.05f,0.4f);
    private float GraundDistance = 0.826f;
    private float WallDistance = 0.525f;
    private float wallJumpTimer = 0.0f;
    public static int life = 3;
    private bool wallJumpTime = false;
    private bool died = false;
    bool wallwait;//벽에 닿았다 인식 함수
    Vector2 left_right_change = Vector2.right;
    Vector2 left_right_change_jump = new Vector2(1f,1f);
    SpriteRenderer render;
    public static float hitstate_timer;
    public static bool hitstate; // false 상태에서 몬스터에게 닿으면 life 1 달게 하고 true로 바꾸고 몬스터 한테 안맞는 상태로 바꿈 이후 노히트 타이머를 0으로 바꾼다음에 time.delta명렁어에 따라 시간이 지남으로 인해 3초가 지나면 다시 false로 바뀌는 코드가 될 예정
    private int life_switch;
    private float hit_animation_timer;
    private bool Player_hit_stop;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("Player_walk", false);
        animator.SetBool("Player_jump", false);
    } 
    void Start()
    {
        //한번만 실행하게 하는 함수 못찾아서 하는 짓
        life_switch = 3;
//        
        Player_hit_stop = false;
        hitpower = 5;
        hitstate = true;
        life = 3;
        hitstate_timer = .0f;
        render = GetComponent<SpriteRenderer>();
         rigid = transform.GetComponent<Rigidbody2D>();
         JumpPower = 7.0f;
         wallwait = false;
    }

    // Update is called once per frame
    

    void Update()
    {
        if (!died)
        {
            Jump();
            Walljump();
            Landing();
        }
    }
    void FixedUpdate()
    {
        if (!died)
        {
            NoHitState();
            Movement();
            Wallwait();
           Player_Timer();
        }
    }


    void Landing()
    {
        if(!IsOnGround() && !IsOnWall())
        {
            animator.SetBool("Player_jump", true);
        }
        else
        {
            animator.SetBool("Player_jump", false);
        }
    }
    void Jump()
    {
        if (IsOnGround() &&  Input.GetKeyDown(KeyCode.Space))
        {
            PSP.PlayerSFX_SoundPlay(0);
            rigid.AddForce(Vector2.up * JumpPower, ForceMode2D.Impulse);
        }
    }
    void Walljump()
    {
        if(wallwait == true && Input.GetKeyDown(KeyCode.Space))
        {
            PSP.PlayerSFX_SoundPlay(0);
            wallJumpTimer = 0.0f;
            wallJumpTime = true;

            rigid.velocity = Vector2.zero;
            rigid.AddForce(left_right_change_jump * 10f, ForceMode2D.Impulse);
        }
    }
    void Wallwait()
    {
        if(!IsOnGround() && IsOnWall()){
            if (!wallwait)
            {
                wallwait = true;
                rigid.velocity = Vector2.down;
                rigid.gravityScale = 0.3f;
            }
            animator.SetBool("Player_wait_wall", true);
            if(left_right_change == Vector2.right){
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            else{
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }
        else { 
            wallwait = false;
            rigid.gravityScale = 1.0f;
            animator.SetBool("Player_wait_wall", false);
        }
    }
    void Movement()
    {
        if(Input.GetKey (KeyCode.RightArrow)) {
            if(!wallJumpTime && !wallwait && Player_hit_stop == false)
                rigid.velocity = new Vector2(movementSpeed, rigid.velocity.y);
            //rigid.AddForce(Vector2.right * movementSpeed);
            transform.rotation = Quaternion.Euler(0, 0, 0);
            left_right_change = Vector2.right;
            left_right_change_jump = new Vector2(-0.2f,0.7f);
            if(wallwait==false){
                animator.SetBool("Player_walk", true);
                //벽에 붙어있지 않은 상태라면 플레이어 걷기 동작의 매개체 함수를 트루로 바꿔라
            }
        }
        ///////////////////////////////////////// 오른쪽 움직임 코드   
        else if(Input.GetKey (KeyCode.LeftArrow)) {
            if (!wallJumpTime && !wallwait && Player_hit_stop == false)
                rigid.velocity = new Vector2(-movementSpeed, rigid.velocity.y);
            //rigid.AddForce(Vector2.left * movementSpeed);
            transform.rotation = Quaternion.Euler(0, 180, 0);
            left_right_change = Vector2.left;
            left_right_change_jump = new Vector2(0.2f,0.7f);
            if(wallwait==false){
                animator.SetBool("Player_walk", true);
                //벽에 붙어있지 않은 상태라면 플레이어 걷기 동작의 매개체 함수를 트루로 바꿔라
            }
        }
        ///////////////////////////////////////왼쪽 움직임 코드
        else{
            animator.SetBool("Player_walk", false);
        }
    }
    private bool IsOnWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(transform.position, WallboxCastSize, 0f, left_right_change, WallDistance, LayerMask.GetMask("Ground"));
        return (raycastHit.collider != null);
    }//벽과 붙어있는지 확인 함수
    private bool IsOnGround()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(transform.position, GraundboxCastSize, 0f, Vector2.down, GraundDistance, LayerMask.GetMask("Ground"));
        return (raycastHit.collider != null);
    }//땅과 붙어있는지 확인 함수
    void OnDrawGizmos()
    {
        //*
        RaycastHit2D raycastHit = Physics2D.BoxCast(transform.position, WallboxCastSize, 0f, left_right_change, WallDistance, LayerMask.GetMask("Ground"));

        Gizmos.color = Color.red;
        if (raycastHit.collider != null)
        {
            Gizmos.DrawRay(transform.position, left_right_change * raycastHit.distance);
            Gizmos.DrawWireCube(transform.position + Vector3.right * raycastHit.distance, WallboxCastSize);
        }
        else
        {
            Gizmos.DrawRay(transform.position, left_right_change *WallDistance);
        }
        //*/
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////벽 표시
        /*
        RaycastHit2D raycastHit = Physics2D.BoxCast(transform.position, GraundboxCastSize, 0f, Vector2.down, GraundDistance, LayerMask.GetMask("Ground"));

        Gizmos.color = Color.red;
        if (raycastHit.collider != null)
        {
            Gizmos.DrawRay(transform.position, Vector2.down * raycastHit.distance);
            Gizmos.DrawWireCube(transform.position + Vector3.down * raycastHit.distance, GraundboxCastSize);
        }
        else
        {
            Gizmos.DrawRay(transform.position,  Vector2.down * GraundDistance);
        }
        */
    }
    public void killEnemy()
    {
        rigid.velocity = new Vector2(rigid.velocity.x, JumpPower);
    }
    public void ReduceLife()
    {
         if(life_switch == 3)
            GameObject.Find("Game_UI").transform.Find("Life_2").gameObject.SetActive(false);
         if(life_switch == 2)
            GameObject.Find("Game_UI").transform.Find("Life_1").gameObject.SetActive(false);
         if(life_switch == 1){
            GameObject.Find("Game_UI").transform.Find("Life_0").gameObject.SetActive(false);
            GameObject.Find("Game_UI").transform.Find("Face").gameObject.SetActive(false);
            Debug.Log("Die Motion");
            animator.SetTrigger("Player_death");
            died = true;
            GameManager.gm.showGameOver();
         }
        life_switch = life_switch - 1;
    }
    public void PlayerDeath()
    {
        if (life_switch == 3)
            Debug.Log("3"); GameObject.Find("Game_UI").transform.Find("Life_2").gameObject.SetActive(false); life_switch--;
        if (life_switch == 2)
            Debug.Log("2"); GameObject.Find("Game_UI").transform.Find("Life_1").gameObject.SetActive(false); life_switch--;
        if(life_switch == 1)
            Debug.Log("1"); GameObject.Find("Game_UI").transform.Find("Life_0").gameObject.SetActive(false); life_switch--;
        GameObject.Find("Game_UI").transform.Find("Face").gameObject.SetActive(false);
        GameObject.Find("Player").SetActive(false);
        GameManager.gm.showGameOver();
    }
    private void NoHitState(){
        hitstate_timer += Time.deltaTime;

        if (hitstate == false && hitstate_timer < 3)//무적상태 / 맞고 3초 안지난 상태일 때
        {
            render.color = new Color(1, 1, 1, 0.5f);
        }
        else if (hitstate == false && hitstate_timer >= 3)//맞고 3초 이후 무적풀기
        {
            hitstate = true;
            render.color = new Color(1, 1, 1, 1);
        }
    }
    public void Player_hit(Vector2 enemyDirection){
        PSP.PlayerSFX_SoundPlay(1);

        rigid.velocity = Vector2.zero;
        rigid.AddForce(enemyDirection * hitpower,ForceMode2D.Impulse);
        animator.SetBool("Player_hit", true);
        Player_hit_stop = true;
        hit_animation_timer = 0;
    }
    private void Player_Timer(){
         hit_animation_timer += Time.deltaTime;
            if(hit_animation_timer > 0.2){
                animator.SetBool("Player_hit", false);
                Player_hit_stop = false;
                }

            if (wallJumpTime){
                wallJumpTimer += Time.deltaTime;
                if (wallJumpTimer > 0.35f)
                    wallJumpTime = false;
            }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Clear_Item")
        {
            Destroy(col.gameObject);
            GameManager.gm.showStageClear();
        }
    }
}
