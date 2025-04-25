using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public abstract class QuestCondition
{
    public string Description { get; set; }
    public abstract bool IsAchive();
    public abstract string ProgressText();
}