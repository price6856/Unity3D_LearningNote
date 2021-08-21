using UnityEngine;
using System.Collections;
using System.Collections.Generic;



public class CarController : MonoBehaviour
{
    public List<AxleInfo> axleInfos;
    public float maxMotorTorque;
    public float maxSteeringAngle;
    public GameObject frontleftw, frontrightw, rearleftw, rearrightw,car;
    public float time;
    public float time1;
    public Vector3 lastpos,carpos;
    void Start()
    {
        // frontleftw = GameObject.FindWithTag("frontwheel");
        frontleftw = GameObject.Find("frontLeft");
        frontrightw = GameObject.Find("frontRight");
        rearleftw = GameObject.Find("rearLeft");
        rearrightw = GameObject.Find("rearRight");
        car = GameObject.Find("Car8");


    }
    void Update()
    {
        //车轮旋转
        if (Input.GetKey(KeyCode.A))
        {

            if (frontleftw.GetComponent<Transform>().localEulerAngles.y == 0 && frontrightw.GetComponent<Transform>().localEulerAngles.y == 0) {
                //frontleftw.transform.Rotate(0, -30, 0);
                //frontrightw.transform.Rotate(0, -30, 0);
                frontleftw.transform.localEulerAngles = new Vector3(0, -30, 0);
                frontrightw.transform.localEulerAngles = new Vector3(0, -30, 0);
                Debug.Log("向左旋转30°");
            }

        }

        if (Input.GetKey(KeyCode.D))
        {

            if (frontleftw.GetComponent<Transform>().localRotation.y == 0 && frontrightw.GetComponent<Transform>().localRotation.y == 0)
            {
                //frontleftw.transform.Rotate(0, 30, 0);
                //frontrightw.transform.Rotate(0, 30, 0);
                frontleftw.transform.localEulerAngles = new Vector3(0, 30, 0);
                frontrightw.transform.localEulerAngles = new Vector3(0, 30, 0);
                Debug.Log("向右旋转30°");
            } 

        }

        if (Input.GetKeyUp(KeyCode.A))
        {
            frontleftw.transform.localEulerAngles = new Vector3(0, 0, 0);
            frontrightw.transform.localEulerAngles = new Vector3(0, 0, 0);
            Debug.Log("回正");
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            frontleftw.transform.localEulerAngles = new Vector3(0, 0, 0);
            frontrightw.transform.localEulerAngles = new Vector3(0, 0, 0);
            Debug.Log("回正");
        }
        
        //车轮滚动
        if (Input.GetKey(KeyCode.W))
        {
            frontleftw.transform.Rotate(5,0,0);
            frontrightw.transform.Rotate(5,0,0);
            rearleftw.transform.Rotate(5, 0, 0);
            rearrightw.transform.Rotate(5, 0, 0);
            Debug.Log("向前滚动");
        }
        if (Input.GetKey(KeyCode.S))
        {
            frontleftw.transform.Rotate(-5, 0, 0);
            frontrightw.transform.Rotate(-5, 0, 0);
            rearleftw.transform.Rotate(-5, 0, 0);
            rearrightw.transform.Rotate(-5, 0, 0);
            Debug.Log("向后滚动");
        }
        
        if (Input.GetKeyUp(KeyCode.W))
        {
            time1 = Time.time;
            lastpos = frontleftw.transform.position;

        }

        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            Rigidbody rigidbody = car.transform.GetComponent<Rigidbody>();
            rigidbody.velocity = Vector3.zero;
            rigidbody.constraints = RigidbodyConstraints.FreezeAll;
            rigidbody.constraints = RigidbodyConstraints.None;
        }
        
        //lastpos = frontleftw.transform.position;

        if (Time.time - time1 > 0.1)
        {
            time1 = Time.time;
            //判断是否有移动
            if ((frontleftw.transform.position - lastpos).sqrMagnitude > 0.01f && (frontleftw.transform.position - lastpos).z > 0)
            {
                frontleftw.transform.Rotate(240, 0, 0);
                frontrightw.transform.Rotate(240, 0, 0);
                rearleftw.transform.Rotate(240, 0, 0);
                rearrightw.transform.Rotate(240, 0, 0);
            } else if ((frontleftw.transform.position - lastpos).sqrMagnitude > 0.01f && (frontleftw.transform.position - lastpos).z < 0)
            {
                frontleftw.transform.Rotate(-240, 0, 0);
                frontrightw.transform.Rotate(-240, 0, 0);
                rearleftw.transform.Rotate(-240, 0, 0);
                rearrightw.transform.Rotate(-240, 0, 0);
            } 
            else if ((frontleftw.transform.position - lastpos).sqrMagnitude > 0.001f && (frontleftw.transform.position - lastpos).z > 0)
            {
                frontleftw.transform.Rotate(80, 0, 0);
                frontrightw.transform.Rotate(80, 0, 0);
                rearleftw.transform.Rotate(80, 0, 0);
                rearrightw.transform.Rotate(80, 0, 0);

            }
            else if ((frontleftw.transform.position - lastpos).sqrMagnitude > 0.001f && (frontleftw.transform.position - lastpos).z < 0)
            {
                frontleftw.transform.Rotate(-80, 0, 0);
                frontrightw.transform.Rotate(-80, 0, 0);
                rearleftw.transform.Rotate(-80, 0, 0);
                rearrightw.transform.Rotate(-80, 0, 0);
            }
            else
            {
                frontleftw.transform.localEulerAngles = new Vector3(0, 0, 0);
                frontrightw.transform.localEulerAngles = new Vector3(0, 0, 0);
                rearleftw.transform.localEulerAngles = new Vector3(0, 0, 0);
                rearrightw.transform.localEulerAngles = new Vector3(0, 0, 0);
            }
            lastpos = frontleftw.transform.position;
        }



    }

    // finds the corresponding visual wheel
    // correctly applies the transform
    public void ApplyLocalPositionToVisuals(WheelCollider collider)
    {
        if (collider.transform.childCount == 0)
        {
            return;
        }

        Transform visualWheel = collider.transform.GetChild(0);

        Vector3 position;
        Quaternion rotation;
        collider.GetWorldPose(out position, out rotation);

        visualWheel.transform.position = position;
        visualWheel.transform.rotation = rotation;
    }

    public void FixedUpdate()
    {
        float motor = maxMotorTorque * Input.GetAxis("Vertical");
        float steering = maxSteeringAngle * Input.GetAxis("Horizontal");

        foreach (AxleInfo axleInfo in axleInfos)
        {
            if (axleInfo.steering)
            {
                axleInfo.leftWheel.steerAngle = steering;
                axleInfo.rightWheel.steerAngle = steering;
            }
            if (axleInfo.motor)
            {
                axleInfo.leftWheel.motorTorque = motor;
                axleInfo.rightWheel.motorTorque = motor;
            }
            ApplyLocalPositionToVisuals(axleInfo.leftWheel);
            ApplyLocalPositionToVisuals(axleInfo.rightWheel);
        }
    }
}