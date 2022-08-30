using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy: MonoBehaviour
{
    //-일정시간(=Enemy를 생성해주는 시간)
    public float createTime;
    //-경과시간
    float currentTime;
    //-Enemy Prefab
    public GameObject enemyPrefab;

    //-랜덤 최소시간
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
        //일정시간이 되면
        if (currentTime > createTime)
        {
            GameObject enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
            //Enemy를 생성할 때 target을 바라보도록 방향설정
            enemy.transform.forward = transform.position;
            // 1. Target 찾아 지정
            enemy.GetComponent<Rabbit>().target = GameObject.FindGameObjectWithTag("Target").transform;
            // 2. Ground 지정
            enemy.GetComponent<Rabbit>().Ground = GameObject.Find("Plane").gameObject;
            // 3. GameManager 찾아주기
            enemy.GetComponent<Rabbit>().manager = GameObject.Find("GameManager").GetComponent<GameManager>();

            currentTime = 0f;

            ResetCreateTime();

        }
    }
    void ResetCreateTime()
    {
        //min~max의 랜덤 시간을 생성
        float randomTime = Random.Range(minRandomTime, maxRandomTime);
        //creatTime에 랜덤시간 할당
        createTime = randomTime;
    }
}
