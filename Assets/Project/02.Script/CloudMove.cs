using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMove : MonoBehaviour
{
    [Header("��� ����")]
    [SerializeField] private GameObject[] cloud_Obj;

    [Header("��������")]
    [SerializeField] float startPos;
    [Header("��������")]
    [SerializeField] float endPos;

    [SerializeField]
    [Header("y�� ���� ���� ��ġ ���� (�ּڰ�)")]
    [Range(-3, 0)]
    float randomY_Min;
    [SerializeField]
    [Header("y�� ���� ���� ��ġ ���� (�ִ�)")]
    [Range(0, 6)]
    float randomY_Max;

    int rand;   //���� �ߺ� ���� 

    private void Awake()
    {
        StartOn();
    }

    private void OnEnable()
    {
        StartCoroutine(BG_Move());
    }

    IEnumerator BG_Move()
    {
        GameObject obj = CreateObject();
        while (true)
        {
            obj.gameObject.transform.Translate(Vector2.left * 4 * Time.deltaTime);

            if (obj.transform.position.x < endPos)
            {
                ReturnObject(obj);
                obj = null;
                obj = CreateObject();
            }
            yield return null;
        }
    }

    void StartOn()
    {
        randomY_Min = randomY_Min > randomY_Max ? randomY_Min : randomY_Max - 1;

        for (int i = 0; i < cloud_Obj.Length; i++)
        {
            cloud_Obj[i].gameObject.SetActive(false);
        }
    }

    int RandomCloud()
    {
        int i;
        do
        {
            i = Random.Range(0, cloud_Obj.Length);
        } while (cloud_Obj[i].gameObject.activeSelf && i != rand);
        rand = i;
        return i;
    }

    Vector2 RandomPos()
    {
        Vector2 randomPos;

        float randomPosY = Random.Range(randomY_Min, randomY_Max);
        randomPos = new Vector2(startPos,
            this.gameObject.transform.position.y + randomPosY);

        return randomPos;
    }

    GameObject CreateObject()//������Ʈ ����
    {
        GameObject cloud = cloud_Obj[RandomCloud()].gameObject as GameObject;
        cloud.SetActive(true);
        cloud.transform.position = RandomPos();

        return cloud;
    }
    GameObject ReturnObject(GameObject cloud)
    {
        cloud.gameObject.SetActive(false);

        return cloud;
    }

}//end class
