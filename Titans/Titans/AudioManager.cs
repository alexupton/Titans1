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
            game = currentGame;
            engine = new AudioEngine("Content\\SFX\\SFX.xgs");
            soundBank = new SoundBank(engine, "Content\\SFX\\Sound Bank.xsb");
            waveBank = new WaveBank(engine, "Content\\SFX\\Wave Bank.xwb");
            fxCat = engine.GetCategory("SFX");
            fxCat.SetVolume(6.0f);


        }

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
            else
            {
                cue = soundBank.GetCue("EDefenderAttack");
            }

            cue.Play();
        }

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
            else if (deadUnit is Defender && !deadUnit.isPlayerUnit)
            {
                cue = soundBank.GetCue("EDefenderDie");
            }
            cue.Play();
        }

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
            else
            {
                cue = soundBank.GetCue("EDefenderMove");
            }

            cue.Play();
        }

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
            else
            {
                cue = soundBank.GetCue("EDefenderSelect");
            }

            cue.Play();
        }

        public void Update()
        {
            engine.Update();
        }

        public void PlayBuzzer()
        {
            fxCat.SetVolume(10f);
            cue = soundBank.GetCue("Buzzer");
            cue.Play();
            fxCat.SetVolume(6f);
        }

        public void setfxfvolume(float volume)
        {
            fxCat.SetVolume(volume);
        }

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
            else
            {
                cue = soundBank.GetCue("EDefenderPass");
            }

            cue.Play();
        }

        
        



    }
}
