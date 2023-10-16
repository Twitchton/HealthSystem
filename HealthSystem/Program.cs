using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthSystem
{
    internal class Program
    {
        //variable declerations
        private static int health, shield, lives, exp, level, score, enemies, floor;
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
            Console.ReadKey();

            while (enemies > 0 && lives > 0)
            {
                //regular combat
                if(enemies > 1)
                {
                    //player won
                    if (Combat())
                    {
                        AddScore(100 * floor);
                        scoreMult += 0.5f;
                        enemies--;
                        Loot();
                    }
                    //player lost
                    else
                    {
                        score = 0;
                        endMessage = "You Lose :(";
                    }
                }

                //Boss combat
                if(enemies == 1)
                {
                    //player won
                    if (BossCombat())
                    {
                        AddScore(500 * floor);
                        Loot();
                        enemies--;
                    }
                    //player lost
                    else
                    {
                        score = 0;
                        endMessage = "You Lose :(";
                    }
                }

                ShowHUD();
                Console.ReadKey();

                //next floor check
                if(enemies <= 0)
                {
                    NextFloor();
                }
            }

            //ending game
            Console.WriteLine("\n" + endMessage + "\n");

            if(lives == 0)
            {
                CalcScore(false);
            }
            else
            {
                CalcScore(true);
            }

            expandedHUD();

            Console.ReadKey();

        }

        //Method for Taking Damage
        private static void TakeDamage(int damage)
        {
            //error checking for negative damage
            if (damage < 0)
            {
                Console.WriteLine("ERROR: Cannot deal negative damage\n");
                return;
            }

            int overflow = 0; //variable to stack spillover

            //shield damage
            if (shield > 0)
            {
                shield -= damage;
                Console.WriteLine("Your Shield took " + damage + " Damage!");
                if (shield <= 0)
                {
                    overflow = -1*shield;

                    shield = 0;
                    Console.WriteLine("Your Shield Broke!!!");
                }
                Console.WriteLine();
            }
            //health damage
            else
            {
                health -= damage;
                Console.WriteLine("You took " + damage + " Damage!\n");
            }

            //spillover
            if(overflow > 0)
            {
                health -= overflow;
                Console.WriteLine("You took " + overflow + " Damage!\n");
            }

            //putting health back in range (this probably wouldn't be necissairy)
            if (health < 0)
            {
                health = 0;
            }
        }

        //Method to deal damage to an enemy
        private static int DealDamage()
        {
            Random rnd = new Random();
            int damage;

            int pDamageMin = 10 + level * 5;
            int pDamageMax = 25 + level * 5;

            Console.WriteLine("You attacked!");

            damage = rnd.Next(pDamageMin, pDamageMax);

            if(rnd.Next(1,20) == 20)
            {
                damage *= 2;
                Console.WriteLine("It was a critical hit!!!");
            }

            Console.WriteLine("You dealt " + damage + " damage!");

            return damage;
        } 

        //Method that heals the player by a given value
        private static void Heal(int hp)
        {
            //error checking to see if hp is negative
            if (hp < 0)
            {
                Console.WriteLine("ERROR: Cannot heal a negative number\n");
                return;
            }

            health += hp;

            if (health > 100)
            {
                health = 100;
            }
        }

        //Method that regenerates shield
        private static void RegenerateShield(int sp)
        {
            //error checking to see if sp is negative
            if (sp < 0)
            {
                Console.WriteLine("ERROR: Cannot regenerate a negative number\n");
                return;
            }

            shield += sp;

            if (shield > 100)
            {
                shield = 100;
            }
        }

        //Method that revives the player
        private static bool Revive()
        {
            bool revived;

            if(lives > 0)
            {
                lives--;
                health = 100;
                shield = 100;

                scoreMult = 1;
                revived = true;
            }
            else
            {
                revived= false;
            }

            return revived;
        }

        //Method for increasing XP
        private static void IncreaseXP(int xp)
        {
            //error checking to see if sp is negative
            if (xp < 0)
            {
                Console.WriteLine("ERROR: Cannot gain negative xp\n");
                return;
            }

            exp += xp;

            //leveling up 
            while (exp >= (level*100))
            {
                exp -= (level*100);
                level++;
                Console.WriteLine("You leveled up!");
            }
        }

        //Method that resets the game to the start
        private static void ResetGame()
        {
            floor = 1;
            enemies = 10;

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
            healthStatus = getStatus();

            Console.WriteLine("----------------");
            Console.WriteLine("GamerTag:\t\t" + gamerTag);
            Console.WriteLine("----------------");
            Console.WriteLine("Shield:\t\t\t" + shield);
            Console.WriteLine("Health:\t\t\t" + health);
            Console.WriteLine("Status:\t\t\t" + healthStatus);
            Console.WriteLine();
            Console.WriteLine("Lives:\t\t\t" + lives);
            Console.WriteLine("----------------");
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
            Console.WriteLine("Floor:\t\t\t" + floor.ToString());
            Console.WriteLine("----------------");
            Console.WriteLine();
        }

        //Method that send player to the next floor of dungeon
        private static void NextFloor()
        {
            char answer = ' ';

            while (answer != 'n' || answer != 'N' || answer != 'y' || answer != 'Y')
            {
                Console.WriteLine("Would you like to progress to the next floor?\n(Y)es\n(N)o");
                answer = Console.ReadKey().KeyChar;

                if(answer == 'y' || answer == 'Y')
                {
                    Console.WriteLine("\nYou descended a floor.\n");
                    //changing floors and population enemies
                    floor++;
                    enemies = 8 + (int)Math.Pow(floor, 2);
                    return;
                }
                else if (answer == 'n' || answer == 'N')
                {
                    Console.WriteLine("\nYou exited the dungeon.\n");
                    endMessage = "You win!";
                    return;
                }

                //error check for invalid input
                else
                {
                    Console.WriteLine("\nERROR: Invalid input, please try again...\n");
                }
            }

        }

        //method that handles combat between player and a generated enemy.
        private static bool Combat()
        {
            Random rnd = new Random();

            int enemyLevel = floor + rnd.Next(2);
            int enemyHealth = 25 + (enemyLevel * 10);
            int eDamageMin = 10 + (enemyLevel * 5);
            int eDamageMax = 25;

            bool won;

            Console.WriteLine("\nYou encountered an Enemy!!!\n");

            //loop to contain combat
            while (lives > 0 && enemyHealth > 0)
            {
                //determining who goes first in combat
                if(enemyLevel > level)
                {
                    TakeDamage(rnd.Next(eDamageMin, eDamageMax) - (level - enemyLevel));
                    if (health <=0)
                    {
                        Revive();
                    }

                    enemyHealth -= DealDamage();
                }
                else
                {
                    enemyHealth -= DealDamage();
                    if (enemyHealth <= 0)
                    {
                        break;
                    }

                    TakeDamage(rnd.Next(eDamageMin, eDamageMax) - (level - enemyLevel));
                    if (health <= 0)
                    {
                        Revive();
                    }
                }

                ShowHUD();

                Console.ReadKey();
            }

            if (lives < 0)
            {
                Console.WriteLine("\nYou died.\n");
                won = false;
            }
            else
            {
                Console.WriteLine("\nYou killed the Enemy!\n");

                int xp = ((enemyLevel * 100) / level);
                IncreaseXP(xp);

                won = true;
            }

            return won;
        }

        //variation on combat for a larger boss enemy
        private static bool BossCombat()
        {
            Random rnd = new Random();

            int enemyLevel = floor + 5;
            int enemyHealth = 25 + enemyLevel * 10;
            int eDamageMin = 10 + enemyLevel * 5;
            int eDamageMax = 25 + enemyLevel * 5;

            bool won;

            Console.WriteLine("\nYou encountered the Boss!!!\n");

            //loop to contain combat
            while (lives > 0 && enemyHealth > 0)
            {
                //determining who goes first in combat
                if (enemyLevel > level)
                {
                    TakeDamage(rnd.Next(eDamageMin, eDamageMax) - (level - enemyLevel));
                    if (health <= 0)
                    {
                        Revive();
                    }

                    enemyHealth -= DealDamage();
                }
                else
                {
                    enemyHealth -= DealDamage();
                    if (enemyHealth <= 0)
                    {
                        break;
                    }

                    TakeDamage(rnd.Next(eDamageMin, eDamageMax) - (level - enemyLevel));
                    if (health <= 0)
                    {
                        Revive();
                    }
                }

                ShowHUD();
                Console.ReadKey();
            }

            if (lives < 0)
            {
                Console.WriteLine("\nYou died.\n");
                won = false;
            }
            else
            {
                Console.WriteLine("\nYou killed the Boss!\n");
                won = true;
            }

            return won;
        }

        //Method to add score to player
        private static void AddScore(int points)
        {
            score += (int)(points * scoreMult);
        }

        //Method to calculate final score
        private static void CalcScore(bool win)
        {
            int fscore;

            if (win)
            {
                fscore = score + ((lives * 500) + health) * floor;    
            }
        }

        //Method that returns health status
        private static string getStatus()
        {
            string status = null;
            if (health == 0)
            {
                status = "Dead as a Doorknob";
            }
            if(health <= 10 && health > 0)
            {
                status = "Imminent Danger";
            }
            else if(health <= 50 && health > 10)
            {
                status = "Badly Hurt";
            }
            else if(health <= 75 && health > 50)
            {
                status = "Hurt";
            }
            else if(health <= 90 && health > 75)
            {
                status = "Healthy";
            }
            else if (health <= 100 && health > 90)
            {
                status = "Perfect Health";
            }
            else
            {
                status = "???";
            }

            return status;
        }

        //Method for looting after combat
        private static void Loot()
        {
            Random rnd = new Random();
            int roll = rnd.Next(1, 100);

            Console.WriteLine("You loot the area.\n");

            //nothing
            if (roll <= 50)
            {
                Console.WriteLine("You don't find anything\n");
                return;
            }
            //health pickup
            else if ( roll <= 70 && roll > 50)
            {
                Console.WriteLine("You found a health pickup!\n");
                Heal(50);
            }
            //shield pickup
            else if (roll <= 80 && roll > 70)
            {
                Console.WriteLine("You found a shield pickup!\n");
                RegenerateShield(50);
            }
            //chest
            else if (roll <= 95 && roll > 80)
            {
                Console.WriteLine("You found a chest!\n");
                AddScore(500);
            }
            //big chest
            else if (roll <= 100 && roll > 95)
            {
                Console.WriteLine("You found a big chest!!!\n");
                AddScore(2000);
            }
            //out of bounds return
            else
            {
                return;
            }

        }

    }
}
