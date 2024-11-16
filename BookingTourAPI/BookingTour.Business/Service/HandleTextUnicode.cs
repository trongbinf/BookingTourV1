using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingTour.Business.Service
{
    public class HandleTextUnicode
    {
        public static string RemoveUnicode(string input)
        {
            string[] vietChars = new string[]
            {
        "aàáảãạăắằẳẵặâấầẩẫậ", "eèéẻẽẹêếềểễệ", "iìíỉĩị", "oòóỏõọôốồổỗộơớờởỡợ", "uùúủũụưứừửữự",
        "yỳýỷỹỵ", "dđ", "AÀÁẢÃẠĂẮẰẲẴẶÂẤẦẨẪẬ", "EÈÉẺẼẸÊẾỀỂỄỆ", "IÌÍỈĨỊ", "OÒÓỎÕỌÔỐỒỔỖỘƠỚỜỞỠỢ",
        "UÙÚỦŨỤƯỨỪỬỮỰ", "YỲÝỶỸỴ", "DĐ"
            };

            foreach (var vietChar in vietChars)
            {
                var replaceChar = vietChar[0].ToString();  // Lấy ký tự không dấu (ví dụ, "a" thay cho "àáảãạ")
                input = input.Replace(vietChar, replaceChar);
            }

            return input;
        }
    }
}
