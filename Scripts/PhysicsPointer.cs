using UnityEngine;

public class PhysicsPointer : MonoBehaviour
{
    public float defaultLength = 3.0f;
    private LineRenderer lineRenderer = null;

    
    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();

    }

    private void Update()
    {
        UpdateLength();
    }
    // ���η����� �׷���
    private void UpdateLength()
    {
        // ���η����� ������(�ڱ��ڽ��� ��ġ = hand)
        lineRenderer.SetPosition(0, transform.position);
        // ���̷������� ����(
        lineRenderer.SetPosition(1, CalculateEnd());
    }

    // ���η����� ������ �׸��� ����ؼ� ��ġ(=endPosition) �˷���
    private Vector3 CalculateEnd()
    {
        RaycastHit hit = CreateForwardRaycast();
        // �ε��� ��ü ���� ���.. DefaultLength ���̱��� Line �׸�
        Vector3 endPosition = DefaultEnd(defaultLength);

        // ���࿡ �ε��� ��ü�� �ִٸ� �ű������ Line �׸�
        if (hit.collider)
        {
            endPosition = hit.point;

            // �� ���� ���콺 Ŭ�� or ��Ʈ�ѷ� Ŭ���� �ϰ� �Ǹ� ��ü�� ���´�.
            if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch))
            {
                // �ε��� ��ü : tag = Weapon
                if (hit.collider.tag == "Weapon")
                {
                    // Weapon�� ������ �ִ� WeaponEvent.cs ��ũ��Ʈ�� �����ؼ� OnCatch()�Լ��� �����ؼ� ��´�.
                    hit.collider.GetComponent<WeaponEvents>().OnCatch();
                }
                // �ε��� ��ü : name = �̼� 3 Ŭ�� ��ư
                else if(hit.collider.name == "*Mission 3 Click Button")
                {
                    Debug.Log("��ư Ŭ��");
                    // Ŭ���Ǵ� �ִϸ��̼� ����
                    //hit.collider.GetComponent<Animator>().Stop(0,0);
                    hit.collider.GetComponent<Animator>().Play(0,0);
                    // Enery �������� ������ 
                    hit.collider.GetComponent<ThirdMission>().IncreaseEnergyGuage();
                }
                else if (hit.collider.name == "Lever")
                {
                   Debug.Log("���� ����");
                    // Ŭ���Ǵ� �ִϸ��̼� ����
                    //hit.collider.GetComponentInChildren<Animator>().Stop(0,0);
                   
                    hit.collider.GetComponent<Animator>().SetTrigger("Lever1");//Play(0,0); 
                    //hit.collider.GetComponent<FifthMission>().VRDrag();
                    hit.collider.GetComponent<FifthMission>().GameClear();
                }
                // �ε��� ��ü : tag = Weapon Spawn Button 
                else if (hit.collider.tag == "WeaponSpawnButton")
                {
                    Debug.Log("Weapon Spawn ��ư Ŭ��");
                    // �ε��� ��ü�� Weapon Spawn Button���, �θ����� "SpawnWeapon.cs"�̶�� ������Ʈ�� �پ����� ��.
                    // �� ������Ʈ���� �ش� ������ ��ư�� ��Ī�Ǵ� Weapon �����϶�� �Լ� ȣ��
                    hit.collider.transform.GetComponentInParent<SpawnWeapon>().CreateWeapon(hit.collider.name);
                }
                // �ε��� ��ü : collider.name == spawnButton[i].name
                // else if (hit.collider.name == spawnButton[i].name)
                //{
                //    Debug.Log("��ư Ŭ��");
                //    GameObject.Find("Weapon Market").GetComponent<SpawnWeapon>();

                // }
            }

        }

        return endPosition;
    }

    private RaycastHit CreateForwardRaycast()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.forward);

        Physics.Raycast(ray, out hit, defaultLength);
        return hit;
    }

    private Vector3 DefaultEnd(float length)
    {
        return transform.position + (transform.forward * length);
    }
}
