using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StoneSpawner : MonoBehaviour
{
    public GameObject[] stonePrefabs; // ������ �������� ������
    public List<Vector3> stoneSpawnPoints; // ������ ������� ��� ��������� ������
    public float spawnInterval = 3f; // �������� ��������� ������
    public float stoneLifetime = 4f; // ����� ����� �����
    public int stonesPerSpawn = 2; // ���������� ������, ������������ �� ���
    [SerializeField] private AudioSource floorSound; // ���� ����

    void Start()
    {
        // ��������� ������ ��� ��������� ������
        StartCoroutine(SpawnStonesWithDelay());
    }

    // �������� ��� ��������� ������ � ������ ����� ������
    private IEnumerator SpawnStonesWithDelay()
    {
        while (true)
        {
            // ����� ����� ���������������� �����
            yield return new WaitForSeconds(0.15f);

            // ������������� ���� ����
            floorSound.Play();

            // ����� ����� ������� ������
            yield return new WaitForSeconds(1f);

            // ������� ��������� ���������� ������ �� ���� �����
            for (int i = 0; i < stonesPerSpawn; i++)
            {
                // �������� ��������� ������� �� ������ stoneSpawnPoints
                Vector3 randomSpawnPoint = stoneSpawnPoints[Random.Range(0, stoneSpawnPoints.Count)];
                // �������� ��������� ����������
                Quaternion randomRotation = Random.rotation;
                // �������� ��������� ������ �� ������� stonePrefabs
                GameObject randomPrefab = stonePrefabs[Random.Range(0, stonePrefabs.Length)];
                // ��������� ����� �� ��������� ������� � ��������� ��������� � ��������� ��������
                GameObject newStone = Instantiate(randomPrefab, randomSpawnPoint, randomRotation);
                // ����������� ����� ����� stoneLifetime ������
                Destroy(newStone, stoneLifetime);
            }

            // ���� �� ���������� ������ ������
            yield return new WaitForSeconds(spawnInterval - 2f);
        }
    }
}
