#region LicenseInformation
// License Info Samples.cs
// 2013 Copyright (c)
#endregion

using System;

namespace SomeRandomAssembly
{
    public class Samples
    {
        public void SomeMethod()
        {
            DateTime someTime = new DateTime();

            someTime.ToString()
        }
    
    }


    public class SomeClass<T>
    {

        public int AddSomething(int value)
        {
            var output = 10 + value;
            return output;
        }

        public int InvokeAddSomething(int value)
        {
            var result = AddSomething(1000);

            return result + value;
        }
    }

  
}
