using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveBullet : MonoBehaviour
{
    public GameObject sparkEffect;

    void OnCollisionEnter(Collision coll)
    {
        if (coll.collider.tag == "BULLET") // �浹 �߻��� coll�� BULLET�̶��
        {
            //�Ѿ��� ����
            Destroy(coll.gameObject);

            ContactPoint cp = coll.GetContact(0);
            // ContactPoint �� �浹���� �޾ƿ��� ��ü parameter�� 0 -> �浹������ �������Ͻ� 0��(index)�� ������
            Vector3 hitPoint = cp.point; // �浹����
            Vector3 hitNormal = -cp.normal; // �浹������ ��������

            Quaternion rot = Quaternion.LookRotation(hitNormal); // Instantiate �� ����° parameter�� Quaternion������ �ڷκ���

            //����ũ ����
            GameObject obj = Instantiate(sparkEffect, hitPoint, rot);
            //����ũ ����
            Destroy(obj,0.4f);        }
    }
}
/*
 * �浹 �ݹ��Լ� (Collision Callback Function) == �̺�Ʈ(Event) �߻�
 * OnCollisionEnter() -> �浹�� �Լ� �߻�(1��)
 * OnCollisionStay() -> �浹 ���϶� �߻�(n��)
 * OnCollisionExit() -> �浹 ������ �߻�(1��)
 * Is Trigger ��üũ �� �߻� (Is Trigger üũ �� �浹 �����Ѵٴ� ��)
 * 
 * Is Trigger üũ�� -> ���� �浹 ���ϰ� �հ� ��������
 * 
 * 
 * �浹 �ݹ��Լ� �߻� ����
 * 1. �浹 �ϴ� �� object �� �� collider��� component�� ������ �־���Ѵ�.
 * 2. �� �� �̵��ϴ� object�� �ݵ�� Rigidbody component�� ������ �־���Ѵ�.
 * 
 */

/*
 * �浹 �� ��ǥ������ ��������� �ݴ�������� ����ũ �߻�(��������)
 * 
 */

/*
 * ����� �߰�
 * 
 * AudioListener ������Ʈ : �Ҹ��� ��� ����. ���� �����ϰ� �����ؾ���(1����)
 * ī�޶� ���� Audio ������Ʈ�� �����ٵ� ����Ҳ� ���� �������� �������� �����������. �߿�
 * 
 * AudioSource : �Ҹ��� �߻���Ű�� ����. ���������� ����. �ѼҸ�, ��ź�Ҹ�, ���Ҹ� ���
 * 
 */