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
		object Parent;
		object Left;
		object Right;
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
			double minDistance=1000;
			int[] minDistanceI;
			int[] minDistanceJ;
			double[,] distanceTable = new double[words.Count,words.Count];
			for(int i=1;i<words.Count;i++)
			{
				for(int j=i+1;j<words.Count;j++)
				{
					distanceTable[i, j] = Math.Sqrt(Math.Pow(words[j].wordLength-words[i].wordLength,2) + Math.Pow(words[j].mediana - words[i].mediana, 2));
					Console.SetCursorPosition(((i-1) * 5), j-1);
					Console.WriteLine(Math.Round(distanceTable[i, j],2) + "\t");
				}
			}
			for (int i = 1; i < words.Count; i++)
			{
				for (int j = i + 1; j < words.Count; j++)
				{
					for(int q=j+1;q<words.Count;q++)
					{
						double maybeMin = distanceTable[i, j]-distanceTable[i, q];
						if (maybeMin < minDistance)
						{
							minDistance = maybeMin;
							minDistanceI = new[] { i, i };
							minDistanceJ = new[] { j, q };
						}

					}
				}
			}
			words.Add(new Word(words[i]))


		}
	}
}
