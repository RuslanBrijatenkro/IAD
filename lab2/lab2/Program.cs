using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2
{
	class Word
	{
		public int group;
		public string word;
		public int wordLength;
		public double mediana;
		public int wordCount;
		public Word(string word, int wordLength, double mediana)
		{
			this.word = word;
			this.wordLength = wordLength;
			this.mediana = mediana;
			wordCount++;
		}
		public double Mediana
		{
			get { return mediana; }
			set
			{
				mediana = (mediana * wordCount + value)/++wordCount;
			}
		}
	}
	class Program
	{
		static void Main(string[] args)
		{
			Algorithm algorithm = new Algorithm();
			algorithm.ReadFile();
			Console.ReadKey();
		}
	}
	class Algorithm
	{
		int group = 0;
		List<Word> words = new List<Word>();
		public void ReadFile()
		{
			using (StreamReader reader = new StreamReader(@"C:\Users\brija\Desktop\1.txt"))
			{
				Dictionary<string,int> uniqueWords = new Dictionary<string, int>();
				double sentenceLength = 0;
				string[] stringMas = reader.ReadToEnd().Split(' ');
				for (int i = 0; i < stringMas.Length; i++)
				{
					stringMas[i]=stringMas[i].ToLower();
					bool endSentence=false;
					stringMas[i]=stringMas[i].Trim(',');
					
					if (stringMas[i].Contains('.'))
						endSentence = true;

					stringMas[i]=stringMas[i].Trim('.');
					//Console.WriteLine(stringMas[i]);

					if (uniqueWords.TryGetValue(stringMas[i], out int index))
						words[index].Mediana = stringMas[i].Length;
					else
					{
						words.Add(new Word(stringMas[i], stringMas[i].Length, sentenceLength));
						uniqueWords.Add(stringMas[i],words.Count-1);
					}
					if(endSentence)
						sentenceLength = 0;
					else
						sentenceLength += stringMas[i].Length;
				}
			}
			Clustering();
		}
		public void Clustering()
		{
			List<Word> localWords = new List<Word>();
			foreach(var word in words)
			{
				localWords.Add(word);
			}
			while (localWords.Count!=1)
			{
				double minDistance = 1000;
				int minDistanceI = 0;
				int minDistanceJ = 0;
				double[,] distanceTable = new double[localWords.Count, localWords.Count];
				for (int i = 0; i < localWords.Count; i++)
				{
					for (int j = i + 1; j < localWords.Count; j++)
					{
						distanceTable[i, j] = Math.Sqrt(Math.Pow(localWords[j].wordLength - localWords[i].wordLength, 2) + Math.Pow(localWords[j].mediana - localWords[i].mediana, 2));
						if (distanceTable[i, j] < minDistance)
						{
							minDistance = distanceTable[i, j];
							minDistanceI = i;
							minDistanceJ = j;
						}
						//Console.SetCursorPosition((i * 5), j);
						//Console.WriteLine(Math.Round(distanceTable[i, j], 2) + "\t");
					}
				}
				localWords.Add(new Word(localWords[minDistanceI].word + " " + localWords[minDistanceJ].word, (localWords[minDistanceI].wordLength + localWords[minDistanceJ].wordLength) / 2, (localWords[minDistanceI].mediana + localWords[minDistanceJ].mediana) / 2d));
				localWords[minDistanceI].group = ++group;
				localWords[minDistanceJ].group = group;
				//words.Add(localWords[localWords.Count - 1]);
				localWords[minDistanceI] = null;
				localWords[minDistanceJ] = null;
				localWords.Remove(null);
				localWords.Remove(null);
				if (localWords.Count == 1)
				{
					localWords[localWords.Count - 1].group = ++group;
				}
			}
			Console.WriteLine("Clasters count:"+group);
			DefineGroup();
		}
		void DefineGroup()
		{
			Console.WriteLine("Entry count of groups:");
			int countOfGroups = Convert.ToInt32(Console.ReadLine());
			int[] count = new int[countOfGroups];
			string[] wordGroups = new string[group];
			foreach(var word in words)
			{
				wordGroups[word.group - 1] +=" / " + word.word;
			}
			for (int i = 0; i < wordGroups.Length; i++)
			{
				if(wordGroups[i]!=null)
					Console.WriteLine($"Group {i+1}: "+wordGroups[i]);
			}
			Console.WriteLine("Entry message:");
			string[] message=Console.ReadLine().Split(' ');
			for (int i = 0; i < message.Length; i++)
			{
				for (int j = 0; j < wordGroups.Length; j++)
				{
					try
					{
						if (wordGroups[j].Contains(message[i]))
						{
							count[j/(group/countOfGroups)] += 1;
						}
					}
					catch(Exception)
					{
					}
				}
			}
			Console.WriteLine("Message group: "+(Array.IndexOf(count,count.Max())+1));
		}
	}
}
