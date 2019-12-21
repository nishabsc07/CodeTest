using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VerivoxCodeTest
{
	interface IProduct
	{
		double annualCost { get; } // Property interface.
		string tariffName { get; } // Property interface. 
		double consumption { get; }
	}
	public class ProductA : IProduct
	{
		public double cost;
		public double lconsumption;

		public double annualCost // Property implementation.
		{
			get { return this.cost; }
		}
		public string tariffName // Property implementation.
		{
			get { return "basic electicity tariff"; }
		}
		public double consumption // Property implementation.
		{
			get { return this.lconsumption; }
		}

		public ProductA(double consumption)
		{
			lconsumption = consumption;
			cost = 5 * 12 + consumption * 0.22;
		}

	}

	public class ProductB : IProduct
	{
		public double cost;
		public double lconsumption;
		public double annualCost // Property implementation.
		{
			get { return this.cost; }
		}
		public string tariffName // Property implementation.
		{
			get { return "Pakaged tariff"; }
		}
		public double consumption // Property implementation.
		{
			get { return this.lconsumption; }
		}

		public ProductB(double consumption)
		{
			lconsumption = consumption;
			cost = consumption <= 4000 ? 800 : (800 + (consumption-4000)*0.3);
		}
	}

	public class Product : IComparable<Product>
	{
		public double annualCost { get; set; }
		public string tariffName { get; set; }
		public double consumption { get; set; }
		public int CompareTo(Product other)
		{
			if (this.annualCost == other.annualCost)
			{
				return other.tariffName.CompareTo(this.tariffName);
			}
			return this.annualCost.CompareTo(other.annualCost);
		}
		
	}

	public class Program
	{
		public static void Main()
		{
			Console.WriteLine("Enter number of consumptions");
			int length;
			bool success = Int32.TryParse(Console.ReadLine(), out length);

			if (success && length > 0)
			{
				double[] consumption = new double[length];
				Console.WriteLine("Enter " + length + " readings of consumptions");
				
				for (int j = 0; j < length; j++)
				{
					//consumption reading from user
					consumption[j] = ReadValue(Console.ReadLine().ToString());
				}

				CreateProducts(length, consumption);				
			}
			else
			{
				Console.WriteLine("Number of consumption should be valid positive number"); 
				Console.ReadKey();
			}
		}

		public static double ReadValue(string inputStr)
		{
			bool flag = true;
			double consume = 0.0;
			do
			{				
				try
				{
					consume = double.Parse(inputStr);
					if(consume > 0)
					{
						flag = false;
					}
					else
					{
						Console.WriteLine("Invalid entry, enter valid consumption value");
						inputStr = Console.ReadLine().ToString();
					}
				}
				catch (Exception ex)
				{
					Console.WriteLine("Invalid entry, enter valid consumption value");
					inputStr = Console.ReadLine().ToString();
				}

			} while(flag);
			
			return consume;
			
		}

		
		public static void CreateProducts(int length,double[] consumptions)
		{
			ProductA[] productA = new ProductA[length];
			ProductB[] productB = new ProductB[length];
			List<Product> list = new List<Product>();
			for (int i = 0; i < length; i++)
			{
				productA[i] = new ProductA(consumptions[i]);// creating n number of Product A
				productB[i] = new ProductB(consumptions[i]); // creating n number of Product B
				list.Add(new Product() { tariffName = productA[i].tariffName, consumption = productA[i].consumption, annualCost = productA[i].annualCost });
				list.Add(new Product() { tariffName = productB[i].tariffName, consumption = productB[i].consumption, annualCost = productB[i].annualCost });
			}

			list.Sort();
			Console.WriteLine("Tariff Name;Annual Cost(€ /Year)");
			foreach (var element in list)
			{
				Console.WriteLine(element.tariffName.ToString() + ";" + element.annualCost);

			}
			Console.ReadKey();
		}
	}
}
