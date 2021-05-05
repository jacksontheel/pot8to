# Pot8to
## A Chip-8 emulator
Pot8to aims to be an accurate emulation of a Chip-8, to provide as authentic an experience as possible. Like most Chip-8 emulators, Pot8to was written as an introduction to emulation, a stepping stone to emulating more complex hardware. Pot8to was written in C# using the Monogame framework. The emulation itself was handled [here](https://github.com/jacksontheel/ChipSharp8) in the ChipSharp8 repository, while this repository handles things like the display and input.

## Getting Started

### Prerequisites
Download the .NET SDK from [here](https://dotnet.microsoft.com/download) to build the source. .NET 5 is used for this emulator.

### Installation
To clone the repository and its submodule, run
```
`$ git clone --recurse-submodules https://github.com/jacksontheel/pot8to.git
````
in a command line with git installed.

After cloning the repo, in the command line of your choice `cd` to the Pot8to working directory and enter the command `dotnet build`. This will create the executable file in the Pot8to/bin/Debug/net5 directory.

### Games and programs
Pot8to supports the vast majority of games and programs written for the Chip-8. These are not provided in the repository, but should be trivial to find.

## Usage
``> Pot8to.exe bin_file_path [t]``

The filepath to the Chip-8 program or game may be relative to Pot8to.exe or absolute. The t argument is used when running a Chip-8 file that relies on typing for input.
* With the `t` argument, a single input will only be sent to the Chip-8 when a key is released.
* Without the `t` argument, holding a key's input will continue to be sent to the Chip-8 for as long as it is held down.
