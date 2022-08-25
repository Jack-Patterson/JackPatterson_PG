# Games Development Project - Resource Collection Game

This project was created using *Unity* for my **Games Development Project** done during the second semester of second year in college. The aim with this project was to create a functional game essentially all on our own without the assistance of the lecturer to the greatest extent possible. The game I chose was originally a building game but I changed it to more of a resource collection game for practicality due to time constraints. The game relied heavily on UI to control the characters.

## Project Structure
### Managers
The project made heavy use of *Manager*s to make the game function as intended. All *Manager*s are **singletons** which means there can only be one instance of each *Manager* within a scene of a project. Each *Manager* had a defined area which covers different tasks.

 - **Manager** - The global *Manager* is an exception to the rule and consists of general purpose things which do not fall under the coverage of a clearer defined *Manager*. It also includes handling for basic IO which reads from a file. Each AI character has a gender and the Manager assigns it a name depending on its gender. It also is where information such as the count of players is stored in case a method needs it.
 - **BuildManager** - A remenent of the previously mentioned building system. While it technically works it is inefficient and not recommended to be viewed.
 - **QuestsManager** - It handles *Quests* and assigning them to the player in return for a reward.
 - **ResourceManager** - Handles everything to do with game resources and handles placing trees and rocks which can be mined for resources once the game is started.

### User Interface
The UI is used to show different information of key facts of the game.

 - **Character** - Shows information about their Character such as their job, name and what they are currently doing.
 - **Game Menu** - Is the pause menu which allows you to exit the game.
 - **Quests** - Place where the games quests are physically shown to the player.
 - **Resource** - Global counter of all the resources acquired by the player.

### Interaction
Interaction deals with all objects which the player interacts with in some way. All interactable objects implement the interface *IInteractable* or *IQuestable* to allow them to communicate with the player effectively.

 - **HarvestableObject** - Script places on all objects which spawn in the world with a resource. The player seeks these out in return for a resource. The script will reset itself if depleted after a certain amount of time.
 - **StorageItem** - Is an item resources can be stored in. Once the player has mines all they can carry they'll move it to a storage to be kept safely.
 - **Item** - Only one of two exceptions to the interface rule and that is because it is the base object each resource is.
 - **Quests** - Other exception to the interface rule and is the base object each individual *Quest* is built upon.

### AI
AI deals with any character which is capable of moving and performing actions without the direct input of the player. Each of them implement the *ControlScript*  which contains the controls for the AI.

 - **CharacterControl** - Handles how the player moves and uses a state machine to perform a set number of actions. Each character also has a gender and will be assigned a name.
 - **ForestCreature** - Is a creature such as a fox or rabbit and uses a state machine to perform a set number of actions.