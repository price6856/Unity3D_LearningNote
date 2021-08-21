using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class CarCameraFollow : MonoBehaviour
{
    
    private Transform target;
    private Vector3 fixedDistance;
    private Vector3 tempPos;
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;//找到标签目标物体
       
        fixedDistance = new Vector3(-2.0f, 1.5f, -2.5f);//相机处于赛车背后的初始位置，需要进行测试
    }
    void FixedUpdate()
    { 
       
        //transformDirection（new Vector3）可以将Vector3的坐标转换为世界坐标；
        tempPos = target.TransformDirection(fixedDistance) + target.position;
        transform.position = Vector3.Lerp(transform.position, tempPos, Time.fixedDeltaTime * 3);
        transform.LookAt(target);//使相机自身z坐标对齐物体的自身z坐标（注意不是世界坐标）
    }
    
    

    /*
    private Transform player;
    private Vector3 repos;
    private Vector3 direction;
    void Awake()
    {
        //获取到player的transform(Tags.Player是player的标签，如果有不明白什么意思的，可以看我前几篇文章，标签的管理)
        player = GameObject.FindWithTag("Player").transform;
        
        //计算player到camera的方向向量的距离
        direction = player.position - transform.position;
        repos = GetRelativePosition(player, transform.position);
    }
    void Update()
    {
        //移动摄像机，使摄像机与player保持一定的方向向量
        //camera当前的位置=player的位置减去方向向量
        //transform.position = player.position - direction;
        transform.position = player.position+repos;
        //transform.position = player.position - direction;
        //transform.LookAt(player);
    }

    private Vector3 GetRelativePosition(Transform origin, Vector3 position)
    {
        Vector3 distance = position - origin.position;
        Vector3 relativePosition = Vector3.zero;
        relativePosition.x = Vector3.Dot(distance, origin.right.normalized);
        relativePosition.y = Vector3.Dot(distance, origin.up.normalized);
        relativePosition.z = Vector3.Dot(distance, origin.forward.normalized);
        return relativePosition;
    }
    */
}
