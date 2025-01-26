using UnityEngine;

public class ShieldActivator : MonoBehaviour
{
    [SerializeField] private TankShield _shield;

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("SSSS");

        if (other.CompareTag("Player"))
        {
            //_shield.gameObject.SetActive(true);
        }
    }
}
