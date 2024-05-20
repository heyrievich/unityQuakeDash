using UnityEngine;
using System.Collections;

public class Floor : MonoBehaviour
{
    [SerializeField] private AudioSource floorSound; // ���� ����

    void Start()
    {
        // ��������� ����� ����
        floorSound = GetComponent<AudioSource>();

        // ��������� �������� ��� ��������������� ����� � ����������
        StartCoroutine(PlaySoundWithInterval());
    }

    // �������� ��� ��������������� ����� � ����������
    private IEnumerator PlaySoundWithInterval()
    {
        while (true)
        {
            // ������� 2 �������
            yield return new WaitForSeconds(3.5f);

            // ������������� ���� ����
            floorSound.Play();
        }
    }
}
