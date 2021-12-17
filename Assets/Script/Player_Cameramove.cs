using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Player_Cameramove : MonoBehaviour
{
    // Start is called before the first frame update\
    public float xMin, xMax, yMin, yMax;    
     public GameObject target; // 카메라가 따라갈 대상
    public float moveSpeed; // 카메라가 따라갈 속도
    private Vector3 targetPosition; // 대상의 현재 위치

    // Start is called before the first frame update
    void Start()
    {
        moveSpeed = 8.0f;
        xMax =  11.21f;
        yMax = 7.07f;
        xMin = -11.12f;
        yMin = -8.04f;
    }

    // Update is called once per frame
    void Update()
    {
        // 대상이 있는지 체크
        if(target.gameObject != null)
        {
            // this는 카메라를 의미 (z값은 카메라값을 그대로 유지)
            targetPosition.Set(target.transform.position.x, target.transform.position.y, this.transform.position.z);

            // vectorA -> B까지 T의 속도로 이동
            this.transform.position = Vector3.Lerp(this.transform.position, targetPosition, moveSpeed * Time.deltaTime);            

            float x = Mathf.Clamp(transform.position.x, xMin, xMax);
            float y = Mathf.Clamp(transform.position.y, yMin, yMax);

        transform.position = new Vector3(x, y, -10);
        }
        Camera_range();
    }
    void Camera_range()
    {
        if (SceneManager.GetActiveScene().name == "Stage_0"){
             xMax =  5.1f;
             yMax = 3f;
            xMin = -1.16f;
            yMin = -2.07f;
        }
        if (SceneManager.GetActiveScene().name == "Stage_1"){
             xMax =  11.21f;
             yMax = 7.07f;
            xMin = -11.12f;
            yMin = -8.64f;
        }
        if (SceneManager.GetActiveScene().name == "Stage_2"){
             xMax =  6.14f;
             yMax = 14.04f;
            xMin = -20f;
            yMin = -3.5f;
        }
        if (SceneManager.GetActiveScene().name == "Stage_3"){
             xMax =  13.21f;
             yMax = 12f;
            xMin = -16.03f;
            yMin = -4.01f;
        }
        if (SceneManager.GetActiveScene().name == "Stage_4"){
             xMax =  16.06f;
             yMax = 11.87f;
            xMin = -18.11f;
            yMin = -9.27f;
        }
    }
}
