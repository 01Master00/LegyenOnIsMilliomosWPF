using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegyenOnIsMilliomosWPF
{
    internal class Question
    {
        private int questionNumber;
        private string questionText;
        private List<string> answers;
        private string solution;
        private string category;
        public Question(int q, string questionText, List<string> answers, string correctAnswerIndex,string cat)
        {
            this.questionText = questionText;
            this.answers = answers;
            this.solution = correctAnswerIndex;
            this.questionNumber = q;
            this.category = cat;
        }
        
        public string QuestionText { get { return questionText; } }
        public List<string> Answers { get { return answers; } }
        public string Solution { get { return solution; } }
        public int QuestionNumber { get { return questionNumber; } }
        public string Category { get { return category; } }




    }
}
