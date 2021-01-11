# AutopilotUnity
## Introduction
This project is using Unity ML Agents package. It applies reinforcement learning techniques to train the kart and enable the kart to self-drive around the track.

## Environment
### The kart
To simulate model formula cars, the kart is designed to be rear wheel drive. It has four `WheelCollider` and each wheel works independently. The two front WheelColliders control turning. The center of mass of the kart is set lower to make it more stable (harder to flip). A downforce is added to the kart so that the kart can go faster at the corners.

![kart](https://github.com/RaytorHu/AutopilotUnity/blob/master/images/kart.png)

### The tracks
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

