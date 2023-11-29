﻿namespace Domain.Models
{
    public class Passport
    {
        public Passport(int number, int series,
            string name, string surname,
            string patronymic, DateOnly dateOfBirth)
        {
            Number = number;
            Series = series;
            Name = name;
            Surname = surname;
            Patronymic = patronymic;
            BirthDate = dateOfBirth;
        }

        private Passport()
        {
            
        }

        public int Number { get; set; }
        public int Series { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public DateOnly BirthDate { get; set; }
    }
}