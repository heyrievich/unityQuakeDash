using UnityEngine;

public class Inventory : MonoBehaviour
{
    public GameObject[] slots; // Массив объектов слотов инвентаря

    // Добавление предмета в слот инвентаря
    public void AddItem(GameObject item, int slotIndex)
    {
        // Проверяем, существует ли указанный слот
        if (slotIndex >= 0 && slotIndex < slots.Length)
        {
            // Активируем объект предмета в указанном слоте
            item.SetActive(true);

            // Устанавливаем позицию предмета в центре слота
            item.transform.position = slots[slotIndex].transform.position;
        }
    }

    // Очистка инвентаря (скрытие всех предметов в слотах)
    
}
