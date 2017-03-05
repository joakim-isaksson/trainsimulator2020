using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ketsu
{
    public class CameraController : MonoBehaviour
    {
        public Transform Target;
        public Vector3 Offset;
        public float Speed;
        public float Power;

        void Update()
        {
            Vector3 targetPos = Target.position + Offset;

            float distance = Vector3.Distance(transform.position, targetPos);

            transform.position = Vector3.MoveTowards(transform.position, targetPos, Mathf.Pow(Speed * distance, Power));
        }
    }
}