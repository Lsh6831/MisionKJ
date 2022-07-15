using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HPEvent : UnityEngine.Events.UnityEvent<int, int> { }
public class Status : MonoBehaviour
{
    [HideInInspector]
    public HPEvent onHPEvent = new HPEvent();

   [Header("Walk,Run Speed")]
   [SerializeField]
   private float walkSpeed;
   [SerializeField]
   private float runSpeed;

    [Header("HP")]
    [SerializeField]
    private int maxHP = 100;
    private int currentHP;
    [SerializeField]
    private int life = 1;
    private bool whoPlayer = false;

    //외부에서 값 확인하는 용도 get 프로퍼티
   public float WalkSpeed => walkSpeed;
   public float RunSpeed => runSpeed;

   public int CurrentHP => currentHP;
   public int MaxHP => maxHP;

   public bool isDie =false;

//    public EnemyFSM2 enemyFSM2;


    private void Awake()
    {
        currentHP = maxHP;
        // enemyFSM2= GetComponent<EnemyFSM2>();
    }

    public bool DescreaseHP(int damage)
    {
        int previousHP = currentHP;

        currentHP = currentHP - damage > 0 ? currentHP - damage : 0;

        onHPEvent.Invoke(previousHP, currentHP);

        if(currentHP == 0)
        {
            life--;
            if(life==2)
            {
                whoPlayer=true;
                GameManager.instance.Die();
            }
            else if(life==0)
            {
            Debug.Log("죽음");
            isDie=true;
            walkSpeed=0;
            runSpeed=0; 
            if(whoPlayer==true)
            {
                GameManager.instance.Die();
            }
            return true;
            // enemyFSM2.IsDIe();
            }
            else
            {
                GameManager.instance.Die();
            }
        }
        return false;
    }

    public void IncreaseHP(int hp)
    {
        int previousHP =currentHP;
        currentHP = currentHP + hp >maxHP ? maxHP : currentHP +hp;
        onHPEvent.Invoke(previousHP,currentHP);
    }   
}
