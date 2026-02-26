using UnityEngine;


namespace Assets.Scenes.Game2048.Scripts.Managers
{
    // -------------------------------------------------------------------------
    // Public Classes:
    // ---------------
    //   SoundAudioClip
    //   SoundManager
    // -------------------------------------------------------------------------

    #region .  Public Classes  .

    [System.Serializable]
    public class SoundAudioClip
    {
        public SoundManager.Sounds sound;
        public AudioClip           audioClip;

    }   // class SoundAudioClip

    #endregion



    public class SoundManager : Singleton<SoundManager>
    {
        // ---------------------------------------------------------------------
        // Public Enumerations:
        // --------------------
        //   Sounds
        // ---------------------------------------------------------------------

        #region .  Public Enumerations  .

        public enum Sounds
        {
            Game_Over_1,
            Game_Over_2,
            Game_Over_3,
            Lose_1,
            Swish_1,
            Swish_2,
            Swish_3,
            Swish_4,
            Win_Game_1,
            Win_Game_2,
            Win_Game_3
        }

        #endregion



        // ---------------------------------------------------------------------
        // Public Variables:
        // -----------------
        //   Audio
        //   SoundAudioClips
        // ---------------------------------------------------------------------

        #region .  Public Variables  .

        public AudioSource      Audio;
        public SoundAudioClip[] SoundAudioClips;

        #endregion



        // ---------------------------------------------------------------------
        // Public Methods:
        // ---------------
        //   PlaySound()
        //   PlayRandomSound()
        // ---------------------------------------------------------------------

        #region .  PlaySound()  .
        // ---------------------------------------------------------------------
        //  Method.......:  PlaySound()
        //  Description..:  
        //  Parameters...:  None
        //  Returns......:  Nothing
        // ---------------------------------------------------------------------
        public void PlaySound(Sounds sound)
        {
            Audio.PlayOneShot(this.GetAudioClip(sound));

        }   // PlaySound()
        #endregion


        #region .  PlayRandomSound()  .
        // ---------------------------------------------------------------------
        //  Method.......:  PlayRandomSound()
        //  Description..:  
        //  Parameters...:  None
        //  Returns......:  Nothing
        // ---------------------------------------------------------------------
        public void PlayRandomSound(string name, int count)
        {
            name        += Random.Range(1, count).ToString();
            Sounds sound = (Sounds)System.Enum.Parse(typeof(Sounds), name);

            Audio.PlayOneShot(this.GetAudioClip(sound));

        }   // PlayRandomSound()
        #endregion



        // ---------------------------------------------------------------------
        // Private Methods:
        // ----------------
        //  GetAudioClip()
        // ---------------------------------------------------------------------

        #region .  GetAudioClip()  .
        // ---------------------------------------------------------------------
        //  Method.......:  GetAudioClip()
        //  Description..:  
        //  Parameters...:  None
        //  Returns......:  Nothing
        // ---------------------------------------------------------------------
        private AudioClip GetAudioClip(Sounds sound)
        {
            foreach (SoundAudioClip soundAudioClip in SoundAudioClips)
            {
                if (soundAudioClip.sound == sound)
                {
                    return soundAudioClip.audioClip;
                }
            }

            Debug.Log($"Sound {sound} is not found!");
            return null;

        }   // GetAudioClip()
        #endregion


    }   // class SoundManager

}   // namespace Assets.Scenes.Game2048.Scripts
