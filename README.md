# Gator gang

Gator gang adds a co-presence multiplayer mode to [lil gator game](https://www.playtonicgames.com/game/lil-gator-game/) (PC), allowing you to enjoy the game in a more social way.
Each player retains their own progression, inventory, and story state. The goal is to enjoy the atmosphere together, explore side-by-side, and share moments.
Servers can be hosted privately using the executable from The project for the server is available at [lil-gator-game-mp-server](https://github.com/KooperL/lil-gator-game-mp-server/releases).

![Animated image](https://github.com/KooperL/lil-gator-game-mp/blob/Assets/animated1.gif)  ![Animated image](https://github.com/KooperL/lil-gator-game-mp/blob/Assets/animated2.gif)


## Features

 - Multiplayer: See one or more remote users moving through *your* world in real time
 - Join instantly: Host your own server from an open source project or use the free dedicated server
 - No Griefing: Your world state remains entirely under your control
 - Simple Setup: Quick connection process with minimal configuration
 - Password gated sessions: Opt to have a hidden multiplayer session with a password 


## Installation

This mod requires two files in order to run properly. `Assembly-CSharp.dll` and `lggmp_config.ini`.

> [!IMPORTANT]  
> Replacing the `Assembly-CSharp.dll` will overwrite any existing mods that use this file. Getting the vanilla file is always possible but back up this file if necessary. 

### Installing

The directory for the game can be found by right-clicking the game in your steam library, hovering over "Manage", and clicking "Browse local files".
In a ordinary windows environment, the path to the game folder is `C:\Program Files (x86)\Steam\steamapps\common\Lil Gator Game`.

Move the `Assembly-CSharp.dll` to `Lil Gator Game/Lil Gator Game_Data/Managed`. You should see a prompt to replace the file, accept it.

### Configuration

Create (or copy, modify) a file called `lggmp_config.ini` in `Lil Gator Game`. **Make sure it's not a** `.ini.txt` **file**.
This file should appear in the same directory as the `.exe`.
Use the following template:
 ```ini
session_key=
display_name=
server_host=
```
Where:
 - `session_key` is an identifier that will create a private session. The key should be the same for people you wish to play along with. The default key is `gatorverse`.
 - `display_name` is how you will apppear to others.
 - `server_host` is the URL host of the server you wish to connect to. The free dedicated server is `gvming.io:8000`.

**Copy this** to get started quickly: 
```ini
session_key=gatorverse
display_name=<YOUR USERNAME HERE>
server_host=gvming.io:8000
```

To know that a custom config was loaded correctly, check for injected text in the main menu

## Planned features (maybe)

 - Preserve connection across prologue <-> Act1 cutscene
 - Multiplayer UI elements on HUD
 - Show name flag in config
 - Ragdoll / animation state polish

## Accepted Limitations

 - In cases heavy server load, remote players may appear to teleport briefly
 - Actions taken by others do not affect your saves, quests, or resources.
 - Mod merging with other `Assembly-CSharp.dll` mods can be done by others interested. Asset mods will only work for remote players if the remote players have the modified files too.

## Technical notes

Check the logs in `%appdata%\..\LocalLow\MegaWobble\Lil Gator Game`

### Modified existing classes

 - `Game::Start`
   - Attach MultiplayPlayerManager game object to MultiplayerNetworkBootstrap
   - Add MultiplayerPlayerFrameStreamer game object to base game object
 - `Game::Update`
   - `Debug logs`
 - `Game::Awake`
   - Load config files
   - Call initConnection to begin ws
 - `MainMenuToGameplay::Start`
   - Inject main menu button
 - `Weapon::StartSwing`
   - Track attack swing
 - `Weapon::StopSwing`
   - Track attack swing
 - `PlayerItemManager`
   - Track attack swing

### Added classes

 - `MultiplayerCommunicationService`
 - `MultiplayerConfigLoader`
 - `MultiplayerInjectButtonToMainMenu`
 - `MultiplayerNetworkBootstrap`
 - `MultiplayerPlayerFrameStreamer`
 - `MultiplayerPlayerManager`
 - `MultiplayerJSONHelper`

