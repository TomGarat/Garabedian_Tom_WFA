using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



/*
 * La classe "Settings" fournit des configurations de base pour le jeu du serpent.
 * Elle définit les dimensions (largeur et hauteur) des blocs ou cellules utilisés pour dessiner le serpent et la nourriture sur l'écran.
 * Elle contient également une propriété pour suivre la direction actuelle du mouvement du serpent.
 * Cette classe est essentielle pour initialiser et gérer les paramètres du jeu qui sont utilisés à travers d'autres parties du programme.
 */




namespace Classic_Snakes_Game_Tutorial___MOO_ICT
{
    class Settings
    {
        public static int Width { get; set; }

        public static int Height { get; set; }

        public static string directions;

        public Settings()
        {
            Width = 16;
            Height = 16;
            directions = "left";
        }


    }
}
