using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RabbitEvent : MonoBehaviour
{
    private NavMeshAgent agent;
    public GameObject enemyParent;

    public AudioSource enemyAttackAudio;
    // Start is called before the first frame update
    void Start()
    {
        agent = enemyParent.GetComponent<NavMeshAgent>();
    }


    public void StopMove()
    {
        agent.isStopped = true;
    }

    public void StartMove()
    {
        agent.isStopped = false;
    }
    public void EventAttack(int damage)
    {
        enemyAttackAudio.Play();
        //플레이어를 공격
        PlayerControll playerEvent = GameObject.Find("Player").GetComponent<PlayerControll>();
        playerEvent.Damage(damage);

    }

}
