VAR weapon_main = "NON"
VAR weapon_secondary = "NON"

->main_weapon


=== main_weapon ===
CHOOSE YOUR WEAPONS
CHOOSE YOUR MAIN WEAPON
*Bow and Arrow
    ~weapon_main = "Bow And Arrow"
*Longsword
    ~weapon_main = "Longsword"
*Spear
    ~weapon_main = "Spear"

- ->secondary_weapon


=== secondary_weapon ===
CHOOSE YOUR SECONDARY WEAPON #inventory:{weapon_main}
*Dagger
    ~weapon_secondary = "Dagger"
*Throwing Knives
    ~weapon_secondary = "Throwing Knives"
*Small Axe
    ~weapon_secondary = "Small Axe"

- ->ready_check

=== ready_check ===
Are you ready? #inventory:{weapon_secondary}
*Yes
*No #scene:restart

- ->start_fight

=== start_fight ===
You've picked {weapon_main} as your main weapon, and {weapon_secondary} as your secondary weapon.


FIGHT

->END