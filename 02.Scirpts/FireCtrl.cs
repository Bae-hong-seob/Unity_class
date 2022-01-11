#pragma warning disable IDE0051, IDE0052, CS0108

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCtrl : MonoBehaviour


{
    public Transform firePos; // �Ѿ� ���� ��ġ
    public GameObject bulletPrefab; // �Ѿ� ������ ���庯��
    public AudioClip fireSfx; // �� �߻� ���� ���� ����

    [HideInInspector]
    public MeshRenderer muzzleFlash; // �ѱ� ȭ�� �̹��� ������ ����

    private AudioSource audio; // ����� ���� ���� ( ��ü�̸� �����̸� )

    void Start()
    {
        audio = GetComponent<AudioSource>();
        muzzleFlash = firePos.Find("MuzzleFlash").GetComponent<MeshRenderer>();
        // Gameobject.Find("MuzzleFlash") -> Find �޼ҵ�� parameter�� ������ �̸��� ���� object�� �˻���.
        // ���� : Find�� �ð��� ���� �ɸ��� �޼ҵ�� Update �Լ� �ȿ����� ���� �����ϸ� �ȵȴ�.
        muzzleFlash.enabled = false; // muzzleFalsh�� MeshRenderer ������Ʈ�� ��� ����
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) //0�� ���� ���콺��ư, 1�� ������ ���콺 ��ư
        {
            Fire();
        }   
    }

    void Fire()
    {
        // �Ѿ��� ����
        Instantiate(bulletPrefab, firePos.position, firePos.rotation);
        // �ѼҸ� ����
        audio.PlayOneShot(fireSfx, 0.8f);
        // �ѱ� ȭ�� ȿ�� �߻�
        StartCoroutine(ShowMuzzleFlash()); // �ڷ�ƾ �Լ� ȣ�� �޼��� ���
    }

    IEnumerator ShowMuzzleFlash() // void�� �ƴ� -> �� ��ƾ(co-routine) �Լ�
    {
        // �ؽ�ó�� ��ȯ - Offset ���� --> �ѱ� ȭ�� �̹��� 4���߿� ���� ����
        /*
         * ���� �߻� -> 1024x1024 �̹����� 4���� �׸��� �����Ƿ�
         * (0,0) , (0.5,0) , (0.5, 0.5) , (0, 0.5) ���� �װ� �߻����Ѿ���
         * Random.Range(min,max)
         * Random.Range(0,10)
         */

        Vector2 offset = new Vector2(Random.Range(0, 2), Random.Range(0, 2)) * 0.5f; // ���� 0 or 1 ���� �� 0.5�� �����ִ� ���
        muzzleFlash.material.mainTextureOffset = offset;

        // �ؽ�ó(�ѱ� ȭ�� �̹���) ũ�� ����
        float scale = Random.Range(1.0f, 3.0f);
        muzzleFlash.transform.localScale = Vector3.one * scale; // new Vector(scale, scale, scale); �̰ź��� Vector3.one * scale���ξ� ������

        // MuzzleFlash Ȱ��ȭ
        muzzleFlash.enabled = true; // Ư�� attribute Ȱ��/��Ȱ��ȭ -> enabled

        // Waiting ������ �־�� ����ڰ� �ð������� Ȯ�� ����
        yield return new WaitForSeconds(0.1f); // yield(�纸�Ѵ�) return(����loop��) [�ð�]
        // --> ���� ������ ����� ����� ��� 

        // MuzzleFlash ��Ȱ��ȭ
        muzzleFlash.enabled = false;
    }

    /*
     */
}
