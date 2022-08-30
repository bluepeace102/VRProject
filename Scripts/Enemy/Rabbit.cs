using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Rabbit : MonoBehaviour
{
    //FSM ����:�̵�,����,����
    enum EnemyState
    {
        Move,
        Attack,
        Damage,
        Die,
        Land
    }
    EnemyState enemyState = EnemyState.Move;

    //-���� ���� : attackRange
    public float attackRange = 2f;          //-���� �ð�(�ı��� ������ �ɸ��� �ð�)
    public float destroyDelayTime = 2.5f;   //-��� �ð� :currentTime
    public int damage;
    public int MaxHP;
    public float lerpSpeed;
    public Slider HPBar;

    public Transform target; //-������ ���
    public GameObject Ground; // ��
    public GameObject player; //�÷��̾�
    public GameManager manager;

    public AudioSource enemyDieAudio;
    public AudioSource enemyDamageAudio;
    public GameObject damageEffect;

    float currentTime = 0f;
    float HP;
    
    private NavMeshAgent agent;
    private Animator animator;      //-Enemy�� Animator ������Ʈ

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

        //2.�ʿ��� ������Ʈ�� ������ �Ҵ�/
        agent.enabled = true;
        animator = player.GetComponent<Animator>();
        //1.target�� Hierarchy���� Tower ������Ʈ�� ã�� �Ҵ����ش�.
        //target = GameObject.Find("Tower").transform;
        //3.State�� Move�� �ʱ�ȭ
        ChangeState(EnemyState.Move);

        
    }

    // Update is called once per frame
    void Update()
    {
        if (HPBar.value != HP / MaxHP)
        {
            HPBar.value = Mathf.Lerp(HPBar.value, HP / MaxHP, lerpSpeed * Time.deltaTime);
        }

        //FSM�� ���� ���� �Լ� ����
        switch (enemyState)
        {
            case EnemyState.Move: Move(); break;
            case EnemyState.Damage: Damage(); break;
            case EnemyState.Die: Die(); break;
        }
    }
    private void OnDrawGizmosSelected()
    {
        //Vector3 offsetY = new Vector3(0, 2.5f, 0); //Gizmos �׸��� ���� ����
        Gizmos.color = Color.cyan;              //Gizmos ����
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
    void Move()
    {

        //Target�� ���������,target�� �����ϵ��� Attack ���·� �ٲ����.
        //�� ���ݹ��� ������ Target�� ������ ����
        //3.Target�� �� ������ �Ÿ�
        float distance = Vector3.Distance(target.position, transform.position);

        //2.���� ���� ������ Target�� ������ ��,
        if (distance < attackRange)
        {
            Vector3 direction = transform.position - target.transform.position;
            direction.y = 0;

            ChangeState(EnemyState.Attack);
            Quaternion dir = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, dir, 100f * Time.deltaTime);
            
            //    //1.���ݻ��·� ��ȯ
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
    //�ǰ� �ÿ� ������ �Լ�
  
    void Die()
    {
        agent.baseOffset = agent.baseOffset - 0.1f * Time.deltaTime;
        enemyDieAudio.Play();

        //������,�����ð� ���Ŀ� �ı��ϰ� �ʹ�/�װ� �ʹ�.
        //1.�ð��� �帥��.
        currentTime += Time.deltaTime;
        //2.����ð��� �����ð����� Ŀ����
        if (currentTime > destroyDelayTime)
        {
            manager.rabbitScore++;
            Destroy(gameObject);
            //3.���ڽ��� �ı��Ѵ�.
        }
        
    }
    void ChangeState(EnemyState state)
    {
        //state�� ���� Move ���¶��, ������ ����
        if (state == EnemyState.Move)
        {
            //agent�� ������(=target) ����
            agent.SetDestination(target.position);
            //+Move animation����
            //animator.SetTrigger("Move");
        }
        else if (state == EnemyState.Attack)
        {
            //agent �̵� ���߱�
            agent.isStopped = true;
            //+Attack animation ����
            //animator.SetTrigger("Attack")
        }
        else if (state == EnemyState.Die)
        {
            //agent �̵� ���߱�
            agent.isStopped = true;
        }
        else if(state == EnemyState.Damage)
        {
            agent.isStopped = true;
        }
        //state ���� �´� animation����
        string animationName = state.ToString();
        animator.SetTrigger(animationName);
        //state ����
        enemyState = state;
    }
    //Enemy�� ������ �ִ� Animation ���ۿ� ���߾� ����Ǵ� �̺�Ʈ �Լ�
    

    private void OnCollisionEnter(Collision collision)
    {
        //���� ������ ���� ��� > move
        if (collision.gameObject.CompareTag("Ground"))
        {
            ChangeState(EnemyState.Land);
        }
    }

    public void DamageProcess()
    {
        //���� =>���� ���·� ����
        ChangeState(EnemyState.Damage);
        Debug.Log("�¾ҳ�.. �ù���. ..");

        HP= HP - 5;

        //ü�� 0 ���ϸ� ����
        if (HP < 1)
        {
            ChangeState(EnemyState.Die);
        }
    }
}
 