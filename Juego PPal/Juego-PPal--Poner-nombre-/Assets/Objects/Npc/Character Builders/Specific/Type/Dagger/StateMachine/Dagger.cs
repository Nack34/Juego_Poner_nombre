using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dagger : Entity
{   

    // en este metodo se hacen los new de los States de ataque
    public override void InitializeAllOpponentDetectedSubState(){
        base.InitializeAllOpponentDetectedSubState();
        
        // pasar los random selectors a attackState. IMPLEMENTAR
        opponentDetectedState.Initialize( new Dagger_OpDetSS_FTFB (this, stateMachine, AnimationStrings.OpponentDetectedSubState/*, randomFTFRangeCombatActionSelector*/),
                                new Dagger_OpDetSS_ShortRB (this, stateMachine, AnimationStrings.OpponentDetectedSubState/*, randomCloseRangeCombatActionSelector*/),
                                new Dagger_OpDetSS_LongRB (this, stateMachine, AnimationStrings.OpponentDetectedSubState/*, randomLongRangeCombatActionSelector*/) );
        
    }
    
}
