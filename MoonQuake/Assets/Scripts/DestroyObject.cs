using UnityEngine;

public class DisappearAfterStart : MonoBehaviour
{
    // �����, ����� ������� ������� ����� �������
    public float disappearDelay = 2f;

    // �������, ������� ����� �������� ����� ������ �����
    public GameObject object1;
    public GameObject object2;
    public GameObject object3;
    public GameObject object4;

    void Start()
    {
        // ������� ������� ����� ��������� �����
        Destroy(object1, disappearDelay);
        Destroy(object2, disappearDelay);
        Destroy(object3, disappearDelay);
        Destroy(object4, disappearDelay);
    }
}
