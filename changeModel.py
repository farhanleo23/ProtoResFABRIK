import onnx
import numpy as np
from onnx import numpy_helper


model = onnx.load("/Users/FarhanHussain/Downloads/logs 2/model.onnx")

if_node = None
for node in model.graph.node:
    if node.op_type == "If":
        if_node = node
        break

input_A = if_node.input[0]
output_Z = if_node.output[0]

model.graph.node.remove(if_node)

from onnx import helper

# Add 'Equal' node
equal_output = input_A + "_equal"
equal_node = helper.make_node(
    "Equal",
    inputs=[input_A, "tensor_B"],
    outputs=[equal_output],
    name="Equal"
)

# Add 'Squeeze' node
squeeze_output = input_A + "_squeeze"
squeeze_node = helper.make_node(
    "Squeeze",
    inputs=[input_A],
    outputs=[squeeze_output],
    name="Squeeze",
    axes=[1]
)

# Add 'Identity' node
identity_output = input_A + "_identity"
identity_node = helper.make_node(
    "Identity",
    inputs=[input_A],
    outputs=[identity_output],
    name="Identity"
)

# Add 'Where' node
where_node = helper.make_node(
    "Where",
    inputs=[equal_output, squeeze_output, identity_output],
    outputs=[output_Z],
    name="Where"
)

model.graph.node.extend([equal_node, squeeze_node, identity_node, where_node])


tensor_B = np.array([1]).astype(np.float32)
tensor_B_initializer = numpy_helper.from_array(tensor_B, name="tensor_B")
model.graph.initializer.append(tensor_B_initializer)

onnx.save(model, "/Users/FarhanHussain/Downloads/logs 2/modified_model.onnx")


