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
        fixedDistance = new Vector3(-2.08f, 1.58f, -2.97f);//���������������ĳ�ʼλ�ã���Ҫ���в���
    }
    void FixedUpdate()
    {
        //transformDirection��new Vector3�����Խ�Vector3������ת��Ϊ�������ꣻ
        tempPos = target.TransformDirection(fixedDistance) + target.position;
        transform.position = Vector3.Lerp(transform.position, tempPos, Time.fixedDeltaTime * 3);
    transform.LookAt(target);//ʹ�������z����������������z���꣨ע�ⲻ���������꣩
    }

}
