using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SprtaaaaDungeon
{
    public class QuestInfoScene : Scene
    {
        private QuestController questController;
        public QuestInfoScene(SceneController controller) : base(controller)
        {
            questController = GameManager.Instance.QuestController;
        }

        public override void End()
        {
            throw new NotImplementedException();
        }

        public override void Start()
        {
            throw new NotImplementedException();
        }

        public override void Update()
        {
            throw new NotImplementedException();
        }

        void DrawRewardText(QuestReward reward)
        {
            foreach (var item in reward.ItemCountDict)
            {
                DrawString($"{item.Key} + {item.Value}\n\n");
            }

            if (reward.Gold > 0)
            {
                DrawString($"+ {reward.Gold} G\n\n");
            }

            if (reward.Exp > 0)
            {
                DrawString($"+ {reward.Exp} EXP\n\n");
            }
        }
    }
}
