using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;


public class ExportModelToSTL : MonoBehaviour
{


    //���ڼ���ģ����С����λ�ã���Ϊstl���㲻��Ϊ������
    static Vector3 min = new Vector3(Mathf.Infinity, Mathf.Infinity, Mathf.Infinity);

    public static void Export(GameObject go)
    {
        if (go == null)
        {
            Debug.LogError("��������Ϊnull��");
            return;
        }

        MeshFilter[] filters = go.GetComponentsInChildren<MeshFilter>();

        if (filters == null || filters.Length <= 0)
        {
            Debug.LogError("��������û��MeshFilter���");
            return;
        }

        StringBuilder sb = new StringBuilder();

        //��ȡ��Сֵ����Ϊstl�ж��㲻�����и�ֵ
        foreach (var item in filters)
        {
            Vector3 boundsMin = item.mesh.bounds.min;

            if (min.x > boundsMin.x) min.x = boundsMin.x;
            if (min.y > boundsMin.y) min.y = boundsMin.y;
            if (min.z > boundsMin.z) min.z = boundsMin.z;
        }


        sb.Append("solid ")   //����ļ�ͷ  solid filename stl
               .Append(go.name)
               .Append(" stl\n\n");

        foreach (var filter in filters)
        {
            ExportToAsc(sb, filter);
        }

        sb.Append("endsolid ")  //stl������־
            .Append(go.name)
            .Append(" stl");


        using (StreamWriter sw = new StreamWriter(Application.dataPath + "/" + go.name + ".stl"))
        {
            sw.Write(sb);
            Debug.Log(Application.dataPath + "/" + go.name + ".stl");
            Debug.Log("�������");
        }
    }

    private static void ExportToAsc(StringBuilder sb, MeshFilter filter)
    {

        Vector3[] vertices = filter.mesh.vertices;
        int[] triangles = filter.mesh.triangles;
        Vector3[] normals = filter.mesh.normals;

        //sb.Append("solid ")   //����ļ�ͷ  solid filename stl
        //  .Append(filter.name)
        //  .Append(" stl\n\n");

        //����������
        for (int i = 0; i < triangles.Length; i += 3)
        {

            sb.Append("facet normal ")    //����ÿ�������� �ȱ��淨����Ϣ  facet normal x y z
                .Append(normals[triangles[i]].x)
                .Append(" ")
                .Append(normals[triangles[i]].y)
                .Append(" ")
                .Append(normals[triangles[i]].z)
                .Append("\n");



            sb.Append("outer loop\n");  //��ʼ���涥�� ��־

            sb.Append("vertex ")  //���浱ǰ�����εĵ�һ������
                .Append(vertices[triangles[i]].x - min.x)
                .Append(" ")
                .Append(vertices[triangles[i]].y - min.y)
                .Append(" ")
                .Append(vertices[triangles[i]].z - min.z)
                .Append("\n");

            sb.Append("vertex ")  //���浱ǰ�����εĵڶ�������
                .Append(vertices[triangles[i + 2]].x - min.x)
                .Append(" ")
                .Append(vertices[triangles[i + 2]].y - min.y)
                .Append(" ")
                .Append(vertices[triangles[i + 2]].z - min.z)
                .Append("\n");

            sb.Append("vertex ")  //���浱ǰ�����εĵ���������
                .Append(vertices[triangles[i + 1]].x - min.x)
                .Append(" ")
                .Append(vertices[triangles[i + 1]].y - min.y)
                .Append(" ")
                .Append(vertices[triangles[i + 1]].z - min.z)
                .Append("\n");

            sb.Append("endloop\n");  //���㱣�������־

            sb.Append("endfacet\n");  //������Ƭ���������־
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
