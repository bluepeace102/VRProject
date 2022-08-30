using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy: MonoBehaviour
{
    //-�����ð�(=Enemy�� �������ִ� �ð�)
    public float createTime;
    //-����ð�
    float currentTime;
    //-Enemy Prefab
    public GameObject enemyPrefab;

    //-���� �ּҽð�
    public float minRandomTime;

    public float maxRandomTime;


    // Start is called before the first frame update
    void Start()
    {
        ResetCreateTime();
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        //�����ð��� �Ǹ�
        if (currentTime > createTime)
        {
            GameObject enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
            //Enemy�� ������ �� target�� �ٶ󺸵��� ���⼳��
            enemy.transform.forward = transform.position;
            // 1. Target ã�� ����
            enemy.GetComponent<Rabbit>().target = GameObject.FindGameObjectWithTag("Target").transform;
            // 2. Ground ����
            enemy.GetComponent<Rabbit>().Ground = GameObject.Find("Plane").gameObject;
            // 3. GameManager ã���ֱ�
            enemy.GetComponent<Rabbit>().manager = GameObject.Find("GameManager").GetComponent<GameManager>();

            currentTime = 0f;

            ResetCreateTime();

        }
    }
    void ResetCreateTime()
    {
        //min~max�� ���� �ð��� ����
        float randomTime = Random.Range(minRandomTime, maxRandomTime);
        //creatTime�� �����ð� �Ҵ�
        createTime = randomTime;
    }
}
