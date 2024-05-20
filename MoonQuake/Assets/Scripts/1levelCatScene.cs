using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ImageBlink : MonoBehaviour
{
    public Image imageToBlink; // ������ �� �����������, ������� ����� �������

    void Start()
    {
        // ��������� �������� ��� �������� �����������
        StartCoroutine(BlinkImage());
    }

    private IEnumerator BlinkImage()
    {
        // ��������� ����������� ���� �������� �����������
        while (true)
        {
            // ���������� �����������
            imageToBlink.gameObject.SetActive(true);
            // ���� 0.5 �������
            yield return new WaitForSeconds(1f);
            // ������������ �����������
            imageToBlink.gameObject.SetActive(false);
            // ���� ��� 0.5 �������
            yield return new WaitForSeconds(0.5f);
        }
    }
}
