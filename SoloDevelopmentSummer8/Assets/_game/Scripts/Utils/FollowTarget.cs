using System;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{

    [SerializeField] private Transform _target;


    public void LateUpdate()
    {
        transform.position = _target.position;
    }
}
