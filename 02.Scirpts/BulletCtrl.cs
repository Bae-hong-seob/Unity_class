using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCtrl : MonoBehaviour
{
    private Rigidbody rb; //속성 추가 -> onclick메소드랑 비슷
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddRelativeForce(Vector3.forward * 800.0f);
    }

    // Update is called once per frame
    // Update 함수 -> 매 프레임마다 실행되는 함수.
    // 따라서 쓰지 않는다면 삭제해줘야한다.

}
