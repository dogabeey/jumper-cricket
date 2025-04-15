using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Lionsfall
{
    public class LevelScene : SingletonComponent<LevelScene>
    {

        public string levelName;
        public int currentTurn;
        public LevelEditor levelEditor;

        internal bool isWin;
        internal bool isLose;
        internal bool isEnded;
        internal Player player;

        private void Start()
        {
            EventManager.TriggerEvent(Const.GameEvents.LEVEL_STARTED, new EventParam());
        }

        private void Update()
        {
            if (isEnded) return;
            if (isWin) // PUT YOUR WIN CONDITIONS HERE
            {
                isEnded = true;
                EventParam param = new EventParam();
                EventManager.TriggerEvent(Const.GameEvents.LEVEL_COMPLETED, param); // You can trigger this event anywhere and It will trigger On Win actions in the inspector, along with regular Level Completion events. This one also passes the time it took to win the level.
            }
            if (isLose) // PUT YOUR LOSE CONDITIONS HERE
            {
                isEnded = true;
                EventParam param = new EventParam();
                EventManager.TriggerEvent(Const.GameEvents.LEVEL_FAILED, param); // You can trigger this event anywhere and It will trigger It will trigger On Lose actions in the inspector, along with regular Level Failure events. This one also passes the time it took to lose the level.
            }
        }

        public void PassTurn()
        {
            currentTurn++;
            EventManager.TriggerEvent(Const.GameEvents.TURN_PASSED, new EventParam());
        }
    }
}