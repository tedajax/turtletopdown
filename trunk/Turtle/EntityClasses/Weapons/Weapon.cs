using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Turtle.Weapons
{
    enum WeaponCode 
    {
        Nothing,
        Recoil
    }
    abstract class Weapon
    {
        /// <summary>
        /// Used when the player is selecting weapons in the customization screen
        /// </summary>
        protected String name;
        protected String description;
        public String Name
        {
            get { return this.name; }
        }
        public String Description
        {
            get { return this.description; }
        }
        /// <summary>
        /// Update loop is called as long as the Player that owns this weapon is active. The method allows for each individual weapon to implement as it pleases
        /// </summary>
        /// <param name="inFocus">If inFocus is true, the player has selected this weapon as the current weapon to use</param>
        /// <param name="isActivated">If isActivated is true, the player has pressed the key/button that corresponds to "fire"</param>
        /// <param name="Manager">Any projectiles instantiated by the Weapon should be placed inside this Manager to be handled and drawn</param>
        /// <returns>Returns a WeaponCode specifying a reaction for the playerShip</returns>
        public abstract WeaponCode Update(bool inFocus, bool isActivated, ProjectileManager Manager);
        public abstract Projectile returnProjectile();
        
    }
}
