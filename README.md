## ReadMe

## Download the Folder called protoinf with the Trained ProtoRes Model & ```run.py``` file.
The ```run.py``` will be used to communicate with our Unity project as it will do the following: 
1. get input from unity
2. pass input into the ```model.onnx``` file and run an inference
3. get the output from the model and pass it back to unity.
 
<img width="399" alt="Screenshot 2023-04-20 at 12 38 56 PM" src="https://user-images.githubusercontent.com/8142476/233470714-50658348-0984-4131-a1c8-f06068993696.png">

## Install requirements

run the follow command in the ```protoinf``` folder.
```bash
pip install -r requirements.txt
```

## Open the Unity Project
Ensure that you are in 'Scene 1' and you should have 4 assets in the assets folder:
1. ```PoseManipulation.cs```
2. ```JointNames.cs```
3. ```FabrikSolver.cs```
4. ```X_Bot.fbx```

<img width="1025" alt="Screenshot 2023-04-20 at 11 43 13 AM" src="https://user-images.githubusercontent.com/8142476/233458776-af8fb25b-6877-4e19-b7fd-36b78a3bb060.png">


## View X Bot in the inspector

Select 'X Bot' from the Scene explorer to view its details in the inspector.
It should look the following image, You need to make changes to the variables in the green box:

<img width="487" alt="Screenshot 2023-04-20 at 12 07 59 PM" src="https://user-images.githubusercontent.com/8142476/233464132-b82f0849-9869-4d0a-85ea-1618cdb0b0f3.png">

For ```Python Path```, give the path of python on computer. To find this address, run on terminal/command line
For MAC:
```bash
which python
``` 
For Windows:
```bash
where python
```
For ```Run Path```, give the location of the ```run.py``` file we have in the ```protoinf``` folder.

For ```Input Json Path``` and ```Output Json Path``` we need to give the location of where the script should create the output and input files (also in the ```protoinf``` folder).

For ```Model Path```, give the path of the ```model.onnx``` file in the ```protoinf``` folder. 


## Run in UNITY

Modify the pose of the humanoid or rotation accordingly and then press run to give the input into the protores+FABRIK algorithm to get a pose output in real time. 

<img width="666" alt="Screenshot 2023-04-18 at 12 06 26 AM" src="https://user-images.githubusercontent.com/8142476/232698769-c55407fb-d7a9-41fc-afc2-fb1560233f44.png">

## Important Links

DEMO VIDEO available here: https://youtu.be/C2zU3rCezGs

PROJECT REPO ON GITHUB: https://github.com/farhanleo23/ProtoResFABRIK

TRAINED PROTORES MODEL: https://drive.google.com/file/d/14U4zk96E4X0IaJfAsDKau5kDv0wFboyl/view?usp=share_link
