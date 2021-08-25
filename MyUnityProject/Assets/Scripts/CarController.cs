using UnityEngine;
using System.Collections;
using System.Collections.Generic;



public class CarController : MonoBehaviour
{
    public List<AxleInfo> axleInfos;
    public float maxMotorTorque;
    public float maxSteeringAngle;
    public GameObject frontleftw, frontrightw, rearleftw, rearrightw,car,carbody;
    public float time;
    public float time1;
    public Vector3 lastpos,carpos;

    private MeshFilter mf = null;
    private Vector3 scale;
    private float sum;
    private string sum1;


    void Start()
    {
        // frontleftw = GameObject.FindWithTag("frontwheel");
        frontleftw = GameObject.Find("frontLeft");
        frontrightw = GameObject.Find("frontRight");
        rearleftw = GameObject.Find("rearLeft");
        rearrightw = GameObject.Find("rearRight");
        car = GameObject.Find("Car8");
        carbody = GameObject.Find("CarBody");
        Debug.Log(GetLocalBounds(car, true));
        Debug.Log("车整体:"+GetBounds(car, true));
        Debug.Log("前左车轮:" + GetBounds(frontleftw, false));
        Debug.Log("前右车轮:" + GetBounds(frontrightw, false));
        Debug.Log("后左车轮:" + GetBounds(rearleftw, false));
        Debug.Log("后右车轮:" + GetBounds(rearrightw, false));
        Debug.Log("--------分割线--------");
        var size = carbody.transform.GetComponent<Renderer>().bounds.size;
        Debug.Log("x: " + size.x + ",y: " + size.y);

        /*
        this.mf = this.GetComponent<MeshFilter>();
        //有损缩放
        this.scale = this.transform.lossyScale;
        this.CalculateSumArea();
        this.CalculateSumVolume();
        */
        
        this.mf = frontleftw.GetComponent<MeshFilter>();
        //有损缩放
        this.scale = frontleftw.transform.lossyScale;
        this.CalculateSumArea();
        this.CalculateSumVolume();
        


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

    private void CalculateSumVolume()
    {
        Vector3[] arrVertices = this.mf.mesh.vertices;
        int[] arrTriangles = this.mf.mesh.triangles;
        float sum = 0.0f;
        for (int i = 0; i < this.mf.mesh.subMeshCount; i++)
        {
            int[] arrIndices = this.mf.mesh.GetTriangles(i);
            for (int j = 0; j < arrIndices.Length; j += 3)
                sum += this.CalculateVolume(arrVertices[arrIndices[j]]
                                        , arrVertices[arrIndices[j + 1]]
                                        , arrVertices[arrIndices[j + 2]]);
        }


        Debug.Log("Volume= " + Mathf.Abs(sum));
    }
    private void CalculateSumArea()
    {
        Vector3[] arrVertices = this.mf.mesh.vertices;
        int[] arrTriangles = this.mf.mesh.triangles;
        float sum1 = 0.0f;
        for (int i = 0; i < this.mf.mesh.subMeshCount; i++)
        {
            int[] arrIndices = this.mf.mesh.GetTriangles(i);
            for (int j = 0; j < arrIndices.Length; j += 3)
                sum1 += this.CalculateArea(arrVertices[arrIndices[j]]
                                        , arrVertices[arrIndices[j + 1]]
                                        , arrVertices[arrIndices[j + 2]]);
        }

        Debug.Log("Area = " + sum1);
    }
    private float CalculateVolume(Vector3 pt0, Vector3 pt1, Vector3 pt2)
    {
        pt0 = new Vector3(pt0.x * this.scale.x, pt0.y * this.scale.y, pt0.z * this.scale.z);
        pt1 = new Vector3(pt1.x * this.scale.x, pt1.y * this.scale.y, pt1.z * this.scale.z);
        pt2 = new Vector3(pt2.x * this.scale.x, pt2.y * this.scale.y, pt2.z * this.scale.z);
        float v321 = pt2.x * pt1.y * pt0.z;
        float v231 = pt1.x * pt2.y * pt0.z;
        float v312 = pt2.x * pt0.y * pt1.z;
        float v132 = pt0.x * pt2.y * pt1.z;
        float v213 = pt1.x * pt0.y * pt2.z;
        float v123 = pt0.x * pt1.y * pt2.z;
        return (1.0f / 6.0f) * (-v321 + v231 + v312 - v132 - v213 + v123);
    }
    private float CalculateArea(Vector3 pt0, Vector3 pt1, Vector3 pt2)
    {
        pt0 = new Vector3(pt0.x * this.scale.x, pt0.y * this.scale.y, pt0.z * this.scale.z);
        pt1 = new Vector3(pt1.x * this.scale.x, pt1.y * this.scale.y, pt1.z * this.scale.z);
        pt2 = new Vector3(pt2.x * this.scale.x, pt2.y * this.scale.y, pt2.z * this.scale.z);
        float a = (pt1 - pt0).magnitude;
        float b = (pt2 - pt1).magnitude;
        float c = (pt0 - pt2).magnitude;
        float p = (a + b + c) * 0.5f;
        return Mathf.Sqrt(p * (p - a) * (p - b) * (p - c));
    }


    public Bounds GetLocalBounds(GameObject target, bool include_children = true)
    {

        MeshFilter[] mfs = target.gameObject.GetComponentsInChildren<MeshFilter>();
        Vector3 center = target.transform.localPosition;
        Bounds bounds = new Bounds(center, Vector3.zero);
        if (include_children)
        {
            if (mfs.Length != 0)
            {
                foreach (MeshFilter mf in mfs)
                {
                    if (mf.mesh)
                    {
                        bounds.Encapsulate(mf.mesh.bounds);
                    }
                }
            }
        }
        else
        {
            MeshFilter mf = target.GetComponentInChildren<MeshFilter>();
            if (mf && mf.mesh)
            {
                bounds = mf.mesh.bounds;
            }
        }

        return bounds;

    }

    public Bounds GetBounds(GameObject target, bool include_children = true)
    {

        Renderer[] mrs = target.gameObject.GetComponentsInChildren<Renderer>();
        Vector3 center = target.transform.position;
        Bounds bounds = new Bounds(center, Vector3.zero);
        if (include_children)
        {
            if (mrs.Length != 0)
            {
                foreach (Renderer mr in mrs)
                {
                    bounds.Encapsulate(mr.bounds);
                }
            }
        }
        else
        {
            Renderer rend = target.GetComponentInChildren<Renderer>();
            if (rend)
            {
                bounds = rend.bounds;
            }
        }

        return bounds;

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