using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BullseyeHandleCollide : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ball")) 
        {
            gameObject.SetActive(false);

            FindObjectOfType<SetBullseye>().SetNewBullseye();
        }
    }
}
