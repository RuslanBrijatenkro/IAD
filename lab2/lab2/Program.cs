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
				int sentenceLength = 0;
				string[] stringMas = reader.ReadToEnd().Split(' ');
				for (int i = 0; i < stringMas.Length; i++)
				{
					bool endSentence=false;
					stringMas[i].Remove(',');
					if (stringMas[i].Contains('.'))
						endSentence = true;
					stringMas[i].Remove('.');
					if (uniqueWords.TryGetValue(stringMas[i], out int index))
						words[index].Mediana = stringMas[i].Length;
					else
						words.Add(new Word(stringMas[i], stringMas[i].Length, sentenceLength));
					if(endSentence)
						sentenceLength = 0;
					else
						sentenceLength += stringMas[i].Length;
				}
			}
		}
		public void Clustering()
		{
			double[,] distanceTable = new double[words.Count,words.Count];
		}
	}
}
