using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public Transform target; //玩家对象的Transform组件
    public float moveSpeed = 0.125f; //相机平滑度
    public Vector3 offset; //相机与玩家的相对位置
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 movePosition = Vector3.Lerp(transform.position, desiredPosition, moveSpeed);
        transform.position = movePosition;
    }
}
