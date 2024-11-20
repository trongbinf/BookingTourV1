using BookingTour.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingTour.Business.Service
{
    public static class HandleTextUnicode
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

        public static PaginatedList<T> ToPaginatedList<T>(List<T> source, int currentPage, int pageSize)
        {
            if (currentPage < 1) currentPage = 1;
            if (pageSize < 1) pageSize = 6;

            Console.WriteLine($"page size in handle: {pageSize}");
            Console.WriteLine($"current page in handle: {currentPage}");
            Console.WriteLine($"Source: {source.Count()}");

            var totalCount = source.Count();
            var items = source.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
            Console.WriteLine($"page size in handle: {items.Count()}");

            return new PaginatedList<T>(items, totalCount, currentPage, pageSize);
        }
    }
}
