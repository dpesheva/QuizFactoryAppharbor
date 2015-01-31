﻿namespace QuizFactory.Mvc.Areas.Users.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Linq.Expressions;
    using QuizFactory.Data.Models;

    public class QuestionUserViewModel
    {
        public static Expression<Func<QuestionDefinition, QuestionUserViewModel>> FromQuestionDefinition
        {
            get
            {
                return question => new QuestionUserViewModel
                {
                    Id = question.Id,
                    QuestionText = question.QuestionText,
                    Number = question.Number,
                    QuizId = question.QuizDefinition.Id
                };
            }
        }

        public int Id { get; set; }

        [Required]
        [Display(Name = "Question")]

        [DataType(DataType.MultilineText)]
        public string QuestionText { get; set; }

        [Required]
        public int Number { get; set; }

        public int QuizId { get; set; }

        [Required]
        public string CorrectAnswer { get; set; }

        public IList<string> Answers { get; set; }
    }
}