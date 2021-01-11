# AutopilotUnity
## Introduction
This project is using Unity ML Agents package. It applies reinforcement learning techniques to train the kart and enable the kart to self-drive around the track.

### DEMO
[![DEMO_Training](https://github.com/RaytorHu/AutopilotUnity/blob/master/images/DEMO_Training.png)](https://youtu.be/QRNAbKOJYkk)

## Environment
### The Kart
To simulate model formula cars, the kart is designed to be rear wheel drive. It has four `WheelCollider` and each wheel works independently. The two front WheelColliders control turning. The center of mass of the kart is set lower to make it more stable (harder to flip). A downforce is added to the kart so that the kart can go faster at the corners.

![kart](https://github.com/RaytorHu/AutopilotUnity/blob/master/images/kart.png)

### The Tracks
Two tracks are built to train and test the agent.

![track1](https://github.com/RaytorHu/AutopilotUnity/blob/master/images/track1.png)
![track2](https://github.com/RaytorHu/AutopilotUnity/blob/master/images/track2.png)

## Reinforcement Learning
The project uses Unity ML Agents to train an auto-pilot driver which can drive the car itself. 

### Action Space
On each step, the agent chooses two inputs:
* Steering angle (continuous, ranging from -20 to 20 )
* Acceleration and Brake (continuous, ranging from -1 to 1)

### Reward Function
Some checkpoints are set along the track. The agent will get some rewards once it reaches the checkpoints. The agent will get a negative reward every frame. The negative reward encourages the agent to go faster.

### State / Observation
The state of the agent is defined by the observations from `ray perception` sensors. More sensors are put in the front of the car so that it can get more information from the front.

![perception](https://github.com/RaytorHu/AutopilotUnity/blob/master/images/perception.png)

## Experiment
To explore what elements impact the training process, the following experiments are done.

### Train Multiple Agents Simultaneously
First train the network with only one agent. Then add 20 more agents and train them simultaneously. Compared the cumulative reward via time and the result is shown below:

![cumulative_reward1](https://github.com/RaytorHu/AutopilotUnity/blob/master/images/cumulative_reward1.png)

A larger cumulative reward means the agent is closer to the final goal (finish a lap). As shown on the graph, training multiple agents simultaneously can significantly increase the learning speed which saves a large amount of training time.

### Remove Distance and Direction Information to the Next Checkpoint
Besides the observations from ray perception sensors, the distance and direction information to the next checkpoint are also provided to the car. This information is defined as a 3D vector:

`vector = nextCheckPoint.position - car.position`

![cumulative_reward2](https://github.com/RaytorHu/AutopilotUnity/blob/master/images/cumulative_reward2.png)

Without the vector, the agents have a hard time going through the first corner. The vector is leading the agent going in the corrected direction. In real life, this vector can be provided by GPS. To some extent, this proves that GPS is playing a key role in autopilot.

### Compare Different Reward Functions
Do an experiment with different reward functions. For example, the later checkpoints provide more rewards. There are no obvious changes with different reward functions. The agent tends to achieve as many rewards as it can, no matter how large the rewards are.

### Add More Checkpoints
Adding more checkpoints along the track can increase the learning speed. Because the agents can get rewards more frequently. Based on the feedback, the agents are easier to know whether they are doing correctly or not.

A downside is observed of adding too many checkpoints. When an agent hits the wall and moved backward, it accidentally touches the next checkpoint. For a long time, the agents were trying to go through that corner by going backward, which is a mistake.

## Validation
Train the agents by letting them go clockwise. Based on the experiment above, pick the model which is trained by multiple agents, with the information of distance and direction to the next checkpoint. Each checkpoint has the same reward. 

Test the model by letting the agent go anticlockwise. In this case, the track is completely new to the agent. And, the agent can successfully complete a lap.

[![DEMO_Validation](https://github.com/RaytorHu/AutopilotUnity/blob/master/images/DEMO_Validation.png)](https://youtu.be/dke58wgUoqI)

## Reference