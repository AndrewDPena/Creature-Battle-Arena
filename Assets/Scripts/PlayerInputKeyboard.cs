using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputKeyboard : MonoBehaviour
{
    
    public IUnityService UnityService;

    private void Start()
    {
        if (UnityService == null)
        {
            UnityService = new UnityService();
        }
    }

    private void Update()
    {
        var move = new Vector2(UnityService.GetAxis("Horizontal"), UnityService.GetAxis("Vertical"));
        gameObject.GetComponent<CreatureMove>().SetVelocity(move);

        // REMOVE ME LATER
        // This code is for early player testing of the health bar
        // This should be removed once a second creature is on the field and health can be tested that way
        if (UnityService.GetKeyDown("left shift"))
        {
            gameObject.GetComponent<Creature>().TakeDamage(10);
        }

        if (UnityService.GetKeyDown("left ctrl"))
        {
            gameObject.GetComponent<Creature>().Attack1(move);
        }
    }
}
