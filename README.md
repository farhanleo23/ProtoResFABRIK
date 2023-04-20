## ReadMe

## Download the Folder called protoinf with Trained Model & run.py file.
The run.py will be used to communicate with our Unity project as it will do the following: 
1. get input from unity
2. pass input into the model.onnx file and run an inference
3. get the output from the model and pass it back to unity.
 
 *Note: You will not see the input.json and output.json till you actually run the program in unity*
<img width="448" alt="Screenshot 2023-04-20 at 11 41 21 AM" src="https://user-images.githubusercontent.com/8142476/233458363-1ec97679-fa65-42dc-99c8-59d52e911471.png">


## Open the Unity Project
Ensure that you are in 'Scene 1' and you should have 4 assets in the assets folder:
1. PoseManipulation.cs
2. JointNames.cs
3. FabrikSolver.cs
4. X_Bot.fbx

<img width="1025" alt="Screenshot 2023-04-20 at 11 43 13 AM" src="https://user-images.githubusercontent.com/8142476/233458776-af8fb25b-6877-4e19-b7fd-36b78a3bb060.png">


## View X Bot in the inspector

Select 'X Bot' from the Scene explorer to view its details in the inspector.
It should look the following image:

For Python Script Path, give the path of the run.py file we have in the protoinf folder.

For Input Json Path and Output Json Path we need to give the location of where the script should create the output and input files (also in the protoinf folder).

For Model Path, give the path of the model.onnx file in the protoinf folder. 

<img width="487" alt="Screenshot 2023-04-20 at 11 24 56 AM" src="https://user-images.githubusercontent.com/8142476/233458146-d9300155-35f3-418b-972e-8e5b1730fd57.png">


## Run in UNITY

Modify the pose of the humanoid or rotation accordingly and then press run to give the input into the protores+FABRIK algorithm to get a pose output in real time. 

<img width="666" alt="Screenshot 2023-04-18 at 12 06 26 AM" src="https://user-images.githubusercontent.com/8142476/232698769-c55407fb-d7a9-41fc-afc2-fb1560233f44.png">

## Important Links

DEMO VIDEO available here: https://youtu.be/C2zU3rCezGs

PROJECT REPO ON GITHUB: https://github.com/farhanleo23/ProtoResFABRIK

TRAINED PROTORES MODEL: https://drive.google.com/file/d/14U4zk96E4X0IaJfAsDKau5kDv0wFboyl/view?usp=share_link
