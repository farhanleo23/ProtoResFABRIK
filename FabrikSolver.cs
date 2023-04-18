using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FabrikSolver : MonoBehaviour
{
    [SerializeField] public Transform[] joints;
    [SerializeField] public Transform target;
    [SerializeField] public float tolerance = 0.01f;
    [SerializeField] public int maxIterations = 10;

    private float[] boneLengths;
    private float totalLength;

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        boneLengths = new float[joints.Length - 1];
        totalLength = 0;

        for (int i = 0; i < joints.Length - 1; i++)
        {
            boneLengths[i] = Vector3.Distance(joints[i].position, joints[i + 1].position);
            totalLength += boneLengths[i];
        }
    }

    private void Update()
    {
        SolveIK();
    }


    private void SolveIK()
    {
        if (target == null)
            return;

        if (Vector3.Distance(joints[0].position, target.position) > totalLength)
        {
            for (int i = 0; i < joints.Length - 1; i++)
            {
                Vector3 direction = (target.position - joints[i].position).normalized;
                joints[i + 1].position = joints[i].position + direction * boneLengths[i];
            }
        }
        else
        {
            for (int iteration = 0; iteration < maxIterations; iteration++)
            {
                // Forward pass
                for (int i = joints.Length - 1; i > 0; i--)
                {
                    if (i == joints.Length - 1)
                        joints[i].position = target.position;
                    else
                        joints[i].position = joints[i + 1].position + (joints[i].position - joints[i + 1].position).normalized * boneLengths[i];
                }

                // Backward pass
                for (int i = 0; i < joints.Length - 1; i++)
                {
                    joints[i + 1].position = joints[i].position + (joints[i + 1].position - joints[i].position).normalized * boneLengths[i];
                }

                if ((joints[joints.Length - 1].position - target.position).sqrMagnitude < tolerance * tolerance)
                    break;
            }
        }

        for (int i = 0; i < joints.Length - 1; i++)
        {
            Vector3 direction = (joints[i + 1].position - joints[i].position).normalized;
            joints[i].rotation = Quaternion.LookRotation(direction) * Quaternion.Euler(0, 0, 0);
        }
    }
}

