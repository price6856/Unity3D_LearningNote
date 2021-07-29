using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

public class DrawStlMesh : MonoBehaviour
{
    private string _fileName = "";
    private string _trianglescount = "";

    private int _total;//�����θ�����_total*3��Ϊ�����ζ������
    private int _number;
    private BinaryReader _binaryReader;

    private List<Vector3> _vertices;
    private List<int> _triangles;

    /// <summary>
    ///���STLģ�����ƺ�����������
    /// </summary>
    private void GetFileNameAndTrianglesCount()
    {
        //string fullPath = Path.GetFullPath(AssetDatabase.GetAssetPath(target));
        string fullPath = "C:\\GitHub\\Unity3D_LearningProject\\MyUnityProject\\Assets\\ImportSTL\\��������������ն��.stl";

        using (BinaryReader br = new BinaryReader(File.Open(fullPath, FileMode.Open)))
        {
            _fileName = Encoding.UTF8.GetString(br.ReadBytes(80));//stl�������ļ��У�ǰ80���ֽ�Ϊģ������
            _trianglescount = BitConverter.ToInt32(br.ReadBytes(4), 0).ToString();//֮��4���ֽڴ洢����ģ������������
        }
    }
    /// <summary>
    /// ����STLģ��ʵ��
    /// </summary>
    private void CreateInstance()
    {
        string fullPath = "C:\\GitHub\\Unity3D_LearningProject\\MyUnityProject\\Assets\\ImportSTL\\��������������ն��.stl";
        int gameObjectCount = 60000;//����һ�������а������ٸ��㣨Unity�е���Mesh�����������Ϊ65000����

        _total = int.Parse(_trianglescount);
        _number = 0;
        _binaryReader = new BinaryReader(File.Open(fullPath, FileMode.Open));

        //����ǰ84���ֽ�
        _binaryReader.ReadBytes(84);

        _vertices = new List<Vector3>();//�洢�����ζ�������
        _triangles = new List<int>();//�洢����������

        while (_number < _total)
        {
            byte[] bytes;
            //ÿ50���ֽ�һ�飬�洢�������εķ�ʸ����������Ķ�������
            bytes = _binaryReader.ReadBytes(50);

            if (bytes.Length < 50)
            {
                _number += 1;
                continue;
            }
            //����ֻ���������εĶ������ݣ�����ʸ��
            Vector3 vec1 = new Vector3(BitConverter.ToSingle(bytes, 12), BitConverter.ToSingle(bytes, 16), BitConverter.ToSingle(bytes, 20));
            Vector3 vec2 = new Vector3(BitConverter.ToSingle(bytes, 24), BitConverter.ToSingle(bytes, 28), BitConverter.ToSingle(bytes, 32));
            Vector3 vec3 = new Vector3(BitConverter.ToSingle(bytes, 36), BitConverter.ToSingle(bytes, 40), BitConverter.ToSingle(bytes, 44));

            _vertices.Add(vec1);
            _vertices.Add(vec2);
            _vertices.Add(vec3);

            _number += 1;
        }

        //������д���б��У�ֵΪ0��ÿ�������ж��������һ
        for (int triNum = 0; triNum < _vertices.Count; triNum++)
        {
            int gameObhectIndex = triNum / gameObjectCount;//����ŵ�ǰ���ڸ��ڼ������帳ֵ
            _triangles.Add(triNum - gameObhectIndex * gameObjectCount);
        }

        for (int meshNumber = 0; meshNumber < _vertices.Count; meshNumber += gameObjectCount)
        {
            //����GameObject
            GameObject tem = new GameObject(Path.GetFileNameWithoutExtension(fullPath));
            tem.name = meshNumber.ToString();
            MeshFilter mf = tem.AddComponent<MeshFilter>();
            MeshRenderer mr = tem.AddComponent<MeshRenderer>();

            Mesh m = new Mesh();
            mr.name = meshNumber.ToString();
            if ((_vertices.Count - meshNumber) >= gameObjectCount)
            {
                m.vertices = _vertices.ToArray().Skip(meshNumber).Take(gameObjectCount).ToArray();
                m.triangles = _triangles.ToArray().Skip(meshNumber).Take(gameObjectCount).ToArray();
            }
            else
            {
                m.vertices = _vertices.ToArray().Skip(meshNumber).Take(_vertices.Count - meshNumber).ToArray();
                m.triangles = _triangles.ToArray().Skip(meshNumber).Take(_vertices.Count - meshNumber).ToArray();
            }
            m.RecalculateNormals();

            mf.mesh = m;
            mr.material = new Material(Shader.Find("Standard"));

            _binaryReader.Close();

            //Debug.Log(tem.name + ":�������� " + _vertices.Count);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        GetFileNameAndTrianglesCount();
        CreateInstance();
    }

    // Update is called once per frame
    void Update()
    {

    }
}

