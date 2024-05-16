using CapitalTask.Models;

namespace CapitalTask.Models
{
    public class Application
    {
        public string? id { get; set; }
        public string programId { get; set; }
        public PersonalInfoAnswers[] PersonalInformation { get; set; }
        public QuestionAnswers[] questions { get; set; }
    }

    public class PersonalInfoAnswers : PersonalInfoModel
    {
        public string answer { get; set; }
    }

    public class QuestionAnswers : Question
    {
        public Answer answer { get; set; }
    }

    public class Answer
    {
        public bool isOther { get; set; }
        // There will be one value if the question type is Paragraph, Yes/No, Date, Number and Drop down
        // and There will be multiple values in the array if the question type is Multiple Choice
        public string[] values { get; set; }
    }
}
