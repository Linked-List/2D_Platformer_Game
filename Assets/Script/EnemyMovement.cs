using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField]
    private bool startLeft;
    private Vector2 direction; 
    [SerializeField]
    private float movementSpeed;
    // Start is called before the first frame update
    void Start()
    {
        if (startLeft)
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            direction = Vector2.left;
        }
        else
        {
            direction = Vector2.right;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position += new Vector3(direction.x*movementSpeed*Time.deltaTime,0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        changeDirection();
    }
    public Vector2 getDirection(){
        return direction;
    }
    public void changeDirection()
    {
        direction *= -1;
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }
    public void killSelf()
    {
        gameObject.SetActive(false);
    }
}
