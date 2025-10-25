using System;
using System.Collections.Generic;
using System.Linq;

namespace TestPortalApp
{
    // ====== CLASS QUESTION ======
    public class Question
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string OptionA { get; set; }
        public string OptionB { get; set; }
        public string OptionC { get; set; }
        public string OptionD { get; set; }
        public string CorrectAnswer { get; set; } // "A","B","C","D" hoặc "True"/"False"
    }

    // ====== CLASS TEST ======
    public class Test
    {
        public string TestCode { get; set; }
        public string Title { get; set; }
        public List<Question> Questions { get; set; } = new List<Question>();
    }

    // ====== MAIN PROGRAM ======
    internal class Program
    {
        // Danh sách đề
        static List<Test> tests = new List<Test>();

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            // Khởi tạo 1 đề mẫu
            InitializeSampleData();

            while (true)
            {
                Console.WriteLine("========== TEST PORTAL ==========");
                Console.WriteLine("1. Xem danh sách đề");
                Console.WriteLine("2. Làm bài theo mã đề");
                Console.WriteLine("3. Quản lý câu hỏi (CRUD)");
                Console.WriteLine("4. Thoát");
                Console.Write("Chọn chức năng: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        DisplayTests();
                        break;
                    case "2":
                        TakeTest();
                        break;
                    case "3":
                        ManageQuestions();
                        break;
                    case "4":
                        Console.WriteLine("Tạm biệt!");
                        return;
                    default:
                        Console.WriteLine("Lựa chọn không hợp lệ.\n");
                        break;
                }
            }
        }

        // ====== KHỞI TẠO DỮ LIỆU MẪU ======
        static void InitializeSampleData()
        {
            var test1 = new Test
            {
                TestCode = "T01",
                Title = "Đề Kiểm tra Lập trình C#"
            };
            test1.Questions.Add(new Question
            {
                Id = 1,
                Content = "C# là ngôn ngữ của nền tảng nào?",
                OptionA = ".NET",
                OptionB = "Java",
                OptionC = "Python",
                OptionD = "C++",
                CorrectAnswer = "A"
            });
            test1.Questions.Add(new Question
            {
                Id = 2,
                Content = "Kiểu dữ liệu lưu trữ số thực là?",
                OptionA = "int",
                OptionB = "string",
                OptionC = "double",
                OptionD = "bool",
                CorrectAnswer = "C"
            });
            test1.Questions.Add(new Question
            {
                Id = 3,
                Content = "Câu lệnh dùng để in ra màn hình?",
                OptionA = "Console.ReadLine()",
                OptionB = "Console.WriteLine()",
                OptionC = "Console.Input()",
                OptionD = "print()",
                CorrectAnswer = "B"
            });
            tests.Add(test1);
        }

        // ====== HIỂN THỊ DANH SÁCH ĐỀ ======
        static void DisplayTests()
        {
            Console.WriteLine("\nDanh sách đề thi:");
            foreach (var t in tests)
            {
                Console.WriteLine($"- Mã đề: {t.TestCode} | Tiêu đề: {t.Title}");
            }
            Console.WriteLine();
        }

        // ====== LÀM BÀI ======
        static void TakeTest()
        {
            Console.Write("Nhập mã đề muốn làm: ");
            string code = Console.ReadLine();
            var test = tests.FirstOrDefault(t => t.TestCode.Equals(code, StringComparison.OrdinalIgnoreCase));

            if (test == null)
            {
                Console.WriteLine("❌ Không tìm thấy mã đề này!\n");
                return;
            }

            Console.WriteLine($"\n===== {test.Title} =====\n");
            var userAnswers = new Dictionary<int, string>();

            foreach (var q in test.Questions)
            {
                Console.WriteLine($"{q.Id}. {q.Content}");
                Console.WriteLine($"A. {q.OptionA}");
                Console.WriteLine($"B. {q.OptionB}");
                Console.WriteLine($"C. {q.OptionC}");
                Console.WriteLine($"D. {q.OptionD}");
                Console.Write("Đáp án của bạn: ");
                string ans = Console.ReadLine().Trim().ToUpper();
                userAnswers[q.Id] = ans;
                Console.WriteLine();
            }

            int score = SubmitTest(test, userAnswers);
            Console.WriteLine($"✅ Kết quả: Bạn đạt {score}/{test.Questions.Count} điểm.\n");
        }

        // ====== CHẤM ĐIỂM ======
        static int SubmitTest(Test test, Dictionary<int, string> userAnswers)
        {
            int score = 0;
            foreach (var q in test.Questions)
            {
                if (userAnswers.ContainsKey(q.Id) &&
                    userAnswers[q.Id].Equals(q.CorrectAnswer, StringComparison.OrdinalIgnoreCase))
                {
                    score++;
                }
            }
            return score;
        }

        // ====== QUẢN LÝ CÂU HỎI ======
        static void ManageQuestions()
        {
            Console.Write("Nhập mã đề muốn quản lý: ");
            string code = Console.ReadLine();
            var test = tests.FirstOrDefault(t => t.TestCode.Equals(code, StringComparison.OrdinalIgnoreCase));

            if (test == null)
            {
                Console.WriteLine("Không tìm thấy mã đề!\n");
                return;
            }

            while (true)
            {
                Console.WriteLine($"\n===== QUẢN LÝ CÂU HỎI ({test.TestCode}) =====");
                Console.WriteLine("1. Xem câu hỏi");
                Console.WriteLine("2. Thêm câu hỏi");
                Console.WriteLine("3. Sửa câu hỏi");
                Console.WriteLine("4. Xóa câu hỏi");
                Console.WriteLine("5. Quay lại");
                Console.Write("Chọn: ");
                string c = Console.ReadLine();

                switch (c)
                {
                    case "1":
                        DisplayQuestions(test);
                        break;
                    case "2":
                        AddQuestion(test);
                        break;
                    case "3":
                        UpdateQuestion(test);
                        break;
                    case "4":
                        DeleteQuestion(test);
                        break;
                    case "5":
                        return;
                    default:
                        Console.WriteLine("Lựa chọn không hợp lệ!");
                        break;
                }
            }
        }

        // ====== HIỂN THỊ CÂU HỎI ======
        static void DisplayQuestions(Test test)
        {
            Console.WriteLine($"\n--- Danh sách câu hỏi trong {test.TestCode} ---");
            foreach (var q in test.Questions)
            {
                Console.WriteLine($"{q.Id}. {q.Content} (Đáp án đúng: {q.CorrectAnswer})");
            }
            Console.WriteLine();
        }

        // ====== THÊM CÂU HỎI ======
        static void AddQuestion(Test test)
        {
            var q = new Question();
            q.Id = test.Questions.Count > 0 ? test.Questions.Max(x => x.Id) + 1 : 1;
            Console.Write("Nội dung câu hỏi: ");
            q.Content = Console.ReadLine();
            Console.Write("A: "); q.OptionA = Console.ReadLine();
            Console.Write("B: "); q.OptionB = Console.ReadLine();
            Console.Write("C: "); q.OptionC = Console.ReadLine();
            Console.Write("D: "); q.OptionD = Console.ReadLine();
            Console.Write("Đáp án đúng (A/B/C/D): ");
            q.CorrectAnswer = Console.ReadLine().Trim().ToUpper();

            test.Questions.Add(q);
            Console.WriteLine("✅ Đã thêm câu hỏi!\n");
        }

        // ====== SỬA CÂU HỎI ======
        static void UpdateQuestion(Test test)
        {
            Console.Write("Nhập ID câu hỏi cần sửa: ");
            int id = int.Parse(Console.ReadLine());
            var q = test.Questions.FirstOrDefault(x => x.Id == id);
            if (q == null)
            {
                Console.WriteLine("Không tìm thấy câu hỏi!");
                return;
            }

            Console.WriteLine("Để trống nếu không muốn thay đổi.");
            Console.Write($"Nội dung ({q.Content}): ");
            string content = Console.ReadLine();
            if (!string.IsNullOrEmpty(content)) q.Content = content;

            Console.Write($"A ({q.OptionA}): "); string a = Console.ReadLine(); if (!string.IsNullOrEmpty(a)) q.OptionA = a;
            Console.Write($"B ({q.OptionB}): "); string b = Console.ReadLine(); if (!string.IsNullOrEmpty(b)) q.OptionB = b;
            Console.Write($"C ({q.OptionC}): "); string c = Console.ReadLine(); if (!string.IsNullOrEmpty(c)) q.OptionC = c;
            Console.Write($"D ({q.OptionD}): "); string d = Console.ReadLine(); if (!string.IsNullOrEmpty(d)) q.OptionD = d;

            Console.Write($"Đáp án đúng ({q.CorrectAnswer}): ");
            string correct = Console.ReadLine();
            if (!string.IsNullOrEmpty(correct)) q.CorrectAnswer = correct.ToUpper();

            Console.WriteLine("✅ Đã cập nhật!\n");
        }

        // ====== XÓA CÂU HỎI ======
        static void DeleteQuestion(Test test)
        {
            Console.Write("Nhập ID câu hỏi cần xóa: ");
            int id = int.Parse(Console.ReadLine());
            var q = test.Questions.FirstOrDefault(x => x.Id == id);
            if (q != null)
            {
                test.Questions.Remove(q);
                Console.WriteLine("✅ Đã xóa câu hỏi!\n");
            }
            else
            {
                Console.WriteLine("Không tìm thấy câu hỏi!\n");
            }
        }
    }
}
