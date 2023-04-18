## ReadMe
## Train the model

```python
conda create --name protores python=3.8
```

```python
conda activate protores
```
```python
pip install -r requirements.txt -f https://download.pytorch.org/whl/torch_stable.html
```

## launch training session

```python
python run.py --config=protores/configs/experiments/default.yaml
```

## Create a Unity Project
Initialise empty Unity project
## Import files:
1. modified_model.onnx
2. FabrikController.cs
3. FabrikSolver.cs
4. X_Bot.fbx files

Place the 3d humanoid into the scene, and attach the FabrikController script to it.

<img width="479" alt="Screenshot 2023-04-18 at 12 09 47 AM" src="https://user-images.githubusercontent.com/8142476/232699200-1da4cd1e-448b-4b61-a58f-d5c0e55c544e.png">

Once you do that, you will see 4 input Transforms, select leftleg, leftarm, rightleg, rightarm.
<img width="361" alt="Screenshot 2023-04-18 at 12 06 37 AM" src="https://user-images.githubusercontent.com/8142476/232698772-f25fe8e6-6e2f-459a-8ba1-42445abcb68c.png">

Modify the pose of the humanoid or rotation accordingly and then press run to give the input into the protores+FABRIK algorithm to get a pose output in real time. 

<img width="666" alt="Screenshot 2023-04-18 at 12 06 26 AM" src="https://user-images.githubusercontent.com/8142476/232698769-c55407fb-d7a9-41fc-afc2-fb1560233f44.png">

## Important Links

DEMO VIDEO available here: https://youtu.be/C2zU3rCezGs

PROJECT REPO ON GITHUB: https://github.com/farhanleo23/ProtoResFABRIK

TRAINED PROTORES MODEL: https://drive.google.com/file/d/14U4zk96E4X0IaJfAsDKau5kDv0wFboyl/view?usp=share_link

