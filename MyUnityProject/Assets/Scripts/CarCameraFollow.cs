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
        target = GameObject.FindGameObjectWithTag("Player").transform;//�ҵ���ǩĿ������
       
        fixedDistance = new Vector3(-2.0f, 1.5f, -2.5f);//���������������ĳ�ʼλ�ã���Ҫ���в���
    }
    void FixedUpdate()
    { 
       
        //transformDirection��new Vector3�����Խ�Vector3������ת��Ϊ�������ꣻ
        tempPos = target.TransformDirection(fixedDistance) + target.position;
        transform.position = Vector3.Lerp(transform.position, tempPos, Time.fixedDeltaTime * 3);
        transform.LookAt(target);//ʹ�������z����������������z���꣨ע�ⲻ���������꣩
    }
    
    

    /*
    private Transform player;
    private Vector3 repos;
    private Vector3 direction;
    void Awake()
    {
        //��ȡ��player��transform(Tags.Player��player�ı�ǩ������в�����ʲô��˼�ģ����Կ���ǰ��ƪ���£���ǩ�Ĺ���)
        player = GameObject.FindWithTag("Player").transform;
        
        //����player��camera�ķ��������ľ���
        direction = player.position - transform.position;
        repos = GetRelativePosition(player, transform.position);
    }
    void Update()
    {
        //�ƶ��������ʹ�������player����һ���ķ�������
        //camera��ǰ��λ��=player��λ�ü�ȥ��������
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
