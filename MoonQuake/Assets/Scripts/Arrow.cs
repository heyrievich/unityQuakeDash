using UnityEngine;

public class MoveObject : MonoBehaviour
{
    public float moveSpeed = 1f; // �������� �������� �������
    private float initialPositionY; // ��������� ������� �� ��� Y
    private float targetPositionY; // �������� ������� �� ��� Y
    private bool moveForward = true; // ���� ��� ����������� ����������� ��������

    void Start()
    {
        // ������ ��������� ������� �� ��� Y
        initialPositionY = transform.position.y;
        // ������ �������� ������� �� ��� Y
        targetPositionY = initialPositionY + 0.05f; // ������� ������ �� ��������� �������
    }

    void Update()
    {
        // ��������� ����������� �������� � ������ ���, ���� �������� �������� ������� ��� ���������
        if (transform.position.y >= targetPositionY)
        {
            moveForward = false;
        }
        else if (transform.position.y <= initialPositionY)
        {
            moveForward = true;
        }

        // ������� ������ ������ ��� ����� � ����������� �� ����������� ��������
        if (moveForward)
        {
            transform.Translate(0f, moveSpeed * Time.deltaTime, 0f);
        }
        else
        {
            transform.Translate(0f, -moveSpeed * Time.deltaTime, 0f);
        }
    }
}
