using UnityEngine;

public class Inventory : MonoBehaviour
{
    public GameObject[] slots; // ������ �������� ������ ���������

    // ���������� �������� � ���� ���������
    public void AddItem(GameObject item, int slotIndex)
    {
        // ���������, ���������� �� ��������� ����
        if (slotIndex >= 0 && slotIndex < slots.Length)
        {
            // ���������� ������ �������� � ��������� �����
            item.SetActive(true);

            // ������������� ������� �������� � ������ �����
            item.transform.position = slots[slotIndex].transform.position;
        }
    }

    // ������� ��������� (������� ���� ��������� � ������)
    
}
