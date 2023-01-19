using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dagger : Entity
{   

    // en este metodo se hacen los new de los States de ataque
    public override void InitializeAllCombatSubStates(){
        base.InitializeAllCombatSubStates();
        
        combatState.Initialize( new CloseRgNPC_FTFRg_CSState (this, stateMachine, AnimationStrings.CombatSubState, randomFTFRangeCombatActionSelector),
                                new CloseRgNPC_ShortRg_CSState (this, stateMachine, AnimationStrings.CombatSubState, randomCloseRangeCombatActionSelector),
                                new CloseRgNPC_LongRg_CSState (this, stateMachine, AnimationStrings.CombatSubState, randomLongRangeCombatActionSelector) );
        
    }
    
}
