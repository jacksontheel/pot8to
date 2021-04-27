# Pot8to
## A Chip-8 emulator

This repository consists of two projects: Chip8 and Pot8to. In short, Chip8 is an object that represents a Chip-8 virtual machine, while Pot8to creates an executable file for running
Chip-8 files, using the aforementioned Chip8 object.


### Chip8
Chip8 is a simple implementation of a Chip-8 which has public methods for loading a program, and running a single 
cycle of the emulated-cpu. The Chip8 object's "display" is a boolean array with a size of [64 * 32], the size of the original Chip-8. This display can be publically accessed.
Chip8 is capable of being used completely independently of Pot8to; if you'd like to create a project of your own that requires an implementation of a Chip-8 in the form of a
C# object, feel free to use this.

### Pot8to
The other project in this repository is Pot8to, which uses a Chip8 object and the Monogame framework to create an authentic Chip-8 experience. At the point of writing this, input
is not yet supported, but is on the way.
