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
    // 라인랜더러 그려줌
    private void UpdateLength()
    {
        // 라인랜더러 시작점(자기자신의 위치 = hand)
        lineRenderer.SetPosition(0, transform.position);
        // 라이랜더러의 끝점(
        lineRenderer.SetPosition(1, CalculateEnd());
    }

    // 라인랜더를 어디까지 그릴지 계산해서 위치(=endPosition) 알려줌
    private Vector3 CalculateEnd()
    {
        RaycastHit hit = CreateForwardRaycast();
        // 부딪힌 물체 없는 경우.. DefaultLength 길이까지 Line 그림
        Vector3 endPosition = DefaultEnd(defaultLength);

        // 만약에 부딪힌 물체가 있다면 거기까지만 Line 그림
        if (hit.collider)
        {
            endPosition = hit.point;

            // 그 순간 마우스 클릭 or 컨트롤러 클릭을 하게 되면 물체를 집는다.
            if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch))
            {
                // 부딪힌 물체 : tag = Weapon
                if (hit.collider.tag == "Weapon")
                {
                    // Weapon이 가지고 있는 WeaponEvent.cs 스크립트에 접근해서 OnCatch()함수를 실행해서 잡는다.
                    hit.collider.GetComponent<WeaponEvents>().OnCatch();
                }
                // 부딪힌 물체 : name = 미션 3 클릭 버튼
                else if(hit.collider.name == "*Mission 3 Click Button")
                {
                    Debug.Log("버튼 클릭");
                    // 클릭되는 애니메이션 실행
                    //hit.collider.GetComponent<Animator>().Stop(0,0);
                    hit.collider.GetComponent<Animator>().Play(0,0);
                    // Enery 게이지가 차도록 
                    hit.collider.GetComponent<ThirdMission>().IncreaseEnergyGuage();
                }
                else if (hit.collider.name == "Lever")
                {
                   Debug.Log("레버 내림");
                    // 클릭되는 애니메이션 실행
                    //hit.collider.GetComponentInChildren<Animator>().Stop(0,0);
                   
                    hit.collider.GetComponent<Animator>().SetTrigger("Lever1");//Play(0,0); 
                    //hit.collider.GetComponent<FifthMission>().VRDrag();
                    hit.collider.GetComponent<FifthMission>().GameClear();
                }
                // 부딪힌 물체 : tag = Weapon Spawn Button 
                else if (hit.collider.tag == "WeaponSpawnButton")
                {
                    Debug.Log("Weapon Spawn 버튼 클릭");
                    // 부딪힌 물체가 Weapon Spawn Button라면, 부모한테 "SpawnWeapon.cs"이라는 컴포넌트가 붙어있을 것.
                    // 그 컴포넌트에서 해당 선택한 버튼에 매칭되는 Weapon 생성하라는 함수 호출
                    hit.collider.transform.GetComponentInParent<SpawnWeapon>().CreateWeapon(hit.collider.name);
                }
                // 부딪힌 물체 : collider.name == spawnButton[i].name
                // else if (hit.collider.name == spawnButton[i].name)
                //{
                //    Debug.Log("버튼 클릭");
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
