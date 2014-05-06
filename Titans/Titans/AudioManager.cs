using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Audio;

namespace Titans
{
    public class AudioManager
    {
        public Game1 game;
        AudioEngine engine;
        SoundBank soundBank;
        WaveBank waveBank;
        Cue cue;
        AudioCategory fxCat;

        public AudioManager(Game1 currentGame)
        {
            //Set up respective sound engines for different types of sounds; music and SFX
            game = currentGame;
            engine = new AudioEngine("Content\\SFX\\SFX.xgs");
            soundBank = new SoundBank(engine, "Content\\SFX\\Sound Bank.xsb");
            waveBank = new WaveBank(engine, "Content\\SFX\\Wave Bank.xwb");
            fxCat = engine.GetCategory("SFX");
            fxCat.SetVolume(10f);


        }

	//Determines what sound to play when a friendly or enemy unit is attacking
        public void PlayAttackSound(Unit attacker)
        {
            if (attacker is Soldier && attacker.isPlayerUnit)
            {
                cue = soundBank.GetCue("SoldierAttack");
            }
            else if (attacker is Soldier && !attacker.isPlayerUnit)
            {
                cue = soundBank.GetCue("ESoldierAttack");
            }
            else if (attacker is Ranger && attacker.isPlayerUnit)
            {
                cue = soundBank.GetCue("RangerAttack");
            }
            else if (attacker is Ranger && !attacker.isPlayerUnit)
            {
                cue = soundBank.GetCue("ERangerAttack");
            }
            else if (attacker is Defender && attacker.isPlayerUnit)
            {
                cue = soundBank.GetCue("DefenderAttack");
            }
            else if (attacker is Cavalry && !attacker.isPlayerUnit)
            {
                cue = soundBank.GetCue("ESpearmanAttack");
            }
            else if (attacker is Cavalry && attacker.isPlayerUnit)
            {
                cue = soundBank.GetCue("SpearmanAttack");
            }
            else if (attacker is Mage && !attacker.isPlayerUnit)
            {
                cue = soundBank.GetCue("EMageAttack");
            }
            else if (attacker is Mage && attacker.isPlayerUnit)
            {
                cue = soundBank.GetCue("MageAttack");
            }
            else if (attacker is Artillery && !attacker.isPlayerUnit)
            {
                cue = soundBank.GetCue("EArtilleryAttack");
            }
            else if (attacker is Artillery && attacker.isPlayerUnit)
            {
                cue = soundBank.GetCue("ArtilleryAttack");
            }
            else
            {
                cue = soundBank.GetCue("EDefenderAttack");
            }

            cue.Play();
        }

	//Determines what sound to play when a friendly or enemy unit is dead
        public void PlayDieSound(Unit deadUnit)
        {
            if (deadUnit is Soldier && deadUnit.isPlayerUnit)
            {
                cue = soundBank.GetCue("SoldierDie");
            }
            else if (deadUnit is Soldier && !deadUnit.isPlayerUnit)
            {
                cue = soundBank.GetCue("ESoldierDie");
            }
            else if (deadUnit is Ranger && deadUnit.isPlayerUnit)
            {
                cue = soundBank.GetCue("RangerDie");
            }
            else if (deadUnit is Ranger && !deadUnit.isPlayerUnit)
            {
                cue = soundBank.GetCue("ERangerDie");
            }
            else if (deadUnit is Defender && deadUnit.isPlayerUnit)
            {
                cue = soundBank.GetCue("DefenderDie");
            }
            else if (deadUnit is Cavalry && !deadUnit.isPlayerUnit)
            {
                cue = soundBank.GetCue("ESpearmanDie");
            }
            else if (deadUnit is Cavalry && deadUnit.isPlayerUnit)
            {
                cue = soundBank.GetCue("SpearmanDie");
            }
            else if (deadUnit is Mage && !deadUnit.isPlayerUnit)
            {
                cue = soundBank.GetCue("EMageDie");
            }
            else if (deadUnit is Mage && deadUnit.isPlayerUnit)
            {
                cue = soundBank.GetCue("MageDie");
            }
            else if (deadUnit is Artillery && !deadUnit.isPlayerUnit)
            {
                cue = soundBank.GetCue("EArtilleryDie");
            }
            else if (deadUnit is Artillery && deadUnit.isPlayerUnit)
            {
                cue = soundBank.GetCue("ArtilleryDie");
            }

            else
            {
                cue = soundBank.GetCue("EDefenderDie");
            }
            cue.Play();
        }

	//Determines what sound to play based on wether a friendly or enemy unit is moving
        public void PlayMoveSound(Unit mover)
        {
            if (mover is Soldier && mover.isPlayerUnit)
            {
                cue = soundBank.GetCue("SoldierMove");
            }
            else if (mover is Soldier && !mover.isPlayerUnit)
            {
                cue = soundBank.GetCue("ESoldierMove");
            }
            else if (mover is Ranger && mover.isPlayerUnit)
            {
                cue = soundBank.GetCue("RangerMove");
            }
            else if (mover is Ranger && !mover.isPlayerUnit)
            {
                cue = soundBank.GetCue("ERangerMove");
            }
            else if (mover is Defender && mover.isPlayerUnit)
            {
                cue = soundBank.GetCue("DefenderMove");
            }
            else if (mover is Cavalry && !mover.isPlayerUnit)
            {
                cue = soundBank.GetCue("ESpearmanMove");
            }
            else if (mover is Cavalry && mover.isPlayerUnit)
            {
                cue = soundBank.GetCue("SpearmanMove");
            }
            else if (mover is Mage && !mover.isPlayerUnit)
            {
                cue = soundBank.GetCue("EMageMove");
            }
            else if (mover is Mage && mover.isPlayerUnit)
            {
                cue = soundBank.GetCue("MageMove");
            }
            else if (mover is Artillery && !mover.isPlayerUnit)
            {
                cue = soundBank.GetCue("EArtilleryMove");
            }
            else if (mover is Artillery && mover.isPlayerUnit)
            {
                cue = soundBank.GetCue("ArtilleryMove");
            }
            else
            {
                cue = soundBank.GetCue("EDefenderMove");
            }

            cue.Play();
        }

	//Determines what sound to play on wether a friendly or enemy unit is active(at beginning of that units turn)
        public void PlaySelectSound(Unit selected)
        {
            if (selected is Soldier && selected.isPlayerUnit)
            {
                cue = soundBank.GetCue("SoldierSelect");
            }
            else if (selected is Soldier && !selected.isPlayerUnit)
            {
                cue = soundBank.GetCue("ESoldierSelect");
            }
            else if (selected is Ranger && selected.isPlayerUnit)
            {
                cue = soundBank.GetCue("RangerSelect");
            }
            else if (selected is Ranger && !selected.isPlayerUnit)
            {
                cue = soundBank.GetCue("ERangerSelect");
            }
            else if (selected is Defender && selected.isPlayerUnit)
            {
                cue = soundBank.GetCue("DefenderSelect");
            }
            else if (selected is Cavalry && !selected.isPlayerUnit)
            {
                cue = soundBank.GetCue("ESpearmanSelect");
            }
            else if (selected is Cavalry && selected.isPlayerUnit)
            {
                cue = soundBank.GetCue("SpearmanSelect");
            }
            else if (selected is Mage && !selected.isPlayerUnit)
            {
                cue = soundBank.GetCue("EMageSelect");
            }
            else if (selected is Mage && selected.isPlayerUnit)
            {
                cue = soundBank.GetCue("MageSelect");
            }
            else if (selected is Artillery && !selected.isPlayerUnit)
            {
                cue = soundBank.GetCue("EArtillerySelect");
            }
            else if (selected is Artillery && selected.isPlayerUnit)
            {
                cue = soundBank.GetCue("ArtillerySelect");
            }
            else
            {
                cue = soundBank.GetCue("EDefenderSelect");
            }

            cue.Play();
        }

	//Apply changes
        public void Update()
        {
            engine.Update();
        }

	//Play a buzzer sound when player clicks on something that doesn't do anything
        public void PlayBuzzer()
        {
            //fxCat.SetVolume(5f);
            cue = soundBank.GetCue("Buzzer");
            cue.Play();
            //fxCat.SetVolume(6f);
        }

	//Set volume based on currently selected volume
        public void setfxfvolume(float volume)
        {
            fxCat.SetVolume(volume);
            
        }

	//Determines what sound to make when an enemy or friendly unit passes
        public void PlayPassSound(Unit selected)
        {

            if (selected is Soldier && selected.isPlayerUnit)
            {
                cue = soundBank.GetCue("SoldierPass");
            }
            else if (selected is Soldier && !selected.isPlayerUnit)
            {
                cue = soundBank.GetCue("ESoldierPass");
            }
            else if (selected is Ranger && selected.isPlayerUnit)
            {
                cue = soundBank.GetCue("RangerPass");
            }
            else if (selected is Ranger && !selected.isPlayerUnit)
            {
                cue = soundBank.GetCue("ERangerPass");
            }
            else if (selected is Defender && selected.isPlayerUnit)
            {
                cue = soundBank.GetCue("DefenderPass");
            }
            else if(selected is Defender && !selected.isPlayerUnit)
            {
                cue = soundBank.GetCue("EDefenderPass");
            }
            else if (selected is Cavalry && !selected.isPlayerUnit)
            {
                cue = soundBank.GetCue("ESpearmanPass");
            }
            else if (selected is Cavalry && selected.isPlayerUnit)
            {
                cue = soundBank.GetCue("SpearmanPass");
            }
            else if (selected is Mage && !selected.isPlayerUnit)
            {
                cue = soundBank.GetCue("EMagePass");
            }
            else if (selected is Mage && selected.isPlayerUnit)
            {
                cue = soundBank.GetCue("MagePass");
            }
            else if (selected is Artillery && !selected.isPlayerUnit)
            {
                cue = soundBank.GetCue("EArtilleryPass");
            }
            else
            {
                cue = soundBank.GetCue("ArtilleryPass");
            }


            cue.Play();
        }

	//Determines what sound to make when an enemy or friendly unit defends
        public void PlayDefendSound(Unit selected)
        {
            if (selected is Soldier && selected.isPlayerUnit)
            {
                cue = soundBank.GetCue("SoldierDefend");
            }
            else if (selected is Soldier && !selected.isPlayerUnit)
            {
                cue = soundBank.GetCue("ESoldierDefend");
            }
            else if (selected is Ranger && selected.isPlayerUnit)
            {
                cue = soundBank.GetCue("RangerDefend");
            }
            else if (selected is Ranger && !selected.isPlayerUnit)
            {
                cue = soundBank.GetCue("ERangerDefend");
            }
            else if (selected is Defender && selected.isPlayerUnit)
            {
                cue = soundBank.GetCue("DefenderDefend");
            }
            else if (selected is Cavalry && !selected.isPlayerUnit)
            {
                cue = soundBank.GetCue("ESpearmanDefend");
            }
            else if (selected is Cavalry && selected.isPlayerUnit)
            {
                cue = soundBank.GetCue("SpearmanDefend");
            }
            else if (selected is Mage && !selected.isPlayerUnit)
            {
                cue = soundBank.GetCue("EMageDefend");
            }
            else if (selected is Artillery && selected.isPlayerUnit)
            {
                cue = soundBank.GetCue("ArtilleryDefend");
            }
            else if (selected is Mage && !selected.isPlayerUnit)
            {
                cue = soundBank.GetCue("EArtilleryDefend");
            }
            else
            {
                cue = soundBank.GetCue("MageDefend");
            }


            cue.Play();
        }

        public void PlaySpecialSound(Unit selected, int specialNumber)
        {
            if (selected.isPlayerUnit)
            {
                cue = soundBank.GetCue(selected.GetType() + "Special" + specialNumber.ToString());
            }
            else
            {
                cue = soundBank.GetCue("E" + selected.GetType() + "Special" + specialNumber.ToString());
            }
            cue.Play();
        }

        
        



    }
}
