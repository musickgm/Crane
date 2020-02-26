Geoff Musick
CPSC 6820
2/26/20
Assignment 4: The Crane: Using Foward Kinematics

FUNCTIONALITY:
The functionality of this project includes a player controlled crane. The player can use the mouse
to rotate the base joint (j1) using horizontal and vertical mouse input. Additionally, the player
can rotate the middle joint (j2) using keyboard input (sw or arrows). The theme of the crane game
involves a tooth fairy / dentist approach where you have to collect "teeth" and place them on a
pillow to collect money (your score) in a certain amount of time. The teeth have a "mouth" parent
that moves to make collection more challenging. 


IMPLEMENTATION:
For implementation, the controls center around a "CraneController" script that takes in player
input to be used for rotating joints on the crane. The main challenge for this type of controller
is constraining the joints so that they don't have "unnatural" rotations. For me this means keeping
the end of the crane from going below the ground and keeping joints from rotating backwards. To keep
the end from going underground, I used a "CraneEnd" script attached to a gameobject with a collider
to look for collisions on the ground or with the crane itself. If this happened, I prevented the
crane's joints from rotating in certain directions. I also used a clamp function to keep the joints
from rotating backwards. The other important implementation involves moving the collectable without
using parenting. For this I used Unity's Matrix4x4 to create matrices for each joint (rotation) and
appendage (length/translation). After that I used matrix multiplcation to extract/assign position
and rotation to the collectable while it was "attached" to the crane's end.
Other implementations involved simple scripts to handle moving the "mouths", picking up/dropping
"teeth", a game manager to handle the game's flow, and a collider script to destroy teeth that fall
off the map. Finally, I adapted a 3rd person spring camera follow script from the previous maze 
assignment. 


DESIGN CHOICES
The main design choices I made pertained to how the crane rotates and constraints on the crane's
movements. I think I did a decent job making the crane's movement feel natural; however, this 
type of controller (rotating different joints) is not a normal one, so I'm guessing that my
controller won't feel natural at first. I feel pretty good about preventing bad rotations with
my clamping and using a collider to keep the crane from going underground. However, I could 
perceive a user maybe being able to put the crane in an awkward position where they get stuck
because of this. I wasn't able to create this bug, but I could definitely see its possibility.
Other bugs that I "squashed" involve using a cool down time once a collectable is dropped so that
it's not instantaneously picked up. This is necessary since no button input is needed to pick an
item up (just collision) - I thought this feature would be useful since some camera angles make
it hard to see where the end of the crane is, so picking up items could be challenging without
"auto-picking up". Another bug I fixed is items being "auto dropped" into the pillow. I wanted
the player to have to press a button (input) in order to drop. So the collider for the collectables
is removed until the item is "dropped" by the player. 


SOURCES:
-Panorama dentist office image from: https://walkthru360.com/city-smiles-dental-office-360-google-maps-virtual-tour/
-Drill sfx taken from soundbible.com (public domain)
-CHaching sfx taken from soundboard.com (personal or commercial use)
