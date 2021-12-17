using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Kill : MonoBehaviour
{
    private Animator animator;
    [SerializeField]
    // Start is called before the first frame update
    void Start()
    {
        animator = transform.parent.gameObject.GetComponent<Animator>();
        Debug.Log(animator);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            PlayerMove_Contoller p = collision.gameObject.GetComponent<PlayerMove_Contoller>();
            p.killEnemy();
            animator.SetTrigger("death");
        }
    }
}
