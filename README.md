# PAV_Salad_Chef_Game
 Cooking Game
 
Goal:

To create a “programmer-art” version of a salad chef simulation in Unity.

Features:
• Static top-down view on the level or camera that pans in/out depending on the distance between players in the level.
• “Couch Co-Op”: Two players play on one machine using different keys to control two characters
o Make the characters distinguishable somehow (colour, nameplate, ...) 
o Optimally with both players using the keyboard, no gamepad
• Implementation of different vegetables

• Item placing by player:
o Vegetables can be picked up from both sides of the room  o A picked vegetable can be placed on the chopping board. It takes some time
to chop the vegetable. Meanwhile, the player cannot move. o A Player can carry two vegetables at a time. The item, which was picked up first, will also be placed on the chopping board first. There should be an indicator for items that are currently picked up (e.g. HUD draw). o Once the item is chopped, other items can be placed on the same chopping board
to create different combinations. o A combination can be picked up from the chopping board again and placed on a table for a customer. The player can also throw the salad into
the trashcan. This will result in some minus points. o The player can place one vegetable on the plate next to the chopping board to
be able to pick up a combination from the chopping board.
• Customer interaction:
o The score count increases when the player gives the right combination to a
customer
o A customer waits for a defined period before leaving dissatisfied. The period
depends on the number of ingredients. o Delivering in time adds points to the player’s score count. If the time runs
out, the customer will leave which will result in minus points for both players. o Incorrect combinations will make the customer angry. o Angry customer: The waiting time for the customer decreases faster. If he does not get fed correctly within that time, the minus points are doubled and given solely to the player that delivered the wrong meal. If both players delivered incorrectly, both are penalized. o If a salad is given to a customer before 70% of the waiting time, the customer will award the player with a pickup. The pickup will be spawned at a random free spot in the level and can only be picked up by the player that satisfied the customer.
• Implementation of different pickups:
o Speed: Increases the player’s movement speed for a period of time o Time: Increases the overall time that the player has left o Score: Adds some points to the player score count
• The game will end if the timer of both players ran out.
• Winning message gets displayed (player with higher score count)
• A high score list will be displayed with the top 10 scores.
• Reset option on screen: Starts another round.
