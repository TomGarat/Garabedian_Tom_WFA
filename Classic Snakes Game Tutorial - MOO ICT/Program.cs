using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;


/*
 * La classe "Program" est le point d'entrée de l'application de jeu du serpent.
 * Elle initialise l'environnement de l'application Windows Forms et lance le formulaire principal du jeu, "Form1".
 * 
 * L'attribut [STAThread] est spécifique aux applications Windows et indique que l'application fonctionne dans un thread STA (Single-Threaded Apartment). 
 * Ceci est nécessaire car de nombreuses fonctionnalités de l'interface utilisateur de Windows sont STA et ne peuvent fonctionner correctement que dans un thread STA.
 *
 * La méthode Main():
 * 1. Active les styles visuels pour l'application, rendant l'interface utilisateur plus attrayante.
 * 2. Configure la méthode de rendu du texte pour être compatible avec les versions antérieures de Windows.
 * 3. Lance le formulaire principal du jeu, "Form1".
 */



namespace Classic_Snakes_Game_Tutorial___MOO_ICT
{
    static class Program
    {
        
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
