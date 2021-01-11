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

