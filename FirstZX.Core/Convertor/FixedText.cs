namespace FirstZX.Core.Convertor
{
    public class FixedText
    {
        public static string FixEmail(string email)
        {
            if (email!=null)
            {
                return email.Trim().ToLower();
            }
            else
            {
                return null;
            }
            
        }
    }
}