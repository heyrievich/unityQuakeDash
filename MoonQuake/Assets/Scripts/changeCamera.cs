using UnityEngine;

public class changeCamera : MonoBehaviour
{
    public Camera mainCamera1;
    public Camera mainCamera2;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SwitchCameras();
        }
    }

    void SwitchCameras()
    {
        mainCamera1.enabled = !mainCamera1.enabled;
        mainCamera2.enabled = !mainCamera2.enabled;
    }
}
