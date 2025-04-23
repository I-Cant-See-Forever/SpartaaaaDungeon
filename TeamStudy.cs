using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//단일상속
//다중상속 안됨
public class TeamStudy : IStudyA, IStudyB
{
    public void TestA()
    {
        throw new NotImplementedException();
    }

    public void TestB()
    {
        throw new NotImplementedException();
    }
}


public interface IStudyA
{
    public void TestA();
}

public interface IStudyB
{
    public void TestB();
}
