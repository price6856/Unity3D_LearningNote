using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public Rigidbody rd;

    public int score = 0;

    public Text scoreText;

    public GameObject winText;

    public Material ma;

    //public GameObject player;

    // Start is called before the first frame update
    void Start()//只执行一次
    {
        rd = GetComponent<Rigidbody>();

        //Debug.Log("Game Start");


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
