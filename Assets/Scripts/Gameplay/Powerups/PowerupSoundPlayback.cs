using System;
using System.Collections;
using UnityEngine;

namespace Gameplay.Powerups
{
    
    [RequireComponent(typeof(AudioSource))]
    public class PowerupSoundPlayback : MonoBehaviour
    {
        [SerializeField] private AudioClip jumpFx;
        [SerializeField] private AudioClip speedFx;

        private PowerupTypes Type;
    
        #region converter
        AudioClip GetSoundFromType(PowerupTypes type)
        {
            switch (type)
            {
                case PowerupTypes.JUMP_BOOST:
                    return jumpFx;
                case PowerupTypes.SPEED:
                    return speedFx;
            }

            throw new NotImplementedException("There is no sound for this yet lmao");
        }
        #endregion

        public void SetType(PowerupTypes aType)
        {
            Type = aType;
        }
    
        // Start is called before the first frame update
        void Start()
        {
            AudioClip sfx = GetSoundFromType(Type);
            var source = GetComponent<AudioSource>();
            source.clip = sfx;
            source.Play();
            StartCoroutine(DestoryAfter(sfx.length));
        }

        IEnumerator DestoryAfter(float time)
        {
            yield return new WaitForSeconds(time);
            Destroy(gameObject);
        }
    }
}
