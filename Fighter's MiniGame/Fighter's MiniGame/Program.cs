using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fighter_s_MiniGame
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello player!\nCreate new figher?");
            string answer;
            do
            {
                Console.WriteLine("pls,write 'yes'.");
                answer = Console.ReadLine();
            }
            while (answer != "yes");
            Console.WriteLine("\npls write name of your fighter");
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            string name = Console.ReadLine();
            Console.ResetColor();
            Console.WriteLine("\nStart?");
            answer = Console.ReadLine();
            Fighter fighter = new Fighter(name);
            Random rnd = new Random();
            for (int i = 1; answer == "yes"|| answer == "Continue"; i++)
            {
                Fighter enemy = new Fighter("Bot" + i);
                Fight();

                void Fight()
                {
                    while (fighter.Hp >= 0 && enemy.Hp >= 0)
                    {
                        string move;
                        Console.WriteLine("\nYour move?");
                        move = Console.ReadLine();
                        switch (move)
                        {
                            case "Attack":
                                {
                                    var damage = fighter.Attack();
                                    enemy.GetDamage(damage);
                                    damage = Math.Round(damage * 2 / enemy.Power, 1);
                                    fighter.GetInfo();
                                    Console.ForegroundColor = ConsoleColor.Blue;
                                    Console.WriteLine("нанес" + " " + damage + " " + "урона!\n");
                                    Console.ResetColor();
                                    break;
                                }

                            case "Wait":
                                {
                                    fighter.Wait();
                                    fighter.GetInfo();
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.WriteLine("получил 5 хп!\n");
                                    Console.ResetColor();
                                    break;
                                }

                            case "Inventory":
                                {
                                    fighter.Inventory();
                                    fighter.GetInfo();
                                    break;
                                }
                        }
                        if (enemy.Hp <= 0) break;
                        int enemyMove = rnd.Next(0, 2);
                        if (enemyMove == 1)
                        {
                            var damage = enemy.Attack();
                            fighter.GetDamage(damage);
                            damage = Math.Round(damage * 2 / fighter.Power, 1);
                            fighter.GetInfo();
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("получил" + " " + damage + " " + "урона!");
                            Console.ResetColor();
                        }
                        else if (enemyMove == 0)
                        {
                            enemy.Wait();
                            fighter.GetInfo();
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("враг не напал в этот раз...");
                            Console.ResetColor();
                        }
                        fighter.Age++;
                        enemy.Age++;
                    }
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine(enemy.Hp <= 0 ? "\nyour fighter is win!" : "\nyour fighter is dead...");
                    Console.ResetColor();
                    fighter.GetInfo();
                    enemy.GetInfo();
                }
                if (fighter.Hp >= 0)
                {
                    fighter.Gold = rnd.Next(0, 500);
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("\nfighter got " + fighter.Gold + " gold!");
                    int healChance = rnd.Next(0, 10);
                    if (healChance == 1 || healChance == 2 || healChance == 3) Console.WriteLine("you faund the chest!");
                    switch (healChance)
                    {
                        case 1:
                            {
                                HealPotion smallPotion = new HealPotion(100, Enums.HealPotionSize.Small);
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("in the chest you found a small healing potion!(+20 hp)");
                                if (fighter.inventory.ContainsKey(smallPotion))
                                {
                                    fighter.inventory[smallPotion]++;
                                }
                                else
                                {
                                    fighter.inventory.Add(smallPotion,1);
                                }
                                break;
                            }
                        case 2:
                            {
                                HealPotion mediumPotion = new HealPotion(200, Enums.HealPotionSize.Medium);
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.WriteLine("in the chest you found a small healing potion!(+50 hp)");
                                if (fighter.inventory.ContainsKey(mediumPotion))
                                {
                                    fighter.inventory[mediumPotion]++;
                                }
                                else
                                {
                                    fighter.inventory.Add(mediumPotion, 1);
                                }
                                break;
                            }
                        case 3:
                            {
                                HealPotion largePotion = new HealPotion(100, Enums.HealPotionSize.Large);
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("in the chest you found a great healing potion!(+100 hp)");
                                if (fighter.inventory.ContainsKey(largePotion))
                                {
                                    fighter.inventory[largePotion]++;
                                }
                                else
                                {
                                    fighter.inventory.Add(largePotion, 1);
                                }
                                break;
                            }
                    }
                    Console.ResetColor();
                    while (answer != "Continue")
                    {
                        Console.WriteLine("\nContinue or inventory?");
                        answer = Console.ReadLine();
                        if (answer == "Inventory")
                        {
                            fighter.Inventory();

                        }
                        else if (answer == "Shop")
                        {
                        
                        }
                    }
                }
                else
                {
                    break;
                }
            }
            Console.ReadKey();
        }
    }
}
