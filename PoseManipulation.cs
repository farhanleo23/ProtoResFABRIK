using System.Collections.Generic;
using UnityEngine;
using Unity.Barracuda;

public class PoseManipulation : MonoBehaviour
{
    public NNModel modelAsset;
    private Model m_RuntimeModel;
    private IWorker m_Worker;

    public Transform characterRoot;
    private Dictionary<string, Transform> mixamoJoints;

    private const int num_pos_effectors = 3;
    private const int num_rot_effectors = 3;
    private const int num_lookat_effectors = 1;

    void Start()
    {
        m_RuntimeModel = ModelLoader.Load(modelAsset);
        m_Worker = WorkerFactory.CreateWorker(WorkerFactory.Type.ComputePrecompiled, m_RuntimeModel);

        mixamoJoints = new Dictionary<string, Transform>();

        AddJointsRecursive(characterRoot);
    }

    void Update()
    {
        int headId = 55;
        int leftHandId = 48;
        int rightHandId = 36;

        Vector3 lookAtTarget = new Vector3(0, 2, 5);

        float[] position_data_values = new float[9] {
            mixamoJoints["mixamorig:Head"].localPosition.x, mixamoJoints["mixamorig:Head"].localPosition.y, mixamoJoints["mixamorig:Head"].localPosition.z,
            mixamoJoints["mixamorig:LeftHand"].localPosition.x, mixamoJoints["mixamorig:LeftHand"].localPosition.y, mixamoJoints["mixamorig:LeftHand"].localPosition.z,
            mixamoJoints["mixamorig:RightHand"].localPosition.x, mixamoJoints["mixamorig:RightHand"].localPosition.y, mixamoJoints["mixamorig:RightHand"].localPosition.z
        };

        float[] rotation_data_values = new float[12] {
            mixamoJoints["mixamorig:Head"].localRotation.x, mixamoJoints["mixamorig:Head"].localRotation.y, mixamoJoints["mixamorig:Head"].localRotation.z, mixamoJoints["mixamorig:Head"].localRotation.w,
            mixamoJoints["mixamorig:LeftHand"].localRotation.x, mixamoJoints["mixamorig:LeftHand"].localRotation.y, mixamoJoints["mixamorig:LeftHand"].localRotation.z, mixamoJoints["mixamorig:LeftHand"].localRotation.w,
            mixamoJoints["mixamorig:RightHand"].localRotation.x, mixamoJoints["mixamorig:RightHand"].localRotation.y, mixamoJoints["mixamorig:RightHand"].localRotation.z, mixamoJoints["mixamorig:RightHand"].localRotation.w
        };

        float[] lookat_data_values = new float[6] {
        mixamoJoints["mixamorig:Head"].position.x, mixamoJoints["mixamorig:Head"].position.y, mixamoJoints["mixamorig:Head"].position.z,
        lookAtTarget.x, lookAtTarget.y, lookAtTarget.z
        };

        var position_data = new Tensor(new TensorShape(1, num_pos_effectors, 3), position_data_values, "position_data");
        var position_weight = new Tensor(new TensorShape(1, num_pos_effectors), new float[] { headId, leftHandId, rightHandId }, "position_weight");
        var position_tolerance = new Tensor(new TensorShape(1, num_pos_effectors), new float[] { 1f, 1f, 1f }, "position_tolerance");
        var position_id = new Tensor(new TensorShape(1, num_pos_effectors), new float[] { 0.1f, 0.1f, 0.1f }, "position_id");

        var rotation_data = new Tensor(new TensorShape(1, num_rot_effectors, 4), rotation_data_values, "");
        var rotation_weight = new Tensor(new TensorShape(1, num_rot_effectors), new float[] { headId, leftHandId, rightHandId }, "");
        var rotation_tolerance = new Tensor(new TensorShape(1, num_rot_effectors), new float[] { 1f, 1f, 1f }, "");
        var rotation_id = new Tensor(new TensorShape(1, num_rot_effectors), new float[] { 0.1f, 0.1f, 0.1f }, "");

        var lookat_data = new Tensor(new TensorShape(1, num_lookat_effectors, 6), lookat_data_values, "");
        var lookat_weight = new Tensor(new TensorShape(1, num_lookat_effectors), new float[] { headId }, "");
        var lookat_tolerance = new Tensor(new TensorShape(1, num_lookat_effectors), new float[] { 1f }, "");
        var lookat_id = new Tensor(new TensorShape(1, num_lookat_effectors), new float[] { 0.1f }, "");


        // position_data.SetData(position_data_values);
        // position_id.SetData(new int[] { headId, leftHandId, rightHandId });
        // position_weight.SetData(new float[] { 1f, 1f, 1f });
        // position_tolerance.SetData(new float[] { 0.1f, 0.1f, 0.1f });

        // rotation_data.SetData(rotation_data_values);
        // rotation_id.SetData(new int[] { headId, leftHandId, rightHandId });
        // rotation_weight.SetData(new float[] { 1f, 1f, 1f });
        // rotation_tolerance.SetData(new float[] { 0.1f, 0.1f, 0.1f });

        // lookat_data.SetData(lookat_data_values);
        // lookat_id.SetData(new int[] { headId });
        // lookat_weight.SetData(new float[] { 1f });
        // lookat_tolerance.SetData(new float[] { 0.1f });

        m_Worker.Execute(new Dictionary<string, Tensor> {
        { "position_data", position_data },
        { "position_id", position_id },
        { "position_weight", position_weight },
        { "position_tolerance", position_tolerance },

        { "rotation_data", rotation_data },
        { "rotation_id", rotation_id },
        { "rotation_weight", rotation_weight },
        { "rotation_tolerance", rotation_tolerance },

        { "lookat_data", lookat_data },
        { "lookat_id", lookat_id },
        { "lookat_weight", lookat_weight },
        { "lookat_tolerance", lookat_tolerance }
        });

        var joint_positions = m_Worker.PeekOutput("joint_positions");
        var joint_rotations = m_Worker.PeekOutput("joint_rotations");
        var root_joint_position = m_Worker.PeekOutput("root_joint_position");

        // Apply output to the Mixamo character
        ApplyOutputToCharacter(joint_positions, joint_rotations, root_joint_position);

        // Dispose input and output tensors
        position_data.Dispose();
        position_weight.Dispose();
        position_tolerance.Dispose();
        position_id.Dispose();
        rotation_data.Dispose();
        rotation_weight.Dispose();
        rotation_tolerance.Dispose();
        rotation_id.Dispose();
        lookat_data.Dispose();
        lookat_weight.Dispose();
        lookat_tolerance.Dispose();
        lookat_id.Dispose();
        joint_positions.Dispose();
        joint_rotations.Dispose();
        root_joint_position.Dispose();
    }

    void AddJointsRecursive(Transform t)
    {
        if (t == null) return;

        if (!mixamoJoints.ContainsKey(t.name))
        {
            mixamoJoints.Add(t.name, t);
        }

        for (int i = 0; i < t.childCount; ++i)
        {
            AddJointsRecursive(t.GetChild(i));
        }
    }

    private void ApplyOutputToCharacter(Tensor joint_positions, Tensor joint_rotations, Tensor root_joint_position)
    {
        characterRoot.position = new Vector3(root_joint_position[0, 0], root_joint_position[0, 1], root_joint_position[0, 2]);

        for (int i = 0; i < 64; i++)
        {
            string jointName = "mixamorig:Joint" + i;
            if (mixamoJoints.ContainsKey(jointName))
            {
                Transform jointTransform = mixamoJoints[jointName];

                // Apply joint position
                jointTransform.localPosition = new Vector3(joint_positions[0, i, 0].ReadFloat(), joint_positions[0, i, 1], joint_positions[0, i, 2]);

                // Apply joint rotation
                Vector3 rotationAxis = new Vector3(joint_rotations[0, i, 0].ReadFloat(), joint_rotations[0, i, 1], joint_rotations[0, i, 2]);
                float rotationAngle = joint_rotations[0, i, 3];
                jointTransform.localRotation = Quaternion.AngleAxis(rotationAngle, rotationAxis);
            }
        }
    }

    void OnDestroy()
    {
        m_Worker.Dispose();
    }
}
