using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab1
{
	class Program
	{
		static void Main(string[] args)
		{
			SortMessages sortMessages = new SortMessages();
			sortMessages.Start();
			Console.ReadKey();
		}
		class SortMessages
		{
			string lowword;
			int key;
			double[] doublemas = new double[2];
			string[] message= new string[100];
			bool status;
			StreamWriter writer;
			List<Dictionary<string,int>> dictionaries = new List<Dictionary<string,int>>();
			Dictionary<string, double> dictionary = new Dictionary<string, double>();
			Dictionary<string, double> normalizeSpam = new Dictionary<string, double>();
			Dictionary<string, double> normalizeNotSpam = new Dictionary<string, double>();
			public void Start()
			{
				dictionaries.Add(new Dictionary<string, int>());
				dictionaries.Add(new Dictionary<string, int>());
				ReadFiles();
				Initialization();
				while (key!=2)
				{
					Console.WriteLine("1-Study");
					Console.WriteLine("2-End");
					Console.WriteLine("3-Spam or not spam");
					try
					{
						key = Convert.ToInt32(Console.ReadLine());
					}
					catch(Exception)
					{
						Console.Clear();
						Console.WriteLine("Enter right value: ");
						key = 4;
					}
					switch (key)
					{
						case 1:
							Studing();
							break;
						case 2:
							Console.WriteLine("See you later");
							break;
						case 3:
							SpamOrNotSpam();
							break;
						case 4:
							break;
					}
				}
			}
			public void Initialization()
			{
				foreach(KeyValuePair<string,int> keyValuePair in dictionaries[0])
				{
					dictionary.Add(keyValuePair.Key, keyValuePair.Value);
				}
				foreach (KeyValuePair<string, int> keyValuePair in dictionaries[1])
				{
					try
					{
						dictionary[keyValuePair.Key] = dictionary[keyValuePair.Key] + keyValuePair.Value;
					}
					catch(Exception)
					{
						dictionary.Add(keyValuePair.Key, keyValuePair.Value);
					}
				}
				foreach(KeyValuePair<string, double> keyValuePair in dictionary)
				{
					try
					{
						normalizeNotSpam.Add(keyValuePair.Key, ((dictionaries[0][keyValuePair.Key] + 0.5) / (keyValuePair.Value + 1)));
					}
					catch(Exception)
					{
						normalizeNotSpam.Add(keyValuePair.Key, 1-((dictionaries[1][keyValuePair.Key] + 0.5) / (keyValuePair.Value + 1)));
					}
					
				}
				foreach (KeyValuePair<string, double> keyValuePair in dictionary)
				{
					try
					{
						normalizeSpam.Add(keyValuePair.Key, ((dictionaries[1][keyValuePair.Key] + 0.5) / (keyValuePair.Value + 1)));
					}
					catch(Exception)
					{
						normalizeSpam.Add(keyValuePair.Key, 1-((dictionaries[0][keyValuePair.Key] + 0.5) / (keyValuePair.Value + 1)));
					}
				}
			}
			public void SpamOrNotSpam()
			{
				Console.WriteLine("Enter message: ");
				message=Console.ReadLine().Split(' ');
				doublemas[0] = Math.Log(0.5);
				doublemas[1] = Math.Log(0.5);
				foreach (string element in message)
				{
					lowword = element.ToLower();
					try
					{
						Console.WriteLine(normalizeNotSpam[lowword] + normalizeSpam[lowword]);
						doublemas[0] += Math.Log(normalizeNotSpam[lowword]);
						doublemas[1] += Math.Log(normalizeSpam[lowword]);
					}
					catch (Exception) { }
					
				}
				if(doublemas[0]>doublemas[1])
					Console.WriteLine("Not a spam");
				else
					Console.WriteLine("Spam");

			}
			public void WriteFiles()
			{
				using(StreamWriter writer= new StreamWriter(@"C:\Users\brija\Desktop\'.net'\IAD\lab1\lab1\notSpamFile.txt"))
				{
					foreach (KeyValuePair<string, int> keyValuePair in dictionaries[0])
					{
						//Console.WriteLine(keyValuePair.Key + " " + keyValuePair.Value);
						writer.WriteLine(keyValuePair.Key + " " + keyValuePair.Value);
					}
				}
				using(StreamWriter writer = new StreamWriter(@"C:\Users\brija\Desktop\'.net'\IAD\lab1\lab1\spamFile.txt", false))
				{
					foreach (KeyValuePair<string, int> keyValuePair in dictionaries[1])
					{
						//Console.WriteLine(keyValuePair.Key + " " + keyValuePair.Value);
						writer.WriteLine(keyValuePair.Key + " " + keyValuePair.Value);
					}
				}
			}
			public void ReadFiles()
			{
				try
				{
					using (StreamReader reader = new StreamReader(@"C:\Users\brija\Desktop\'.net'\IAD\lab1\lab1\notSpamFile.txt"))
					{
						while ((message = reader.ReadLine().Split(' ')) != null)
						{
							dictionaries[0].Add(message[0], Convert.ToInt32(message[1]));
						}
					}
				}
				catch(Exception)
				{
					Console.WriteLine("NotSpamFile already read");
				}
				try
				{
					using (StreamReader reader = new StreamReader(@"C:\Users\brija\Desktop\'.net'\IAD\lab1\lab1\spamFile.txt"))
					{
						while ((message = reader.ReadLine().Split(' ')) != null)
						{
							dictionaries[1].Add(message[0], Convert.ToInt32(message[1]));
						}
					}
				}
				catch(Exception)
				{
					Console.WriteLine("SpamFile already read");
				}
				
			}
			public void Studing()
			{
				while(true)
				{
					Console.WriteLine("Enter the message: ");
					message = Console.ReadLine().Split(' ');
					Console.WriteLine("Is it spam?(true or false):");
					status = Convert.ToBoolean(Console.ReadLine());
					foreach (var word in message)
					{
						lowword = word.Replace(',', ' ');
						lowword = lowword.Trim();
						lowword = lowword.ToLower();
						//Console.WriteLine(lowword + "_");
						if (!dictionaries[Convert.ToInt32(status)].ContainsKey(lowword))
							dictionaries[Convert.ToInt32(status)].Add(lowword, 1);
						else
							dictionaries[Convert.ToInt32(status)][lowword] = (int)dictionaries[Convert.ToInt32(status)][lowword] + 1;

					}
					Console.WriteLine("Do you want to continue?(true or false)");
					if (!Convert.ToBoolean(Console.ReadLine()))
						break;
				}
				WriteFiles();
				
			}
		}
	}
}
