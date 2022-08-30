using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnWeapon : MonoBehaviour
{
    public Transform spawnPoint; // 무기 스폰 장소
    public GameObject[] spawnButton; //자판기 버튼

    public GameObject[] weaponPrefabs; // 무기 프리팹
    public int[] weaponPrice;

    GameManager manager;


    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }


    // PhysicsPointer 가 충돌한 WeaponMarket의 버튼을 눌렀을 경우 호출되는 함수
    // - PhysicsPointer 선택한 오브젝트(=무기 생성 버튼)의 이름을 전달
    public void CreateWeapon(string selectButtonName)
    {
        for (int i = 0; i < weaponPrefabs.Length; i++)
        {
            
            if (selectButtonName == spawnButton[i].name)
            {
                spawnButton[i].GetComponent<Animator>().Play(0, 0);
                Debug.Log(" Weapon Spawn 버튼 클릭 실행");
                GameObject weapon = Instantiate(weaponPrefabs[i], spawnPoint.position, Quaternion.identity);
                weapon.transform.forward = transform.position;
               

                weapon.GetComponent<Rigidbody>().AddForce(spawnPoint.transform.up * 0.3f, ForceMode.Impulse);
                manager.rabbitScore -= weaponPrice[i];
            }
        }
    }
}
