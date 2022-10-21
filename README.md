# Quest-System (Work in progress)

This project provides a module dedicated to storing and managing quests. This design lends itself to a few useful properties, which led to the motivation behind completing it in the first place.

## Why use this module?

1. Designed with easability from a development perspective in mind. 
    - Your game can exist completely seperately from the quest system. Game objects call simple 'events' to the quest manager, quests are updated accordingly.
    - Flexible enough to be used in any use-case. Main quests, subquests, daily/weekly challenges. You can build any game around this quest manager.
    - Unit tests with Unity's testing framework.
2. Hierarchical structure.
    - The philosophy of using this manager is that each quest will be a subquest of the "game", which is the root level quest (to beat the game). 
    - Quests having children allows for a tree-like structure that has some useful properties.
      - Enables developers to get as creative with quests as they want.
        - Quests can have subquests upon subquests (upon subquests).
      - Updates to quests further down the tree cascade upwards to parent quests in an efficient manner. More detail further down. 
3. Quest Manager functionality.
    - Extra structures allow new quests to be added efficiently, as well as referencing quests and updating quest markers. **No O(N) operations where N is the number of quests / objectives in the game.** 
    
---

## Overview

### How are these quests defined?

Loosely: A quest is an object that keeps track of whether or not a certain task, or collection of tasks are complete.

Practically we can use this definition to determine what the base-case level quests would be. A task can be: independant, a parent of other quests that can be done in any order, or a parent of other quests that must be done in a specific order.

1. Objective Quests. Quest completed if all objectives are completed. Objectives can be any event fired by your game.
2. Multi-subquest Quests. Quest is completed if children are completed. Allows children to be completed in any order. 
3. Sequence Quests. Quest is completed if children are completed. Children must be completed in a specific order.

Where are fetch quests? Waypoints? Delivery quests? Kill quests? Story quests? All of them can be represented by these three very easily. 

### Example:

![example](https://user-images.githubusercontent.com/33844493/197101396-adb146ce-595f-4f30-bcd4-524d92a5ce0c.jpg)

The main game is a multiquest with two subquests, the main story and side quests. The game is complete when its children are complete.
The main story must be completed in a specific order, and so quests will not be available until then.
Side quests can be completed in any order the player wishes. However, certain quests may require conditions to be met first, and so they would be part of a larger sequence quest within the structure.

### How does my game update existing quests?

To make this as generic and useful as possible, the quest system will sit on top of any game that uses it. A single QuestManager object will keep track of the state of quests, objectives, and manage interfacing between the game and data structure. 

**What developers need to do:**
Game objects that update quests can import the package needed to do so [will update name here]. To update a quest, a single function is called `markEvent()`. The intention is that any event that can be tracked, will always mark (mock emit) an event. 

If no quests are listening to it, nothing will happen. This check is done in O(1) time.  
If quests are listening to an emitted event, the quest manage will trigger an update, and each quest will be updated accordingly.

**Example emits:**

Quest: Jump 10 times.
Player: markEvent("player_jump"). Triggered every time the player jumps.

Quest: Pick up 10 flowers.
Player: markEvent(itemPickedUp, ChangeCountInInventory). Triggered whenever the player picks up an item (event name changes based on item grabbed)

Quest: Kill 10 Orcs.
Orc: markEvent("orc_killed"). Triggered every time an orc's health reaches 0.

Quest: Walk to X location.
boxTrigger: markEvent("entered_X");

### Why is this useful?

If it isn't clear, entire quests listening to the same events can all be updated with single event calls. No need for referencing quests or updating anything by hand. Your game can exist as it is, and with single method calls update progress accordingly.

### More to come...

...

---
## Quest Structure

![QuestDataStructure](https://user-images.githubusercontent.com/33844493/197097237-60af9b28-019e-43e9-9cc5-ee24e23753dd.jpg)
