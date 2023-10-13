using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthSystem
{
    internal class Program
    {
        //variable declerations
        private static int health, shield, lives, exp, level, score, enemies, enemyValue, eDamageMin, eDamageMax, pDamageMin, pDamageMax;
        private static string healthStatus, name, gamerTag, studioName, gameTitle, endMessage;
        private static float scoreMult;

        //Main Method to run code
        static void Main(string[] args)
        {
            //initializing game names
            studioName = "Benedict Games";
            gameTitle = "NO GAME QUEST";
            ResetGame();


            //Prompt player to enter their name and GamerTag
            ShowTitle();
            Console.WriteLine("Please enter your NAME and press ENTER button to submit:\n");
            name = Console.ReadLine();
            Console.WriteLine("\nPlease enter your GAMER TAG (username) and press ENTER button to submit:\n");
            gamerTag = Console.ReadLine();

            //gameStart
            Console.WriteLine("GAME START!\n");
            ShowHUD();

            while (enemies > 0 || lives > 0)
            {

            }


        }

        //Method for Taking Damage
        private static void TakeDamage(int damage)
        {
            if (shield > 0)
            {
                shield -= damage;
                Console.WriteLine("Your Shield took Damage!");
                if (shield < 0)
                {
                    shield = 0;
                    Console.WriteLine("Your Shield Broke!!!");
                }
                Console.WriteLine();
            }
            else
            {
                health -= damage;
                Console.WriteLine("You took Damage!\n");
            }
        }

        //Method that heals the player by a given value
        private static void Heal(int hp)
        {

        }

        //Method that regenerates shield
        private static void RegenerateShield(int sp)
        {

        }

        //Method that revives the player
        private static void Revive()
        {

        }

        //Method for increasing XP
        private static void IncreaseXP(int xp)
        {
            exp += enemyValue / level;
        }

        //Method that levels up player
        private static void LevelUP()
        {

        }

        //Method that resets the game to the start
        private static void ResetGame()
        {
            enemies = 10;
            enemyValue = 100;

            lives = 3;
            health = 100;
            shield = 100;
            level = 1;
            exp = 0;
            score = 0;
            scoreMult = 1.0f;
        }

        //Methods that shows the HUD for theoretical gameplay
        private static void ShowHUD()
        {
            Console.WriteLine("----------------");
            Console.WriteLine("GamerTag:\t\t" + gamerTag);
            Console.WriteLine("Shield:\t\t\t" + shield);
            Console.WriteLine("Health:\t\t\t" + health);
            Console.WriteLine("Lives:\t\t\t" + lives);
            Console.WriteLine("Level:\t\t\t" + level);
            Console.WriteLine("EXP:\t\t\t" + exp);
            Console.WriteLine("----------------");
            Console.WriteLine();
        }

        //Method that shows the title and studio of the game
        private static void ShowTitle()
        {
            Console.WriteLine("----------------");
            Console.WriteLine(gameTitle + ":");
            Console.WriteLine("----------------");
            Console.WriteLine("By: " + studioName);
            Console.WriteLine();
        }

        //Method that shows title and all HUD elements
        private static void expandedHUD()
        {
            ShowTitle();
            ShowHUD();
            Console.WriteLine("----------------");
            Console.WriteLine("Name:\t\t\t" + name);
            Console.WriteLine("GamerTag:\t\t" + gamerTag);
            Console.WriteLine("Score:\t\t\t" + score.ToString());
            Console.WriteLine("Score Multiplier:\t" + scoreMult.ToString());
            Console.WriteLine("----------------");
            Console.WriteLine();
        }

    }
}
