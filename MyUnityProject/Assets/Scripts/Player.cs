using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public Rigidbody rd;

    public int score = 0;

    public Text scoreText;

    public GameObject winText,player;

    public Material ma;


    private MeshFilter mf = null;
    private Vector3 scale;
    private float sum;
    private string sum1;

    //public GameObject player;

    // Start is called before the first frame update
    void Start()//只执行一次
    {
        rd = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
        
        //GetLocalBounds(player,false);
        Debug.Log(GetLocalBounds(player, false));
        Debug.Log(GetBounds(player, false));

        this.mf = this.GetComponent<MeshFilter>();
        //有损缩放
        this.scale = this.transform.lossyScale;
        this.CalculateSumArea();
        this.CalculateSumVolume();



    }

    // Update is called once per frame
    void Update()//持续运行
    {
        //Debug.Log("Game is running");
        //(x,y,z)(1,0,0)
        //rd.AddForce(Vector3.right);//三维向量表示力 ,放update是因为给的持续力,若放入start,则只执行一次,效果很难显示出来
        //rd.AddForce(Vector3.left);
        //rd.AddForce(Vector3.up);
        //rd.AddForce(Vector3.forward);
        //rd.AddForce(Vector3.back);
        //rd.AddForce(Vector3.down);
        //rd.AddForce(new Vector3(5, 0, 0));//(x,y,z)

        float h = Input.GetAxis("Horizontal");//获取键盘按下的值,水平,A,D
        float v = Input.GetAxis("Vertical");//垂直(前后),W,S
        //Debug.Log(h);
        rd.AddForce(new Vector3(h, 0, v)*5);//*5表示力乘5

    }

    //碰撞系统事件
    //private void OnCollisionEnter(Collision collision)//开始碰撞
    //{

    //    if (collision.gameObject.tag == "Food")//获取游戏物体的标签,若为Food,则销毁该Food
    //    {
    //        Destroy(collision.gameObject);
    //    }
    //}
    //private void OnCollisionExit(Collision collision)//碰撞结束
    //{

    //}
    //private void OnCollisionStay(Collision collision)//正在碰撞
    //{

    //}

    //触发检测

    private void OnTriggerEnter(Collider other)//开始触发
    {
        if (other.tag == "Food")//获取游戏物体的标签,若为Food,则销毁该Food
        {
            Destroy(other.gameObject);

            score++;

            scoreText.text = "Score:"+score;

            if (score == 5)
            {
                winText.SetActive(true);
            }

        }

    }
    //private void OnTriggerExit(Collider other)//离开触发区域
    //{

    //}
    //private void OnTriggerStay(Collider other)//正在触发
    //{

    //}

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

    private void OnGUI()
    {
        if (GUI.Button(new Rect(Vector2.up * 480, new Vector2(100, 50)), "线框视图"))
        {
            MaterialSwitchtoWireframe();
        }

        if (GUI.Button(new Rect(Vector2.up * 540, new Vector2(100, 50)), "渲染视图"))
        {
            MaterialSwitchtoStandard();
        }
    }

    #region 视图切换
    private void MaterialSwitchtoWireframe()
    {
        Material material = new Material(Shader.Find("SuperSystems/Wireframe-Transparent"));
        GetComponent<Renderer>().material = material;

    }
    private void MaterialSwitchtoStandard()
    {
        Material material = new Material(Shader.Find("Standard"));
        //material.SetVector("_Color", new Vector4(1,1, 1,1));
        Color c = new Color(173f / 255f,69f / 255f, 40f / 255f);
        material.color = c;
        GetComponent<Renderer>().material = material;
        
    }

    #endregion
}
