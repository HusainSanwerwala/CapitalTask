namespace CapitalTask.Models
{
    public class _Program
    {
        public string? id { get; set; }
        public string title { get; set; }
        public string description { get; set; }

        public PersonalInfoModel[] personalInformation { get; set; }

        public Question[] customQuestions { get; set; }
    }

    public class PersonalInfoModel
    {
        public string title { get; set; }
        public bool required { get; set; }
        public bool isInternal { get; set; }
        public bool isHide { get; set; }
    }

    public class Question
    {
        public string id { get; set; }
        public string title { get; set; }
        public string type { get; set; }
        public string[]? choices { get; set; }
        public bool? isOther { get; set; }
        public int? maxChoiceAllowed { get; set; }
    }
}
