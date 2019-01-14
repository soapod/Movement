using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class C_Input : MonoBehaviour
{
    public enum StickDirection { Neutral, Up, UpLeft, UpRight, Left, Right, Down, DownLeft, DownRight}

    public StickDirection stickDirection;
    [Range(0, .5f)]
    public float deadZone = .2f;
    public float leftStickHorizontal;
    public float leftStickVertical;
    public float leftStickMagnitude;
    public float leftStickAngle;
    public float leftTrigger;
    public bool dodge;
    public bool attack;
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
        dodge = _player.GetButtonDown("Dodge");
        attack = _player.GetButtonDown("Attack");
        GetStickDirection();
    }

    void ProcessInput()
    {
        _stickInput = new Vector2(leftStickHorizontal, leftStickVertical);
        leftStickMagnitude = _stickInput.magnitude;

        // get the angle of the stick from 0 - 360
        leftStickAngle = Mathf.Atan2(leftStickHorizontal, leftStickVertical) * Mathf.Rad2Deg;
        if(Mathf.Sign(leftStickAngle) == -1f)
            leftStickAngle = 360f - Mathf.Abs(leftStickAngle);

        if (leftTrigger >= lTargetingThreshold)
            lTargeting = true;
        else
            lTargeting = false;
    }

    void GetStickDirection()
    {
        if (leftStickAngle >= 337 || leftStickAngle <= 23)
            stickDirection = StickDirection.Up;

        if (leftStickAngle >= 24 && leftStickAngle <= 66)
            stickDirection = StickDirection.UpRight;

        if (leftStickAngle >= 67 && leftStickAngle <= 113)
            stickDirection = StickDirection.Right;

        if (leftStickAngle >= 114 && leftStickAngle <= 156)
            stickDirection = StickDirection.DownRight;

        if (leftStickAngle >= 157 && leftStickAngle <= 203)
            stickDirection = StickDirection.Down;

        if (leftStickAngle >= 204 && leftStickAngle <= 246)
            stickDirection = StickDirection.DownLeft;

        if (leftStickAngle >= 247 && leftStickAngle <= 293)
            stickDirection = StickDirection.Left;

        if (leftStickAngle >= 294 && leftStickAngle <= 336)
            stickDirection = StickDirection.UpLeft;

        if (leftStickMagnitude <= deadZone)
            stickDirection = StickDirection.Neutral;
    }
}
