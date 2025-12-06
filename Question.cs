using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegyenOnIsMilliomosWPF
{
    internal class Question
    {
        private string questionText;
        private List<string> answers;
        private int correctAnswerIndex;
        public Question(string questionText, List<string> answers, int correctAnswerIndex)
        {
            this.questionText = questionText;
            this.answers = answers;
            this.correctAnswerIndex = correctAnswerIndex;
        }
        
        public string QuestionText { get { return questionText; } }
        public List<string> Answers { get { return answers; } }
        public int CorrectAnswerIndex { get { return correctAnswerIndex; } }




    }
}
