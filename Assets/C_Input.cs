using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class C_Input : MonoBehaviour
{
    public float leftStickHorizontal;
    public float leftStickVertical;
    public float leftStickMagnitude;
    public float leftTrigger;
    [Range(0, 1f)]
    public float lTargetingThreshold = .2f;
    public bool lTargeting;

    Player _player;
    Vector2 _stickInput;


    void Awake()
    {
        // Get the Rewired Player object for this player and keep it for the duration of the character's lifetime
        _player = ReInput.players.GetPlayer(0);
    }

    private void Update()
    {
        GetInput();
        ProcessInput();
    }

    void GetInput()
    {
        leftStickHorizontal = _player.GetAxis("Move Horizontal");
        leftStickVertical = _player.GetAxis("Move Vertical");
        leftTrigger = _player.GetAxis("L Targeting");
    }

    void ProcessInput()
    {
        _stickInput = new Vector2(leftStickHorizontal, leftStickVertical);
        leftStickMagnitude = _stickInput.magnitude;

        if (leftTrigger >= lTargetingThreshold)
            lTargeting = true;
        else
            lTargeting = false;
    }
}
