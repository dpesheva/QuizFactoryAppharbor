﻿namespace QuizFactory.Mvc.ViewModels.Play
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using AutoMapper;
    using QuizFactory.Data.Models;
    using QuizFactory.Mvc.Mapping;

    public class QuestionPlayViewModel : IMapFrom<QuestionDefinition>, IHaveCustomMappings
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Question")]
        public string QuestionText { get; set; }

        [Required]
        public int Number { get; set; }

        public int QuizId { get; set; }

        public ICollection<AnswerViewModel> Answers { get; set; }
        
        public void CreateMappings(IConfiguration configuration)
        {
            Mapper.CreateMap<QuestionDefinition, QuestionPlayViewModel>()
                  .ForMember(a => a.Answers, opt => opt.MapFrom(a => a.AnswersDefinitions.Where(o => !o.IsDeleted)))
                  .ReverseMap();

            Mapper.CreateMap<QuestionDefinition, QuestionPlayViewModel>().ReverseMap();
        }
    }
}