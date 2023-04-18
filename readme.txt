#creating a readme
#To train the model

conda create --name protores python=3.8

conda activate protores

pip install -r requirements.txt -f https://download.pytorch.org/whl/torch_stable.html

#launch training session

python run.py --config=protores/configs/experiments/default.yaml

#Create a Unity Project
#import modified_model.onnx, FabrikController.cs, FabrikSolver.cs & X_Bot.fbx files

#Place the 3d humanoid into the scene, and attach the FabrikController script to it.
#Once you do that, you will see 4 input Transforms, select leftleg, leftarm, rightleg, rightarm.

#Modify the pose of the humanoid or rotation accordingly and then press run to give the input into the protores+FABRIK algorithm to get a pose output in real time. 


