using Paralelismo.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Paralelismo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Paralelismo!");

            var acao1 = new Action(Processo1);
            var acao2 = new Action(Processo2);
            var acao3 = new Action(Processo3);

            var stopWatch = new Stopwatch();
            stopWatch.Start();

            Parallel.Invoke(acao1, acao2, acao3);

            stopWatch.Stop();

            Console.WriteLine($"O tempo de processamento total é de {stopWatch.ElapsedMilliseconds} ms");
            Console.WriteLine();

            string[] ceps = new string[5];
            ceps[0] = "04625902";
            ceps[1] = "70835100";
            ceps[2] = "22780160";
            ceps[3] = "35180122";
            ceps[4] = "85903470";


            stopWatch.Reset();

            Console.WriteLine("Sequencial");
            stopWatch.Start();
            foreach(var cep in ceps)
            {
                Console.WriteLine(new ViaCepService().GetCep(cep) + $"Thread: {Thread.CurrentThread.ManagedThreadId}");
            }
            stopWatch.Stop();

            Console.WriteLine($"O tempo de processamento total é de {stopWatch.ElapsedMilliseconds} ms");
            Console.WriteLine();

            
            Console.WriteLine("Paralelismo");
            var parallelOptions = new ParallelOptions();
            parallelOptions.MaxDegreeOfParallelism = 8; // usar no máximo N núcleos

            stopWatch.Reset();
            stopWatch.Start();

            var listaCep = new List<CepModel>();
            Parallel.ForEach(ceps, parallelOptions, cep =>
            {
                listaCep.Add(new ViaCepService().GetCep(cep));
            });

            listaCep.OrderBy(cep => cep.Cep).ToList().ForEach(cep => Console.WriteLine(cep));

            stopWatch.Stop();

            Console.WriteLine($"O tempo de processamento total é de {stopWatch.ElapsedMilliseconds} ms");
        }

        static void Processo1() 
        {
            Console.WriteLine($"Processo 1 finalizado Thread: {Thread.CurrentThread.ManagedThreadId}.");
            Thread.Sleep(1000);
        }

        static void Processo2() 
        {
            Console.WriteLine($"Processo 2 finalizado Thread: {Thread.CurrentThread.ManagedThreadId}.");
            Thread.Sleep(1000);
        }

        static void Processo3() 
        {
            Console.WriteLine($"Processo 3 finalizado Thread: {Thread.CurrentThread.ManagedThreadId}.");
            Thread.Sleep(1000);
        }
    }
    
}
