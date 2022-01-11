using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveBullet : MonoBehaviour
{
    public GameObject sparkEffect;

    void OnCollisionEnter(Collision coll)
    {
        if (coll.collider.tag == "BULLET") // 충돌 발생한 coll가 BULLET이라면
        {
            //총알을 삭제
            Destroy(coll.gameObject);

            ContactPoint cp = coll.GetContact(0);
            // ContactPoint 는 충돌지을 받아오는 객체 parameter는 0 -> 충돌지점이 여러개일시 0번(index)를 가져옴
            Vector3 hitPoint = cp.point; // 충돌지점
            Vector3 hitNormal = -cp.normal; // 충돌지점의 법선벡터

            Quaternion rot = Quaternion.LookRotation(hitNormal); // Instantiate 의 세번째 parameter로 Quaternion형으로 자로변형

            //스파크 생성
            GameObject obj = Instantiate(sparkEffect, hitPoint, rot);
            //스파크 삭제
            Destroy(obj,0.4f);        }
    }
}
/*
 * 충돌 콜백함수 (Collision Callback Function) == 이벤트(Event) 발생
 * OnCollisionEnter() -> 충돌시 함수 발생(1번)
 * OnCollisionStay() -> 충돌 중일때 발생(n번)
 * OnCollisionExit() -> 충돌 끝날때 발생(1번)
 * Is Trigger 언체크 시 발생 (Is Trigger 체크 시 충돌 무시한다는 뜻)
 * 
 * Is Trigger 체크시 -> 벽에 충돌 안하고 뚫고 나갈거임
 * 
 * 
 * 충돌 콜백함수 발생 조건
 * 1. 충돌 하는 두 object 둘 다 collider라는 component를 가지고 있어야한다.
 * 2. 둘 중 이동하는 object는 반드시 Rigidbody component를 가지고 있어야한다.
 * 
 */

/*
 * 충돌 시 좌표값에서 진행방향의 반대방향으로 스파크 발생(법선벡터)
 * 
 */

/*
 * 오디오 추가
 * 
 * AudioListener 컴포넌트 : 소리를 듣는 역할. 씬에 유일하게 존재해야함(1개만)
 * 카메라 마다 Audio 컴포넌트가 있을텐데 사용할꺼 말고 나머지는 수동으로 삭제해줘야함. 중요
 * 
 * AudioSource : 소리를 발생시키는 역할. 여러가지가 있음. 총소리, 폭탄소리, 물소리 등등
 * 
 */