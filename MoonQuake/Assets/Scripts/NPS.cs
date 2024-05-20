using UnityEngine;

public class MoveAndDisappear : MonoBehaviour
{
    public Transform targetObject;
    public float moveSpeed = 5f;
    private bool moving = true;
    private float disappearTime = 520f; // �������� �� 8 ������

    void Start()
    {
        Invoke("Disappear", disappearTime);
    }

    void Update()
    {
        if (moving)
        {
            // �������� ����������� � �������� �������
            Vector3 direction = (targetObject.position - transform.position).normalized;

            // ������������ ��������� � ����������� ��������
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);

            // ������� ��������� � �������� �������
            transform.position = Vector3.MoveTowards(transform.position, targetObject.position, moveSpeed * Time.deltaTime);

            // ���� �������� ������ ����, ������������� ���
            if (transform.position == targetObject.position)
            {
                moving = false;
            }
        }
    }

    void Disappear()
    {
        // �������� ����� �������� �����
        Destroy(gameObject);
    }
}
