using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;


public class ExportModelToSTL : MonoBehaviour
{


    //用于计算模型最小顶点位置，因为stl顶点不能为负。。
    static Vector3 min = new Vector3(Mathf.Infinity, Mathf.Infinity, Mathf.Infinity);

    public static void Export(GameObject go)
    {
        if (go == null)
        {
            Debug.LogError("传入物体为null！");
            return;
        }

        MeshFilter[] filters = go.GetComponentsInChildren<MeshFilter>();

        if (filters == null || filters.Length <= 0)
        {
            Debug.LogError("传入物体没有MeshFilter组件");
            return;
        }

        StringBuilder sb = new StringBuilder();

        //获取最小值，因为stl中顶点不允许有负值
        foreach (var item in filters)
        {
            Vector3 boundsMin = item.mesh.bounds.min;

            if (min.x > boundsMin.x) min.x = boundsMin.x;
            if (min.y > boundsMin.y) min.y = boundsMin.y;
            if (min.z > boundsMin.z) min.z = boundsMin.z;
        }


        sb.Append("solid ")   //添加文件头  solid filename stl
               .Append(go.name)
               .Append(" stl\n\n");

        foreach (var filter in filters)
        {
            ExportToAsc(sb, filter);
        }

        sb.Append("endsolid ")  //stl结束标志
            .Append(go.name)
            .Append(" stl");


        using (StreamWriter sw = new StreamWriter(Application.dataPath + "/" + go.name + ".stl"))
        {
            sw.Write(sb);
            Debug.Log(Application.dataPath + "/" + go.name + ".stl");
            Debug.Log("导出完成");
        }
    }

    private static void ExportToAsc(StringBuilder sb, MeshFilter filter)
    {

        Vector3[] vertices = filter.mesh.vertices;
        int[] triangles = filter.mesh.triangles;
        Vector3[] normals = filter.mesh.normals;

        //sb.Append("solid ")   //添加文件头  solid filename stl
        //  .Append(filter.name)
        //  .Append(" stl\n\n");

        //遍历三角形
        for (int i = 0; i < triangles.Length; i += 3)
        {

            sb.Append("facet normal ")    //对于每个三角形 先保存法线信息  facet normal x y z
                .Append(normals[triangles[i]].x)
                .Append(" ")
                .Append(normals[triangles[i]].y)
                .Append(" ")
                .Append(normals[triangles[i]].z)
                .Append("\n");



            sb.Append("outer loop\n");  //开始保存顶点 标志

            sb.Append("vertex ")  //保存当前三角形的第一个顶点
                .Append(vertices[triangles[i]].x - min.x)
                .Append(" ")
                .Append(vertices[triangles[i]].y - min.y)
                .Append(" ")
                .Append(vertices[triangles[i]].z - min.z)
                .Append("\n");

            sb.Append("vertex ")  //保存当前三角形的第二个顶点
                .Append(vertices[triangles[i + 2]].x - min.x)
                .Append(" ")
                .Append(vertices[triangles[i + 2]].y - min.y)
                .Append(" ")
                .Append(vertices[triangles[i + 2]].z - min.z)
                .Append("\n");

            sb.Append("vertex ")  //保存当前三角形的第三个顶点
                .Append(vertices[triangles[i + 1]].x - min.x)
                .Append(" ")
                .Append(vertices[triangles[i + 1]].y - min.y)
                .Append(" ")
                .Append(vertices[triangles[i + 1]].z - min.z)
                .Append("\n");

            sb.Append("endloop\n");  //顶点保存结束标志

            sb.Append("endfacet\n");  //三角面片保存结束标志
            sb.Append("\n");
        }
    }



    void Start()
    {
        Export(gameObject);
    }

    void Update()
    {
        
    }
}
