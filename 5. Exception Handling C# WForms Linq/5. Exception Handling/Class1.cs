using System;
using System.Collections.Generic;
using System.Text;

namespace _5._Exception_Handling
{
    class Class1
    {
    }
    class Bludo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? Trud { get; set; }
        public Bludo(int Id, string Name, int? Trud)
        {
            this.Id = Id;
            this.Name = Name;
            this.Trud = Trud;
        }

    }

    class Postavka
    {
        public int PostavshikId { get; set; }
        public decimal? Objem { get; set; }
        public DateTime? Date { get; set; }
        public decimal? Stoim { get; set; }

        public Postavka(int PostavshikId, decimal? Objem, DateTime? Date, decimal? Stoim)
        {
            this.PostavshikId = PostavshikId;
            this.Objem = Objem;
            this.Date = Date;
            this.Stoim = Stoim;
        }
    }
    class Postavshik
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Gorod { get; set; }

        public Postavshik(int Id, string Name, string Gorod)
        {
            this.Id = Id;
            this.Name = Name;
            this.Gorod = Gorod;
        }
    }

    class Produkt
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Sklad Nalichiye { get; set; }
        public List<Spisok> Bluda { get; set; }
        public List<Postavka> Postavki { get; set; }

        public Produkt(int Id, string Name)
        {
            this.Id = Id;
            this.Name = Name;
            Nalichiye = new Sklad();
            Bluda = new List<Spisok>();
            Postavki = new List<Postavka>();
        }
    }
    class Sklad
    {
        public decimal? Kolvo { get; set; }
        public decimal? Stoimost { get; set; }
        public Sklad()
        {
        }

        public Sklad(decimal? Kolvo, decimal? Stoimost)
        {
            this.Kolvo = Kolvo;
            this.Stoimost = Stoimost;
        }
    }

    class Spisok
    {
        public int BludoId { get; set; }
        public int? Ves { get; set; }

        public Spisok(int BludoId, int? Ves)
        {
            this.BludoId = BludoId;
            this.Ves = Ves;
        }
    }
}
