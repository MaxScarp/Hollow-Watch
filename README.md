# Hollow Watch

A stealth prototype in Unity 6. You are hunted by a creature that cannot see you but hears everything: footsteps, actions and the objects you throw to distract it.

## How the creature works

- The player emits sounds through `NoiseEmitter`: footsteps while moving, actions, decoys. Each sound is published on a `GameEventBus` as a `SoundStimulusEvent` with position, intensity and source.
- `CreatureController` listens to those events, keeps only the sounds within hearing range and passes them to the state machine. The creature moves with a NavMeshAgent and its speed follows its state.
- `CreatureStateMachine` has three states: Patrol (loops through waypoints), Alert (goes to the sound origin, with a timer that resets on every new sound) and Chase (in progress). Every transition publishes an `AIStateChangedEvent`, which the HUD uses to show "!" and "!!" over the creature.

## Code layout

- `Assets/_Core/Scripts/_Main`: the state machine, the context, the event bus and the events. Plain C#, no UnityEngine reference, so it can be tested outside Unity.
- `Assets/_Core/Scripts/_Unity`: MonoBehaviours for player, creature, input and UI, plus the ScriptableObject configs for player and creature.
- `Assets/_Core/Scenes/M1Scene`: the test scene with a NavMesh floor.

## Status

Prototype. Movement, sound emission, hearing and the Patrol/Alert loop work; Chase is still a stub. Visuals are placeholders.

Unity 6000.3, Universal Render Pipeline, new Input System.
