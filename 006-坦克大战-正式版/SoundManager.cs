using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using _006_坦克大战_正式版.Properties;

namespace _006_坦克大战_正式版
{
    class SoundManager
    {
        //private static SoundPlayer startPlayer = new SoundPlayer();
        //private static SoundPlayer addPlayer = new SoundPlayer();
        //private static SoundPlayer blastPlayer = new SoundPlayer();
        //private static SoundPlayer firePlayer = new SoundPlayer();
        //private static SoundPlayer hitPlayer = new SoundPlayer();

        public static void InitSound()
        {
            //startPlayer.Stream = Resources.start;
            //addPlayer.Stream = Resources.add;
            //blastPlayer.Stream = Resources.blast;
            //firePlayer.Stream = Resources.fire;
            //hitPlayer.Stream = Resources.hit;
        }
        public static void PlayStart()
        {
            SoundPlayer startPlayer = new SoundPlayer(Resources.start);
            //startPlayer.Stream = Resources.start;
            startPlayer.Play();
        }
        public static void PlayAdd()
        {
            SoundPlayer addPlayer = new SoundPlayer(Resources.add);
            //addPlayer.Stream = Resources.add;
            addPlayer.Play();
        }
        public static void PlayBlast()
        {
            SoundPlayer blastPlayer = new SoundPlayer(Resources.blast);
            //blastPlayer.Stream = Resources.blast;
            blastPlayer.Play();
        }
        public static void PlayFire()
        {
            SoundPlayer firePlayer = new SoundPlayer(Resources.fire);
            //firePlayer.Stream = Resources.fire;
            firePlayer.Play();
        }
        public static void PlayHit()
        {
            SoundPlayer hitPlayer = new SoundPlayer(Resources.hit);
            //hitPlayer.Stream = Resources.hit;
            hitPlayer.Play();
        }
    }
}
