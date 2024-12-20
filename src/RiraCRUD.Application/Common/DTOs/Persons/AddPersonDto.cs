﻿using AutoMapper;
using RiraCRUD.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiraCRUD.Application.Common.DTOs.Persons
{
    public class AddPersonDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NationalId { get; set; }
        public DateTime DateOfBirth { get; set; }
        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<AddPersonDto, RiraCRUD.Domain.Entities.Person>();
            }
        }
    }
}
