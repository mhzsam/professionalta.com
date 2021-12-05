using System;

namespace FirstZX.Core.Generator
{
    public class CharGenerator
    {
        public static string UnicCodeGenerate()
        {
            return Guid.NewGuid().ToString().Replace("-", "");
        }

        public static int DigitalNumberGenerate()
        {
            int num = new Random().Next(1000, 9999);
            return num;

        }
    }
}