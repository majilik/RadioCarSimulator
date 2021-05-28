# Radio Car Simulator

## Assumptions
* The different cars have different speeds. As such, cars can be made with speed as an abstraction.
* The size of the room is input as: `<width> <length>`
* The start position is input as: `<x> <y>`

## Design
The simulator is responsible for gathering user input, 
converting it to commands and invoking said commands to simulate the car driving in the room.

The command pattern is used for encapsulating the commands that the simulator can invoke.

## Target Platform
Target Platform is Windows