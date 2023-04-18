using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FabrikController : MonoBehaviour
    {
        // Declare the Transform variables for the limbs
        public Transform leftArmTransform;
        public Transform rightArmTransform;
        public Transform leftLegTransform;
        public Transform rightLegTransform;

        // Declare a list of FabrikSolver components
        private List<FabrikSolver> solvers;

        private void Start()
        {
            solvers = new List<FabrikSolver>();

            // Create FabrikSolver components at runtime and add them to the solvers list
            solvers.Add(CreateSolver(leftArmTransform));
            solvers.Add(CreateSolver(rightArmTransform));
            solvers.Add(CreateSolver(leftLegTransform));
            solvers.Add(CreateSolver(rightLegTransform));

            InitializeSolvers();
        }

        private FabrikSolver CreateSolver(Transform limbTransform)
        {
            // Add a FabrikSolver component to the limb GameObject
            FabrikSolver solver = limbTransform.gameObject.AddComponent<FabrikSolver>();

            solver.target = limbTransform;

            return solver;
        }

        private void InitializeSolvers()
        {
            // Initialize each solver with the appropriate joint references.
            for (int i = 0; i < solvers.Count; i++)
            {
                FabrikSolver solver = solvers[i];
                switch (i)
                {
                    case 0: // Left Arm
                        solver.joints = new Transform[]
                        {
                            transform.Find("mixamorig:Hips/mixamorig:Spine/mixamorig:Spine1/mixamorig:Spine2/mixamorig:LeftShoulder/mixamorig:LeftArm"),
                            transform.Find("mixamorig:Hips/mixamorig:Spine/mixamorig:Spine1/mixamorig:Spine2/mixamorig:LeftShoulder/mixamorig:LeftArm/mixamorig:LeftForeArm"),
                            transform.Find("mixamorig:Hips/mixamorig:Spine/mixamorig:Spine1/mixamorig:Spine2/mixamorig:LeftShoulder/mixamorig:LeftArm/mixamorig:LeftForeArm/mixamorig:LeftHand")
                        };
                        break;
                    case 1: // Right Arm
                        solver.joints = new Transform[]
                        {
                            transform.Find("mixamorig:Hips/mixamorig:Spine/mixamorig:Spine1/mixamorig:Spine2/mixamorig:RightShoulder/mixamorig:RightArm"),
                            transform.Find("mixamorig:Hips/mixamorig:Spine/mixamorig:Spine1/mixamorig:Spine2/mixamorig:RightShoulder/mixamorig:RightArm/mixamorig:RightForeArm"),
                            transform.Find("mixamorig:Hips/mixamorig:Spine/mixamorig:Spine1/mixamorig:Spine2/mixamorig:RightShoulder/mixamorig:RightArm/mixamorig:RightForeArm/mixamorig:RightHand")
                        };

                                    break;
                    case 2: // Left Leg
                        solver.joints = new Transform[]
                        {
                            transform.Find("mixamorig:Hips/mixamorig:LeftUpLeg"),
                            transform.Find("mixamorig:Hips/mixamorig:LeftUpLeg/mixamorig:LeftLeg"),
                            transform.Find("mixamorig:Hips/mixamorig:LeftUpLeg/mixamorig:LeftLeg/mixamorig:LeftFoot")
                        };
                        break;
                    case 3: // Right Leg
                        solver.joints = new Transform[]
                        {
                            transform.Find("mixamorig:Hips/mixamorig:RightUpLeg"),
                            transform.Find("mixamorig:Hips/mixamorig:RightUpLeg/mixamorig:RightLeg"),
                            transform.Find("mixamorig:Hips/mixamorig:RightUpLeg/mixamorig:RightLeg/mixamorig:RightFoot")
                        };
                        break;
                }
            }
        }
    }

