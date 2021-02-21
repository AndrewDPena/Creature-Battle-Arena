# CREATURE BATTLE ARENA

A game design proof of concept

Version 0.4

# TABLE OF CONTENTS
* [About the Project](#1-about-the-project)
* [Technologies](#2-technologies)
* [Gameplay](#3-gameplay)
* [Specifications](#4-specifications)
* [User Interface](#5-user-interface)

## 1. ABOUT THE PROJECT
Creature Battle Arena is an isometric 2.5D combat system designed to be an alternative to menu-based combat found in certain JRPG franchises.
This menu-based design is static and outdated, and has not grown and innovated along with the rest of the industry. 

C. B. A. will use real-time combat, including player-driven creature movement and button-based combat.  
It is not intended to be a game concept in and of itself; rather, it is intended to be attached to an existing concept as a different form of 
gameplay which is more entertaining and more modern.

## 2. TECHNOLOGIES
This project will be designed using:
* Unity: 2019.4.19f1
* C\#

## 3. GAMEPLAY
### 3.1 CONCEPT
Creature Battle Arena is designed to be a real-time, action-oriented combat system designed around a game which uses collectable creatures
as the basis for its gameplay system. Instead of menu-based combat and limited player input, the player will actively control the movement and
abilities of their respective creature through specified button inputs.

### 3.2 PLAYERS
This project is limiting scope to one human player, and one AI-controlled player. The human player will have full control over the movement and 
abilities of their creature/party. The AI will control an opposing creature, which can interact with the player-controlled creature in
glorious combat. 
#### 3.2.1 TWO-PLAYER MODE
Future work may implement a two-player mode.

### 3.3 SETTING
The game will take place entirely on a single arena moderate size, ie., only about 20-40 times the space that a single creature takes up. The map
should have various obstacles and environment objects to interact with. This includes hiding behind trees, climbing/jumping on top of rocks, or 
being slowed by certain terrains like sand.
#### 3.3.1 INITIAL MAP
For ease of design, and in order to implement a variet of obstacles, the first map will be a meadow. This map will include trees, rocks, sand.
If there is additional time, the terrain can also be made uneven, ie., a hill can be added into the map.
#### 3.3.2 ADDITIONAL MAPS
Additional maps may be added by future development work.

### 3.4 INTERACTION
The player will be able to control their creature in a 1-vs-1 fight against another creature. With direct control over the creature, players
will be able to strategically use terrain and creature placement, and carefully aim and line up their abilities. Players may be able to dodge
away from attacks, or jump over them, or hide from them behind map obstacles. The player will also have a reserve team of creatures to swap in 
out of combat with.
#### 3.4.1 INTERACTABLE MAPS
Future development may include direct manipulation of the map (ie., burning a bush, cutting down a tree, breaking a rock), but that is beyond
the scope of current development.

### 3.5 OBJECTIVE
The player will aim to deal enough damage to the opponent creature(s) to knock them out, without losing all of their creatures in the process.
By swapping creatures, the player can manage resources, and potentially counter the current opponent creature in order to gain an extra advantage.

### 3.6 CREATURES
The creatures in this game will be placeholder creatures in order to test the concept. The system will be designed to support a variety of
imaginative types and designs for creatures. We will use basic polygons for now.
#### 3.6.1 TYPES
In order to fit with the pre-existing genre that Creature Battle Arena is designed to inherit, the game should support several types of
creatures. These creature types (and corresponding attack types) will be part of an intricate rock-paper-scissors style of combat wherein
certain types of creatures are naturally more effective than others.
#### 3.6.2 NUMBER OF CREATURES
In order for Creature Battle Arena to successfully show off the concept, it is hoped that anywhere from 3-6 creatures can be implemented by
the end of the project period. This will allow for both the player and the AI opponent to have unique teams of up to 3 creatures each. The 
minimum required will be 2 creatures, which will allow for the player and the AI opponent to each have one unique creature.
#### 3.6.3 CREATURE ATTACKS
For now, it will be enough for creatures to simply have differently-shaped attacks, in order to show off the advantages of real-time
movement in combat. This could be a cone/breath attack, or a targeted ranged attack, etc. If development time permits, then each attack can
be given "typing", such as changing the breath to fire breath, or the ranged attack to a lightning bolt. This will allow an exploration of how
type advantage will be used in the game.
#### 3.6.4 CREATURE ABILITIES
All creatures will have access to basic movement, a jump, and a dodge. 2-4 attacks should be easily useable with minimal button pressing. If
time permits, a "sprint" feature can be considered. For the minimum product, creature abiltiies will be equal across all implemented creatures.
However, if time permits, creatures should be designed in such a way that they can be given tailored ability values. Lighter creatures may be
faster or jump higher, but easier to knock around. Heavier creatures may not be able to jump as high, but may have an easier time breaking
a rock with their body. This distinction will help add flavor to each creature, and further show off the importance of a diverse team.

### 3.7 PLAYER CHARACTER
At this time, no player avatar is planned. While making design decisions, it may be wise to remember that such a player avatar would likely
exist in a fully fledged game. Certain things like a player name, or a visible avatar on the edge of the arena, could be implemented at a later
date.

## 4. SPECIFICATIONS
TBD


## 5. USER INTERFACE
TBD