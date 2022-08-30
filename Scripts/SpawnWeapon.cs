using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnWeapon : MonoBehaviour
{
    public Transform spawnPoint; // ���� ���� ���
    public GameObject[] spawnButton; //���Ǳ� ��ư

    public GameObject[] weaponPrefabs; // ���� ������
    public int[] weaponPrice;

    GameManager manager;


    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }


    // PhysicsPointer �� �浹�� WeaponMarket�� ��ư�� ������ ��� ȣ��Ǵ� �Լ�
    // - PhysicsPointer ������ ������Ʈ(=���� ���� ��ư)�� �̸��� ����
    public void CreateWeapon(string selectButtonName)
    {
        for (int i = 0; i < weaponPrefabs.Length; i++)
        {
            
            if (selectButtonName == spawnButton[i].name)
            {
                spawnButton[i].GetComponent<Animator>().Play(0, 0);
                Debug.Log(" Weapon Spawn ��ư Ŭ�� ����");
                GameObject weapon = Instantiate(weaponPrefabs[i], spawnPoint.position, Quaternion.identity);
                weapon.transform.forward = transform.position;
               

                weapon.GetComponent<Rigidbody>().AddForce(spawnPoint.transform.up * 0.3f, ForceMode.Impulse);
                manager.rabbitScore -= weaponPrice[i];
            }
        }
    }
}
