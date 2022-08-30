using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    public WeaponType weaponType;
    public GameObject weaponEffect;
    public AudioSource weaponAudio;
    public enum WeaponType
    {
        Throw,
        Bomb
    }
    //�ܼ� ������ �������� ��ź����
    // ���⿡ ���� ����� ����
    //�䳢�� ������ ����� �߻�
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.gameObject.tag == "Enemy")
        {
            Debug.Log("���� ����");
            Rabbit rabbit = collision.transform.GetComponentInParent<Rabbit>();
            rabbit.DamageProcess();

            GameObject bombEffect = Instantiate(weaponEffect);
            bombEffect.transform.position = transform.position;
            bombEffect.transform.rotation = transform.rotation;
            //bombEffect.GetComponent<ParticleSystem>().Play();
            weaponAudio.Play();

            Destroy(gameObject);
        }
    }
}
