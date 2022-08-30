using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Rabbit : MonoBehaviour
{
    //FSM 상태:이동,공격,죽음
    enum EnemyState
    {
        Move,
        Attack,
        Damage,
        Die,
        Land
    }
    EnemyState enemyState = EnemyState.Move;

    //-공격 범위 : attackRange
    public float attackRange = 2f;          //-일정 시간(파괴할 때까지 걸리는 시간)
    public float destroyDelayTime = 2.5f;   //-경과 시간 :currentTime
    public int damage;
    public int MaxHP;
    public float lerpSpeed;
    public Slider HPBar;

    public Transform target; //-때리는 대상
    public GameObject Ground; // 땅
    public GameObject player; //플레이어
    public GameManager manager;

    public AudioSource enemyDieAudio;
    public AudioSource enemyDamageAudio;
    public GameObject damageEffect;

    float currentTime = 0f;
    float HP;
    
    private NavMeshAgent agent;
    private Animator animator;      //-Enemy의 Animator 컴포넌트

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.enabled = false;
        HP = MaxHP;
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    void Start()
    {
        if (target == null && Ground == null)
        {
            target = GameObject.Find("Target").transform;
            Ground = GameObject.Find("Ground");
        }

        HPBar = GetComponentInChildren<Slider>();

        //2.필요한 컴포넌트를 가져와 할당/
        agent.enabled = true;
        animator = player.GetComponent<Animator>();
        //1.target에 Hierarchy에서 Tower 오브젝트를 찾아 할당해준다.
        //target = GameObject.Find("Tower").transform;
        //3.State를 Move로 초기화
        ChangeState(EnemyState.Move);

        
    }

    // Update is called once per frame
    void Update()
    {
        if (HPBar.value != HP / MaxHP)
        {
            HPBar.value = Mathf.Lerp(HPBar.value, HP / MaxHP, lerpSpeed * Time.deltaTime);
        }

        //FSM에 따른 상태 함수 실행
        switch (enemyState)
        {
            case EnemyState.Move: Move(); break;
            case EnemyState.Damage: Damage(); break;
            case EnemyState.Die: Die(); break;
        }
    }
    private void OnDrawGizmosSelected()
    {
        //Vector3 offsetY = new Vector3(0, 2.5f, 0); //Gizmos 그릴때 센터 조절
        Gizmos.color = Color.cyan;              //Gizmos 색상
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
    void Move()
    {

        //Target이 가까워지면,target이 공격하도록 Attack 상태로 바꿔야함.
        //내 공격범위 안으로 Target이 들어오면 공격
        //3.Target과 나 사이의 거리
        float distance = Vector3.Distance(target.position, transform.position);

        //2.공격 범위 안으로 Target이 들어왔을 때,
        if (distance < attackRange)
        {
            Vector3 direction = transform.position - target.transform.position;
            direction.y = 0;

            ChangeState(EnemyState.Attack);
            Quaternion dir = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, dir, 100f * Time.deltaTime);
            
            //    //1.공격상태로 전환
        }
    }
    void Damage()
    {
        GameObject effect = Instantiate(damageEffect);


        
        effect.transform.position = transform.position;
        effect.transform.rotation = transform.rotation;
        //effect.transform.localScale = new Vector3(10,10,10);
        effect.GetComponent<ParticleSystem>().Play();
        enemyDamageAudio.Play();

        ChangeState(EnemyState.Move);
    }
    //피격 시에 실행할 함수
  
    void Die()
    {
        agent.baseOffset = agent.baseOffset - 0.1f * Time.deltaTime;
        enemyDieAudio.Play();

        //죽을때,일정시간 이후에 파괴하고 싶다/죽고 싶다.
        //1.시간이 흐른다.
        currentTime += Time.deltaTime;
        //2.경과시간이 일정시간보다 커지면
        if (currentTime > destroyDelayTime)
        {
            manager.rabbitScore++;
            Destroy(gameObject);
            //3.나자신을 파괴한다.
        }
        
    }
    void ChangeState(EnemyState state)
    {
        //state가 만일 Move 상태라면, 목적지 설정
        if (state == EnemyState.Move)
        {
            //agent의 목적지(=target) 설정
            agent.SetDestination(target.position);
            //+Move animation실행
            //animator.SetTrigger("Move");
        }
        else if (state == EnemyState.Attack)
        {
            //agent 이동 멈추기
            agent.isStopped = true;
            //+Attack animation 실행
            //animator.SetTrigger("Attack")
        }
        else if (state == EnemyState.Die)
        {
            //agent 이동 멈추기
            agent.isStopped = true;
        }
        else if(state == EnemyState.Damage)
        {
            agent.isStopped = true;
        }
        //state 별로 맞는 animation실행
        string animationName = state.ToString();
        animator.SetTrigger(animationName);
        //state 변경
        enemyState = state;
    }
    //Enemy가 가지고 있는 Animation 동작에 맞추어 실행되는 이벤트 함수
    

    private void OnCollisionEnter(Collision collision)
    {
        //땅에 닿으면 착지 모션 > move
        if (collision.gameObject.CompareTag("Ground"))
        {
            ChangeState(EnemyState.Land);
        }
    }

    public void DamageProcess()
    {
        //죽음 =>죽은 상태로 변경
        ChangeState(EnemyState.Damage);
        Debug.Log("맞았네.. 시무룩. ..");

        HP= HP - 5;

        //체력 0 이하면 죽음
        if (HP < 1)
        {
            ChangeState(EnemyState.Die);
        }
    }
}
 