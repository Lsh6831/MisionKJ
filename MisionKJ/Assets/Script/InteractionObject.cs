using UnityEngine;

public abstract class InteractionObject : MonoBehaviour
{
    [Header("InteractionObject")]
    [SerializeField]
    protected int maxHp = 100;
    protected int currentHP;

    private void Awake()
    {
        currentHP =maxHp;
    }

    public abstract void TakeDamage(int damage);
}
