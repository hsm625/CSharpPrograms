using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Character_Creator
{
    class Character
    {
        //As the character only needs to be protected as long as it takes to send the information to the database,
        //I felt it would be satisfactory to make the fields public and modify them directly as opposed to creating
        //getters and setters for all of them.
        public string Name;
        public string Type;
        public string Species;
        public float Age;
        public string Neighbourhood;
        public string Appearance;
        public string Personality;
        public string Notes;
        public short S, P, E, C, I, A, L;

        //default constructor
        public Character()
        {

        }

        //constructor directly setting all of the data at once.
        public Character(string nm, string type, string spcs, float age, string nbhd, string aprnc, string prsnlty, string notes, short s, short p, short e, short c, short i, short a, short l)
        {
            this.Name = nm;
            this.Type = type;
            this.Species = spcs;
            this.Age = age;
            this.Neighbourhood = nbhd;
            this.Appearance = aprnc;
            this.Personality = prsnlty;
            this.Notes = notes;
            this.S = s;
            this.P = p;
            this.E = e;
            this.C = c;
            this.A = a;
            this.L = l;
        }
    }
}
