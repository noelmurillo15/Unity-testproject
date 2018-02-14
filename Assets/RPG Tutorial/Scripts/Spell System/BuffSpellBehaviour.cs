﻿// Allan Murillo : Unity RPG Core Test Project
using UnityEngine;


namespace RPG {

    public class BuffSpellBehaviour : SpellBehaviour {


        public override void Activate(GameObject spellParams)
        {
            ApplyBuff(spellParams);
        }

        private void ApplyBuff(GameObject spellParams)
        {
            var buffSpellConfig = (config as BuffSpellConfig);

            spellParams.GetComponent<HealthSystem>().Heal(buffSpellConfig.GetStatChangeAmount());

            PlayParticleEffect();
        }
    }
}