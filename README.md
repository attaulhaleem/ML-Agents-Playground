# ML Agents Playground

A Unity ML-Agents lab for my HCI course: a ball that learns to roll to a target, trained with reinforcement learning. Based on Unity's [RollerBall tutorial](https://docs.unity3d.com/Packages/com.unity.ml-agents@4.0/manual/Learning-Environment-Create-New.html).

The full walkthrough is in the blog post [ML Agents in Unity](https://attaul.com/en/blog/human-computer-interaction/week-4/ml-agents-in-unity).

![The trained blue ball rolling straight to the green cube, again and again as the cube moves to new random spots](docs/trained-ball.gif)

## Requirements

- [Unity 6.5](https://unity.com/download) (6000.5.2f1)
- [ML-Agents 4.1](https://github.com/Unity-Technologies/ml-agents) (`com.unity.ml-agents`, installed through the Package Manager)
- [Python 3.10](https://www.python.org/downloads/release/python-31012/) (3.10.1 to 3.10.12) with the `mlagents` package, only needed for training

## Project Layout

```text
ML Agents Playground
├── Assets
│   ├── Materials/              agent, target, floor and wall materials
│   ├── Models
│   │   └── RollerBall.onnx     trained model
│   ├── Scenes
│   │   └── SampleScene.unity   training area: floor, walls, agent and target
│   └── Scripts
│       └── RollerAgent.cs      observations, actions and reward
├── docs/                       README media
└── rollerball_config.yaml      PPO training settings
```

## Running the Trained Model

Open the project in Unity Hub, open `SampleScene` and press Play. The agent uses `RollerBall.onnx` and chases the green cube on its own.

To drive the ball yourself with `W`, `A`, `S` and `D`, set _Behavior Type_ on the agent's _Behavior Parameters_ to _Heuristic Only_.

## Training

```bash
conda create -n mlagents python=3.10.12
conda activate mlagents
git clone https://github.com/Unity-Technologies/ml-agents.git
python -m pip install ./ml-agents/ml-agents-envs
python -m pip install ./ml-agents/ml-agents
```

From this project's folder, start the trainer, then press Play in Unity when it asks you to:

```bash
mlagents-learn rollerball_config.yaml --run-id=RollerBall
```

The model is saved to `results/RollerBall/RollerBall.onnx`, which isn't tracked in Git. Copy it into `Assets/Models` to use it. Training statistics can be viewed with `tensorboard --logdir results`.
