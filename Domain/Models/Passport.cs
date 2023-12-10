﻿namespace Domain.Models
{
    public class Passport
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public string Series { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public DateOnly BirthDate { get; set; }
    }
}
