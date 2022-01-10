using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    private float v;
    private float h;
    private float r;

    [System.NonSerialized] //C# ���� ��� �Ȱ���. ��������� ����.
    [HideInInspector]      //Unity API -> Inspector���� animation  �׸� �Ⱥ��̰� �ϱ�. �򰥸��� �ʰ� �ϱ� ����

    public Animation anim;

    public float speed = 0.01f;
    // 1ȸ ȣ��
    void Start()
    {
        anim = GetComponent<Animation>(); // ���ʸ�Ÿ��(Generic Type) �������� -> ���� ������ ������ �� ����
        // == anim = this.gameObject.GetComponent<Animation>(); -> anim �ڱ��ڽ� component �� animation component�� get
        anim.Play("Idle");
    }

    // �� ������ ���� ȣ��, ȭ���� �������ϴ� �ֱ�
    void Update()
    {
        v = Input.GetAxis("Vertical"); //Up, Down, W, S // -1.0f ~ 0.0f ~ +1.0f
        h = Input.GetAxis("Horizontal");// -1.0f ~ 0.0f ~ +1.0f
        r = Input.GetAxis("Mouse X"); //���콺 
        Debug.Log("v=" + v); // �ܼ� �信 �޽��� ���
        Debug.Log("h=" + h); // �ܼ� �信 �޽��� ���

        // transform.Translate(���� * �ӵ� * ����)
        // transform.Translate(Vector3.forward * 0.1f * v); //����/����
        // transform.Translate(Vector3.right * 0.1f * h);   //��/��

        // ������ ���� ����
        // Vector3 moveDir = (����������) + (�¿캤��)
        // Vector3 moveDir = (Vector3.forward * speed *v) + (Vector3.right * speed * h);
        //�̵�ó��
        Vector3 moveDir = (Vector3.forward * v) + (Vector3.right * h);

        transform.Translate(moveDir.normalized * Time.deltaTime * speed);

        //ȸ��ó��
        transform.Rotate(Vector3.up * 8.0f * r); //r�� ���콺 ������

        //Debug.Log("����ȭ ���� ���� = " + moveDir.magnitude);
        //Debug.Log("����ȭ ���� = " + moveDir.normalized.magnitude);

        //�ִϸ��̼� ó��
        PlayerAnimation();
    }

    void PlayerAnimation()
    {
        if (v >= 0.1f) // ����
        {
            //anim.Play("RunF"); -> ���ڱ� ����� �ٲ�� �� ���������� �׷��� Play�Լ��� �� �Ⱦ�
            anim.CrossFade("RunF", 0.25f); // �ι�° �Ķ���� -> �������ϰ� �ٲ�� ����� �ɸ��� �ð�
        }
        else if (v <= -0.1f) // ����
        {
            anim.CrossFade("RunB", 0.25f);
        }
        else if (h >= 0.1f) // ������
        {
            anim.CrossFade("RunR", 0.25f);
        }
        else if (h <= -0.1f) // ����
        {
            anim.CrossFade("RunL", 0.25f);
        }
        else // ��������
        {
            anim.CrossFade("Idle", 0.1f);
        }
    }
    /*
     * ����ȭ ���� , ���� ����
     * 
     * Vector3.forward = Vector3(0,0,1)
     * Vector3.up = Vector3(0,1,0)
     * Vector3.right = Vector3(1,0,0)
     * �ݴ������ -1�� ���ϴ°ɷ� ǥ�� -> ���ʹϱ�
     * 
     * Vector3.one = Vector3(1,1,1)
     * Vector3.zero = Vector3(0,0,0)
     */
}