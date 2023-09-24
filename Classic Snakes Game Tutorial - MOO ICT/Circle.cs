using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/*
 * La classe "Circle" représente un élément basique dans le jeu du serpent.
 * Elle est utilisée pour définir les positions (X et Y) d'un segment individuel du serpent ou de la nourriture.
 * Chaque segment du serpent ou un morceau de nourriture est représenté comme un cercle dans l'espace de jeu.
 * Lorsque le serpent se déplace ou s'agrandit, de nouveaux cercles sont ajoutés ou manipulés en utilisant cette structure simple.
 */



namespace Classic_Snakes_Game_Tutorial___MOO_ICT
{
    class Circle
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Circle()
        {
            X = 0;
            Y = 0;
        }


    }
}
