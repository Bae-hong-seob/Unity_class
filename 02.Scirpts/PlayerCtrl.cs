using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    private float v;
    private float h;
    private float r;

    [System.NonSerialized] //C# 둘이 기능 똑같음. 쓰고싶은거 쓰자.
    [HideInInspector]      //Unity API -> Inspector에서 animation  항목 안보이게 하기. 헷갈리지 않게 하기 위해

    public Animation anim;

    public float speed = 0.01f;
    // 1회 호출
    void Start()
    {
        anim = GetComponent<Animation>(); // 제너릭타입(Generic Type) 문법구조 -> 내가 문법을 지정할 수 있음
        // == anim = this.gameObject.GetComponent<Animation>(); -> anim 자기자신 component 중 animation component만 get
        anim.Play("Idle");
    }

    // 매 프레임 마다 호출, 화면을 랜더링하는 주기
    void Update()
    {
        v = Input.GetAxis("Vertical"); //Up, Down, W, S // -1.0f ~ 0.0f ~ +1.0f
        h = Input.GetAxis("Horizontal");// -1.0f ~ 0.0f ~ +1.0f
        r = Input.GetAxis("Mouse X"); //마우스 
        Debug.Log("v=" + v); // 콘솔 뷰에 메시지 출력
        Debug.Log("h=" + h); // 콘솔 뷰에 메시지 출력

        // transform.Translate(방향 * 속도 * 변위)
        // transform.Translate(Vector3.forward * 0.1f * v); //전진/후진
        // transform.Translate(Vector3.right * 0.1f * h);   //좌/우

        // 벡터의 덧셈 연산
        // Vector3 moveDir = (전후진벡터) + (좌우벡터)
        // Vector3 moveDir = (Vector3.forward * speed *v) + (Vector3.right * speed * h);
        //이동처리
        Vector3 moveDir = (Vector3.forward * v) + (Vector3.right * h);

        transform.Translate(moveDir.normalized * Time.deltaTime * speed);

        //회전처리
        transform.Rotate(Vector3.up * 8.0f * r); //r은 마우스 변위값

        //Debug.Log("정규화 이전 벡터 = " + moveDir.magnitude);
        //Debug.Log("정규화 벡터 = " + moveDir.normalized.magnitude);

        //애니메이션 처리
        PlayerAnimation();
    }

    void PlayerAnimation()
    {
        if (v >= 0.1f) // 전진
        {
            //anim.Play("RunF"); -> 갑자기 모션이 바뀌면 좀 억지스러움 그래서 Play함수는 잘 안씀
            anim.CrossFade("RunF", 0.25f); // 두번째 파라미터 -> 스묻스하게 바뀌는 장면이 걸리는 시간
        }
        else if (v <= -0.1f) // 후진
        {
            anim.CrossFade("RunB", 0.25f);
        }
        else if (h >= 0.1f) // 오른쪽
        {
            anim.CrossFade("RunR", 0.25f);
        }
        else if (h <= -0.1f) // 왼쪽
        {
            anim.CrossFade("RunL", 0.25f);
        }
        else // 정지상태
        {
            anim.CrossFade("Idle", 0.1f);
        }
    }
    /*
     * 정규화 벡터 , 유닛 벡터
     * 
     * Vector3.forward = Vector3(0,0,1)
     * Vector3.up = Vector3(0,1,0)
     * Vector3.right = Vector3(1,0,0)
     * 반대방향은 -1을 곱하는걸로 표현 -> 벡터니까
     * 
     * Vector3.one = Vector3(1,1,1)
     * Vector3.zero = Vector3(0,0,0)
     */
}