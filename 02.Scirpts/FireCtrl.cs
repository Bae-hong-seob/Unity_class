#pragma warning disable IDE0051, IDE0052, CS0108

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCtrl : MonoBehaviour


{
    public Transform firePos; // 총알 생성 위치
    public GameObject bulletPrefab; // 총알 프리팹 저장변수
    public AudioClip fireSfx; // 총 발사 사운드 저장 변수

    [HideInInspector]
    public MeshRenderer muzzleFlash; // 총구 화염 이미지 가져올 변수

    private AudioSource audio; // 오디오 저장 변수 ( 객체이름 변수이름 )

    void Start()
    {
        audio = GetComponent<AudioSource>();
        muzzleFlash = firePos.Find("MuzzleFlash").GetComponent<MeshRenderer>();
        // Gameobject.Find("MuzzleFlash") -> Find 메소드는 parameter와 동일한 이름을 가진 object를 검색함.
        // 주의 : Find는 시간이 많이 걸리는 메소드라 Update 함수 안에서는 절대 존재하면 안된다.
        muzzleFlash.enabled = false; // muzzleFalsh는 MeshRenderer 컴포넌트를 담는 변수
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) //0이 왼쪽 마우스버튼, 1이 오른쪽 마우스 버튼
        {
            Fire();
        }   
    }

    void Fire()
    {
        // 총알을 생성
        Instantiate(bulletPrefab, firePos.position, firePos.rotation);
        // 총소리 생성
        audio.PlayOneShot(fireSfx, 0.8f);
        // 총구 화염 효과 발생
        StartCoroutine(ShowMuzzleFlash()); // 코루틴 함수 호출 메서드 사용
    }

    IEnumerator ShowMuzzleFlash() // void가 아님 -> 코 루틴(co-routine) 함수
    {
        // 텍스처를 교환 - Offset 변경 --> 총구 화염 이미지 4개중에 랜덤 선택
        /*
         * 난수 발생 -> 1024x1024 이미지에 4개의 그림이 있으므로
         * (0,0) , (0.5,0) , (0.5, 0.5) , (0, 0.5) 난수 네개 발생시켜야함
         * Random.Range(min,max)
         * Random.Range(0,10)
         */

        Vector2 offset = new Vector2(Random.Range(0, 2), Random.Range(0, 2)) * 0.5f; // 난수 0 or 1 생성 후 0.5를 곱해주는 방법
        muzzleFlash.material.mainTextureOffset = offset;

        // 텍스처(총구 화염 이미지) 크기 변경
        float scale = Random.Range(1.0f, 3.0f);
        muzzleFlash.transform.localScale = Vector3.one * scale; // new Vector(scale, scale, scale); 이거보다 Vector3.one * scale이훨씬 직관적

        // MuzzleFlash 활성화
        muzzleFlash.enabled = true; // 특성 attribute 활성/비활성화 -> enabled

        // Waiting 과정이 있어야 사용자가 시각적으로 확인 가능
        yield return new WaitForSeconds(0.1f); // yield(양보한다) return(메인loop로) [시간]
        // --> 다중 쓰레드 기법과 비슷한 기능 

        // MuzzleFlash 비활성화
        muzzleFlash.enabled = false;
    }

    /*
     */
}
