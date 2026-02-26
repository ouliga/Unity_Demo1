using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battle_Block : MonoBehaviour
{
    [SerializeField]private float _speed_Change_Value;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        AirShip_Controller airShip_Controller = collision.GetComponent<AirShip_Controller>();
        if(airShip_Controller != null)
        {
            Debug.Log("·É´¬½Ó´¥¿ÕÆøÇ½");
            airShip_Controller.Change_AirShip_Speed_Y(_speed_Change_Value);
        }
    }
}
