using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//让相机跟着player移动
public class FollowTarget : MonoBehaviour
{
    public Transform playerTransform;//定义player的位置
    private Vector3 offset;//外界定义,不然update用不了start的值
    // Start is called before the first frame update
    void Start()
    {
        
        //transform.position是主相机的位置
        offset = transform.position - playerTransform.position;//计算相机和player的偏移
        //然后通过拖拽为主相机定义transform

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = playerTransform.position + offset;//始终让相机保持此偏移
    }
}
