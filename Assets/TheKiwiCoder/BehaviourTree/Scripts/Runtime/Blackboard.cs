using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheKiwiCoder {

    // This is the blackboard container shared between all nodes.
    // Use this to store temporary data that multiple nodes need read and write access to.
    // Add other properties here that make sense for your specific use case.
    [System.Serializable]
    public class Blackboard {

        [Header("血量信息")]
        public float CurHealth;
        public float MaxHealth;

        [Header("受击信息")]
        public bool BeHit = false;
        public Vector2 HitBackVelocity;

        public bool Dead = false;
        
        public bool Intro = false;
        public bool Introed = false;
    }
}