using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) 
    {
        if( other.CompareTag("Item"))
        {
            Debug.Log("충돌");
            other.GetComponent<ItemBase>().Use(transform.parent.gameObject);
        }
    }
}
